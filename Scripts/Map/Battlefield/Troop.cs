using UnityEngine;
using System.Collections;

public class Troop : MonoBehaviour {
    public TroopStats troop_stat;
    private TroopGraphic troop_graphic;

    //Movement Related
    private Vector2 direction;
    private float speed;
    private Vector2 position { get { return new Vector2(this.transform.position.x, this.transform.position.y); } }

    //Public Accessor
    public Vector2 Direction { get { return direction; } }
    public string Name { get { return troop_stat.Type.ToString() + " of " + troop_stat.Faction.Name; } }

    //Initialize the Troop as Battlefield unit. Called Exclusively By BattleFieldManager
    public static GameObject instantiate(TroopInfo troopInfo, Vector2 position)
    {
        // Initialize GameObject
        GameObject g = new GameObject(troopInfo.Faction.Name.ToString() + " " + troopInfo.Type.ToString());
        g.tag = "Unit";
        g.transform.position = new Vector3(position.x, position.y, BattleFieldManager.troopHeight);
        g.AddComponent<CircleCollider2D>().radius = 2f;  //This is for making hovering highlight happen

        /* Selection Process is gonna be done by bounding box check at BattleFieldManager level
        g.AddComponent<CircleCollider2D>().isTrigger = true;
        g.GetComponent<CircleCollider2D>().radius = 2f;
        g.AddComponent<Rigidbody2D>().gravityScale = 0f;
        */
        Troop t = g.AddComponent<Troop>();
        t.direction = new Vector2(1, 1);

        t.troop_stat = g.AddComponent<TroopStats>();
        t.troop_stat.initialize(troopInfo);
        t.troop_graphic = g.AddComponent<TroopGraphic>();
        t.troop_graphic.initialize(t.troop_stat);

        //Change Scale To Make The Icon Size Reasonable
        t.transform.localScale = Vector3.one * 0.1f;
        return g;
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void highLight(bool flag)
    {
        this.troop_graphic.setHighLight(flag);
    }

    void OnMouseEnter()
    {
        highLight(true);
    }

    void OnMouseExit()
    {
        highLight(false);
    }
}
