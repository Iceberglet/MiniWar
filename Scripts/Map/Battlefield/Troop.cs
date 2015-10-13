using UnityEngine;
using System.Collections;

public class Troop : MonoBehaviour {
    public TroopStats troop_stat;
    private TroopGraphic troop_graphic;



    //Movement Related
    private Vector2 direction;
    private float speed;
    private Vector2 position { get { return new Vector2(this.transform.position.x, this.transform.position.y); } }

    public void initialize(int troopType, int troopNumber, int rank, int faction)
    {
        troop_stat = new TroopStats();
        troop_graphic = new TroopGraphic();
        troop_stat.initialize(troopType,troopNumber,rank,faction);
        troop_graphic.initialize(troop_stat);
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
