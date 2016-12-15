using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public Transform[] respawnPoints;

	public bool canMakeEnemy;

	public GameObject[] enemies;
	public GameObject[] effects;



	// Use this for initialization
	void Start () {
		canMakeEnemy = true;
	}

	public void InitEnemies(){
		var allEnemies = GameObject.FindGameObjectsWithTag ("Enemy");

		for(int i =0;i<allEnemies.Length;i++){
			if(null==allEnemies[i].GetComponent<Unit>()){
				Debug.LogError ("Enemy Object haven't Unit Component in InitEnemies Method Process");
				return;
			}
			//스텟 업데이트
			allEnemies [i].GetComponent<Unit> ().Init (UnitManager.GetInstance.GetItem (allEnemies [i].GetComponent<Unit> ().id));
		}

	}

	public GameObject MakeEnemy(int id){
		if (canMakeEnemy) {
			var newEnemy = Instantiate (enemies[id],respawnPoints[Random.Range(0,respawnPoints.Length)]) as GameObject;
			newEnemy.transform.localPosition = Vector3.right*Random.Range(-0.5f,0.5f) + Vector3.up*Random.Range(-0.5f,0.5f);
			newEnemy.transform.rotation = Quaternion.identity;
			if(null==newEnemy.GetComponent<Unit>())
				newEnemy.AddComponent<Unit> ();
			newEnemy.GetComponent<Unit> ().Init (UnitManager.GetInstance.GetItem (id));
			newEnemy.name = newEnemy.GetComponent<Unit> ().name;
			//newEnemy.GetComponent<EnemyAI> ().target = GameObject.FindObjectOfType<Player> ().transform;
			effects [0].transform.parent = newEnemy.transform;
			newEnemy.SetActive (false);
			return newEnemy;
		}
		return null;
	}

	public GameObject MakeEnemy(int id, int rpIndex){
		if (canMakeEnemy) {
			var newEnemy = Instantiate (enemies[id],respawnPoints[rpIndex]) as GameObject;
			newEnemy.transform.localPosition = Vector3.right*Random.Range(-0.5f,0.5f) + Vector3.up*Random.Range(-0.5f,0.5f);
			newEnemy.transform.rotation = Quaternion.identity;
			if(null==newEnemy.GetComponent<Unit>())
				newEnemy.AddComponent<Unit> ();
			newEnemy.GetComponent<Unit> ().Init (UnitManager.GetInstance.GetItem (id));
			newEnemy.name = newEnemy.GetComponent<Unit> ().name;
			//newEnemy.GetComponent<EnemyAI> ().target = GameObject.FindObjectOfType<Player> ().transform;
			effects [0].transform.parent = newEnemy.transform;
			newEnemy.SetActive (false);
			return newEnemy;
		}
		return null;
	}

	public GameObject[] MakeEnemies(int id, int num){
		GameObject[] newEnemies = new GameObject[num];
		for(int i =0;i<num;i++){
			newEnemies[i] = MakeEnemy (id);
		}
		return newEnemies;
	}

	public GameObject[] MakeEnemies(int id, int num, int rpIndex){
		GameObject[] newEnemies = new GameObject[num];
		for(int i=0;i<num;i++){
			newEnemies[i] = MakeEnemy (id, rpIndex);
		}
		return newEnemies;
	}

	public GameObject[][] MakeEnemiesSet(int[] id, int[] num){
		if (id.Length != num.Length)
			return null;

		GameObject[][] newEnemiesSet = new GameObject[id.Length][];

		for(int i =0;i<id.Length;i++){
			newEnemiesSet[i] = new GameObject[num[i]];
			newEnemiesSet[i] = MakeEnemies (id [i], num [i]);
		}
		return newEnemiesSet;
	}

	public GameObject[][] MakeEnemiesSet(int[] id, int[] num, int[] rpIndex){
		if(id.Length!=num.Length||id.Length!=rpIndex.Length){
			return null;
		}

		GameObject[][] newEnemiesSet = new GameObject[id.Length][];

		for(int i =0;i<id.Length;i++){
			newEnemiesSet [i] = new GameObject[num [i]];
			newEnemiesSet [i] =  MakeEnemies (id[i],num[i],rpIndex[i]);
		}

		return newEnemiesSet;
	}
}
