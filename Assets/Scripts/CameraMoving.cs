using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour {
	public Camera mainCamera;

	public Transform player;

	public Vector3 mousePosition;

	public float maxCameraDis;

	public void Start(){
		mainCamera = GetComponent<Camera> ();
		player = GameObject.FindObjectOfType<Player> ().transform;
		Cursor.lockState = CursorLockMode.Confined;
	}

	public void Update(){
		mousePosition = Input.mousePosition;
		var tmpPosition = player.position;
		tmpPosition.z = -10;
		var dir = (mousePosition - player.position).normalized;
		dir.z = 0;
		var dis = Vector3.Distance (mousePosition, player.position);

		if (dis > maxCameraDis) {
			dis = maxCameraDis;
		}

		transform.position = Vector3.Lerp (transform.position, tmpPosition + dir * dis, Time.deltaTime*10);

	}

}
