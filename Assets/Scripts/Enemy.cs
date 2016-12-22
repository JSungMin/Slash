using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyPatternModule {

	public Unit unitInfo;

	public ParticleSystem dieParticle;
	public Player player;


	// Use this for initialization
	void Start () {
		unitInfo = GetComponent<Unit> ();
		dieParticle = transform.GetComponentInChildren<ParticleSystem> ();
		player = GameObject.FindObjectOfType<Player> ();
		StartCoroutine (SearchPlayer (player.transform));

		if (canMovePattern1) {
			enemyMovePattern += new EnemyMovePattern(MovePattern01);
			Debug.Log ("MovePattern01 Added");
		}
		if(canMovePattern2){
			enemyMovePattern += new EnemyMovePattern (MovePattern02);
			Debug.Log ("MovePattern02 Added");
		}

		if (canAttackPattern1) {
			enemyAttackPattern += new EnemyAttackPattern(AttackPattern01);
			Debug.Log ("AttackPattern01 Added");
		}


		StartCoroutine ("CheckDie");
	}

	public void Die(){
		dieParticle.transform.parent = transform.parent;
		dieParticle.Play();	
		DestroyObject (this.gameObject);
	}

	public IEnumerator CheckDie(){
		while(!unitInfo.GetIsDie()){
			yield return null;
			if (unitInfo.hp <= 0) {
				unitInfo.SetIsDie (true);
				Die ();
			}
		}
		StopCoroutine ("CheckDie");
	}

	// Update is called once per frame
	void Update () {
		if (enemyMovePattern != null) {
			enemyMovePattern ();
		}
		if (enemyAttackPattern != null) {
			enemyAttackPattern ();
		}
	}
}
