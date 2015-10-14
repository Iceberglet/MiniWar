using UnityEngine;
using System.Collections;

public class Troop : MonoBehaviour {
    public TroopStats troop_stat;
    private TroopGraphic troop_graphic;

    //Movement Related
    private Vector2 direction;
    public Vector2 Direction { get { return direction; } }
    private float speed;
    private Vector2 position { get { return new Vector2(this.transform.position.x, this.transform.position.y); } }

    //Initialize the Troop as Battlefield unit. Called Exclusively By BattleFieldManager
    public static GameObject instantiate(TroopInfo troopInfo, Vector3 position)
    {
        // Initialize GameObject
        GameObject g = new GameObject();
        g.transform.position = position;

        Troop t = g.AddComponent<Troop>();
        t.direction = new Vector2(1, 1);

        t.troop_stat = g.AddComponent<TroopStats>();
        t.troop_stat.initialize(troopInfo);
        t.troop_graphic = g.AddComponent<TroopGraphic>();
        t.troop_graphic.initialize(t.troop_stat);

        t.transform.localScale = Vector3.one * 0.1f;
        return g;
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
