using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aa_Scene_Maker : MonoBehaviour 
{

	[SerializeField]
	rootScene                                                              aa_rootscene=null;
 
	public ArrayLayout                                                      data;

 


	// long, but simple example
	private Example1Manager m_Example1Manager;

	 
	void Start()
	{

		//true means it will make random scene
		//false means we dont make random Scene we use it for general Scene making
		aa_rootscene.a_run_mode          = false;
		aa_rootscene.b_ROWS              = Game_Controller.Instance.ROWS;
		aa_rootscene.c_COLUMNS           = Game_Controller.Instance.COLUMNS;

		//Set the array of Tiles
		aa_rootscene.set2DArray();


		//m_Example1Manager = new Example1Manager();

		m_Example1Manager =this.GetComponent<Example1Manager>();



	}


	public void makePrefab_()
	{
		// activate example 1
		//	m_Example1Manager.GoGoGo();


		int I = 0;
		for (int i = 0; i <  Game_Controller.Instance.ROWS; i++) 

		{
			//bool check_row = true;
			for (int j = 0; j < Game_Controller.Instance.COLUMNS; j++) 
			{

				floor sample = Instantiate (Game_Controller.Instance.Floor_Tile) as floor;
				sample.transform.position = this.transform.position + new Vector3 (i * Game_Controller.Instance.HEIGHT, 0, j *  Game_Controller.Instance.WIDTH);
				sample.transform.parent = aa_rootscene.transform;
				sample.transform.name = I.ToString ();
				sample.GetComponent<floor> ().I = i;
				sample.GetComponent<floor> ().J = j;
		 
				sample.GetComponent<floor> ().ID = I;
				aa_rootscene.data.rows [i].col [j] = sample;

				I++;


			} 






		}  

	}




}
