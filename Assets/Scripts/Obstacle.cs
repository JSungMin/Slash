using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	public bool canStand = false;

	public bool isTrap = false;

	public int trapDamage;

	public void OnTriggerEnter2D(Collider2D col){
		if(isTrap){
			if(col.CompareTag("Player")){
				col.GetComponent<Player> ().hp -= trapDamage;
				col.GetComponent<Player> ().isDamaged = true;
				col.GetComponent<Rigidbody2D> ().AddForce ((col.transform.position - transform.position).normalized * trapDamage*50,ForceMode2D.Impulse);
			}
		}
	}
		
	public void Start(){
		if (canStand) {
			GetComponent<BoxCollider2D> ().isTrigger = true;
		} else {
			GetComponent<BoxCollider2D> ().isTrigger = false;
		}
	}
}
