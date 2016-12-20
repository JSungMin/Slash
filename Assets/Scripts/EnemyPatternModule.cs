using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatternModule : MonoBehaviour {

	protected delegate void EnemyAttackPattern();
	protected delegate void EnemyMovePattern();

	event EnemyAttackPattern enemyAttackPattern;
	event EnemyMovePattern enemyMovePattern;

	public bool canMovePattern1;

	public bool canAttackPattern1;

	public float enemySearchRadius;
	private bool isFindPlayer;


	//Always Follow Player
	private void MovePattern01(){

	}

	//찾으면 항상 달려든다.
	private void AttackPattern01(){

	}

	// Use this for initialization
	void Start () {
		if(canMovePattern1){
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
