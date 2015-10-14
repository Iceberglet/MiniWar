using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TroopInfo : MonoBehaviour
{
    protected const float MAX_STAT = 100;
    public enum TroopType
    {
        Pikeman = 0, Skirmisher = 1, Archer = 2, Crossbowman = 3, Heavy_Pikeman = 4,
        Lancer = 5, Mounted_Archer = 6, Heavy_Cavalry = 7
    } //, Artillery = 8 };    //to access the implicit type, cast it to int:  (int)myTroopType
    public enum TroopRank { Normal, Trained, Elite }
    
    protected TroopType type;
    protected int number;
    protected TroopRank rank;
    protected Faction faction;
    protected float morale;

    public TroopType Type { get { return type; } }
    public TroopRank Rank { get { return rank; } }
    public Faction Faction { get { return faction; } }
    public int Number { get { return number; } }
    public float Morale { get { return morale; } }

    public void initialize(TroopType type, int number, TroopRank rank, Faction faction, float morale = MAX_STAT)
    {
        this.type = type;
        this.number = number;
        this.rank = rank;
        this.faction = faction;
        this.morale = morale;
    }
}
