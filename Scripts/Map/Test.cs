using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	private TextureGenerator terrain;

	// Use this for initialization
	void Start () {
		terrain = this.gameObject.GetComponent<TextureGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("Camera Height: " + Camera.main.pixelHeight);
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, -Input.mousePosition.y + Camera.main.pixelHeight, 0));
		RaycastHit hit;
		
		//rayhit on any collider other than the ghost ones AND it's a new position
		if(Physics.Raycast (ray, out hit, Mathf.Infinity))
		{
			Vector3 hitPoint = hit.point;
			float height = terrain.getHeight(hitPoint.x, -hitPoint.y);
			//Debug.Log ("Point hit on plane: "+hitPoint.x+", "+hitPoint.y+" which is of height: "+height);
		}

	}
}
