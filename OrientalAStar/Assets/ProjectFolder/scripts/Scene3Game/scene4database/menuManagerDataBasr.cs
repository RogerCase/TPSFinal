using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManagerDataBasr : MonoBehaviour {
	public void clickquitButton()
	{
		Application.Quit();
	}
		public void clickplayagainButton()
	{
		Debug.Log (" Button Clicked");
		SceneManager.LoadScene ("1_MenuScene");
	}
}
