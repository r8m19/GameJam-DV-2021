using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public ChakraSkill[] chakraSkills = new ChakraSkill[7];
    public ChakraIcon[]  chakraIcons  = new ChakraIcon[7];
    public KeyCode[]     attackInputs = new KeyCode[7];

    [HideInInspector] public Vector2 aimVector;
    [HideInInspector] public Animator anim;

    private bool invulnerable = false;

    private void Awake()
    {
        if (!anim)
            anim = GetComponent<Animator>();

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
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InitializeSkills()
    {
        chakraSkills[0] = new JabSkill(this, chakraIcons[0]);
        chakraSkills[1] = new FireballSkill(this, chakraIcons[1]);
        chakraSkills[2] = new DashSkill(this, chakraIcons[2]);
        chakraSkills[3] = new HeartSkill(this, chakraIcons[3]);
        chakraSkills[4] = new HeavySkill(this, chakraIcons[4]);

        for (int i = 5; i < 7; i++)
        {
            chakraSkills[i] = new JabSkill(this, chakraIcons[i]);
        }        
    }

    void Move()
    {
        aimVector.x = Input.GetAxisRaw("Horizontal");
        aimVector.y = Input.GetAxisRaw("Vertical");
        aimVector.Normalize();

        transform.position += (Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.up) * Time.deltaTime * speed;

        if (aimVector == Vector2.zero)
            aimVector = transform.right;
        transform.right = aimVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 11)
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        if (invulnerable)
            return;

        int rng; 
        do
        {
            rng = Random.Range(0, 7);
        } while (!chakraSkills[rng].open);

        chakraSkills[rng].Close();
    }

    public IEnumerator Invencibility(int frames)
    {
        invulnerable = true;
        for (int i = 0; i <= frames; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        invulnerable = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + aimVector);
    }
}
