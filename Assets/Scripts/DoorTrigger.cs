using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

	public bool isOpen;
	public bool isClear;

	public GameObject inLevel;
	public GameObject outLevel;

	IEnumerator FadeIn(SpriteRenderer sprite){
		var alpha = sprite.color.a;
		while(sprite.color.a<1){
			yield return null;
			alpha += Time.deltaTime;
			sprite.color = new Color (sprite.color.r,sprite.color.g,sprite.color.b,alpha);
		}
		StopCoroutine ("FadeIn");
	}

	public void OnTriggerEnter2D(Collider2D col){
		if(col.CompareTag("Player")){
			if (isOpen) {
				if (col.GetComponent<Player> ().isInDungeon) {
					Debug.Log ("Door Open");
					var sprites = outLevel.GetComponentsInChildren<SpriteRenderer> ();
					for (int i = 0; i < sprites.Length; i++) {
						StartCoroutine ("FadeIn", sprites [i]);
					}
					if (col.GetComponent<Player> ().nowLevel == outLevel)
						col.GetComponent<Player> ().EnterNewLevel (inLevel);
					else {
						//col.GetComponent<Player> ().EnterNewLevel (outLevel);
					}
				}
			}
		}
	}
}
