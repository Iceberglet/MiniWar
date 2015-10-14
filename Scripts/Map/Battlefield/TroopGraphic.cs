using UnityEngine;
using System.Collections;

public class TroopGraphic : MonoBehaviour {

    //Each as his own sprite
    private GameObject icon_top;
    private GameObject icon_middle;
    private GameObject icon_bottom;    //Uses Color of Faction
    private GameObject icon_advance;   //Only this one can rotate
    //**** Rotation **** Depends on Troop's direction
    //need only rotate z, and it is counter-clockwise

    private TroopStats troop_stat { get { return this.GetComponent<TroopStats>(); } }
    private Troop troop { get { return this.GetComponent<Troop>(); } }

    private int number { get { return troop_stat.Number; } }   //Used to change the 1. outer aura size and color 2. Collider size?
    private Color factionColor { get { return troop_stat.Faction.Color; } }

    public void initialize(TroopStats troopStat)
    {
        int type = (int)troopStat.Type;
        Sprite top;
        Sprite middle;
        Sprite bottom;
        Sprite advance;
        Color c = troop_stat.Faction.Color;
        Data_Manager.ObtainTroopSprite(type, out top, out middle, out bottom, out advance);
        //TODO: create four gameObjects for the sprites
        attachIcon(top, ref icon_top, 2);
        attachIcon(middle, ref icon_middle, 1);
        attachIcon(bottom, ref icon_bottom, 0);
        attachIcon(advance, ref icon_advance, 3);
        icon_bottom.GetComponent<SpriteRenderer>().color = c;
        icon_advance.GetComponent<SpriteRenderer>().color = c;
    }

    void attachIcon(Sprite s, ref GameObject g, int layer)
    {
        g = new GameObject("An Icon, nothing more");
        g.transform.position = this.transform.position;
        g.transform.SetParent(this.transform);
        g.AddComponent<SpriteRenderer>().sprite = s;
        g.GetComponent<SpriteRenderer>().sortingOrder = layer;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Rotate the Frontier All The Time
        if (icon_advance != null)
            icon_advance.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-troop.Direction.x, troop.Direction.y));
	}
}
