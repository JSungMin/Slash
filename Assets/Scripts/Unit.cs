using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public int id;//한 씬내에 객체를 구분할 수 있는 고유번호

	public string name;//해당 name을 통해 parameter data를 불러 온다.

	//parameter data
	public int hp;
	public int mp;
	public int str;
	public int def;
	public int dex;

	public float speed;

	private bool isDie = false;

	public bool GetIsDie(){
		return isDie;
	}
	public void SetIsDie(bool val){
		isDie = val;
	}
	public void Init(Unit info){
		id = info.id;
		name = info.name;
		hp = info.hp;
		mp = info.mp;
		str = info.str;
		def = info.def;
		dex = info.dex;
		speed = info.speed;
	}

	void Update(){
		
	}
}
