using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Target
{
    //if target is a troop or stationary pos
    public readonly bool isTroop;
    //the troop if isTroop == true
    public readonly TroopOnField troop;
    public readonly bool doubleTime;

    //The initial position of the target
    private Vector3 initialPos;
    public Vector3 targetPos
    {
        get
        {
            if (isTroop)
                return troop.transform.position;
            else return initialPos;
        }
    }

    public Target(bool isTroopFlag, Vector3 initialPosition, bool flagDoubleTime = false, TroopOnField t = null)
    {
        isTroop = isTroopFlag;
        doubleTime = flagDoubleTime;   //TODO: implement detection of doubletime
        initialPos = initialPosition;
        troop = t;
    }
}
