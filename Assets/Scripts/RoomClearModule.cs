using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClearModule : MonoBehaviour {

	public bool isAnnihilation;
	public bool isTimeAttack;
	public bool isFree;

	public float timeAttackTime;
	protected float invadeTime;//For TimAttack

	protected bool CheckAllEnemyCleared(Transform enemyPool){
		if(enemyPool.childCount==0){
			return true;
		}
		return false;
	}

	protected bool CheckTimeAttack(){
		if(Time.time - invadeTime>=timeAttackTime){
			return true;
		}
		return false;
	}
}
