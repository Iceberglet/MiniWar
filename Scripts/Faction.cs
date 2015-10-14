using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Faction
{
    private string name;
    private Color factionColor;
    private List<Army> factionArmy;
    //private List<City> factionCity;

    public string Name { get { return name; } }
    public Color Color { get { return factionColor; } }
    public List<Army> Armies { get { return factionArmy; } }

    public void initialize(string name, Color c)
    {
        this.name = name;
        this.factionColor = c;
    }
    //To Add: 
    //1. Resources
    //2. Territory
    //3. Technology and Corresponding Troop-anti data
}
