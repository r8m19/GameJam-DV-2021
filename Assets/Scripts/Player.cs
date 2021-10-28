using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float baseSpeed;
    public float slowSpeed;
    public float currentSpeed;
    public bool weakState = false;
    public ChakraSkill[] chakraSkills = new ChakraSkill[7];
    public ChakraIcon[]  chakraIcons  = new ChakraIcon[7];
    public KeyCode[]     attackInputs = new KeyCode[7];
    public GameObject    pointer;

    [HideInInspector] public Vector2 aimVector;
    [HideInInspector] public Animator anim;

    private bool invulnerable = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (!anim)
            anim = GetComponent<Animator>();
        if (!spriteRenderer)
            spriteRenderer = GetComponent<SpriteRenderer>();

        RestoreSpeed();

        InitializeSkills();
    }

    private void OnEnable()
    {
        EventManager.Instance.Subscribe("OnChakraClosed", OnChakraClosed);
        EventManager.Instance.Subscribe("OnChakraOpen", OnChakraOpen);
        RestoreSpeed();
        currentSpeed = baseSpeed;
    }
    private void OnDisable()
    {
        EventManager.Instance.Unsubscribe("OnChakraClosed", OnChakraClosed);
        EventManager.Instance.Unsubscribe("OnChakraOpen", OnChakraOpen);
        RestoreSpeed();
        currentSpeed = baseSpeed;
    }

    void OnChakraClosed(params object[] parameters)
    {
        IEnumerable<ChakraSkill> openChakras =
       from ch in chakraSkills
       where ch.open == true
       select ch;

        if (openChakras.Count() == 0)
        {
            EnterWeakState();
        }
    }
    void OnChakraOpen(params object[] parameters)
    {
        IEnumerable<ChakraSkill> openChakras =
       from ch in chakraSkills
       where ch.open == true
       select ch;

        if (openChakras.Count() == 1)
        {
            ExitWeakState();
        }
    }

    private void Update()
    {
        for (int i = 0; i < 7; i++)
        {
            if (Input.GetKeyDown(attackInputs[i]))
            {
                if(i < chakraSkills.Length)
                    chakraSkills[i]?.TryExecute();
            }
        }

        anim.SetFloat("spd", Mathf.Abs( Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical")));
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InitializeSkills()
    {
        chakraSkills = new ChakraSkill[chakraIcons.Length];


        if (chakraSkills.Length > 0) chakraSkills[0] = new JabSkill(this, chakraIcons[0]);
        if (chakraSkills.Length > 1) chakraSkills[1] = new FireballSkill(this, chakraIcons[1]);
        if (chakraSkills.Length > 2) chakraSkills[2] = new DashSkill(this,  chakraIcons[2]);
        if (chakraSkills.Length > 3) chakraSkills[3] = new HeartSkill(this, chakraIcons[3]);
        if (chakraSkills.Length > 4) chakraSkills[4] = new HeavySkill(this, chakraIcons[4]);
        if (chakraSkills.Length > 5) chakraSkills[5] = new ThirdEyeSkill(this, chakraIcons[5]);
        if (chakraSkills.Length > 6) chakraSkills[6] = new CrownSkill(this, chakraIcons[6]);

        for (int i = 0; i < chakraSkills.Length; i++)
        {
            chakraSkills[i].Open();
            chakraIcons[i].gameObject.SetActive(true);
        }
    }

    void Move()
    {
        aimVector.x = Input.GetAxisRaw("Horizontal");
        aimVector.y = Input.GetAxisRaw("Vertical");
        aimVector.Normalize();

        transform.position += (Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.up) * Time.deltaTime * currentSpeed;

        if (aimVector == Vector2.zero)
            aimVector = transform.right;
        pointer.transform.right = aimVector;
        transform.right = new Vector2(aimVector.x, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 11) //Enemy or Enemy Projectile
        {
            TakeDamage(collision.transform.position);
        }

        if (collision.gameObject.layer == 12) //Meditation Point
        {
            IEnumerable<ChakraSkill> closedChakras =
                from ch in chakraSkills
                where ch.open == false
                select ch;

            if (closedChakras.Count() > 0)
            {
                anim.SetTrigger("meditate");
                Invencibility(240);
                foreach (ChakraSkill item in closedChakras)
                {
                    item.Open();
                }
            }
        }

        if(collision.gameObject.layer == 13) //Level End
        {
            anim.SetTrigger("meditate");
            Invencibility(60);
            StartCoroutine(NextScene());
        }
    }

    void TakeDamage(Vector2 hitPosition)
    {
        if (invulnerable)
            return;

        IEnumerable<ChakraSkill> openChakras =
            from ch in chakraSkills
            where ch.open == true
            select ch;

        StartCoroutine(Invencibility(100, true));
        StartCoroutine(Hurt(15, hitPosition));

        if (openChakras.Count() > 0)
        {
            int rng;
            do
            {
                rng = Random.Range(0, chakraSkills.Length);
            } while (!chakraSkills[rng].open);

            chakraSkills[rng].Close();
        }
        else
        {
            //Die / Muerte / Funcion de muerte / m word / morir
        }
    }

    public IEnumerator Invencibility(int frames, bool display = false)
    {
        invulnerable = true;
        int visible = 5;
        for (int i = 0; i <= frames; i++)
        {
            if (display)
            {
                visible--;
                if (visible <= 0)
                {
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                    visible = 5;
                }
            }

            yield return new WaitForFixedUpdate();
        }
        invulnerable = false;
    }

    public IEnumerator Hurt(int frames, Vector2 hitPosition)
    {
        anim.SetBool("hurt", true);
        Vector2 backDir = ((Vector2)transform.position - hitPosition).normalized;
        for (int i = 0; i <= frames; i++)
        {
            transform.position += (Vector3)backDir * 3f * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        anim.SetBool("hurt", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + aimVector);
    }

    public void RestoreSpeed()
    {
        currentSpeed = baseSpeed;
    }
    public void SpeedTo0()
    {
        currentSpeed = 0;
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void EnterWeakState()
    {
        if (!weakState)
        {
            baseSpeed -= slowSpeed;
            weakState = true;
            RestoreSpeed();
        }
    }

    void ExitWeakState()
    {
        if (weakState)
        {
            baseSpeed += slowSpeed;
            weakState = false;
            RestoreSpeed();
        }
    }
}
