using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Transition
{
    public virtual bool isValid()
    {
        Debug.Log("Error, in Transition base class isValid()");
        return false;
    }

    public readonly TroopState fromState;
    public readonly TroopState nextState;
    //public abstract void onTransition();
    public Transition(TroopState from, TroopState next)
    {
        fromState = from;
        nextState = next;
    }
}

public class T_S_M : Transition     //Transition Standby to March
{
    public T_S_M(TroopState f, TroopState t) : base(f,t) { }

    public override bool isValid()
    {
        //if has target, we go march!
        if (fromState.troop.target != null)
            return true;
        else return false;
    }
}

public class T_M_S : Transition    //Transition March to Standby
{
    public T_M_S(TroopState f, TroopState t) : base(f, t) { }

    public override bool isValid()
    {
        return fromState.checkTaskDone();
    }
}


