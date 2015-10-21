using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	private TextureGenerator terrain;
    
	void Start () {
		terrain = this.gameObject.GetComponent<TextureGenerator> ();

        ///******** Testing For Sprite Displays ********
        Faction f = new Faction();
        f.initialize("A Faction", Color.black);

        Reader r = new Reader();
        Troop t = this.gameObject.AddComponent<Troop>();
        TroopType tt = Reader.getTroopType("cavalry_heavy");
        tt.faction = f;
        t.initialize(tt, 500);

        GameObject troopObj = TroopOnField.instantiate(t, new Vector2(0, 0), this.gameObject);
        //TroopOnField.instantiate(t, new Vector2(1, 0), this.gameObject);
        //TroopOnField.instantiate(t, new Vector2(1, 1), this.gameObject);
        troopObj.GetComponent<TroopOnField>().troop_stat.status = TroopStats.TroopStatus.Rout;
    }
	
	void Update () {
        /******* Dynamic Height Detector *******
		//Debug.Log ("Camera Height: " + Camera.main.pixelHeight);
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, -Input.mousePosition.y + Camera.main.pixelHeight, 0));
		RaycastHit hit;
		
		//rayhit on any collider other than the ghost ones AND it's a new position
		if(Physics.Raycast (ray, out hit, Mathf.Infinity))
		{
			Vector3 hitPoint = hit.point;
			float height = terrain.getHeight(hitPoint.x, -hitPoint.y);
			//Debug.Log ("Point hit on plane: "+hitPoint.x+", "+hitPoint.y+" which is of height: "+height);
		}*/
	}
}
