using UnityEngine;
using System.Collections;

public class TroopStats : Troop {
    public enum TroopStatus { Stationary, Marching, Charging, UnderAttack, RangedAttack, Melee, Chaos, Rout };   //Attacking for ranged troops remains ranged 

    //********* Troop Action ******



    //********* Private Data ******
    protected float rankMultiplier { get { return 1f + 0.1f * rank; } }
    protected float stamina;  //between 0 to stamina_max;
    protected float stamina_threshold;
    protected float stamina_max  //between 50 to 100, starts at 100
    {
        get { return (stamina_threshold + MAX_STAT) / 2; }
    }
    public TroopStatus status;

    //********* Public Accessors ******
    public float Stamina { get { return stamina; } }
    public TroopStatus Status { get { return status; } }


    public void initialize(Troop troop)
    {
        type = troop.Type;
        rank = troop.Rank;
        number = troop.Number;
        morale = troop.Morale * rankMultiplier;
        stamina = stamina_threshold = MAX_STAT * rankMultiplier;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
