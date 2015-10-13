using UnityEngine;
using System.Collections;

public class Troop : MonoBehaviour {
    public TroopStats troop_stat;
    private TroopGraphic troop_graphic;

    public enum TroopType { Pikeman, Skirmisher, Archer, Crossbowman, Heavy_Pikeman,
                            Lancer, Mounted_Archer, Heavy_Cavalry, Artillery };    //to access the implicit type, cast it to int:  (int)myTroopType

    private TroopType troopType;   //can be assigned an integer in the list
    public TroopType type { get { return troopType; } }
    private int faction; //which country are you fighting for?

    //Movement Related
    private Vector2 direction;
    private float speed;
    private Vector2 position { get { return new Vector2(this.transform.position.x, this.transform.position.y); } }

    public void initialize(int troopType, int troopNumber, int troopLevel, Color fractionColor)
    {
        troop_stat = new TroopStats();
        troop_graphic = new TroopGraphic();
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
