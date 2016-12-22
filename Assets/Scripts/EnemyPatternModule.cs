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

	public float attackDistance;

	public float distanceIntence;

	private bool isFindPlayer;

	[SerializeField]
	private bool isReloading = true;
	private bool isDamaged = false;


	protected Vector3 dir;
	[SerializeField]
	protected float dis;
	[SerializeField]
	protected bool isAttack = false;

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

	//서치가 안되면 플레이어 중심적으로 랜덤으로 움직인다.
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
		yield return new WaitForSeconds (1);
		isReloading = false;
	}


	//찾으면 항상 달려든다.
	protected void AttackPattern01(){
		if (isFindPlayer && !isReloading && !isAttack) {
			if (dis <= attackDistance) {
				isAttack = true;
				attackDir = dir;
				targetPos = transform.position;
				StartCoroutine ("DelayAttack");
			}
		} else if (isAttack) {
			transform.position = Vector3.MoveTowards (transform.position,targetPos + attackDir*attackDistance,Time.deltaTime*3);
		}
	}
}
