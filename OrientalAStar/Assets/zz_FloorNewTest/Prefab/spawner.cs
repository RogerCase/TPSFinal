using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour 
{        
     [SerializeField]
     bool test = false;
     [SerializeField]
     Transform [] spawnpoints;
	// Use this for initialization
	void Start () {

		if(test)
			Make_objects_on_tile();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Make_objects_on_tile()
	{



		for(int i=0;i<spawnpoints.Length;i++)
		{
			if (spawnpoints [i].transform.childCount > 0)
				DestroyImmediate (spawnpoints [i].transform.GetChild (0).gameObject);

			 GameObject sample = Instantiate(Game_Controller.Instance.TileObjects[Random.Range(0,Game_Controller.Instance.TileObjects.Length)]) as GameObject;
			 
				
 
				sample.transform.position = spawnpoints[i].transform.position ;
				sample.transform.parent = spawnpoints[i].transform;
				sample.transform.name = "S_"+i;

		}
 
		 
	    
	}



	public void  Select_on_tile()
	{

		Debug.Log ("hi--*");

		for (int i = 0; i < spawnpoints.Length; i++) 
		{
			Debug.Log ("hi--#");
			int select =Random.Range(0, spawnpoints[i].transform.childCount);
			Debug.Log ("hi--# test"+select+"::"+i);

			for (int j = 0; j < spawnpoints[i].transform.childCount; j++) 
			{
				Debug.Log ("hi--@");
				if (j == select)
				{
					Debug.Log ("hi--1");
					spawnpoints[i].GetChild(j).gameObject.SetActive(true);
				} 
				else
				{
					spawnpoints[i].GetChild(j).gameObject.SetActive(false);
					Debug.Log ("hi--2");
				}

			}

 

		}
	}
}
