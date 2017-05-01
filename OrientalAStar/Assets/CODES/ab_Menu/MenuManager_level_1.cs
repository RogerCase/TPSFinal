using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager_level_1 : MonoBehaviour 
{
	public void clickProceed()
	{
		Debug.Log (" Button Clicked");
		SceneManager.LoadScene ("4_Astar_Window");
	}

	public void menu()
	{
		Debug.Log (" Button Clicked");
		SceneManager.LoadScene ("1_MenuScene");
	}



	 


}
