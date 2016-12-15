using UnityEngine;
using System.Collections;

public class ZC_AnimDemo_FREE_ButtonManager1 : MonoBehaviour {

	GameObject player;
	Animator anim;

	/* ************************************************** Find Player ************************************************** */

	void Start () {
		InvokeRepeating ("FindPlayer", 0.01f, 2.0f);
	}

	void RespawnPlayer () {
		Instantiate (Resources.Load ("ZombieCube"));
	}

	void FindPlayer(){
		if(player == null || anim == null)
		{
			player = GameObject.FindGameObjectWithTag ("Player");

			if (player != null) 
			{anim = player.GetComponentInChildren <Animator> ();}
			else {return;}
		}
	}

	/* ************************************************** Animations ************************************************** */

	// IDLE //
	public void PlayIdle () {
		if (anim != null) {
			anim.Play ("ZC_Idle");
		}
	}

	// WALK //
	public void PlayWalk () {
		if (anim != null) {
			anim.Play ("ZC_Walk");
		}
	}

	// CROUCH //
	public void PlayCrouch () {
		if (anim != null) {
			anim.Play ("ZC_Crouch");
		}
	}

	// JUMP //
	public void PlayJump () {
		if (anim != null) {
			anim.Play ("ZC_Jump");
		}
	}


} // <--- END SCRIPT
