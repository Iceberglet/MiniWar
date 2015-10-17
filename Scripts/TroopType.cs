using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


// Each Faction Should Have Its Own List Of TroopTypes
public class TroopType
{
    public const float MAX_ARMOUR = 10f;
    public Faction faction;
    public bool activated;

    //in small letters with underscore, e.g. japanese_samurai_archer
    //the name of corresponding resource icon sprite must be
    // icon_main: "icon_" followed by this name
    // icon_top: icon_horse / icon_empty
    // icon_bottom: icon_heavy_armour / icon_light_armour
    // icon_bottom_highlight: icon_heavy_armour_highlight / icon_light_armour_highlight
    // icon_advance: icon_advance
    public string type_name;
    public string icon_name;
    public static string sprite_prefix = "Graphics/icon_";
    public Sprite icon_main
    {
        get { return Resources.Load<Sprite>(sprite_prefix + icon_name); }
    }
    public Sprite icon_top
    {
        get {
            string s = (mounted?"horse":"empty");
            return Resources.Load<Sprite>(sprite_prefix + s);
        }
    }
    public Sprite icon_bottom
    {
        get
        {
            string s = (armour > MAX_ARMOUR * 0.5f ? "heavy_armour" : "light_armour");
            return Resources.Load<Sprite>(sprite_prefix + s);
        }
    }
    public Sprite icon_bottom_highlight
    {
        get
        {
            string s = (armour > MAX_ARMOUR * 0.5f ? "heavy_armour_highlight" : "light_armour_highlight");
            return Resources.Load<Sprite>(sprite_prefix + s);
        }
    }
    public Sprite icon_advance
    {
        get { return Resources.Load<Sprite>(sprite_prefix + "advance"); }
    }

    public bool mounted;
    public bool ranged;
    private const float baseRange = 0.3f;
    public float range;  //Distance at which to charge/release projectile

    private const float baseMarchSpeed = 0.05f;
    private const float baseRunSpeed = 0.12f;
    private float marchSpeed;    //autmatically multiplies the baseMarchSpeed? NO
    public float MarchSpeed { get { return baseMarchSpeed * marchSpeed; } }
    private float runSpeed;
    public float RunSpeed { get { return baseRunSpeed * runSpeed; } }
    public float ChargeSpeed { get { return 1.5f * RunSpeed; } }
    public const float CombatStaminaCost = 0.08f;  //Same For All Troops
    private const float baseRunStaminaCost = 0.04f;
    private float runStaminaCost;   //actual staminaCost need to multiply the base
    public float RunStaminaCost { get { return baseRunStaminaCost * runStaminaCost; } }

    public float armour;
    public float armourPierce;
    public float damage;   //Actual Damage = damage*amplifiers*max((armourPierce-armour+MAX_ARMOUR)/MAX_ARMOUR, 1);
    public float counterMounted;    //set to 1f if no effect
    public float chargeDamage;
    public float cost;
    

    public TroopType(string data)
    {
        //Debug.Log("TroopType Constructor For: " + data);
        string[] entries = data.Split(',');
        //Debug.Log("First Entry: " + entries[0]);
        type_name = entries[0];
        icon_name = entries[1];
        mounted = Convert.ToBoolean(entries[2]);
        ranged = Convert.ToBoolean(entries[3]);
        range = Convert.ToSingle(entries[4]);

        marchSpeed = Convert.ToSingle(entries[5]);
        runSpeed = Convert.ToSingle(entries[6]);
        runStaminaCost = Convert.ToSingle(entries[7]);

        armour = Convert.ToSingle(entries[8]);
        /*
        if (armour > MAX_ARMOUR)
            armour = MAX_ARMOUR;*/
        armourPierce = Convert.ToSingle(entries[9]);
        damage = Convert.ToSingle(entries[10]);
        counterMounted = Convert.ToSingle(entries[11]);
        chargeDamage = Convert.ToSingle(entries[12]);
        cost = Convert.ToSingle(entries[13]);
    }
}
