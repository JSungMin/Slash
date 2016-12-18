using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

	public bool isOpen;
	public bool isLock;

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
				if (col.GetComponent<Player> ().doorCheck == 0) {
					var sprites = outLevel.GetComponentsInChildren<SpriteRenderer> ();
					for (int i = 0; i < sprites.Length; i++) {
						StartCoroutine ("FadeIn", sprites [i]);
					}
					col.GetComponent<Player> ().doorCheck = 1;
					col.GetComponent<Player> ().EnterNewLevel (inLevel);
				} else if (col.GetComponent<Player> ().doorCheck == 1) {
					var sprites = inLevel.GetComponentsInChildren<SpriteRenderer> ();
					for (int i = 0; i < sprites.Length; i++) {
						StartCoroutine ("FadeIn", sprites [i]);
					}
					col.GetComponent<Player> ().doorCheck = 0;

					col.GetComponent<Player> ().EnterNewLevel (outLevel);
				}
				col.transform.position += (this.transform.position - col.transform.position).normalized*10;
			}
		}
	}
}
