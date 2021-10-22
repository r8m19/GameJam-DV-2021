using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public ChakraSkill[] chakraSkills = new ChakraSkill[7];
    public KeyCode[] attackInputs = new KeyCode[7];

    [HideInInspector] public Animator anim;

    private void Awake()
    {
        if (!anim)
            anim = GetComponent<Animator>();

        InitializeSkills();
    }

    private void Update()
    {
        Move();

        for (int i = 0; i < 7; i++)
        {
            if (Input.GetKeyDown(attackInputs[i]))
            {
                chakraSkills[i].TryExecute();
            }
        }
    }

    void InitializeSkills()
    {
        for (int i = 0; i < 7; i++)
        {
            chakraSkills[i] = new JabSkill(this);
        }
    }

    void Move()
    {
        transform.position += (Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.up) * Time.deltaTime * speed;
    }
}
