using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AstarMenuwait : MonoBehaviour 
{
	[SerializeField]
	GameObject PlayButton=null;

	[SerializeField]
	GameObject WaitMessage=null;

	[SerializeField]
	GameObject PlayMessage=null;

	[SerializeField]
	bool need_check = true;

	// Use this for initialization
	void Start () 
	{
		PlayButton.gameObject.SetActive(false);
		WaitMessage.gameObject.SetActive(true);
		PlayMessage.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (need_check && Game_Controller.Instance.playerA_Star && Game_Controller.Instance.playerB_Star)
		{

			PlayButton.gameObject.SetActive(true);
			WaitMessage.gameObject.SetActive(false);
			PlayMessage.gameObject.SetActive(true);
			need_check = false;
		}

	}

	public void clickPlay()
	{
		Debug.Log (" Button Clicked");
		SceneManager.LoadScene ("5_GameScene");
	}
}
