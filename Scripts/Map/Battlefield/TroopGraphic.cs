using UnityEngine;
using System.Collections;

public class TroopGraphic : MonoBehaviour {

    //Each as his own sprite
    private GameObject icon_top;
    private GameObject icon_middle;
    private GameObject icon_bottom;    //Uses Color of Faction
    private GameObject icon_highlight;
    private GameObject icon_advance;   //Only this one can rotate
    //**** Rotation **** Depends on Troop's direction
    //need only rotate z, and it is counter-clockwise

    private TroopStats troop_stat { get { return this.GetComponent<TroopStats>(); } }
    private Troop troop { get { return this.GetComponent<Troop>(); } }

    private int number { get { return troop_stat.Number; } }   //Used to change the 1. outer aura size and color 2. Collider size?
    private Color factionColor { get { return troop_stat.Faction.Color; } }

    //Animation Parameters
    private const float blinkRate = 1.5f;  //Higher Rate Means Faster Blink
    private float blink = 0f; //constantly adds Time.deltaTime and resets to zero if > than blinkRate;
    private Color blinkColor = Color.white;

    public void initialize(TroopStats troopStat)
    {
        int type = (int)troopStat.Type;
        Sprite top;
        Sprite middle;
        Sprite advance;
        Sprite bottom_normal;
        Sprite bottom_highlight;
        Data_Manager.ObtainTroopSprite(type, out top, out middle, out bottom_normal, out bottom_highlight, out advance);
        //TODO: create four gameObjects for the sprites
        attachIcon(top, ref icon_top, 3);
        attachIcon(middle, ref icon_middle, 2);
        attachIcon(bottom_normal, ref icon_bottom, 1);
        attachIcon(advance, ref icon_advance, 4);
        attachIcon(bottom_highlight, ref icon_highlight, 0);
        icon_bottom.GetComponent<SpriteRenderer>().color = factionColor;
        icon_advance.GetComponent<SpriteRenderer>().color = factionColor;
        setHighLight(false);
    }

    void attachIcon(Sprite s, ref GameObject g, int layer)
    {
        g = new GameObject("Icon layer "+layer.ToString());
        g.transform.position = this.transform.position;
        g.transform.SetParent(this.transform);
        g.AddComponent<SpriteRenderer>().sprite = s;
        g.GetComponent<SpriteRenderer>().sortingOrder = layer;
        //g.GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Diffuse");
    }

    public void setHighLight(bool flag)
    {
        if (flag)
            icon_highlight.SetActive(true);
        else
            icon_highlight.SetActive(false);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Rotate the Frontier All The Time
        if (icon_advance != null)
        {
            icon_advance.transform.rotation = Quaternion.Euler(0, 0, 180/Mathf.PI*Mathf.Atan2(-troop.Direction.x, troop.Direction.y));
        }

        //Blink Yellow if Status in fight, blink Red if Status is rout
        blink += Time.deltaTime * blinkRate;
        if (blink > 1.6f)
            blink = 0f;
        switch(troop_stat.Status)
        {
            case TroopStats.TroopStatus.Attacking:
            case TroopStats.TroopStatus.Melee_fight:
                blinkColor = Color.yellow;
                break;
            case TroopStats.TroopStatus.Rout:
                blinkColor = Color.red;
                break;
            case TroopStats.TroopStatus.Double_time:
                blinkColor = Color.blue;
                break;
            default:
                blinkColor = Color.white;
                break;
        }
        blinkColor += (Color.white - blinkColor)*(blink > 1f? 1f : blink);
        icon_middle.GetComponent<SpriteRenderer>().color = blinkColor;
        icon_top.GetComponent<SpriteRenderer>().color = blinkColor;
        //Enlarge in size with camera size, and fade the advance icon to introduce the new frontier *à faire*
    }
}
