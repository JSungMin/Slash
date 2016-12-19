using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour {

	private Player player;

	public Transform enemyPool;

	public DoorTrigger[] doorTriggers;

	public int enemyNum;

	public int GetEnemyNum(){
		return enemyNum;
	}

	public int SearchAndGetEnemyNum(){
		enemyNum = enemyPool.childCount;
		return GetEnemyNum();
	}

	public void Start(){
		player = GameObject.FindObjectOfType<Player> ();
		if (SearchAndGetEnemyNum () != 0) {
			for(int i =0;i<doorTriggers.Length;i++){
				
			}
		}
	}
	bool isClear = false;
	public void Update(){
		if (SearchAndGetEnemyNum () == 0&&!isClear) {
			for(int i =0;i<doorTriggers.Length;i++){
				
			}
		}
	}

}
