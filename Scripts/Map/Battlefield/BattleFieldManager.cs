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
    private static int speed = 1;        //From 0 to 3, under player control
    //private TroopOnField[] troopList_1;
    //private TroopOnField[] troopList_2;

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
    private static List<TroopOnField> selectedTroops;

    //Accessible Variables
    public static float deltaTime    // accessible to all troops
    {
        get { return Time.deltaTime * speed; }
    }

	// Use this for initialization
	void Start () {
        selectionBox = null;
        selectedTroops = new List<TroopOnField>();
        customStyle = new GUIStyle();
        customStyle.normal.background = boxTexture;
        customStyle.border.bottom = customStyle.border.top = customStyle.border.left = customStyle.border.right = 3;
    }
	

	// Update is called once per frame
	void Update () {
        if (treatLeftClick())
            return;
        if (treatRightClick())
            return;
    }

    //Returns true on successful treatment with commands
    bool treatRightClick()
    {
        //**********  Command Right Click **********
        if (Input.GetMouseButtonUp(1))
        {
            if (selectedTroops.Count == 0)
                return false;
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            clickPos.z = troopHeight;
            Target t = null;
            //Targeting a troop?
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Unit"))
            {
                // Check if click pos falls onto this gameObject
                if (Vector3.Distance(g.transform.position, clickPos) < TroopOnField.colliderSize* TroopOnField.scale)
                {                
                    // Check we are not clicking on ourselves
                    if (selectedTroops.Contains(g.GetComponent<TroopOnField>()))
                    {
                        Debug.Log("Clicked on one of your troop! returning");
                        return false;
                    }
                    t = new Target(true, g.transform.position, g.GetComponent<TroopOnField>());
                    break;
                }
            }
            //Targeting just a place?
            if(t == null)
                t = new Target(false, clickPos);
            foreach (TroopOnField troop in selectedTroops)
            {
                troop.target = t;
            }
            Debug.Log("Target taken for " + selectedTroops.Count + " troops towards initialPos " + t.targetPos);
            return true;
        }
        return false;
    }

    bool treatLeftClick()
    {
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
            return true;
        }
        else if (Input.GetMouseButton(0))
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
            //Adding selected and highlight them
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Unit"))
            {
                if (selectionBox.GetComponent<BoxCollider2D>().OverlapPoint(g.transform.position))
                    addToSelection(g);
            }
            //Debug.Log("Box at: " + selectionBox.transform.position +" of size "+ selectionBox.GetComponent<BoxCollider2D>().size);
            return true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            //Debug.Log("Mouse Up! at pos: " + Input.mousePosition + ", resulting boxEnd Point: " + boxEnd);
            Destroy(selectionBox);
            //Debug.Log("You have selected: " + selectedTroops.Count + " troops");

            //TODO: If SelectionBox does not detect anything, CHECK IF CLICKED ON COLLIDER OF ANY TROOP
            if (selectedTroops.Count == 0)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Unit"))
                {
                    if (g.GetComponent<CircleCollider2D>().OverlapPoint(pos))
                    {
                        addToSelection(g);
                        break;
                    }
                }
            }
            return true;
        }
        return false;
    }

    void addToSelection(GameObject g)
    {
        TroopOnField t = g.GetComponent<TroopOnField>();
        t.highLight(true);
        t.ignoreMouse = true;
        selectedTroops.Add(t);
    }

    void clearSelected()
    {
        foreach (TroopOnField t in selectedTroops)
        {
            t.highLight(false);
            t.ignoreMouse = false;
        }
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
