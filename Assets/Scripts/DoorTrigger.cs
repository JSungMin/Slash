using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

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

	public void Start(){
	}

	private void GoThroughByDoor(Collision2D col){
		if (isOpen) {
			GetComponent<Obstacle> ().SetCanStand (true);
		}
	}

	public void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.CompareTag("Player")){
			GetComponent<AudioSource> ().Play ();
			GoThroughByDoor (col);
		}
	}
}
