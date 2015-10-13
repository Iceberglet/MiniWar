using UnityEngine;
using System.Collections;

public class TroopStats : MonoBehaviour {
    private static int MAX_STAT;

    public enum TroopType
    {
        Pikeman = 0, Skirmisher = 1, Archer = 2, Crossbowman = 3, Heavy_Pikeman = 4,
        Lancer = 5, Mounted_Archer = 6, Heavy_Cavalry = 7
    } //, Artillery = 8 };    //to access the implicit type, cast it to int:  (int)myTroopType
    public enum Status { Stationary, Advance, Double_time, Charge, Attacking, Melee_fight, Rout};   //Attacking for ranged troops remains ranged 

    //********* Private Data ******
    protected int number;   //between 100 and 500
    protected int morale;   //between 0 and 200, initialized as 100 (subject to advancement such as patriotism)
    protected int stamina;  //between 0 to stamina_max;
    protected int stamina_threshold;
    protected int stamina_max  //between 50 to 100, starts at 100
    {
        get { return (stamina_threshold + MAX_STAT) / 2; }
    }

    protected TroopType type;   //can be assigned an integer in the list
    protected int rank;
    protected int faction; //which country are you fighting for?

    //********* Public Accessors ******
    public TroopType Type { get { return type; } }
    public int Rank { get { return rank; } }
    public int Faction { get { return faction; } }
    public int Number { get { return number; } }
    public int Morale { get { return morale; } }
    public int Stamina { get { return stamina; } }
    
    public void initialize(int type, int number, int rank, int faction)
    {
        stamina = MAX_STAT;
        stamina_threshold = MAX_STAT;
        morale = MAX_STAT;

        this.type = (TroopType)type;
        this.number = number;
        this.rank = rank;
        this.faction = faction;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
