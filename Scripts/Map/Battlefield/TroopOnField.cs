using UnityEngine;
using System.Collections;

public class TroopOnField : MonoBehaviour {
    public TroopStats troop_stat;
    private TroopGraphic troop_graphic;
    private BattleFieldManager mother;
    private TroopStateController controller;

    public static float colliderSize = 2f; //size of collider for highlight purpose
    public static float obstacleDetectionRadius = 6f; //size of "radius" for local avoidance
    public static float scale = 0.03f;

    //Movement Related
    public Target target;
    //private Vector2 targetDirection { get { return (target.targetPos - this.transform.position).normalized; } }
    private Vector2 position { get { return new Vector2(this.transform.position.x, this.transform.position.y); } }


    public static float turnAngle = 5f; // / 180f * Mathf.PI;

    //Public Accessor
    public Vector3 direction = new Vector2(0, 1).normalized;
    public string Name { get { return troop_stat.Type.ToString() + " of " + troop_stat.Faction.Name; } }
    public bool ignoreMouse = false;   //For mouse hover highlighting

    //Initialize the Troop as Battlefield unit. Called Exclusively By BattleFieldManager
    public static GameObject instantiate(Troop troopInfo, Vector2 position, GameObject mother)
    {
        // Initialize GameObject
        GameObject g = new GameObject(troopInfo.Faction.Name.ToString() + " " + troopInfo.Type.ToString());
        g.tag = "Unit";
        g.transform.position = new Vector3(position.x, position.y, BattleFieldManager.troopHeight);
        g.transform.SetParent(mother.transform);
        g.AddComponent<CircleCollider2D>().radius = colliderSize;  //This is for making hovering highlight happen

        /* Selection Process is gonna be done by bounding box check at BattleFieldManager level
        g.AddComponent<CircleCollider2D>().isTrigger = true;
        g.GetComponent<CircleCollider2D>().radius = 2f;
        g.AddComponent<Rigidbody2D>().gravityScale = 0f;
        */
        TroopOnField t = g.AddComponent<TroopOnField>();
        t.mother = mother.GetComponent<BattleFieldManager>();

        t.troop_stat = g.AddComponent<TroopStats>();
        t.troop_stat.initialize(troopInfo);
        t.troop_graphic = g.AddComponent<TroopGraphic>();
        t.troop_graphic.initialize(t.troop_stat);

        //Change Scale To Make The Icon Size Reasonable
        t.transform.localScale = Vector3.one * scale;

        //State Controller Machine to be in charge of everything
        t.controller = new TroopStateController(t);

        return g;
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        controller.Update();
	}
    /*
    public void setTarget(Target t)
    {
    }*/

    public void highLight(bool flag)
    {
        this.troop_graphic.setHighLight(flag);
    }

    void OnMouseEnter()
    {
        if(!ignoreMouse)
            highLight(true);
    }

    void OnMouseExit()
    {
        if(!ignoreMouse)
            highLight(false);
    }
    
}
