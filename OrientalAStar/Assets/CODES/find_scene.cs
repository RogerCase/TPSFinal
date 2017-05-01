using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class find_scene : MonoBehaviour 
{
	[SerializeField]
	bool need_scene = true;

	[SerializeField]
	GameObject Proceed =null;

	[SerializeField]
	GameObject Distribution =null;

	public GameObject scene = null;

	 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (need_scene) 
		{
			Proceed.gameObject.SetActive(false);
			Distribution.gameObject.SetActive(false);

			scene =   GameObject.FindWithTag("rootScene");
			if ( scene!= null) 
			{
				Proceed.gameObject.SetActive(true);
				Distribution.gameObject.SetActive(true);
				need_scene = false;
				newScene ();
				 
			}
				

		}
	}

	public void newScene()
	{
		scene.transform.GetComponent<rootScene>().GeneralD ();

	}

  
}
