using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floorbase : MonoBehaviour 
{

	 
      public bool                                                            a_run_mode =false;
      public  int                                                                    b_ROWS =0;
 	  public  int                                                                 c_COLUMNS =0;
	  public bool                                                         d_randomChild =false;
      public bool                                              e_disable_mesh_rendering =false;
      public bool                                                        f_RACE_GAME    =true;

	  public controllerCar                                                     g_car_prefab=null;

	  public int                                                              f_number_of_car=0;
	  [SerializeField]
	  int                                                                       g_car_counter=0; 
	   [SerializeField]
	  GameObject                                                               main_camera=null; 
	  [SerializeField]
	  Material []                                                              car_material=null; 

	                                                                                                                
                                                                                                                     


#if TEST_COLOR_ASTAR
      [SerializeField]
	  bool                                                            g_AstartestColor   =true;
	  [SerializeField]
	  Material[]                                                               AstarColor=null;
 #endif





    

	  public ArrayLayout                                                      data;
	  public controllerCar []                                                    cars=null;
 
 

 



	public void set2DArray()
	{

		data = new ArrayLayout();
		ArrayLayout.COLUMNS =c_COLUMNS;
		data.rows = new ArrayLayout.rowData[b_ROWS];
		data.make_columns();

	} 


 

	// Use this for initializatio

	void Awake()
	{
		this.transform.tag = "floorbase";

	}
	void Start () 
	{

		cars = new controllerCar[f_number_of_car];
		 
			       



		 if(a_run_mode)
	       distributerandomTileChild();





	 
		
	 
		

	}

	void distributerandomTileChild()
	{
	             
		        Control_Child_Creation.empty_Count_per_row=0;
		        int i=0,j=0;

		        for(  i=0;i<b_ROWS;i++)
				{

			         Control_Child_Creation. empty_Count_per_row =0;

#if TEST_FILE_ENTRY
			         if( (i%2) ==0)
			               	Debug.Log("Calling Full empty  full Empty ############");
                     else
				            Debug.Log("Calling Single CHild ############");

			      
			#endif     
			           int  first = Random.Range(1,  c_COLUMNS-1);
			           int  second =  Random.Range(1,  c_COLUMNS-1);

			           int  third =  Random.Range(1,  c_COLUMNS-1);

			       
			           if(first ==second)
					         second =  Random.Range(1,  c_COLUMNS-1);
		               if(first ==second)
					         second =  Random.Range(1,  c_COLUMNS-1);
	
			           if(third ==second)
				             third =  Random.Range(1,  c_COLUMNS-1);
			           if(third ==second)
				            third =  Random.Range(1,  c_COLUMNS-1);
			           
 			          
					for(  j=0;j<c_COLUMNS;j++)
				    {

				 

				        if(j==0 || j==c_COLUMNS-1 || i==0 )
				        {
					       data.rows[i].col[j].transform.GetChild(1).gameObject.SetActive(true);
				          	data.rows[i].col[j].GetComponent<floor>().obstacle=true;
					        data.rows[i].col[j].GetComponent<floor>().ChildIDSelected=1;		
				        }
				        else
				        {
					       if(  i==b_ROWS-1)
				           {

						          data.rows[i].col[j].transform.GetChild(2).gameObject.SetActive(true);
						          data.rows[i].col[j].GetComponent<floor>().ChildIDSelected=2;	
						           
				           }
				           else
				           {
						general_distribution( i, j,first,second,third);
						   }
				           
				        }

 


			 
								       
				//f_number_of_car, g_car_prefab,g_car_counter  


				        if((!data.rows[i].col[j].gameObject.GetComponent<floor>().obstacle ))
				    	{

								 

					            if((g_car_counter<f_number_of_car) && Random.Range(0,4)<2)
					            {
						            cars[g_car_counter] = Instantiate(g_car_prefab) as controllerCar;
									if(g_car_counter==0)
									{
							            cars[g_car_counter].transform.GetComponent< AI>().enabled = false;
							            cars[g_car_counter].transform.tag="player";
								        cars[g_car_counter].transform.position= data.rows[i].col[j].transform.GetChild(0).transform.position;

								          // car.transform.RotateAround=position_of_empty_object;
							            cars[g_car_counter].player=true;
							        
							            // main_camera.GetComponent<CameraChase>().m_Car=cars[g_car_counter];
							            // main_camera.GetComponent<CameraChase>().chase_camera=true;
							             cars[g_car_counter].transform.GetChild(3).gameObject.SetActive(true);
							            cars[g_car_counter].transform.GetComponent<PathFinding>().enabled=false;

							            cars[g_car_counter].gameObject.transform.GetChild(0).GetComponent<Renderer>().material= car_material[g_car_counter];

							          
							            

									}
									else
									{

							          //    main_camera.GetComponent<CameraChase>().m_Car=cars[g_car_counter];
							            //  main_camera.GetComponent<CameraChase>().chase_camera=true;

							              cars[g_car_counter].transform.GetComponent< controllerCar>().enabled = false;
							              cars[g_car_counter].transform.tag="enemy";

							              cars[g_car_counter].transform.position= data.rows[i].col[j].transform.GetChild(0).transform.position;
							              cars[g_car_counter].transform.GetComponent<AudioSource>().enabled=false;

							 

							             cars[g_car_counter].transform.GetComponent<PathFinding>().Start_AStar=data.rows[i].col[j];
								         cars[g_car_counter].transform.GetComponent<PathFinding>().End_AStar=data.rows[b_ROWS-1].col[Random.Range(1,c_COLUMNS-2)];
								         cars[g_car_counter].transform.GetComponent<PathFinding>().test_Color=true;
							             cars[g_car_counter].transform.GetChild(3).gameObject.SetActive(false);
							             cars[g_car_counter].transform.GetChild(4).gameObject.SetActive(false);
							             cars[g_car_counter].gameObject.transform.GetChild(0).GetComponent<Renderer>().material= car_material[g_car_counter];



									/*
							             cars[g_car_counter].transform.GetComponent< controllerCar>().enabled = false;
							             
							            cars[g_car_counter].transform.tag="enemy";
										int index_of_empty =  data.rows[i].col[j].GetComponent<RandomScene>().g_FinalSelectionChild;
								        Vector3 position_of_empty_object = data.rows[i].col[j].transform.GetChild(index_of_empty ).transform.position;
							            cars[g_car_counter].transform.position=position_of_empty_object;
							            cars[g_car_counter].player=false;
							           // cars[g_car_counter].gameObject.transform.GetChild(0).GetComponent<Renderer>().material= car_material[1];

							            cars[g_car_counter].transform.GetComponent<AudioSource>().enabled=false;


							            cars[g_car_counter].transform.GetComponent<PathFinding>().Start_AStar=data.rows[i].col[j];


								        cars[g_car_counter].transform.GetComponent<PathFinding>().Start_AStar=data.rows[i].col[j];
								        cars[g_car_counter].transform.GetComponent<PathFinding>().End_AStar=data.rows[b_ROWS-1].col[Random.Range(1,c_COLUMNS-2)];
								        cars[g_car_counter].transform.GetComponent<PathFinding>().test_Color=true;
								                // car.transform.GetComponent<AstarCarRace>().rows = b_ROWS ;
								                // car.transform.GetComponent<AstarCarRace>().columns = c_COLUMNS ;
								               //  car.transform.GetComponent<PathFinding>().ApplyAStar();
								                 
							           cars[g_car_counter].transform.GetChild(3).gameObject.SetActive(false);
						               cars[g_car_counter].player=false;

							           cars[g_car_counter].gameObject.transform.GetChild(0).GetComponent<Renderer>().material= car_material[g_car_counter];

							           //cars[g_car_counter].GetComponent<carChaseAI>().Chaser_Object(cars[g_car_counter].transform.GetChild(0).gameObject ,car_material[g_car_counter]);
							           g_car_counter++;		    */      
								  
									}
						            g_car_counter++;
								}
						 }
							        
 						 
		        
			         

					}//second for




			     }//first for 


			     for(int k=1;k<cars.Length;k++)
			     {
			           cars[k].GetComponent<PathFinding>().ApplyAStar();
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
			                  data.rows[i].col[j].transform.GetChild(0).gameObject.SetActive(true);
			                  data.rows[i].col[j].GetComponent<floor>().obstacle=false;
			                  data.rows[i].col[j].GetComponent<floor>().ChildIDSelected=0;
					           
					          

							}
							else
							{      

			                       data.rows[i].col[j].transform.GetChild(1).gameObject.SetActive(true);
					             // data.rows[i].col[j].gameObject.GetComponent<Control_Child_Creation>().make_single_child_enable();
			                       data.rows[i].col[j].GetComponent<floor>().ChildIDSelected=1;
			                      data.rows[i].col[j].GetComponent<floor>().obstacle=true;		  
							}	

	}




	 

  }
