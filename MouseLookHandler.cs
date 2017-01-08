using UnityEngine;
using System.Collections;

public class MouseLookHandler : MonoBehaviour {

	public enum RotationAxes
	{
		MouseXAndY = 0,
		MouseX = 1, 
		MouseY = 2
	}

	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float speed = 6.0f;
	public float sensitivityHor = 9.0f;
	public float sensitivityVert = 9.0f;


	void Start () {
		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;

	}

	void Update () {
		handleMouseLook ();
	}


	void handleMouseLook (){
		switch (axes) {
		case RotationAxes.MouseX:
			
			transform.Rotate (0, Input.GetAxis ("Mouse X") * sensitivityHor, 0);
			break;
		case RotationAxes.MouseY:
			transform.Rotate (-Input.GetAxis ("Mouse Y") * sensitivityVert, 0, 0);
			break;
		default:
			
			break;
		}
	}
}
