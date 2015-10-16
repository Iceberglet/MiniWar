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
    private List<Troop> selectedTroops;
    public Texture2D boxTexture;
    private bool isDragging;
    
    //Accessible Variables
    public static float deltaTime    // accessible to all troops
    {
        get { return Time.deltaTime * speed; }
    }

	// Use this for initialization
	void Start () {
        /*
        boxTexture = new Texture2D(1, 1);
        boxTexture.SetPixel(1, 1, new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 0.4f));
        boxTexture.wrapMode = TextureWrapMode.Repeat;
        boxTexture.Apply();*/
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
            Debug.Log("You Are Holding Mouse! Start at " + boxStartScreen + " end " +boxEndScreen);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            //Debug.Log("Mouse Up! at pos: " + Input.mousePosition + ", resulting boxEnd Point: " + boxEnd);
        }
    }

    void OnGUI()
    {
        if (isDragging)
        {
            float x = (boxStartScreen.x < boxEndScreen.x) ? boxStartScreen.x : boxEndScreen.x;
            float y = Screen.height - ((boxStartScreen.y > boxEndScreen.y) ? boxStartScreen.y : boxEndScreen.y);
            GUI.Box(new Rect(x, y, Mathf.Abs(boxStartScreen.x - boxEndScreen.x), Mathf.Abs(boxStartScreen.y - boxEndScreen.y)), "", customStyle);
            //GUI.Box(new Rect(10, 10, 50, 100), "");
        }
    }
}
