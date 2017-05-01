using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
 
public class ThreadProcessor :MonoBehaviour
{
	
	public rootScene RootScene;
 
	public void Process(  bool wait) 
 
	{

		// number of items to process
		if (Game_Controller.Instance.ROWS == 0 || Game_Controller.Instance.COLUMNS == 0 ) return;

		//int items_count_perCol = Game_Controller.Instance.ROWS;
		
		// thread array
		List<Thread> threads = new List<Thread>();
	
		/*
		// max number of possible thread, depending on hardware and number of items
		int maxThreads = Environment.ProcessorCount;
		//Debug.Log (maxThreads);
		if (maxThreads < 1) maxThreads = 1;
		if (maxThreads > items_count_perCol) maxThreads = items_count_perCol;*/

		// items per thread, perhaps
		//int itemsPerThread = items_count_perCol / maxThreads;
        
		// number of items to process per thread
		//int itemsToProcess = Game_Controller.Instance.ROWS;

		// loop through all threads
		for (int threadIndex = 0; threadIndex < Game_Controller.Instance.ROWS; ++threadIndex) 
		//for (int threadIndex = 0; threadIndex <1; ++threadIndex) 
		{
            
			
			 
			

			Thread thread = new Thread(() => Action(  threadIndex ));
			
			// add it to threads array
			threads.Add(thread);
			
			// and start it...
            thread.Start();
        }
		
		// wait until all threads are finished
        if (wait) 
		{
            // block threads until all calls are finished
			foreach (Thread thread in threads)
			{
                thread.Join();
            }
        }
	}


	 
	public void  Make_Row(int ColIndex,int i)
	{

		floor sample = Instantiate (Game_Controller.Instance.Floor_Tile) as floor;
		sample.transform.position = Game_Controller.Instance.transform.position + new Vector3 (i * Game_Controller.Instance. WIDTH, 0, ColIndex*  Game_Controller.Instance.HEIGHT);
		sample.transform.parent = Game_Controller.Instance.transform.GetChild(0).transform;

		sample.GetComponent<floor> ().I = i;
		sample.GetComponent<floor> ().J = ColIndex;
		sample.GetComponent<floor> ().ID = i * Game_Controller.Instance.COLUMNS + ColIndex;

		Game_Controller.Instance.transform.GetChild(0).transform.GetComponent<aa_Scene_Maker>().data.rows [i].col [ColIndex] = sample;

	}
	  
	private   void Action( int ColIndex)
	{


		// Debug.Log ("Col"+ColIndex);
		//RootScene.GeneralD ();

		//float StartT = Time.realtimeSinceStartup;

		//Debug.Log ("Col"+Game_Controller.Instance.ROWS);

		//Game_Controller.Instance.Distribution_number [0,0] = 1;

		//Debug.Log ("Check"+Game_Controller.Instance.Distribution_number [0,0]);

		//Game_Controller.Instance.distributerandomTileChild (ColIndex);

		 
		 
    }

}