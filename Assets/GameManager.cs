﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	string itemText = "You're Holding Nothing";

	void Start () {
		this.gameObject.tag = "Game Controller";
//		GameObject steakObj = new GameObject ();
//		Steak steak = steakObj.AddComponent<Steak>();
//		steak.init (5, -4);
	}
	
	void Update () {
	
	}
		
	void UpdateGUI(string item){
		itemText = "You're Holding " + item; 
	}

	void OnGUI()
	{
		GUI.Label (new Rect (100, 70, 100, 50), itemText);
	}
}
