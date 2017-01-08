using UnityEngine;
using System.Collections;

/*
 * Create a tag named "WorldBlock", make a prefab with a 1x1x1 square, attach tag
 * drag to World Block: field.  Press G to generate world, Press T to destory blocks
 * 
 */

public class WorldGen : MonoBehaviour {

	public enum ReturnValue {
		Float = 0, 
		Int = 1
	}


	[Header("Fill the world with: (a 1x 1 object)")]
	public GameObject worldBlock;

	[Header("World Stats:")]
	public float maxXWidth = 100;
	public float maxYWidth = 1;
	public float maxZWidth = 100;

	[Header("Perlin Noise Resolution:")]
	[Range(0.0f,0.2f)]
	public float resMultiplier = 0.039f;
	[Range(0.0f,100.0f)]
	public float resEnhancer = 16.8f;
	public ReturnValue state = ReturnValue.Int;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		generateWorld ();
	}

	void generateWorld (){
		if (Input.GetKeyDown (KeyCode.G)) {
			StartCoroutine ("startTheWorld");
			//startTheWorld();
		}

		if (Input.GetKeyDown (KeyCode.T)) {
			foreach (GameObject g in GameObject.FindGameObjectsWithTag("WorldBlock")) {
				Destroy (g);
			}

			//startTheWorld();
		}
	}

	void startTheWorldNoCo(){
		//X and Z
		for (float i = 0.0f; i < maxXWidth; i += 1.0f){
			for (float j = 0.0f; j < maxZWidth; j += 1.0f) {
				Instantiate (worldBlock, new Vector3 (i, maxYWidth, j), Quaternion.identity);
			}
		}
	}

	IEnumerator startTheWorld(){
		//X and Z
		for (float i = 0.0f; i < maxXWidth; i += 1.0f){
			for (float j = 0.0f; j < maxZWidth; j += 1.0f) {
				float y = Mathf.PerlinNoise(i*resMultiplier, j*resMultiplier);

				switch (state) {
				case ReturnValue.Float:
					Instantiate (worldBlock, new Vector3 (i, y*resEnhancer, j), Quaternion.identity);
					break;
				case ReturnValue.Int:
					Instantiate (worldBlock, new Vector3 (i, (int)(y*resEnhancer), j), Quaternion.identity);
					break;

				}

			}

		}
		yield return null;
	}
}
