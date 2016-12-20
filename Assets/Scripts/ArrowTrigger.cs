using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrigger : MonoBehaviour {

	public Player player;

	public void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.CompareTag("Enemy")&&player.isAttack){
			col.gameObject.GetComponent<Enemy> ().Die ();
			Camera.main.GetComponent<CameraMoving> ().isShaking = true;
			Camera.main.GetComponent<CameraMoving> ().isZoomIn = true;
			GetComponent<AudioSource> ().Play ();
			Debug.Log ("EnemyDie");
		}
	}
}
