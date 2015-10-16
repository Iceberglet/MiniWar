using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Job Description:
//1. Keep reference to every single troop on the field
//2. Commanding Interface, Player Control
//3. Call troop status update
//4. Time Speed Control

public class BattleFieldManager : MonoBehaviour {

    //Data Storage
    private static int speed;        //From 0 to 3, under player control
    private Troop[] troopList_1;
    private Troop[] troopList_2;

    //Heights
    public const float troopHeight = -3f;
    public const float terrainHeight = -2f;

    //Selection Box
    private GUIStyle customStyle;
    private Vector3 boxStartWorld;
    private Vector3 boxEndWorld;
    private Vector2 boxStartScreen;
    private Vector2 boxEndScreen;
    public Texture2D boxTexture;
    private bool isDragging;
    private GameObject selectionBox;
    private List<Troop> selectedTroops;

    //Accessible Variables
    public static float deltaTime    // accessible to all troops
    {
        get { return Time.deltaTime * speed; }
    }

	// Use this for initialization
	void Start () {
        selectionBox = null;
        selectedTroops = new List<Troop>();
        customStyle = new GUIStyle();
        customStyle.normal.background = boxTexture;
        customStyle.border.bottom = customStyle.border.top = customStyle.border.left = customStyle.border.right = 3;
    }
	

	// Update is called once per frame
	void Update () {
        //******* Selection/Command *********
        // Use Left Mouse For Selection/Drag Box
        // Use Right Mouse For Movement
        if (Input.GetMouseButtonDown(0))
        {
            if (selectionBox != null)
                Destroy(selectionBox);
            //selectedTroops.Clear();
            boxStartScreen = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            boxStartWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            boxStartWorld.z = troopHeight;  //This is legit only because our camera is always facing in positive z direction :D
            //Debug.Log("Mouse Down! at pos: " + Input.mousePosition + ", resulting boxStart Point: " + boxStart);
        }
        if (Input.GetMouseButton(0))
        {
            isDragging = true;
            boxEndScreen = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            boxEndWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            boxEndWorld.z = troopHeight;
            if (!selectionBox)
            {
                selectionBox = new GameObject("Selection Box");
                selectionBox.AddComponent<BoxCollider2D>();
            }
            selectionBox.transform.position = 0.5f * (boxStartWorld + boxEndWorld);
            selectionBox.GetComponent<BoxCollider2D>().size = new Vector2(Mathf.Abs(boxStartWorld.x - boxEndWorld.x), Mathf.Abs(boxStartWorld.y - boxEndWorld.y));
            //Debug.Log("You Are Holding Mouse! Start at " + boxStartScreen + " end " +boxEndScreen);
            clearSelected();
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Unit"))
            {
                if (selectionBox.GetComponent<BoxCollider2D>().OverlapPoint(g.transform.position))
                {
                    g.GetComponent<Troop>().highLight(true);
                    selectedTroops.Add(g.GetComponent<Troop>());
                }
            }
            //Debug.Log("Box at: " + selectionBox.transform.position +" of size "+ selectionBox.GetComponent<BoxCollider2D>().size);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            //Debug.Log("Mouse Up! at pos: " + Input.mousePosition + ", resulting boxEnd Point: " + boxEnd);
            Destroy(selectionBox);
            //Debug.Log("You have selected: " + selectedTroops.Count + " troops");
        }
    }

    void clearSelected()
    {
        foreach (Troop t in selectedTroops)
            t.highLight(false);
        selectedTroops.Clear();
    }

    void OnGUI()
    {
        if (isDragging)
        {
            float x = (boxStartScreen.x < boxEndScreen.x) ? boxStartScreen.x : boxEndScreen.x;
            float y = Screen.height - ((boxStartScreen.y > boxEndScreen.y) ? boxStartScreen.y : boxEndScreen.y);
            GUI.Box(new Rect(x, y, Mathf.Abs(boxStartScreen.x - boxEndScreen.x), Mathf.Abs(boxStartScreen.y - boxEndScreen.y)), "", customStyle);
            //Top-Left is 0,0 But MouseInputPosition's 0,0 is Bottom-Left! Hence needed the conversion
        }
    }
}
