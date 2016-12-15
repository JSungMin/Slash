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

		var newHit =Physics2D.RaycastAll (transform.position,dir, walkDis*Time.deltaTime);

		if(newHit.Length!=0){
			for(int i =0;i<newHit.Length;i++){
				if (newHit [i].transform.CompareTag ("Wall")) {
					
					transform.position += -dir*walkDis*Time.deltaTime;
					break;
				} else {
					transform.Translate (dir*walkDis*Time.deltaTime);
				}
			}
		}


		dir = Vector3.zero;
		xDir = Vector3.zero;
		yDir = Vector3.zero;
	}

	RaycastHit2D[] hit;
	private void MouseInputProcess(){
		leftMouse = Input.GetMouseButton (0);

		if (leftMouse) {
			if (!isReloaing) {
				clickPosition = Input.mousePosition;
				clickPosition = Camera.main.ScreenToWorldPoint (clickPosition);

				attackDir = (clickPosition - transform.position).normalized;
				hit = Physics2D.RaycastAll (transform.position, attackDir, attackDis);

				if(hit.Length!=0){
					for(int i =0;i<hit.Length;i++){
						if (hit [i].transform.CompareTag ("Wall")) {
							if (Vector3.Distance (transform.position, targetPosition) > Vector3.Distance (transform.position, new Vector3 (hit [i].point.x, hit [i].point.y, transform.position.z))) {
								targetPosition = new Vector3 (hit [i].point.x, hit [i].point.y, targetPosition.z);
								break;
							}
						} else {
							targetPosition = transform.position + attackDir * attackDis;
							targetPosition = new Vector3 (targetPosition.x, targetPosition.y, 0);
						}
					}
				}

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
		isAttack = false;
		Debug.Log ("Reloading Finished");
	}


	// Update is called once per frame
	void Update () {
		if (!isReloaing)
			KeyInputProcess ();
		else {
			transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * 20);
		}

		MouseInputProcess ();

		if (Vector3.Distance (transform.position, targetPosition) <= intenceDistance) {
			isAttack = false;
		}
		Debug.DrawLine (transform.position, targetPosition,Color.red);
	}
}
