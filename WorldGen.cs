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
	public bool generateUnderground = false;

	[Header("Perlin Noise Resolution:")]
	[Range(0.0f,0.2f)]
	public float resMultiplier = 0.039f;
	[Range(0.0f,100.0f)]
	public float resEnhancer = 16.8f;
	public ReturnValue state = ReturnValue.Int;
	public float floor = -10.0f;

	[Header("2nd Layer PNoise Resolution:")]
	[Range(0.0f,0.2f)]
	public float secondLayerMultiplier = 0.039f;
	[Range(0.0f,0.2f)]
	public float offset= 0.039f;

	private Vector3[] topBlocks;
	private int c = 0;
	private bool rendered = true;

	// Use this for initialization
	void Start () {
		int bufferSize = (int) maxXWidth * (int) maxZWidth;
		Debug.Log (bufferSize);
		topBlocks = new Vector3[bufferSize];
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
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			if (rendered) {
				Debug.Log ("WorldBlock: MeshRenderer Off");
				rendered = false;
				foreach (GameObject g in GameObject.FindGameObjectsWithTag("WorldBlock")) {
					g.GetComponent<MeshRenderer> ().enabled = false;
				}
			} else {
				rendered = true;
				Debug.Log ("WorldBlock: MeshRenderer On");
				foreach (GameObject g in GameObject.FindGameObjectsWithTag("WorldBlock")) {
					g.GetComponent<MeshRenderer> ().enabled = true;
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.O)) {
			if (rendered) {
				Debug.Log ("WorldBlock: Cast Shadows Off");
				foreach (GameObject g in GameObject.FindGameObjectsWithTag("WorldBlock")) {
					g.GetComponent<MeshRenderer> ().receiveShadows = false;
				}
			} else {
				Debug.Log ("WorldBlock: Cast Shadows On");
				foreach (GameObject g in GameObject.FindGameObjectsWithTag("WorldBlock")) {
					g.GetComponent<MeshRenderer> ().receiveShadows = true;
				}
			}
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

				float z = Mathf.InverseLerp (Mathf.Clamp (y, 0.2f, 0.9f), Mathf.Clamp (y, 0.8f, 1.0f), Mathf.PerlinNoise ((i * secondLayerMultiplier) + offset, (j * secondLayerMultiplier) + offset));
				Vector3 vect;
				switch (state) {
				case ReturnValue.Float:
					vect = new Vector3 (i, z * resEnhancer, j);
					Instantiate (worldBlock, vect, Quaternion.identity);
					addVec3 (vect);
					break;
				case ReturnValue.Int:
					vect = new Vector3 (i,(int)(z*resEnhancer), j);
					addVec3 (vect);
					Instantiate (worldBlock, vect, Quaternion.identity);
					break;

				}

			}

		}

		//Debug.Log ("Blocks Count: " + topBlocks.Length);
		//yield return new WaitForSeconds (2.0f);
		int count = 0;
		foreach (Vector3 v in topBlocks) {
			count++;
		}
		if (generateUnderground)
			StartCoroutine ("startTheUnderworld");
		yield return null;
	}

	IEnumerator startTheUnderworld(){
		foreach (Vector3 v in topBlocks) {
			for (float i = v.y; i > floor+1.0f; i--) {
				Instantiate (worldBlock, new Vector3 (v.x, i-1.0f, v.z), Quaternion.identity);
			}
		}
		yield return null;
	}


	void addVec3(Vector3 vec){
		topBlocks [c] = vec;
		c++;
	}

}
