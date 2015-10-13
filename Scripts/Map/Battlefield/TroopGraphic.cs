using UnityEngine;
using System.Collections;

public class TroopGraphic : MonoBehaviour {

    //Each as his own sprite
    private GameObject icon_top;
    private GameObject icon_middle;
    private GameObject icon_bottom;
    private GameObject icon_advance;

    private TroopStats troop_stat;

    private int number { get { return troop_stat.Number; } }   //Used to change the 1. outer aura size and color 2. Collider size?
    private Color factionColor { get { return Sprite_Dispenser.factionColors[troop_stat.Faction]; } }

    public void initialize(TroopStats troopStat)
    {
        this.troop_stat = troopStat;
        int type = (int)troopStat.Type;
        Sprite top;
        Sprite middle;
        Sprite bottom;
        Sprite advance;
        Sprite_Dispenser.ObtainTroopSprite(type, out top, out middle, out bottom, out advance);
        //TODO: create four gameObjects for the sprites
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
