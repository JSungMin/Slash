using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float walkDis;
	public float attackDis;

	public Vector3 dir;

	public Vector3 xDir;
	public Vector3 yDir;

	public bool leftMouse;

	public Vector3 clickPosition;
	public Vector3 attackDir;
	public Vector3 targetPosition;

	public bool isAttack;
	public bool isReloaing = false;

	public float intenceDistance;

	public float attackDelayTime;

	// Use this for initialization
	void Start () {
		attackDelay = AttackDelay (attackDelayTime);
	}

	private void KeyInputProcess(){
		if(Input.GetKey(KeyCode.A)){
			xDir = Vector3.left;
			dir = (xDir + yDir).normalized;
		}
		if(Input.GetKey(KeyCode.S)){
			yDir = Vector3.down;
			dir = (xDir + yDir).normalized;
		}
		if(Input.GetKey(KeyCode.D)){
			xDir = Vector3.right;
			dir = (xDir + yDir).normalized;
		}
		if(Input.GetKey(KeyCode.W)){
			yDir = Vector3.up;
			dir = (xDir + yDir).normalized;
		}

		transform.Translate (dir*walkDis*Time.deltaTime);

		dir = Vector3.zero;
		xDir = Vector3.zero;
		yDir = Vector3.zero;
	}

	private void MouseInputProcess(){
		leftMouse = Input.GetMouseButton (0);

		if (leftMouse) {
			if (!isReloaing) {
				clickPosition = Input.mousePosition;
				clickPosition = Camera.main.ScreenToWorldPoint (clickPosition);

				attackDir = (clickPosition - transform.position).normalized;

				targetPosition = transform.position + attackDir * attackDis;

				targetPosition = new Vector3 (targetPosition.x, targetPosition.y, 0);

				Debug.Log (attackDelay);

				isAttack = true;
				StopCoroutine ("AttackDelay");
				StartCoroutine ("AttackDelay",attackDelayTime);
			}
		}
	}

	IEnumerator attackDelay;

	public IEnumerator AttackDelay(float time){
		isReloaing = true;

		Debug.Log ("Reloading");

		yield return new WaitForSeconds (time);
		isReloaing = false;
		Debug.Log ("Reloading Finished");
	}

	// Update is called once per frame
	void Update () {
		if (!isAttack)
			KeyInputProcess ();
		else {
			transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * 20);
		}

		MouseInputProcess ();

		if (Vector3.Distance (transform.position, targetPosition) <= intenceDistance) {
			isAttack = false;
		}
	}
}
