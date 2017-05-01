using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor : MonoBehaviour 
{
 
	public int                                                                 I=0,J=0;
	public int                                                                   ID =0;
	public static                                                           int ID1 =0;
	public bool obstacle =false;
	public int ChildIDSelected =0;

	public bool start_node=false;
	public bool end_node=false;

	[SerializeField]
	int HUMP_RANGE =0;
	public bool HUMP_NOT_ALLOWED;



	public void  selectOneChild(int i)
	{

		int numChildren = this.transform.childCount;

		for (int j = 0; j< numChildren; ++j)
		{
			if (i == j)
			{
				 
				this.transform.GetChild(j).gameObject.SetActive(true);
			} 
			else
			{
				this.transform.GetChild(j).gameObject.SetActive(false);
			}

		}

		 

	}






	public void  make_hump()
	{


		transform.GetChild (0).GetChild (0).transform.gameObject.SetActive (true);

		if (I == 1 || J == Game_Controller.Instance.ROWS-1)
			HUMP_NOT_ALLOWED = true;
			

		if (!(Random.Range (0, HUMP_RANGE) <= (HUMP_RANGE / 3))  || HUMP_NOT_ALLOWED) 
		{
			transform.GetChild (0).GetChild (0).transform.gameObject.SetActive (false);

		} 

	}


	void make_Scene( ) 
	{
		Debug.Log ("Make Scene from"+this.name);
	}


	 

	 
	 
}
