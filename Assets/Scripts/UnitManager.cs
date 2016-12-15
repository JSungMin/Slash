using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

	public static UnitManager instance;

	private static object pLock = new object();

	public bool isUnitInfoLoaded = false;

	public static UnitManager GetInstance
	{
		get{
			lock (pLock) {
				if (instance == null) {
					instance = (UnitManager)GameObject.FindObjectOfType (typeof(UnitManager));

					if (FindObjectsOfType (typeof(UnitManager)).Length > 1) {
						return instance;
					}
					if (instance == null) {
						GameObject singleton = new GameObject ();
						instance = singleton.AddComponent<UnitManager>();
						singleton.name = typeof(UnitManager).ToString();

						DontDestroyOnLoad (singleton);
					}
				}
			}
			return instance;
		}
	}

	Dictionary<int,Unit> dicData = new Dictionary<int,Unit>(); 

	public void AddItem (Unit unitInfo){
		if (dicData.ContainsKey (unitInfo.id))
			return;
		dicData.Add (unitInfo.id, unitInfo);
	}

	public Unit GetItem(int id){
		if(dicData.ContainsKey(id)){
			return dicData [id];
		}
		Debug.Log ("NULL");
		return null;
	}
	public Dictionary<int,Unit> GetAllUnits(){
		return dicData;
	}

	public int GetUnitsCount(){
		return dicData.Count;
	}
}
