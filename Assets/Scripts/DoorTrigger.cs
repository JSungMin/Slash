using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {
	
	public GameObject inLevel;
	public GameObject outLevel;

	private Transform inLevelPosition;
	private Transform outLevelPosition;

	public bool canMoveX;
	public bool canMoveY;

	private SpriteRenderer[] inLevelSprites;
	private SpriteRenderer[] outLevelSprites;

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
		inLevelPosition = transform.GetChild (0).transform;
		outLevelPosition = transform.GetChild (1).transform;

		inLevelSprites = inLevel.GetComponentsInChildren<SpriteRenderer> ();
		outLevelSprites = outLevel.GetComponentsInChildren<SpriteRenderer> ();
	}

	private void GoThroughByDoor(Collider2D col){
		var dir = col.GetComponent<Player> ().GetKeyBoardMoveDirection ();
		if (col.GetComponent<Player> ().isReloading)
			dir = col.GetComponent<Player> ().GetAttackDirection ();

		if (canMoveX) {
			if (dir.x > 0) {
				col.transform.position = outLevelPosition.position;

				//안에서 밖으로 나갈때
				for(int i =0;i<inLevelSprites.Length;i++){
					StartCoroutine ("FadeOut", inLevelSprites [i]);
				}
				for(int i =0;i<outLevelSprites.Length;i++){
					StartCoroutine ("FadeIn", outLevelSprites [i]);
				}
			} else if(dir.x<0){
				col.transform.position = inLevelPosition.position;

				//밖에서 안으로 들어올때
				for(int i =0;i<outLevelSprites.Length;i++){
					StartCoroutine ("FadeOut", outLevelSprites [i]);
				}
				for(int i =0;i<inLevelSprites.Length;i++){
					StartCoroutine ("FadeIn", inLevelSprites [i]);
				}
			}
		}
		if(canMoveY){
			if (dir.y > 0) {
				col.transform.position = outLevelPosition.position;

				//안에서 밖으로 나갈때
				for(int i =0;i<inLevelSprites.Length;i++){
					StartCoroutine ("FadeOut", inLevelSprites [i]);
				}
				for(int i =0;i<outLevelSprites.Length;i++){
					StartCoroutine ("FadeIn", outLevelSprites [i]);
				}
			} else if(dir.y<0){
				col.transform.position = inLevelPosition.position;

				//밖에서 안으로 들어올때
				for(int i =0;i<outLevelSprites.Length;i++){
					StartCoroutine ("FadeOut", outLevelSprites [i]);
				}
				for(int i =0;i<inLevelSprites.Length;i++){
					StartCoroutine ("FadeIn", inLevelSprites [i]);
				}
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D col){
		if(col.CompareTag("Player")){
			inLevelSprites = inLevel.GetComponentsInChildren<SpriteRenderer> ();
			outLevelSprites = outLevel.GetComponentsInChildren<SpriteRenderer> ();
			GoThroughByDoor (col);
		}
	}
}
