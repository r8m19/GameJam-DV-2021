using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakraSkill
{
    protected Player _player;
    public bool open = true;

    public void TryExecute()
    {
        if (open)
            Execute();
        else
        {
            //Unable to attack
        }
    }

    protected virtual void Execute()
    {

    }
}
