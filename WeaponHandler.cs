using UnityEngine;
using System.Collections;

public class WeaponHandler : MonoBehaviour {

	private Animator animator;
	private float slide;
	private float slideTarget;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		handleInput ();
	}

	private void handleInput(){

		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D)) {
			animator.SetBool ("Moving", false);
		}
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			animator.SetBool ("Moving", true);
		}


		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			slide = 1.0f;
			slideTarget = 2.7f;
			StartCoroutine ("ChangeSpeed");
		} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
			slide = 2.7f;
			slideTarget = 1.0f;
			StartCoroutine ("ChangeSpeed");
		}

		if (Input.GetMouseButtonDown(0)) {
			animator.SetTrigger ("Swing");
		}
		if (Input.GetMouseButtonDown(1)) {
		}
	}

	IEnumerator ChangeSpeed(){
		if (slide < slideTarget && animator.GetBool("Moving")) {
			for (float i = slide; i < slideTarget; i += 0.1f) {
				animator.SetFloat ("Speed", i);
				yield return new WaitForSeconds (0.05f);
			}	
		} else {
			for (float i = slide; i > slideTarget; i -= 0.1f) {
				animator.SetFloat ("Speed", i);
				yield return new WaitForSeconds (0.05f);
			}
		}
	}
}
