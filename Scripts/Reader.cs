using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Reader
{
    private static List<TroopType> troopTypes;
    public static List<TroopType> TroopTypes { get { return troopTypes; } }
    public static string troopTypeFileName = "TroopTypeData";

    public Reader()
    {
        loadType();
    }

    private bool loadType()
    {
        troopTypes = new List<TroopType>();
        TextAsset text = Resources.Load(troopTypeFileName) as TextAsset;
        string[] lines = text.text.Split('\n');
        for(int i = 1; i < lines.Length; i++)
        {
            if (lines[i] == null || lines[i] == "")
                continue;
            TroopType newType = new TroopType(lines[i]);
            troopTypes.Add(newType);
        }
        return true;
    }

    public static TroopType getTroopType(string name)
    {
        foreach(TroopType t in troopTypes)
        {
            if (t.type_name == name)
                return t;
        }
        return null;
    }
}
