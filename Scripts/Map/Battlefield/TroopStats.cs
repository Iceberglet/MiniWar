using UnityEngine;
using System.Collections;

public class TroopStats : MonoBehaviour {

    public enum Status { Stationary, Advance, Double_time, Charge, Attacking, Melee_fight, Rout};   //Attacking for ranged troops remains ranged 

    private int number;   //between 100 and 500
    private int morale;   //between 0 and 200, initialized as 100 (subject to advancement such as patriotism)
    private int stamina;  //between 0 to stamina_max;
    private int stamina_threshold;
    private int stamina_max;  //between 50 to 100, starts at 100

    public int Number { get { return number; } }
    public int Morale { get { return morale; } }
    public int Stamina { get { return stamina; } }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
