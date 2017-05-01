using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;
public class MenuManager_Selector : MonoBehaviour 
{
	public Material a_S;
	public Material a_E;
	public Material a_O;
	public Material a_C;
	public Material a_Path;
	public Material a_CurrentNode;
	public Thread myThread;
	public int Start_Index_I, Start_Index_J, End_Index_I, End_Index_J;

	public Astar astar_algorithm;

	[SerializeField]
	GameObject Proceed =null;
	[SerializeField]
	GameObject scene =null;
	[SerializeField]
	GameObject makeScene =null;

	void Start()
	{
		Proceed.GetComponent<Button>().interactable = false; 
		scene =   GameObject.FindWithTag("rootScene");
	}
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			astar_algorithm._manualResetEvent.Reset ();
		}

		if (Input.GetKeyDown (KeyCode.W)) 
		{
			astar_algorithm._manualResetEvent.Set ();
		}
		
	}

	public void Generate_AStar_Path()
	{

		Proceed.GetComponent<Button>().interactable = false; 
		makeScene.GetComponent<Button>().interactable = false; 



		for (int threadIndex = 0; threadIndex < Game_Controller.Instance.indexingEnemies.Length; threadIndex++) 
		{
			 
			//Thread thread = new Thread(new ThreadStart(WorkThreadFunction));
			//thread.Start(threadIndex);

			  myThread = new Thread(new ParameterizedThreadStart(WorkThreadFunction));

			// The parameter that is passed to the event handler is passed through
			// the Start(object) method.

			myThread.Start(threadIndex);


		 
		}
		StartCoroutine(thread_join());


	

	}

	public void WorkThreadFunction(object ThreadIndex)
	{
		Debug.Log ("  Thread make Over");


		  astar_algorithm = new Astar ();
		 
		Game_Controller.Instance.indexingEnemies[(int)ThreadIndex].finished_AStar = false;
 
		 

	

	     Start_Index_I=Game_Controller.Instance.indexingEnemies[(int)ThreadIndex].Start_E_I;
		 Start_Index_J=Game_Controller.Instance.indexingEnemies[(int)ThreadIndex].Start_E_J;

		 End_Index_I=Game_Controller.Instance.indexingEnemies[(int)ThreadIndex].End_E_I;
		 End_Index_J=Game_Controller.Instance.indexingEnemies[(int)ThreadIndex].End_E_J;

	
		Game_Controller.Instance.indexingEnemies[(int)ThreadIndex].foundPath= astar_algorithm.FindPathActual (Game_Controller.Instance.data.rows[Start_Index_I].col[Start_Index_J],
		
			Game_Controller.Instance.data.rows[End_Index_I].col[End_Index_J] 
		); 

		//System.Threading.Thread.Sleep(2000);
		 Game_Controller.Instance.indexingEnemies [(int)ThreadIndex].finished_AStar = true;

		Debug.Log ("  Thread make Over end");
	}



	private IEnumerator thread_join()
	{

		float StartT = Time.realtimeSinceStartup;

		while (true)
		{
			//yield return new WaitForSeconds(1);
			yield return null;
			bool full_thread_finished = true;
			for (int i = 0; i <   Game_Controller.Instance.indexingEnemies.Length;i++) 
			{
				if (!Game_Controller.Instance.indexingEnemies [i].finished_AStar)
					full_thread_finished = false;
			}

			if (full_thread_finished)
			{
				Debug.Log ("  Thread Wait Over");
				makeScene.GetComponent<Button>().interactable = true; 
				myThread.Join ();
				break;
			} 
			else
			{
				//Debug.Log ("Inside Thread Wait");

				for (int i = 0; (i < Game_Controller.Instance.ROWS) && Game_Controller.Instance.z_bool_show_Astar; i++) 
				{
					for (int j = 0; j < Game_Controller.Instance.COLUMNS; j++) 
					{
						/*
						if (Game_Controller.Instance.data.rows [i].col [j].Color_Flag == 2)
						{
							if(scene.GetComponent<rootScene> ().data.rows [i].col [j].transform.GetChild(0)!=null)
							scene.GetComponent<rootScene> ().data.rows [i].col [j].transform.GetChild(0).GetComponent<Renderer> ().material.color = Color.red;
						}

						if (Game_Controller.Instance.data.rows [i].col [j].Color_Flag == 3)
						{
							if(scene.GetComponent<rootScene> ().data.rows [i].col [j].transform.GetChild(0)!=null)
								scene.GetComponent<rootScene> ().data.rows [i].col [j].transform.GetChild(0).GetComponent<Renderer> ().material.color = Color.yellow;
						}*/

						floor tile = scene.GetComponent<rootScene> ().data.rows [i].col [j];

						int Child_Index= tile.ChildIDSelected ;

						switch (Game_Controller.Instance.data.rows [i].col [j].Color_Flag  )
						{

						    case  1: 
						//	tile.transform.GetChild(Child_Index).GetComponent<Renderer> ().material.color = Color.yellow;
							tile.transform.GetChild(Child_Index).GetComponent<Renderer> ().material = a_S;
							break;

						    case  2: 
							//tile.transform.GetChild(Child_Index).GetComponent<Renderer> ().material.color = Color.red;
							  tile.transform.GetChild(Child_Index).GetComponent<Renderer> ().material = a_E;
							break;

						case  3: 
							// tile.transform.GetChild(Child_Index).GetComponent<Renderer> ().material.color = Color.green;

					
							if (tile.start_node) 
							{
								tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material = a_S;
								tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material.color = Color.red;
							} 
							else 
							{
								if (tile.end_node) 
								{
									tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material = a_E;
									tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material.color = Color.yellow;
								} 
								else 
								{
									tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material = a_C;
								}
							}
									


							break;

						    case  4: 
							 //tile.transform.GetChild(Child_Index).GetComponent<Renderer> ().material.color = Color.magenta;
							//tile.transform.GetChild(Child_Index).GetComponent<Renderer> ().material  =a_O;


							if (tile.start_node) 
							{
								tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material = a_S;
								tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material.color = Color.green;
							} 
							else 
							{
								if (tile.end_node) 
								{
									tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material = a_E;
									tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material.color = Color.blue;
								} 
								else 
								{
									tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material = a_O;
								}
							}




							break;

						    case  5: 
							  //tile.transform.GetChild(Child_Index).GetComponent<Renderer> ().material.color = Color.cyan;
							//tile.transform.GetChild(Child_Index).GetComponent<Renderer> ().material = a_CurrentNode;

							if (tile.start_node) 
							{
								tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material = a_S;
								tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material.color = Color.cyan;
							} 
							else 
							{
								if (tile.end_node) 
								{
									tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material = a_E;
									tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material.color = Color.magenta;
								} 
								else 
								{
									tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material = a_CurrentNode;
								}
							}


							break;


						}

 
					}
				}

			}





		}

		for (int i = 0; i <   Game_Controller.Instance.indexingEnemies.Length;i++) 
		{
			for (int j = 0;j <   Game_Controller.Instance.indexingEnemies[i].foundPath.Count;j++) 
			{
				AStarNode astarnode = Game_Controller.Instance.indexingEnemies [i].foundPath [j];
				floor tile = scene.GetComponent<rootScene> ().data.rows [astarnode.I].col [astarnode.J];
				int Child_Index= tile.ChildIDSelected ;
				tile.transform.GetChild (Child_Index).GetComponent<Renderer> ().material = Game_Controller.Instance.z_final_pathMaterial;;
			 

			}
		}

		Proceed.GetComponent<Button>().interactable = true; 
		float EndT =Time.realtimeSinceStartup;

		for (int threadIndex = 0; threadIndex < Game_Controller.Instance.indexingEnemies.Length; threadIndex++)
		{
			Game_Controller.Instance.indexingEnemies [threadIndex].aStarFinish_Time = EndT - StartT;
		}

	}

	public void clickProceed()
	{
		Debug.Log (" Button Clicked");
		SceneManager.LoadScene ("4_Astar_Window");
	}

	private   void Action( int ThreadIndex)
	{
		 

		//System.Threading.Thread.Sleep(2000);
		//Game_Controller.Instance.indexingEnemies [ThreadIndex].finished_AStar = true;
		Debug.Log ("Inside thread^^^^^^^^^^^^^^^^");
		// Invoke("change_Flag", 2);

		//StartCoroutine(test_change_thread_finish_flag(2.0f,ThreadIndex));



	}

	 


	void change_Flag(int ThreadIndex)
	{ 
		Game_Controller.Instance.indexingEnemies [ThreadIndex].finished_AStar = true;
	}

}
