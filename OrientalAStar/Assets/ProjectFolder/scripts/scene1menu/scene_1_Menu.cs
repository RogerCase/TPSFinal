using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class scene_1_Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void clickplayButton()
	{
		Debug.Log (" Button Clicked");
		SceneManager.LoadScene ("1_MenuScene");
	}

	public void Quit()
	{
		Debug.Log (" Button Clicked");
		Application.Quit();
	}
}
