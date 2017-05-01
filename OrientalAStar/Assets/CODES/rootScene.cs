using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootScene : MonoBehaviour
{
	public bool                                                            a_run_mode =false;

	public  int                                                                    b_ROWS =0;
	public  int                                                                 c_COLUMNS =0;



	public ArrayLayout                                                                  data;

	int car_count=0;

	private Example1Manager m_Example1Manager;


	// Use this for initialization
	void Start () 
	{
		m_Example1Manager =this.GetComponent<Example1Manager>();
	//	Debug.Log ("Hai");
		 
	}

	 
	 


	public void set2DArray()
	{

		data = new ArrayLayout();
		ArrayLayout.COLUMNS =Game_Controller.Instance.COLUMNS;
		data.rows = new ArrayLayout.rowData[Game_Controller.Instance.ROWS];
		data.make_columns();

	} 




	public void GeneralD()
	{

		car_count = 0;
		
		float StartT = Time.realtimeSinceStartup;

		distributerandomTileChild ();
		float EndT =Time.realtimeSinceStartup;
	//	Debug.Log ("Time for distribution"+(EndT-StartT));
	}

 




	 


	void distributerandomTileChild()
	{

		Control_Child_Creation.empty_Count_per_row=0;
		int i=0,j=0;

		for(  i=0;i<Game_Controller.Instance.ROWS;i++)
		{

			 

			#if TEST_FILE_ENTRY
			if( (i%2) ==0)
			Debug.Log("Calling Full empty  full Empty ############");
			else
			Debug.Log("Calling Single CHild ############");


			#endif     
			int  first = Random.Range(1,  Game_Controller.Instance.COLUMNS-1);
			int  second =  Random.Range(1,   Game_Controller.Instance.COLUMNS-1);

			int  third =  Random.Range(1,   Game_Controller.Instance.COLUMNS-1);


			if(first ==second)
				second =  Random.Range(1,   Game_Controller.Instance.COLUMNS-1);
			if(first ==second)
				second =  Random.Range(1,   Game_Controller.Instance.COLUMNS-1);

			if(third ==second)
				third =  Random.Range(1,   Game_Controller.Instance.COLUMNS-1);
			if(third ==second)
				third =  Random.Range(1,   Game_Controller.Instance.COLUMNS-1);


			for(  j=0;j< Game_Controller.Instance.COLUMNS;j++)
			{

				Debug.Log ("I,J "+i+","+j);

				if(j==0 || j== Game_Controller.Instance.COLUMNS-1 || i==0 )
				{
					//data.rows[i].col[j].transform.GetChild(1).gameObject.SetActive(true);
					data.rows[i].col[j].selectOneChild(1);
					data.rows[i].col[j].transform.GetChild(1).transform.GetComponent<Renderer>().material
					=Game_Controller.Instance.z_obstcale_material;

					data.rows [i].col [j].start_node = false;
					data.rows [i].col [j].end_node = false;

					data.rows[i].col[j].GetComponent<floor>().obstacle=true;
					data.rows[i].col[j].GetComponent<floor>().ChildIDSelected=1;
					data.rows [i].col [j].transform.GetChild (1).GetComponent<spawner>().Make_objects_on_tile ();
					/*
					Game_Controller.Instance.astarNodes [i, j].setAStarNode (i, j,
						data.rows [i].col [j].transform.GetChild (1).transform.position,
						true);*/
					if(Game_Controller.Instance.data.rows[i].col[j]==null)
					Debug.Log ("class Issue");

					if(data.rows[i].col[j]==null)
						Debug.Log ("obj Issue");

					if(data.rows[i].col[j].transform.GetChild(1)==null)
						Debug.Log ("child Issue");
					

					Game_Controller.Instance.data.rows[i].col[j].setAStarNode (i, j,
						data.rows[i].col[j].transform.GetChild (1).transform.position,
						true,0);
					

					 
					 


				}
				else
				{
					if(  i== Game_Controller.Instance.ROWS-1)
					{

						//data.rows[i].col[j].transform.GetChild(2).gameObject.SetActive(true);
						data.rows[i].col[j].GetComponent<floor>().obstacle=false;
						data.rows[i].col[j].selectOneChild(2);
						data.rows[i].col[j].transform.GetChild(2).transform.GetComponent<Renderer>().material
						=Game_Controller.Instance.z_finish_material;
						data.rows[i].col[j].GetComponent<floor>().ChildIDSelected=2;
						data.rows [i].col [j].start_node = false;
						data.rows [i].col [j].end_node = false;

						Game_Controller.Instance.data.rows[i].col[j].setAStarNode (i, j,
							data.rows[i].col[j].transform.GetChild(2).transform.position,
							false,0);
					 
					}
					else
					{
						general_distribution( i, j,first,second,third);
					}

				}






				//f_number_of_car, g_car_prefab,g_car_counter  






			}//second for




		}//first for 



		/*
		for(int k=1;k<cars.Length;k++)
		{
			// cars[k].GetComponent<PathFinding>().ApplyAStar();
		}*/

		for(int k=0;k<Game_Controller.Instance.indexingEnemies.Length;k++)
		{
			// cars[k].GetComponent<PathFinding>().ApplyAStar();
			data.rows [Game_Controller.Instance.indexingEnemies[k].End_E_I].col [Game_Controller.Instance.indexingEnemies[k].End_E_J].end_node = true;
			data.rows [Game_Controller.Instance.indexingEnemies[k].End_E_I].col [Game_Controller.Instance.indexingEnemies[k].End_E_J].start_node = false;

		}








	}


	void general_distribution(int i, int j,int first,int second,int third)
	{


		#if TEST_FILE_ENTRY
		Debug.Log("ROW ::"+i+"COLUMN ::"+j);
		Debug.Log("First ::"+ first + "Second ::" +second);
		#endif 

		if( (i%2) ==1 || j==first || j==second ||j==third)
		{

			//	data.rows[i].col[j].transform.GetChild(2).gameObject.SetActive(true);
			// data.rows[i].col[j].gameObject.GetComponent<Control_Child_Creation>().make_child_full_empty();
			//data.rows[i].col[j].transform.GetChild(0).gameObject.SetActive(true);

		
			data.rows[i].col[j].selectOneChild(0);
			data.rows[i].col[j].transform.GetChild(0).transform.GetComponent<Renderer>().material
			=Game_Controller.Instance.z_road_material;
			data.rows [i].col [j].start_node = false;
			data.rows [i].col [j].end_node = false;

			data.rows[i].col[j].GetComponent<floor>().obstacle=false;
			data.rows[i].col[j].GetComponent<floor>().ChildIDSelected=0;
			data.rows[i].col[j].make_hump();
			//data.rows[i].col[j].transform.GetChild(0).transform.tag="Tiles";

			Game_Controller.Instance.data.rows[i].col[j].setAStarNode (i, j,
				data.rows[i].col[j].transform.GetChild(0).transform.position,
				false,0);

			if (car_count<Game_Controller.Instance.number_of_TPS   && Random.Range(0,5)<3) 
			{
				
				if (car_count == 0) 
				{
 
					Game_Controller.Instance.indexingPlayer.Start_P_I = i;
					Game_Controller.Instance.indexingPlayer.Start_P_J = j;


				} 
				else
				{
 
					Game_Controller.Instance.indexingEnemies[car_count-1].Start_E_I = i;
					Game_Controller.Instance.indexingEnemies[car_count-1].Start_E_J = j;

					data.rows [i].col [j].start_node = true;
					data.rows [i].col [j].end_node = false;
			

					Game_Controller.Instance.indexingEnemies[car_count-1].End_E_I = Game_Controller.Instance.ROWS - 1;
					Game_Controller.Instance.indexingEnemies[car_count-1].End_E_J = Random.Range(1,Game_Controller.Instance.COLUMNS-1);

					          

				}

				car_count++;
			}




		 


		}
		else
		{      

			//data.rows[i].col[j].transform.GetChild(1).gameObject.SetActive(true);
			data.rows[i].col[j].selectOneChild(1);
			data.rows[i].col[j].transform.GetChild(1).transform.GetComponent<Renderer>().material
			=Game_Controller.Instance.z_obstcale_material;
			data.rows [i].col [j].start_node = false;
			data.rows [i].col [j].end_node = false;
			// data.rows[i].col[j].gameObject.GetComponent<Control_Child_Creation>().make_single_child_enable();
			data.rows[i].col[j].GetComponent<floor>().ChildIDSelected=1;
			data.rows[i].col[j].GetComponent<floor>().obstacle=true;	
			data.rows [i].col [j].transform.GetChild (1).GetComponent<spawner> ().Make_objects_on_tile ();

			Game_Controller.Instance.data.rows[i].col[j].setAStarNode (i, j,
				data.rows[i].col[j].transform.GetChild(1).transform.position,
				true,0);
			

			 
		}	

	}





}
