using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatternModule : MonoBehaviour {

	protected delegate void EnemyAttackPattern();
	protected delegate void EnemyMovePattern();

	protected EnemyAttackPattern enemyAttackPattern;
	protected EnemyMovePattern enemyMovePattern;

	public bool canMovePattern1;
	public bool canMovePattern2;

	public bool canAttackPattern1;

	public float enemySearchRadius;
	public float enemySearchDelay;

	private bool isFindPlayer;

	[SerializeField]
	private bool isReloading = true;
	private bool isDamaged = false;

	public float attackDistance;

	protected Vector3 dir;
	public float dis;
	public float distanceIntence;

	//Started in Enemy Script
	protected IEnumerator SearchPlayer(Transform player){
		while (!GetComponent<Unit> ().GetIsDie ()) {
			yield return new WaitForEndOfFrame();
			var playerPos = new Vector2 (player.position.x,player.position.y);
			var thisPos = new Vector2 (transform.position.x,transform.position.y);
			dis = Vector2.Distance (playerPos, thisPos);
			if (dis <= enemySearchRadius) {
				isFindPlayer = true;
				dir = (player.position - transform.position).normalized;
			} else {
				isFindPlayer = false;
			}
		}
	}

	protected IEnumerator DelayedSetBool(string valueName,bool value,float time){
		yield return new WaitForSeconds (time);
		switch(valueName){
		case "isFindPlayer":
			isFindPlayer = value;
			break;
		case"isDamaged":
			isDamaged = value;
			break;
		case "isReloading":
			Debug.Log ("뤼로뒹");
			isReloading = value;
			attackDir = Vector3.zero;
			isAttack = false;
			break;
		}
	}


	bool isAttack = false;

	//Always Follow Player
	protected void MovePattern01(){
		if (isFindPlayer) {
			if (dis <= enemySearchRadius) {
				transform.Translate (dir * GetComponent<Unit> ().speed * Time.deltaTime);
				Debug.Log ("MovePattern01");
			}
		}
	}
		

	public Vector2 randomDir;
	private bool isRandomTime = true;
	public IEnumerator SetRandomTargetPosition(){
		randomDir = new Vector2 (Random.Range(-1,2),Random.Range(-1,2)) + new Vector2(dir.x,dir.y);
		randomDir = randomDir.normalized;
		yield return new WaitForSeconds (2);
		isRandomTime = true;
		yield return new WaitForSeconds (1);
		randomDir = Vector2.zero;
	}
	//Move like Random
	protected void MovePattern02(){
		if(!isFindPlayer){
			if (dis > enemySearchRadius) {
				if (isRandomTime) {
					StartCoroutine (SetRandomTargetPosition ());
					isRandomTime = false;
				}
				transform.Translate (randomDir * GetComponent<Unit> ().speed * Time.deltaTime);
				Debug.Log ("MovePattern02");
			}
		}
	}

	Vector3 targetPos;
	Vector3 attackDir;

	public IEnumerator DelayAttack(){
		yield return new WaitForSeconds (1);
		isReloading = true;
		isAttack = false;
		Debug.Log ("왜 앙ㅐ");
		yield return new WaitForSeconds (1);
		isReloading = false;
	}


	//찾으면 항상 달려든다.
	protected void AttackPattern01(){
		if (isFindPlayer && !isReloading && !isAttack) {
			if (dis <= attackDistance) {
				isAttack = true;
				attackDir = dir;
				Debug.Log (attackDir);
				StartCoroutine ("DelayAttack");
			}
		} else if (isAttack) {
			Debug.Log ("isAttack");
			transform.position = Vector3.MoveTowards (transform.position,transform.position + attackDir*attackDistance,Time.deltaTime*3);
		}
	}
}
