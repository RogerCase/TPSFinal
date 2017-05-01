using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager_level_help : MonoBehaviour {
	public void clickButton()
	{
		Debug.Log (" Button Clicked");
		SceneManager.LoadScene ("1_MenuScene");
	}
}
