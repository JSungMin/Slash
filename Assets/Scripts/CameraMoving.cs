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

	public Vector3 obstacleDir;

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
				mainCamera.orthographicSize = Mathf.Lerp (mainCamera.orthographicSize - 0.1f, maxZoomIn, Time.deltaTime * 8);
			} else {
				isZoomIn = false;
			}
		}
	}

	public IEnumerator ZoomOut(){
		while (true) {
			yield return null;
			if (!isZoomIn&&mainCamera.orthographicSize<maxZoomOut) {
				mainCamera.orthographicSize = Mathf.Lerp (mainCamera.orthographicSize,maxZoomOut,Time.deltaTime*2);
			}
		}
	}

	RaycastHit2D leftHit;
	RaycastHit2D rightHit;
	RaycastHit2D upHit;
	RaycastHit2D downHit;

	public void ProcessOutSide(){
		leftHit = Physics2D.Raycast(player.position, Vector2.left,5,1<<LayerMask.NameToLayer("BlockingLayer"));
		rightHit = Physics2D.Raycast (player.position, Vector2.right,5,1<<LayerMask.NameToLayer("BlockingLayer"));
		upHit = Physics2D.Raycast (player.position,Vector2.up,5,1<<LayerMask.NameToLayer("BlockingLayer"));
		downHit = Physics2D.Raycast (player.position, Vector2.down,5,1<<LayerMask.NameToLayer("BlockingLayer"));

		var dx = rightHit.distance - leftHit.distance;
		if (dx > 0)
			dx -= 5;
		else if (dx < 0)
			dx += 5;
		var dy = upHit.distance - downHit.distance;

		if (dy > 0)
			dy -= 3;
		else if (dy < 0)
			dy += 3;

		obstacleDir = new Vector3 (dx, dy, 0);

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
			
		ProcessOutSide ();

		tmpPosition += dir * dis + obstacleDir;

		transform.position = Vector3.Lerp (transform.position, tmpPosition, Time.deltaTime * 10);

		if (isShaking) {
			angle += Time.deltaTime * 50;
			transform.position += Mathf.Sin (angle)*dir*0.25f;
		}
	}

}
