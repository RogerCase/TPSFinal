using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makePrefab : MonoBehaviour 
{

    [SerializeField]
    bool                                                                     a_run_mode =false;
    //make sure size is odd
    [SerializeField]
	int                                                                              b_ROWS =0;
	[SerializeField]
 	int                                                                           c_COLUMNS =0;
	 

 	[SerializeField]
 	GameObject                                                                   FloorBase=null;
	[SerializeField]
 	floor                                                                       Floor_Tile=null;
 	[SerializeField]
 	float                                                                          WIDTH = 0.0f;
	[SerializeField]
 	float                                                                          HEIGHT = 0.0f;


 	 


 

	void Awake () 
	{
	    //I dont want to make scene when I am running. It avoids enabling and disabling makeobject
		FloorBase.GetComponent<Floorbase>().a_run_mode = a_run_mode;

	   //Update this information in child 
	   //Make sure about script execution order at edit=>projectsettings=>ScriptExecutionOrder
		FloorBase.GetComponent<Floorbase>().b_ROWS = b_ROWS;
		FloorBase.GetComponent<Floorbase>().c_COLUMNS = c_COLUMNS;
		//Prepare array with inspector viewing capacity with nested class
		FloorBase.GetComponent<Floorbase>().set2DArray();

		
	}


 

	 
	// Use this for initialization
	void Start () 
	{
		
		if(!a_run_mode)
		{
		    makePrefab_();
		}		
	

			
	} 

	void makePrefab_()
	{
 
 
	    int I=0;

		for(int i=0;i<b_ROWS;i++)
		{
		    //bool check_row = true;
			for(int j=0;j<c_COLUMNS;j++)
		    {
				 
				floor sample = Instantiate(Floor_Tile) as floor;
				sample.transform.position = this.transform.position+new Vector3(i*WIDTH,0,j*HEIGHT);
				sample.transform.parent = FloorBase.transform;
				sample.transform.name = I.ToString();
				sample.GetComponent<floor>().I =i;
				sample.GetComponent<floor>().J =j;
				sample.GetComponent<floor>().ID =I;
		        FloorBase.GetComponent<Floorbase>().data.rows[i].col[j]=sample;
	        			 
	    		I++;


		    }

			
			


	  
		} 



	 

	 
	}
	
	 
}
