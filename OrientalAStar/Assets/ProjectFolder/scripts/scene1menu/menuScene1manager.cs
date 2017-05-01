using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScene1manager : MonoBehaviour {
	public void clickhelpButton()
	{
		Debug.Log (" Button Clicked");
		SceneManager.LoadScene ("2_helpScene");
	}
	public void clickplayButton()
	{
		Debug.Log (" Button Clicked");
		SceneManager.LoadScene ("3_selector");
	}
}
