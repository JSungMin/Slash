using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Text;
using System;

public class XmlLoader : MonoBehaviour {
	
	public string strName;

	void Awake(){
		StartCoroutine ("Process");
	}

	IEnumerator Process(){
		string strPath = string.Empty;

		#if (UNITY_EDITOR || UNITY_STANDALONE_WIN )
		strPath += ("file:///");
		strPath += (Application.streamingAssetsPath + "/" + strName);
		#elif UNITY_ANDROID
		strPath =  "jar:file://" + Application.dataPath + "!/assets/" + strName;
		#endif
		WWW www = new WWW (strPath);
		yield return www;
		Debug.Log ("Read Content : " + www.text);
		Interpret (www.text);
	}

	private void Interpret(string strSource){

		//if error occured in incoding
		//delete 2byte(BOM DATA) and then make stringReader
		StringReader stringReader = new StringReader (strSource);

		stringReader.Read ();

		XmlNodeList xmlNodeList = null;

		XmlDocument xmlDoc = new XmlDocument ();
		try{
		xmlDoc.LoadXml (stringReader.ReadToEnd ());
		}catch(Exception e){
			xmlDoc.LoadXml (strSource);
		}
		xmlNodeList = xmlDoc.SelectNodes ("Unit");

		foreach(XmlNode node in xmlNodeList){
			if(node.Name.Equals("Unit")&&node.HasChildNodes){
				foreach(XmlNode child in node.ChildNodes){
					Unit unit = new Unit ();

					unit.id = int.Parse (child.Attributes.GetNamedItem("ID").Value);
					unit.name = child.Attributes.GetNamedItem ("NAME").Value;
					unit.hp = int.Parse (child.Attributes.GetNamedItem("HP").Value);
					unit.mp = int.Parse (child.Attributes.GetNamedItem("MP").Value);
					unit.str = int.Parse (child.Attributes.GetNamedItem("STR").Value);
					unit.def = int.Parse (child.Attributes.GetNamedItem("DEF").Value);
					unit.dex = int.Parse (child.Attributes.GetNamedItem("DEX").Value);
					unit.speed = float.Parse (child.Attributes.GetNamedItem("SPEED").Value);

					UnitManager.GetInstance.AddItem (unit);
				}
			}
		}
		UnitManager.GetInstance.isUnitInfoLoaded = true;
	}
}
