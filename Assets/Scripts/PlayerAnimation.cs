﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class PlayerAnimation : MonoBehaviour {


    SkeletonAnimation skel;
    Player player;
    string cur_animation;
    enum Direction { Front, Back, Left, Right };
    Direction cur_dir;
	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
        skel = GetComponentInChildren<SkeletonAnimation>();	

	}
	
	// Update is called once per frame
	void Update () {
		if(player.isAttack == false)
        {
            Debug.Log(player.dir);
            if (Input.GetKey(KeyCode.W))
            {
                cur_dir = Direction.Back;
            }
            if (Input.GetKey(KeyCode.A))
            {
                cur_dir = Direction.Left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                cur_dir = Direction.Right;
            }
            if (Input.GetKey(KeyCode.S))
            {
                cur_dir = Direction.Front;
            }

            if(player.dir != Vector3.zero)
            {
                Debug.Log("noo");
            }

        }else
        {

        }
	}
    void setAnimation(int index, string name, bool loop, float time)
    {
        if(cur_animation == name)
        {
            return;
        }else
        {
            skel.state.ClearTrack(index);
            skel.state.SetAnimation(index, name, loop).TimeScale = time;
            cur_animation = name;
        }
    }
    
}
