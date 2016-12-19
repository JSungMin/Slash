using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour {
	public Camera mainCamera;

	public Transform player;

	public Vector3 mousePosition;

	public float maxCameraDis;

	public bool isShaking = false;

	[SerializeField]
	private float maxZoomIn;
	[SerializeField]
	private float maxZoomOut;

	public bool isZoomIn = false;

	public void Start(){
		mainCamera = GetComponent<Camera> ();
		player = GameObject.FindObjectOfType<Player> ().transform;
		StartCoroutine (ShakeCamera ());
		StartCoroutine (ZoomIn());
		StartCoroutine (ZoomOut());
	}

	public float angle;

	public IEnumerator ShakeCamera(){
		while (true) {
			yield return null;
			if(isShaking){
				yield return new WaitForSeconds (0.2f);
				isShaking = false;
			}
		}
	}

	public IEnumerator ZoomIn(){
		while(true){
			yield return null;
			if (isZoomIn && mainCamera.orthographicSize > maxZoomIn) {
				mainCamera.orthographicSize = Mathf.Lerp (mainCamera.orthographicSize - 0.1f, maxZoomIn, Time.deltaTime * 3);
			} else {
				isZoomIn = false;
			}
		}
	}

	public IEnumerator ZoomOut(){
		while (true) {
			yield return null;
			if (!isZoomIn&&mainCamera.orthographicSize<maxZoomOut) {
				mainCamera.orthographicSize = Mathf.Lerp (mainCamera.orthographicSize,maxZoomOut,Time.deltaTime*3);
			}
		}
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
		if (isShaking) {
			angle += Time.deltaTime * 50;
			transform.position += Mathf.Sin (angle)*dir*0.25f;
		}
	}

}
