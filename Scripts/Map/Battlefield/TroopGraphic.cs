using UnityEngine;
using System.Collections;

public class TroopGraphic : MonoBehaviour {

    private GameObject icon_top;
    private GameObject icon_bottom;
    private Troop troop;

    private int number { get { return troop.troop_stat.Number; } }   //Used to change the 1. outer aura size and color 2. Collider size?

    public void initialize(Troop troop)
    {
        int type = (int)troop.type;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
