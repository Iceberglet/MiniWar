using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class Sprite_Dispenser : MonoBehaviour
{
    public Sprite icon_infantry;
    public Sprite icon_cavalry;
    public Sprite icon_spear;
    public Sprite icon_spear_heavy;
    public Sprite icon_skirmish;
    public Sprite icon_arrow;
    public Sprite icon_crossbow;
    public Sprite icon_armour_light;
    public Sprite icon_armour_heavy;
    public Sprite icon_advance;

    //Keep Stuff Private After Initialization
    private static Sprite sprite_infantry;
    private static Sprite sprite_cavalry;
    private static Sprite sprite_spear;
    private static Sprite sprite_spear_heavy;
    private static Sprite sprite_skirmish;
    private static Sprite sprite_arrow;
    private static Sprite sprite_crossbow;
    private static Sprite sprite_armour_light;
    private static Sprite sprite_armour_heavy;
    private static Sprite sprite_advance;

    void Start()
    {
        sprite_infantry = icon_infantry;
        sprite_cavalry = icon_cavalry;
        sprite_spear = icon_spear;
        sprite_spear_heavy = icon_spear_heavy;
        sprite_skirmish = icon_skirmish;
        sprite_arrow = icon_arrow;
        sprite_crossbow = icon_crossbow;
        sprite_armour_light = icon_armour_light;
        sprite_armour_heavy = icon_armour_heavy;
        sprite_advance = icon_advance;
    }

    public static void ObtainTroopSprite(int type, out Sprite top, out Sprite mid, out Sprite bot, out Sprite advance)
    {
        advance = sprite_advance;
        // Top is infantry OR cavalry
        if (type <= 4)
            top = sprite_infantry;
        else top = sprite_cavalry;

        // Middle is spear, arrow, crossbow, skirmish or heavy_spear;
        switch (type)
        {
            case 0: case 4: case 5:
                mid = sprite_spear;break;
            case 2: case 6:
                mid = sprite_arrow;break;
            case 1:
                mid = sprite_skirmish;break;
            case 3:
                mid = sprite_crossbow;break;
            case 7:
                mid = sprite_spear_heavy;break;
            default:
                Debug.Log("Mid Sprite Not Found: Default of Spear Used");
                mid = sprite_spear; break;
        }
        //Bottom is round shield for light soldier
        switch (type)
        {
            case 0: case 1: case 2: case 3: case 5: case 6:
                bot = sprite_armour_light;
                break;
            case 4: case 7:
                bot = sprite_armour_heavy;
                break;
            default:
                Debug.Log("Bottom Sprite Not Found: Default of Light Armour Used");
                bot = sprite_armour_light;
                break;
        }
    }

    //*************** Faction Colors **************
    public static Color[] factionColors;
    
}