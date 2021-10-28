using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakraSkill
{
    protected Player _player;
    public bool open = true;
    public ChakraIcon icon;

    public virtual void TryExecute()
    {
        if (open)
        {
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

    public virtual void Close()
    {
        open = false;
        icon.UpdateImage(open);
        EventManager.Instance.Trigger("OnChakraClosed");
    }

    public virtual void Open()
    {
        open = true;
        icon.UpdateImage(open);
        EventManager.Instance.Trigger("OnChakraOpen");
    }
}
