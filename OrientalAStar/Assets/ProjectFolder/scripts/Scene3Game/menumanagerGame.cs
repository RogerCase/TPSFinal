using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menumanagerGame : MonoBehaviour {
	public void clickDBButton()
	{
		Debug.Log (" Button Clicked");
		SceneManager.LoadScene ("4_dataBaseScene");
	}



}
