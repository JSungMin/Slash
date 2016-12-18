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

	public Vector3 xDir;
	public Vector3 yDir;

	public bool leftMouse;

	public Vector3 mouseInputPosition;
	public Vector3 attackDir;
	public Vector3 targetPosition;

	public bool isAttack;
	public bool isReloading = false;

	public int doorCheck = 0;

	public float intenceDistance;

	public float attackDelayTime;

	public IEnumerator autoHealingStamina;
	public float staminaHealingDelay;
	public int staminaHealingAmount;

	public GameObject nowLevel;

	public IEnumerator FadeOut(SpriteRenderer sprite){
		var alpha = sprite.color.a;
		while(sprite.color.a>0){
			yield return null;
			alpha -= Time.deltaTime;
			sprite.color = new Color (sprite.color.r,sprite.color.g,sprite.color.b,alpha);
		}
		StopCoroutine ("FadeOut");
	}

	public void EnterNewLevel(GameObject level){
		var sprites = nowLevel.transform.GetComponentsInChildren<SpriteRenderer> ();
		for(int i =0;i<sprites.Length;i++){
			StartCoroutine ("FadeOut", sprites [i]);
		}
		nowLevel = level;
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
		var tmpDir = (mPosition - transform.position);
		float angle = Mathf.Atan2 (tmpDir.x,tmpDir.y)*Mathf.Rad2Deg;

		arrow.transform.localRotation = Quaternion.AngleAxis(angle - 90,Vector3.back);
		arrow.transform.localScale = Vector3.one * Mathf.Clamp (Vector3.Distance (transform.position, mouseInputPosition)-attackDis, 0, 4);
	}

	public void CalculateMousePosition(Vector3 mPosition){
		mouseInputPosition = mPosition;
		mouseInputPosition = Camera.main.ScreenToWorldPoint (mouseInputPosition);
	}

	public bool CheckTargetPosition(){
		hit = Physics2D.RaycastAll (transform.position, attackDir, attackDis);
		if(hit.Length!=0){
			for(int i =0;i<hit.Length;i++){
				if (hit [i].transform.CompareTag ("Wall")) {
					if (Vector3.Distance (transform.position, targetPosition) > Vector3.Distance (transform.position, new Vector3 (hit [i].point.x, hit [i].point.y, transform.position.z))) {
						targetPosition = new Vector3 (hit [i].point.x, hit [i].point.y, targetPosition.z);
						return true;
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
		var newHit = Physics2D.RaycastAll (transform.position,dir, walkDis*Time.deltaTime);

		if(newHit.Length!=0){
			for(int i =0;i<newHit.Length;i++){
				if (newHit [i].transform.CompareTag ("Wall")) {

					transform.position = new Vector3(newHit[i].point.x,newHit[i].point.y,transform.position.z) - dir*walkDis*Time.deltaTime;
					break;
				} else {
					transform.Translate (dir*walkDis*Time.deltaTime);
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


	// Update is called once per frame
	void Update () {
		if (!isReloading)
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
