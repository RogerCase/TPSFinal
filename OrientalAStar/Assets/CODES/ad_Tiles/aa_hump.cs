using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class aa_hump : MonoBehaviour
{
 

	// Use this for initialization
	void Start () 
	{
		
	}


	[SerializeField]
	int HUMP_RANGE =0;
	public bool HUMP_NOT_ALLOWED;
	public void  make_hump()
	{

		transform.GetChild (0).transform.gameObject.SetActive (true);
			
		if (!(Random.Range (0, HUMP_RANGE) <= (HUMP_RANGE / 3))  || HUMP_NOT_ALLOWED) 
		{
			transform.GetChild (0).transform.gameObject.SetActive (false);

		} 
		 
	}
	
	 
}
