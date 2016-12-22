using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : RoomClearModule {

	public bool canMoveX;
	public bool canMoveY;

	public bool isOpen;

	IEnumerator FadeIn(SpriteRenderer sprite){
		var alpha = sprite.color.a;
		while(sprite.color.a<1){
			yield return null;
			alpha += Time.deltaTime*2;
			sprite.color = new Color (sprite.color.r,sprite.color.g,sprite.color.b,alpha);
		}
		sprite.color = new Color (sprite.color.r,sprite.color.g,sprite.color.b,1);
		StopCoroutine ("FadeIn");
	}

	IEnumerator FadeOut(SpriteRenderer sprite){
		var alpha = sprite.color.a;
		while(sprite.color.a>=0){
			yield return null;
			alpha -= Time.deltaTime*2;
			sprite.color = new Color (sprite.color.r,sprite.color.g,sprite.color.b,alpha);
		}
		sprite.color = new Color (sprite.color.r,sprite.color.g,sprite.color.b,0);
		StopCoroutine ("FadeOut");
	}
		
	private void GoThroughByDoor(Collision2D col){
		if (isOpen) {
			GetComponent<AudioSource> ().Play ();
			GetComponent<Obstacle> ().SetCanStand (true);
		}
	}

	public void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.CompareTag("Player")){
			if (isAnnihilation) {
				isOpen = false;
				isOpen = CheckAllEnemyCleared (transform.parent.parent.GetChild(0));
			}
			if (isTimeAttack) {
				isOpen = false;
				isOpen = CheckTimeAttack ();
			}
			if (isFree) {
				isOpen = true;
			}
			GoThroughByDoor (col);
		}
	}

	public void OnCollisionStay2D(Collision2D col){
		if(col.gameObject.CompareTag("Player")){
			if (isAnnihilation) {
				isOpen = false;
				isOpen = CheckAllEnemyCleared (transform.parent.parent.GetChild(0));
			}
			if (isTimeAttack) {
				isOpen = false;
				isOpen = CheckTimeAttack ();
			}
			if (isFree) {
				isOpen = true;
			}
			GoThroughByDoor (col);
		}
	}

	//this mean door is opend now
	public void OnTriggerEnter2D(Collider2D col){
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource> ().Play ();
	}
}
