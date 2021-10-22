using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakraSkill
{
    protected Player _player;
    public bool open = true;
    public ChakraIcon icon;

    public void TryExecute()
    {
        if (open)
        {
            open = false;
            Execute();
        }
        else
        {
            //Unable to attack
        }
    }

    protected virtual void Execute()
    {

    }
}
