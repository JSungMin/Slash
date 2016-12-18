using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Unit unitInfo;

	public ParticleSystem dieParticle;


	// Use this for initialization
	void Start () {
		unitInfo = GetComponent<Unit> ();
		dieParticle = transform.GetComponentInChildren<ParticleSystem> ();
	}

	public void Die(){
		dieParticle.transform.parent = transform.parent;
		dieParticle.Play();	
		DestroyObject (this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		if (unitInfo.hp <= 0)
			Die ();		
	}
}
