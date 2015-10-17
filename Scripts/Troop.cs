using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Troop : MonoBehaviour
{
    protected const float MAX_STAT = 100;
    /*
    public enum TroopType
    {
        Pikeman = 0, Skirmisher = 1, Archer = 2, Crossbowman = 3, Heavy_Pikeman = 4,
        Lancer = 5, Mounted_Archer = 6, Heavy_Cavalry = 7
    } //, Artillery = 8 };    //to access the implicit type, cast it to int:  (int)myTroopType*/

    protected TroopType type;
    protected int rank;   //From 0 to 10;
    protected int number;
    protected float morale;

    public TroopType Type { get { return type; } }
    public int Rank { get { return rank; } }
    public Faction Faction { get { return type.faction; } }
    public int Number { get { return number; } }
    public float Morale { get { return morale; } }

    public void initialize(TroopType type, int number, int rank = 0, float morale = MAX_STAT)
    {
        this.type = type;
        this.rank = rank;
        this.number = number;
        this.morale = morale;
    }
}
