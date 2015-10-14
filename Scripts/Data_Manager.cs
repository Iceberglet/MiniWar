using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Data_Manager : MonoBehaviour
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
    private static Sprite[] troop_sprites;

    private static Sprite sprite_advance;

    void Start()
    {
        troop_sprites = new Sprite[9];
        troop_sprites[0] = icon_infantry;
        troop_sprites[1] = icon_cavalry;
        troop_sprites[2] = icon_spear;
        troop_sprites[3] = icon_spear_heavy;
        troop_sprites[4] = icon_skirmish;
        troop_sprites[5] = icon_arrow;
        troop_sprites[6] = icon_crossbow;
        troop_sprites[7] = icon_armour_light;
        troop_sprites[8] = icon_armour_heavy;
        sprite_advance = icon_advance;
    }
    /*
    public enum TroopType
    {
        Pikeman = 0, Skirmisher = 1, Archer = 2, Crossbowman = 3, Heavy_Pikeman = 4,
        Lancer = 5, Mounted_Archer = 6, Heavy_Cavalry = 7
    } //, Artillery = 8 };    //to access the implicit type, cast it to int:  (int)myTroopType*/
    public static void ObtainTroopSprite(int type, out Sprite top, out Sprite mid, out Sprite bot, out Sprite advance)
    {
        advance = sprite_advance;
        //Top: infantry or cavalry; Middle: Arm Type; Bottom: Armour Type
        int t, m, b;
        switch (type)
        {
            case 0: t = 0; m = 2; b = 7;
                break;
            case 1: t = 0; m = 4; b = 7;
                break;
            case 2: t = 0; m = 5; b = 7;
                break;
            case 3: t = 0; m = 6; b = 7;
                break;
            case 4: t = 0; m = 2; b = 8;
                break;
            case 5: t = 1; m = 2; b = 7;
                break;
            case 6: t = 1; m = 5; b = 7;
                break;
            case 7: t = 1; m = 3; b = 8;
                break;
            default: t = -1; m = -1; b = -1;
                break;
        }
        top = troop_sprites[t];
        mid = troop_sprites[m];
        bot = troop_sprites[b];
    }

    //*************** Faction Colors **************
    //To Be Changed TO Incorporate Everything in Faction Class
    //public static Faction[] factions;
}