using UnityEngine;
using System.Collections;

public class DG_AnimDemo_FREE_ButtonManager : MonoBehaviour {

	GameObject player;
	Animator anim;

	/* ************************************************** Find Player ************************************************** */

	void Start () {
		InvokeRepeating ("FindPlayer", 0.01f, 2.0f);
	}

	void RespawnPlayer () {
		Instantiate (Resources.Load ("DeadGoat"));
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
			anim.Play ("DG_Idle");
		}
	}

	public void PlayIdleEyeBlink () {
		if (anim != null) {
			anim.Play ("DG_Idle(EyeBlink)");
		}
	}

	// WALK //
	public void PlayWalk () {
		if (anim != null) {
			anim.Play ("DG_Walk");
		}
	}

	public void PlayWalkEyeBlink () {
		if (anim != null) {
			anim.Play ("DG_Walk(EyeBlink)");
		}
	}

	// RUN //
	public void PlayRun () {
		if (anim != null) {
			anim.Play ("DG_Run");
		}
	}

	public void PlayRunEyeBlink () {
		if (anim != null) {
			anim.Play ("DG_Run(EyeBlink)");
		}
	}

	// CROUCH //
	public void PlayCrouch () {
		if (anim != null) {
			anim.Play ("DG_Crouch");
		}
	}

	// JUMP //
	public void PlayJump () {
		if (anim != null) {
			anim.Play ("DG_Jump");
		}
	}


} // <--- END SCRIPT
