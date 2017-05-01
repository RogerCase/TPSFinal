using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCar : MonoBehaviour
{
	rootScene root=null;
	int playercount=0;
	float up_player=2.0f;
	[SerializeField]
	ThirdPersonCamera_M camera=null;
	public int Start_Index_I, Start_Index_J, End_Index_I, End_Index_J;
	// Use this for initialization
	void Start ()
	{
		root = GameObject.FindWithTag ("rootScene").GetComponent<rootScene>();

	//	root.transform.GetComponent<AudioSource> ().enabled = false;



		Start_Index_I=Game_Controller.Instance.indexingEnemies[0].Start_E_I;
		Start_Index_J=Game_Controller.Instance.indexingEnemies[0].Start_E_J;

		End_Index_I=Game_Controller.Instance.indexingEnemies[0].End_E_I;
		End_Index_J=Game_Controller.Instance.indexingEnemies[0].End_E_J;


		Game_Controller.Instance.TPSobjects = new CharacterControllerLogic_T[ Game_Controller.Instance.number_of_TPS];

		for (int i = 0; i < Game_Controller.Instance.ROWS; i++)		
		{
			//bool check_row = true;
			for (int j = 0; j < Game_Controller.Instance.COLUMNS; j++) 
			{


				if (!root.data.rows[i].col[j].obstacle)
				{

					if ((playercount < Game_Controller.Instance.number_of_TPS) && Random.Range (0, 4) < 2) 
					{
						
									
						if (playercount == 0)
						{
				

							Game_Controller.Instance.TPSobjects [playercount] = Instantiate (Game_Controller.Instance.TPSobject) as CharacterControllerLogic_T;

							Game_Controller.Instance.TPSobjects [playercount].transform.tag = "player";
							Game_Controller.Instance.TPSobjects [playercount].transform.name = "z_player";
							camera.follow = Game_Controller.Instance.TPSobjects [playercount].transform;
							camera.start_Follow = true;
							//.Instance.TPSobjects [playercount].GetComponent<ThirdPersonCamera_M> ().start_Follow 
							//Game_Controller.Instance.TPSobjects [playercount].GetComponent<ThirdPersonCamera_M> ().start_Follow = true;
							 
							Game_Controller.Instance.TPSobjects [playercount].transform.position = root.data.rows [Start_Index_I].col [Start_Index_J].transform.GetChild (0).transform.position+
								root.data.rows [Start_Index_I].col [Start_Index_J].transform.GetChild (0).transform.up*up_player;

						} 
						else
						{
							Game_Controller.Instance.TPSobjects [playercount] = Instantiate (Game_Controller.Instance.TPSobjectE) as CharacterControllerLogic_T;
							Game_Controller.Instance.TPSobjects [playercount].transform.tag = "enemy";
							Game_Controller.Instance.TPSobjects [playercount].transform.name = "z_enemy";

							Game_Controller.Instance.TPSobjects [playercount].transform.position = root.data.rows [End_Index_I].col [End_Index_J].transform.GetChild (0).transform.position+
								root.data.rows [End_Index_I].col [End_Index_J].transform.GetChild (0).transform.up*up_player;
							Game_Controller.Instance.TPSobjects [playercount].GetComponent<AIforTPS> ().playerIndex = playercount;
							Game_Controller.Instance.TPSobjects [playercount].GetComponent<AIforTPS> ().target = camera.follow;
						
						}//first if 


						
						playercount++;
					}//second if 
				}//if obj

				if (playercount > Game_Controller.Instance.number_of_TPS)
					break;

			} //first for


			if (playercount > Game_Controller.Instance.number_of_TPS)
				break;






		}  // second for 

		 
		for(int k=1;k<Game_Controller.Instance.number_of_TPS;k++)
		{
			 // StartCoroutine(AstarCoroutine(k));

		} 


	} 

	IEnumerator AstarCoroutine(int playerIndex)
	{
		//Game_Controller.Instance.number_of_TPS[playerIndex].GetComponent<PathFinding>().ApplyAStar();

		yield return null;  
		 
	}

	 
	
	// Update is called once per frame
	void Update () 
	{


		
	}
}
