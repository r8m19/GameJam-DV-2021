using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimationEvents : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        if (!player)
            player = GetComponent<Player>();
    }

    public void OnJabStart()
    {
        (player.chakraSkills[1] as JabSkill).OnJabStart();
    }
}
