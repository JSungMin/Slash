using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using Spine.Unity.Modules;

public class PlayerAnimation : MonoBehaviour {

    SkeletonGhost ghost;
    SkeletonAnimation skel;
    Player player;
    string cur_animation;
    enum Direction { Front, Back, Left, Right };
    Direction cur_dir;
    Direction past_dir;
	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
        skel = GetComponentInChildren<SkeletonAnimation>();
        ghost = GetComponentInChildren<SkeletonGhost>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.isAttack == false)
        {
            ghost.color.a = 1;
            if (!(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) && !(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
            {
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
            }
            if (past_dir != cur_dir)
            {
                skel.state.ClearTrack(0);
                past_dir = cur_dir;
            }

            if (player.dir != Vector3.zero)
            {
                setAnimation(0, cur_dir + "_Run", true, 1f);
            }
            else
            {
                setAnimation(0, cur_dir + "_Idle", true, 1f);
            }

        }
        else
        {
            ghost.color.a = 0;


            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float degree = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            if (degree > -45 && degree <= 45)
            {
                cur_dir = Direction.Right;
            }
            else if (degree > 45 && degree <= 135)
            {
                cur_dir = Direction.Back;
            }
            else if (degree > 135 || degree <= -135)
            {
                cur_dir = Direction.Left;
            }
            else
            {
                cur_dir = Direction.Front;
            }
            if (past_dir != cur_dir)
            {
                skel.state.ClearTrack(0);
                past_dir = cur_dir;
            }

            print(degree);
            setAnimation(0, cur_dir + "_Attack_withoutIdle", false, 3f);

        }
	}
    void setAnimation(int index, string name, bool loop, float time)
    {
        if(cur_animation == name)
        {
            return;
        }else
        {
            skel.state.SetAnimation(index, name, loop).TimeScale = time;
            cur_animation = name;
        }
    }
    
}
