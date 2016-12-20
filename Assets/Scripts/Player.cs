using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject arrow;

	public float walkDis;
	public float attackDis;

	public int hp=100;
	public int maxHp;

	public int stamina= 100;
	public int maxStamina;

	public Vector3 dir;

	private Vector3 xDir;
	private Vector3 yDir;

	private bool leftMouse;

	public Vector3 mouseInputPosition;
	private Vector3 attackDir;

	public Vector3 targetPosition;

	public bool isAttack;
	public bool isDamaged = false;
	public bool isReloading = false;
	[SerializeField]
	private bool isDead = false;

	public float intenceDistance;

	public float attackDelayTime;

	public IEnumerator autoHealingStamina;
	public float staminaHealingDelay;
	public int staminaHealingAmount;

	public GameObject nowLevel;

	public GameObject deadEffect;

	public bool GetIsDead(){
		return isDead;
	}

	public Vector3 GetKeyBoardMoveDirection(){
		return dir;
	}

	public Vector3 GetAttackDirection(){
		return attackDir;
	}

	// Use this for initialization
	void Start () {
		attackDelay = AttackDelay (attackDelayTime);
		autoHealingStamina = AutoHealingStamina (staminaHealingDelay);

		StartCoroutine (autoHealingStamina);
	}

	public IEnumerator AutoHealingStamina(float time){
		while (true) {
			yield return new WaitForSeconds (time);
			Debug.Log ("Healing");
			if(stamina + staminaHealingAmount <=maxStamina)
				stamina += staminaHealingAmount;
		}
	}
    
	public bool DecreaseStamina(int amount){
		if (stamina - amount >= 0) {
			stamina -= amount;
			return true;
		}
		return false;
	}
		
	public void CalculateArrow(Vector3 mPosition){
		if (!isAttack) {
			var tmpDir = (mPosition - transform.position);
			float angle = Mathf.Atan2 (tmpDir.x, tmpDir.y) * Mathf.Rad2Deg;

			arrow.transform.localRotation = Quaternion.AngleAxis (angle - 90, Vector3.back);
			//arrow.transform.localScale = Vector3.one * Mathf.Clamp (Mathf.Abs(Vector3.Distance (transform.position, mouseInputPosition)-15), 0, 4);
		}
	}

	public void CalculateMousePosition(Vector3 mPosition){
		mouseInputPosition = mPosition;
		mouseInputPosition = Camera.main.ScreenToWorldPoint (mouseInputPosition);
	}

	public bool CheckTargetPosition(){
		hit = Physics2D.RaycastAll (transform.position, attackDir, attackDis);
		if(hit.Length!=0){
			for(int i =0;i<hit.Length;i++){
				if (hit [i].transform.GetComponent<Obstacle>()!=null) {
					if (!hit [i].transform.GetComponent<Obstacle> ().canStand) {
						if (Vector3.Distance (transform.position, targetPosition) > Vector3.Distance (transform.position, new Vector3 (hit [i].point.x, hit [i].point.y, transform.position.z))) {
							targetPosition = new Vector3 (hit [i].point.x, hit [i].point.y, targetPosition.z);
							return true;
						}
					}
				} else {
					targetPosition = transform.position + attackDir * attackDis;
					targetPosition = new Vector3 (targetPosition.x, targetPosition.y, 0);
				}
			}
		}
		return false;
	}

	RaycastHit2D[] hit;

	private void MouseInputProcess(){
		leftMouse = Input.GetMouseButton (0);

		CalculateMousePosition (Input.mousePosition);
		CalculateArrow (mouseInputPosition);

		if (leftMouse) {
			if (!isReloading&&DecreaseStamina(5)) {
				attackDir = (mouseInputPosition - transform.position).normalized;

				var isCollisionWithWall = CheckTargetPosition ();

				GetComponent<AudioSource> ().Play ();
				isAttack = true;
				StopCoroutine ("AttackDelay");
				StartCoroutine ("AttackDelay",attackDelayTime);
			}
		}
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
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            dir = Vector3.zero;
        }
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            dir = Vector3.zero;
        }
		var newHit = Physics2D.RaycastAll (transform.position,dir, walkDis*Time.deltaTime);

		if(newHit.Length!=0){
			for(int i =0;i<newHit.Length;i++){
				if (newHit [i].transform.GetComponent<Obstacle>()!=null) {
					if(!newHit[i].transform.GetComponent<Obstacle>().canStand){
						//transform.position = new Vector3 (newHit [i].point.x, newHit [i].point.y, transform.position.z);
						break;
					}
				} else if(xDir != Vector3.zero || yDir != Vector3.zero){
					transform.Translate (dir*walkDis*Time.deltaTime);
				}
                else
                {
                    dir = Vector3.zero;
                }
			}
		}

		xDir = Vector3.zero;
		yDir = Vector3.zero;
	}

	IEnumerator attackDelay;

	public IEnumerator AttackDelay(float time){
		isReloading = true;

		Debug.Log ("Reloading");

		yield return new WaitForSeconds (time);
		isReloading = false;
		isAttack = false;
		Debug.Log ("Reloading Finished");
	}

	public IEnumerator Damaged(float time){
		yield return new WaitForSeconds (time);
		if(isDamaged){
			isDamaged = false;
		}
		StopCoroutine ("Damaged");
	}

	// Update is called once per frame
	void Update () {
		if(hp<=0&&!isDead){
			isDead = true;
			Camera.main.GetComponent<CameraMoving> ().isZoomIn = true;
			var newDeadEff = Instantiate (deadEffect,transform) as GameObject;
			newDeadEff.transform.localPosition = new Vector3 (0,0,-5);
			newDeadEff.GetComponent<ParticleSystem> ().Play ();
		}

		if (!isDamaged) {
			if (!isReloading)
				KeyInputProcess ();
			else {
				transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * 20);
			}
			MouseInputProcess ();
		} else {
			StartCoroutine ("Damaged", 0.5f);
		}
	}
}
