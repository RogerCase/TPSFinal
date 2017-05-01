using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game_Controller : MonoBehaviour 
{
 
	public static Game_Controller				Instance=null;
	public float Timer=1000.0f;

	public float                                                                                 Timer_A =10;
	public float                                                                                 Timer_B =12;
	public float                                                                            Timer_Player =13;



//Object Making
	public  int                                                                                      ROWS =0;
	public  int                                                                                   COLUMNS =0;
	public floor                                                                             Floor_Tile=null;
	public float                                                                                WIDTH = 0.0f;
	public float                                                                               HEIGHT = 0.0f;

	 


	public Vector3 Start_pos_A;
	public Vector3 End_pos_A;



	public Vector3 Start_pos_B;
	public Vector3 End_pos_B;



	public Vector3 Start_pos_C;
	public Vector3 End_pos_C;


	//public controllerCar                                                     g_car_prefab=null;
	public CharacterControllerLogic_T                                                     TPSobject=null;
	public CharacterControllerLogic_T[]                                                   TPSobjects=null;

	public CharacterControllerLogic_T                                                     TPSobjectE=null;
	public CharacterControllerLogic_T[]                                                   TPSobjectsEnull;
	//public controllerCar []                                                    cars=null;
	public Material []                                                       car_material=null; 


	public bool playerA_Star =false;
	public bool playerB_Star =false;
	public bool playerC_Star =false;
	//public int number_of_cars = 0;
	public int number_of_TPS = 0;
	public GameObject[] TileObjects;

	public AStarNode[,] astarNodes=null;
	 
	public IndexingEnemies [] indexingEnemies=null ;

	public IndexingPlayer indexingPlayer=null; 
	public Astar2Dlayout                                                                  data;

	public Material z_road_material;
	public Material z_obstcale_material;
	public Material z_finish_material;
 
	public Material z_final_pathMaterial;
	public bool  z_bool_show_Astar = false;

	void Awake()
	{
		 





		if (Instance == null)
		 {
			Instance = this;
			DontDestroyOnLoad (gameObject);

			astarNodes = new AStarNode[ROWS, COLUMNS];

			indexingEnemies = new IndexingEnemies[number_of_TPS - 1];

			for (int i = 0; i < ROWS; i++) 
			{
				for (int j = 0; j < COLUMNS; j++) 
				{
					AStarNode node = new AStarNode ();
					astarNodes [i, j] = node;
				}
			}
		
			for (int i = 0; i < indexingEnemies.Length; i++)
				indexingEnemies [i] = new IndexingEnemies ();

			indexingPlayer = new IndexingPlayer();

			data = new Astar2Dlayout();
			Astar2Dlayout.COLUMNS =Game_Controller.Instance.COLUMNS;
			data.rows = new Astar2Dlayout.rowData[Game_Controller.Instance.ROWS];
			data.make_columns();




		} 
		else
		{
			DestroyImmediate (gameObject);
		}
	}




	 
 

	 
	 



}

