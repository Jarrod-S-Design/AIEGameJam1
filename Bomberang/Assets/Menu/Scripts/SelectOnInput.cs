using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour 
{
	//Variables
	public EventSystem eventsystem;
	public GameObject selectedObject;

	private bool buttonSelected;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Checks if any input is coming from either a gamepad or a keyboard 
		if (Input.GetAxisRaw ("Vertical") != 0 && buttonSelected == false) 
		{
			eventsystem.SetSelectedGameObject (selectedObject);
			buttonSelected = true;
		}
	}

	private void OnDisable()
	{
		buttonSelected = false;
	}
}
