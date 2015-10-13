using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject terrainObj;
	private TextureGenerator terrain;

	private static float maxCameraSize = 3.5f;
	private static float minCameraSize = 0.2f;
	private float cameraSize{
		get{return mainCamera.orthographicSize;}
		set{
			if(value >= minCameraSize && value <= maxCameraSize)
				mainCamera.orthographicSize = value;
		}
	}
	private static Camera mainCamera;
	public bool isActive;

	public float marginRate = 0.05f;
	public float baseScrollSpeed = 1f;
	private int screenMargin;
	private float zoomSpeed = 1f;

	private float limit{
		get{return 0.5f * TextureGenerator.terrainSize - cameraSize;}
	}  //This is dynamically changed by camera size!

	private void constrainCameraPosition(){
		float x = mainCamera.transform.position.x;
		float y = mainCamera.transform.position.y;
        Debug.Log("In Constrain with limit: " + limit + " and x y: " + x + " " + y);
        if (x > limit)
			x = limit;
		if (x < -limit)
			x = -limit;
		if (y > limit)
			y = limit;
		if (y < -limit)
			y = -limit;
		mainCamera.transform.position = new Vector3(x,y,mainCamera.transform.position.z);
	}

	// Use this for initialization
	void Start () {
		terrain = terrainObj.GetComponent<TextureGenerator> ();
		mainCamera = this.GetComponent<Camera> ();
		isActive = true;
		screenMargin = (int)(Screen.width * marginRate);
	}
	
	// Update is called once per frame
	void Update () {

		if (!isActive)
			return;

		//check rollers for zoom-in and zoom-out
		float scrollSpeed = baseScrollSpeed * Mathf.Sqrt (cameraSize / minCameraSize);
		Vector3 newPosition = this.transform.position;
		Vector3 oldPosition = this.transform.position;
		if (Input.mousePosition.x < screenMargin || Input.mousePosition.x > Screen.width - screenMargin)
			newPosition += new Vector3 (Mathf.Sign (Input.mousePosition.x*1f - screenMargin) * scrollSpeed * Time.fixedDeltaTime, 0, 0);
		if (Input.mousePosition.y < screenMargin || Input.mousePosition.y > Screen.height - screenMargin)
			newPosition += new Vector3 (0, Mathf.Sign (Input.mousePosition.y*1f - screenMargin) * scrollSpeed * Time.fixedDeltaTime, 0);
		this.transform.position = newPosition;

		//check scroller

		this.cameraSize += -Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed;

		constrainCameraPosition ();
	}
}
