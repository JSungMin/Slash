using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<ParticleSystem>().time>=GetComponent<ParticleSystem>().duration){
			Debug.Log ("죽어 죽ㅓ");
			DestroyObject (this.gameObject);
		}
	}
}
