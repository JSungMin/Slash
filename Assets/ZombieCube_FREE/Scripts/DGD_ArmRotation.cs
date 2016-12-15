using UnityEngine;
using System.Collections;

public class DGD_ArmRotation : MonoBehaviour {

	[SerializeField]
	int rotationOffset = 90;

	float originalXPosition;
	// DGD_Controller2D player;

	void Start (){
		// player = GetComponentInParent<DGD_Controller2D> ();
		// originalXPosition = transform.localPosition.x;
	}

	void Update () {
		if (Camera.main != null) {
			Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
			difference.Normalize ();

			float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);

			/*
			if (player.flipped == true) {
				//Debug.Log ("X is fliped");
				transform.localPosition = new Vector2 ((originalXPosition * -1), transform.localPosition.y);
			} else {
				//Debug.Log ("X is NOOOOOTTTTT fliped");
				transform.localPosition = new Vector2 (originalXPosition, transform.localPosition.y);
			}
			*/
		}
	}
}
