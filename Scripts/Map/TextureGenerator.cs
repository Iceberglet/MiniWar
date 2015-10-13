using UnityEngine;
using System.Collections;
using System.IO;

public class TextureGenerator : MonoBehaviour {

	private Texture2D heightmap;
	private static int startSize = 64;
	private static int heightmapSize = 513;
	private static float roughness = 1f;
	private float[,] bitMap;
	public static float terrainSize = 10f;   //By Default

    private void initializeTerrainParameters()
    {
        this.heightmap = new Texture2D(heightmapSize, heightmapSize);
        GetComponent<Renderer>().material.mainTexture = heightmap;

        bitMap = new float[heightmapSize, heightmapSize];
        for (int xx = 0; xx < heightmapSize; ++xx)
            for (int yy = 0; yy < heightmapSize; ++yy)
            {
                bitMap[xx, yy] = 0;
            }
    }

    //Used to fill up the heightmap
	private IEnumerator makeTerrain(int initialSize){
		int count = 0;
		while (initialSize >= 2) {
			divide(initialSize);
			initialSize/=2;
			Debug.Log ("Terrain Generation Iteration Count: "+count);
			count++;
			yield return null;
		}
		//coloring!
		float max = 0;
		float min = 0;
		for (int xx = 0; xx < heightmapSize; ++xx)
		for (int yy = 0; yy < heightmapSize; ++yy) {
			if(bitMap[xx,yy] > max)
				max = bitMap[xx,yy];
			if(bitMap[xx,yy] < min)
				min = bitMap[xx,yy];
		}
		for (int xx = 0; xx < heightmapSize; ++xx)
			for (int yy = 0; yy < heightmapSize; ++yy) {

			float depth = 2*(bitMap[xx,yy] - min)/(max - min); // from 0 to 2
			Color newColor = new Color();
			if(depth > 1)
				newColor = new Color(1, 2-depth, 0);
			else
				newColor = new Color(depth, 1,0);
			heightmap.SetPixel(xx,yy,newColor);
		}
		/*	Testing for x and y axis
		for (int xx = 0; xx < heightmapSize; ++xx) {
			heightmap.SetPixel(xx,0,Color.red);
			heightmap.SetPixel(xx,1,Color.red);
			heightmap.SetPixel(0,xx,Color.black);
			heightmap.SetPixel(1,xx,Color.black);
		}*/


		heightmap.Apply();
		byte[] b = (byte[])heightmap.EncodeToPNG ();
		//Debug.Log (Application.persistentDataPath);
		File.WriteAllBytes("D:/" + "new_texture_" + b.GetHashCode() + ".png", b);
		Debug.Log ("Done With Height Map Generation!");
	}
	
    //Method to subdivide terrain and raise/lower vertices situated *size* distance apart
	private void divide(int size){
		if (size <= 1)
			return;
		int half = size / 2;
		float scale = roughness * size;

		for (int y = half; y < heightmapSize; y += size) {
			for (int x = half; x < heightmapSize; x += size) {
				float sum = 0;
				float offset = Random.Range(-1f,1f)*scale;
				if(y - half >= 0){
					if(x - half >= 0)
						sum += bitMap[(x - half), (y - half)];
					if(x + half < bitMap.GetLength(0))
						sum += bitMap[(x + half), (y - half)];
				}
				if(y + half < bitMap.GetLength(1)){
					if(x - half >= 0)
						sum += bitMap[(x - half), (y + half)];
					if(x + half < bitMap.GetLength(0))
						sum += bitMap[(x + half), (y + half)];
				}
				bitMap[x,y] = sum/4 + offset;
			}

		}
		for (int y = 0; y < heightmapSize; y += half) {
			int start = 0;
			if ((y + half)%size != 0)
				start = half;
			for (int x = start; x < heightmapSize; x += size) {
				if (x >= 0 && y >= 0){
					float sum = 0;
					int count = 0;
					float offset = Random.Range(-1f,1f)*scale;
					if(y - half >= 0){
						count++;
						sum += bitMap[x, y - half];
					}
					if(y + half < bitMap.GetLength(1 )){
						count++;
						sum += bitMap[x, (y + half)];
					}
					if(x + half < bitMap.GetLength(0)){
						count++;
						sum += bitMap[(x + half), y];
					}
					if(x - half >= 0){
						count++;
						sum += bitMap[(x - half), y];
					}
					bitMap[x,y] = sum/count + offset;
				}
			}
		}
	}

    // a substitute for Color constructor
	private Color rgb(int r, int g, int b){
		return new Color (1f*r/255, 1f*g/255, 1f*b/255);
	}

	//******************* Public Functions *****************
	// By Default, terrain size is 10*10, also heightmapSize*heightmapSize
	public float getHeight(float x, float y){
		int xx = (int)((x + terrainSize/2) / terrainSize * heightmapSize);
		int yy = (int)((y + terrainSize/2) / terrainSize * heightmapSize);
		//Debug.Log ("xx and yy: " + xx + ", " + yy);
		return bitMap[xx,yy];
	}

	// Use this for initialization
	void Start () {
        initializeTerrainParameters();
		StartCoroutine(makeTerrain (startSize));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
