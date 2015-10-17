using UnityEngine;
using System.Collections;

public class TroopOnField : MonoBehaviour {
    public TroopStats troop_stat;
    private TroopGraphic troop_graphic;
    private BattleFieldManager mother;

    public static float colliderSize = 2f; //size of collider
    public static float scale = 0.03f;

    //Movement Related
    private Target target;
    private Vector2 targetDirection { get { return (target.targetPos - this.transform.position).normalized; } }
    public Vector3 direction = new Vector2(0, 1).normalized;
    private Vector2 position { get { return new Vector2(this.transform.position.x, this.transform.position.y); } }

    private bool arrived;
    private static float turnAngle = 5f; // / 180f * Mathf.PI;
    private int turn_is_clockwise;
    private bool turned;

    //Public Accessor
    public Vector2 Direction { get { return direction; } }
    public string Name { get { return troop_stat.Type.ToString() + " of " + troop_stat.Faction.Name; } }

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

        return g;
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if(target != null)
            moveTowardsTarget();
	}

    public void setTarget(Target t)
    {
        arrived = false;
        turned = false;
        turn_is_clockwise = Vector3.Cross(direction, (t.targetPos - this.transform.position)).z < 0? 1 : -1;
        target = t;
    }

    void moveTowardsTarget()
    {
        if (arrived && turned)
        {
            target = null;
            return;
        }
        if (!arrived)
        {
            //Displacement
            Vector3 displacement = (target.targetPos - this.gameObject.transform.position).normalized * BattleFieldManager.deltaTime * troop_stat.Type.MarchSpeed;
            //check if distance already close
            if (Vector3.Distance(target.targetPos, this.transform.position) < displacement.magnitude)
            {
                this.transform.position = target.targetPos;
                this.arrived = true;
            }
            else this.transform.position += displacement;
        }

        //Turn Angle
        if (!turned)
        {
            Debug.Log("Current Direction: " + direction + " target direction " + (target.targetPos - this.transform.position) + " has angle " + Vector3.Angle(direction, (target.targetPos - this.transform.position).normalized));
            if (Vector3.Angle(direction, (target.targetPos - this.transform.position).normalized) < turnAngle)
            {
                direction = (target.targetPos - this.transform.position).normalized;
                turned = true;
            }
            else direction = (Quaternion.Euler(0, 0,  -turn_is_clockwise * turnAngle) * direction).normalized;
        }
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
