using UnityEngine;
using System.Collections;

public class MovementHandler : MonoBehaviour {
	public float speed;
	private float moveSpeed;
	private float hiSpeed;

	[Header("Movement Smoothing")]
	public float smoothTime = 0.3f;
	private bool moving = false;
	private float zVelocity = 0.0f;
	private float zVelocity2 = 0.0f;
	private float xVelocity = 0.0f;
	private float xVelocity2 = 0.0f;



	// Use this for initialization
	void Start () {
		moveSpeed = speed;
		hiSpeed = speed * 1.5f;

	}
	
	// Update is called once per frame
	void Update () {
		handleInput ();
	}

	void handleInput (){
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			moveSpeed = hiSpeed;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift)) {
			moveSpeed = speed;
		}



		if (Input.GetKey (KeyCode.W)) {
			Mathf.SmoothDamp (0.0f, 1.0f, ref zVelocity, smoothTime);
		} else {
			Mathf.SmoothDamp (0.0f, 0.0f, ref zVelocity, smoothTime);
		}

		if (Input.GetKey(KeyCode.S)){
			Mathf.SmoothDamp (0.0f, 1.0f, ref zVelocity2, smoothTime);
		} else {
			
			Mathf.SmoothDamp (0.0f, 0.0f, ref zVelocity2, smoothTime);
		}

		if (Input.GetKey (KeyCode.D)) {
			Mathf.SmoothDamp (0.0f, 1.0f, ref xVelocity, smoothTime);
		} else {
			Mathf.SmoothDamp (0.0f, 0.0f, ref xVelocity, smoothTime);
		}

		if (Input.GetKey (KeyCode.A)) {
			Mathf.SmoothDamp (0.0f, 1.0f, ref xVelocity2, smoothTime);
		} else {
			Mathf.SmoothDamp (0.0f, 0.0f, ref xVelocity2, smoothTime);
		}

		transform.Translate ((xVelocity-xVelocity2)* moveSpeed * Time.deltaTime, 0, (zVelocity-zVelocity2)* moveSpeed * Time.deltaTime);
	}
}
