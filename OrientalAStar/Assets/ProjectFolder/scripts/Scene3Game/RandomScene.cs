using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScene : MonoBehaviour 
{
    //We have other programs to set the object on top while making scene.If you enable test We check its working
    //It is purely for testing purpose
	[SerializeField]
	bool                                                                               a_TILE_TEST = true;
	[SerializeField]
    bool                                                                                b_no_Scene = true;
	[SerializeField]
	bool                                                                              c_test_Fixed = true;
	[SerializeField]
	int                                                                                  d_Select_child=0;
	[SerializeField]
	bool                                                                                   e_random=false;
	[SerializeField]
	bool                                                                                   e_a_loop=false;
	public int                                                                                f_FLAG =-1;
 	public int                                                                             g_Empty_Index_Start;
	public int                                                                             g_Empty_Index_End;
	public int                                                                             g_Finish_Index;
	public int                                                                             g_Edge_Index;
	public int                                                                             g_FinalSelectionChild;

	int                                                                                OPTION_SELECTOR =0;

 
    [SerializeField]
    public Transform []                                                                  Childs = null;
    [SerializeField]
	public bool                                                                          obstacle=false;

	 


 
	//Rememeber in Test Case you have to fix the parameters before running we put it slize

 
	void Start ()
	{
		
		g_Empty_Index_Start = getEmptyIndex_Start();
		g_Empty_Index_End = getEmptyIndex_End();
		g_Edge_Index = getEdgeIndex();
		g_Finish_Index = getFinishIndex();

	

		TestSingleTile();


	}

	void Update()
	{
		if(e_a_loop)
	    TestSingleTile();

	}



	void setValueFlag()
	{
		        
               if(b_no_Scene)
               {
			     OPTION_SELECTOR = 0;
			     return;
			   }

		       if(c_test_Fixed)
		       {

			     OPTION_SELECTOR = 1;
			     return;
               }


		      if(e_random)
		       {

			     OPTION_SELECTOR = 2;
			     return;
               }



	}

	int getEmptyIndex_Start()
	{
	     int I=0;
		foreach (Transform child in Childs)
		{
				 if(child.tag=="empty")
				  return I;
				  I++;
				 
        }
	   return -1;
	}

	int getEmptyIndex_End()
	{
	     int I=0;
		foreach (Transform child in Childs)
		{
				 if(child.tag=="empty_end")
				  return I;
				  I++;
				 
        }
	   return -1;
	}


	int getEdgeIndex()
	{
	     int I=0;
		foreach (Transform child in Childs)
		{
				 if(child.tag=="edge")
				  return I;
				  I++;
				 
        }
	   return -1;
	}



	int getFinishIndex()
	{
	     int I=0;
		foreach (Transform child in Childs)
		{
				 if(child.tag=="finish")
				  return I;
				  I++;
				 
        }
	   return -1;
	}



	void TestSingleTile()
	{

				if(a_TILE_TEST)
				{   

						setValueFlag();

						switch(OPTION_SELECTOR)
						{
								   //No scene
								   case 0 : 
								                 //Just for getting a good size
						                         f_FLAG =2*Childs.Length;
											     obstacle=false; 
									             break; 
								   //fixed Element	              
					               case 1 :
						                         f_FLAG = d_Select_child;	
									             //Empty object is at the bottom of object list			
				                                 if(f_FLAG>=g_Empty_Index_Start && f_FLAG<= g_Empty_Index_End)
												 {
													obstacle=false;
												 }
												 else
												 {
													obstacle=true;
												 }
									             break;
								    

									//fixed Element	              
					               case 2 :
						                         f_FLAG =Random.Range(0,Childs.Length);				
			                                     if(f_FLAG>=g_Empty_Index_Start && f_FLAG<= g_Empty_Index_End)
												 {
													obstacle=false;
												 }
												 else
												 {
													obstacle=true;
												 }
									             break;

			      
						}

			            avoid_object_with_Child_Index_i (f_FLAG);


				}
	}


	public void avoid_object_with_Child_Index_i (int i)
	{
		
			for (int j=0;j<Childs.Length;j++)
			{
			 

				if(j!=i)
				{
				    Childs[j].gameObject.SetActive(false);
				}
				else
				{
				    Childs[j].gameObject.SetActive(true);
				}



			}
	    

	}


		bool boarder()
		{


			if(this.GetComponent<floor>().I == 0
			    || 
				this.GetComponent<floor>().I == this.transform.parent.GetComponent<Floorbase>().b_ROWS-1
				|| 
				this.GetComponent<floor>().J == 0
				|| 
				this.GetComponent<floor>().J == this.transform.parent.GetComponent<Floorbase>().c_COLUMNS-1)
	      

		 
	 
				return true;
	             else
	            return false;
		}




}
