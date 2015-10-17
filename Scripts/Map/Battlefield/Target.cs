using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Target
{
    private bool isTroop;
    private Troop troop;
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

    public Target(bool isTroopFlag, Vector3 initialPosition, Troop t = null)
    {
        isTroop = isTroopFlag;
        initialPos = initialPosition;
        troop = t;
    }
}
