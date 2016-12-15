using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourutineManager : MonoBehaviour {

	public static CourutineManager instance;

	public static CourutineManager GetInstance{
		get{
			if (instance == null) {
				instance = new CourutineManager ();
			}
			return instance;
		}
	}

	public IEnumerator attackDelay;

	public void Awake(){
		instance = this;
	}
}
