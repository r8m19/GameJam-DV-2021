using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    public float baseSpeed;
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

        currentSpeed = baseSpeed;

        InitializeSkills();
    }

    private void Update()
    {
        for (int i = 0; i < 7; i++)
        {
            if (Input.GetKeyDown(attackInputs[i]))
            {
                chakraSkills[i].TryExecute();
            }
        }

        anim.SetFloat("spd", currentSpeed);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InitializeSkills()
    {
        chakraSkills[0] = new JabSkill(this, chakraIcons[0]);
        chakraSkills[1] = new FireballSkill(this, chakraIcons[1]);
        chakraSkills[2] = new DashSkill(this,  chakraIcons[2]);
        chakraSkills[3] = new HeartSkill(this, chakraIcons[3]);
        chakraSkills[4] = new HeavySkill(this, chakraIcons[4]);
        chakraSkills[5] = new ThirdEyeSkill(this, chakraIcons[5]);
        chakraSkills[6] = new CrownSkill(this, chakraIcons[6]);

        foreach (ChakraSkill item in chakraSkills)
        {
            item.Open();
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
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 11)
        {
            TakeDamage(collision.transform.position);
        }
    }

    void TakeDamage(Vector2 hitPosition)
    {
        if (invulnerable)
            return;

        IEnumerable<ChakraSkill> filteringQuery =
            from ch in chakraSkills
            where ch.open == false
            select ch;
        Debug.Log(filteringQuery.Count());


        StartCoroutine(Invencibility(100, true));
        StartCoroutine(Hurt(15, hitPosition));

        int rng; 
        do
        {
            rng = Random.Range(0, 7);
        } while (!chakraSkills[rng].open);

        chakraSkills[rng].Close();
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
}
