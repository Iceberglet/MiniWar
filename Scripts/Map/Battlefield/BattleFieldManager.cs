using UnityEngine;
using System.Collections;

//Job Description:
//1. Keep reference to every single troop on the field
//2. Commanding Interface, Player Control
//3. Call troop status update
//4. Time Speed Control

public class BattleFieldManager : MonoBehaviour {

    //Data Storage
    private int speed;        //From 0 to 3, under player control
    private Troop[] troopList_1;
    private Troop[] troopList_2;

    //Accessible Variables
    public float deltaTime    // accessible to all troops
    {
        get { return Time.deltaTime * speed; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
