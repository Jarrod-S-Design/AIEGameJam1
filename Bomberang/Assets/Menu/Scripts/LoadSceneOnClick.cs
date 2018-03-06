using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour 
{
	//Clicking a button on the menu passes the scene index through this int
	//This then finds the scene to load using the scene index from the buld settings
	public void LoadByIndex(int sceneIndex)
	{
		SceneManager.LoadSceneAsync ("Main");
        //SceneManager.LoadSceneAsync("HelpOverlay");
	}
}
