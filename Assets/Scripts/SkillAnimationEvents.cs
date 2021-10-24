using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Every skill has an animation by the player character, 
//this script redirects their animation events to the event manager so that they could trigger non-Monobehaviour methods

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
        (player.chakraSkills[0] as JabSkill).OnJabStart();
    }

    public void OnFireballLaunch()
    {
        (player.chakraSkills[1] as FireballSkill).OnFireballLaunch();
    }

    public void OnHeavyStart()
    {
        (player.chakraSkills[4] as HeavySkill).OnHeavyStart();
    }

    public void OnHeavyExplode()
    {
        (player.chakraSkills[4] as HeavySkill).OnHeavyExplode();
    }

    public void OnHeavyEnd()
    {
        (player.chakraSkills[4] as HeavySkill).OnHeavyEnd();
    }
}
