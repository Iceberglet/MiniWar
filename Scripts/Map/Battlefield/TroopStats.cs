using UnityEngine;
using System.Collections;

public class TroopStats : TroopInfo {
    public enum Status { Stationary, Advance, Double_time, Charge, Attacking, Melee_fight, Rout };   //Attacking for ranged troops remains ranged 

    //********* Private Data ******
    protected float stamina;  //between 0 to stamina_max;
    protected float stamina_threshold;
    protected float stamina_max  //between 50 to 100, starts at 100
    {
        get { return (stamina_threshold + MAX_STAT) / 2; }
    }
    
    //********* Public Accessors ******
    public float Stamina { get { return stamina; } }
    
    public void initialize(TroopInfo troopInfo)
    {
        stamina = MAX_STAT;
        stamina_threshold = MAX_STAT;
        number = troopInfo.Number;
        type = troopInfo.Type;
        rank = troopInfo.Rank;
        faction = troopInfo.Faction;
        stamina = stamina_threshold = MAX_STAT;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
