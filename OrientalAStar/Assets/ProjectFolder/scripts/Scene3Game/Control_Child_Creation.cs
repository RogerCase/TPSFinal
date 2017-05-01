using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Child_Creation : MonoBehaviour 
{
             public static int empty_Count_per_row=0;

             // Enable only one child  // Make sure that limit for empty spaces on row
	         public void make_single_child_enable()
			 {
		               int FLAG=-1;

#if SINGLE_CHILD
		               Debug.Log("Inside make_single_child_enable()");	
/*
		                Debug.Log("Start Empty :: "+ this.GetComponent<RandomScene>().g_Empty_Index_Start );	
					   Debug.Log("End Empty   :: "+ this.GetComponent<RandomScene>().g_Empty_Index_End );	
					   Debug.Log("Start Edge  :: "+ this.GetComponent<RandomScene>().g_Edge_Index );	
					    Debug.Log("Finish      :: "+ this.GetComponent<RandomScene>().g_Finish_Index );	*/
#endif
		                //Allow empty if it is before maximum
		                if(Control_Child_Creation. empty_Count_per_row<(this.GetComponentInParent<Floorbase>().c_COLUMNS/2))
			            {
			                    //+1 to account for 0- to n-1 counting
			                    FLAG = Random.Range(0,this.GetComponent<RandomScene>().g_Empty_Index_End+1); 
			                  

#if SINGLE_CHILD        
			                   Debug.Log("A ==> Number of Empty less than :: "+this.GetComponentInParent<Floorbase>().c_COLUMNS/2 +

			                   " Flag::"+ FLAG);
#endif

			                   if( (FLAG >=  this.GetComponentInParent<RandomScene>().g_Empty_Index_Start) &&
				                 (FLAG <=  this.GetComponentInParent<RandomScene>().g_Empty_Index_End))
				                 {

			                        Control_Child_Creation.empty_Count_per_row++;
				                  

#if SINGLE_CHILD
				                     Debug.Log("B ==> Empty Count Status from with in Range"+ Control_Child_Creation.empty_Count_per_row);

#endif 
			                     	 
			                     }
			            }


		                else
			            {       
			                   //If the number of empty reached maximum dont put empty. it avoid empty place coz random . range excludes
			                   //last one
			                     FLAG = Random.Range(0,this.GetComponent<RandomScene>().g_Empty_Index_Start); 
#if SINGLE_CHILD
			                     Debug.Log("C ==> Range over for empty with flag "+FLAG);
#endif			                    
			                    
			            }
 

			            //Take the last column        
		                if(this.GetComponent<floor>().J==this.GetComponentInParent<Floorbase>().c_COLUMNS-2)
		                {

#if SINGLE_CHILD
		                	 Debug.Log("D ==> Reached Last Coulumn"); 
#endif
		                     //If we have no empty so far then put one and prepare the static flag for next item
			                 if(Control_Child_Creation. empty_Count_per_row == 0)
			                 {

				                FLAG = this.GetComponent<RandomScene>().g_Empty_Index_Start;
#if SINGLE_CHILD
								Debug.Log("E  ==> Full Coulmn Make Space with empty :: "+FLAG ); 
#endif
				             }

			                    
			            }


			            
 		              
		                  if( (FLAG >=  this.GetComponentInParent<RandomScene>().g_Empty_Index_Start) &&
				                 (FLAG <=  this.GetComponentInParent<RandomScene>().g_Empty_Index_End))

			              this.GetComponent<RandomScene>().obstacle=false;
                        else
			              this.GetComponent<RandomScene>().obstacle=true;


		                this.GetComponent<RandomScene>().g_FinalSelectionChild = FLAG;
		                this.GetComponent<RandomScene>().avoid_object_with_Child_Index_i(FLAG);
		                 

			 }

	         public void make_child_full_empty()
			 {
			         
			         
		            foreach (Transform child in this.GetComponent<RandomScene>().Childs)
		            {     

			              //Debug.Log("Child"+child.gameObject.name);
			              if(child.gameObject.activeSelf)
			              {
				               child.gameObject.SetActive(false);
				          }

		            }

		             this.transform.GetChild(this.GetComponent<RandomScene>().g_Empty_Index_Start).gameObject.SetActive(true);
		             this.GetComponent<RandomScene>().g_FinalSelectionChild =this.GetComponent<RandomScene>().g_Empty_Index_Start;
		             this.GetComponent<RandomScene>().obstacle=false;

#if SINGLE_CHILD
		             Debug.Log("F ==> make_child_full_empty"+ Control_Child_Creation.empty_Count_per_row);

#endif 

		             
		            

			 }


	         public void make_child_fence()
			 {
			         
			        
		            this.GetComponent<RandomScene>().avoid_object_with_Child_Index_i(this.GetComponent<RandomScene>().g_Edge_Index);
		            this.GetComponent<RandomScene>().g_FinalSelectionChild =this.GetComponent<RandomScene>().g_Edge_Index;
		            this.GetComponent<RandomScene>().obstacle=true;
		             
		      // this.GetComponent<RandomScene>().avoid_object_with_Child_Index_i(-1);
		            

			 }


	         public void front_and_back_fence(bool f_RACE_GAME,int i,int j)
			 {

		         if(!f_RACE_GAME)
		         {
			         this.GetComponent<RandomScene>().avoid_object_with_Child_Index_i(this.GetComponent<RandomScene>().g_Edge_Index);
			         this.GetComponent<RandomScene>().g_FinalSelectionChild =this.GetComponent<RandomScene>().g_Edge_Index;
			         this.GetComponent<RandomScene>().obstacle=true;
		         }
		         else
		         {

		            if(i==0)
		            {
				           this.GetComponent<RandomScene>().avoid_object_with_Child_Index_i(this.GetComponent<RandomScene>().g_Edge_Index);
				           this.GetComponent<RandomScene>().g_FinalSelectionChild =this.GetComponent<RandomScene>().g_Edge_Index;
				           this.GetComponent<RandomScene>().obstacle=true;
		            }
		            else
		            {
			          	this.GetComponent<RandomScene>().avoid_object_with_Child_Index_i(this.GetComponent<RandomScene>().g_Finish_Index);
				        this.GetComponent<RandomScene>().g_FinalSelectionChild =this.GetComponent<RandomScene>().g_Finish_Index;
				        this.GetComponent<RandomScene>().obstacle=false;
		            }

		         }
			        
 
		            

			 }
}
