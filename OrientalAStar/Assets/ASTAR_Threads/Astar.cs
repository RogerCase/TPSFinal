using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
[System.Serializable]
public class Astar 
{   
	[SerializeField]
	List<AStarNode> foundPath;
	[SerializeField]
	List<AStarNode> openSet;

	[SerializeField]
	List<AStarNode> closedSet_Test;

	[SerializeField]
	HashSet<AStarNode> closedSet;
	[SerializeField]
	AStarNode currentNode=null;


	public ManualResetEvent _manualResetEvent = new ManualResetEvent(true);

	public  Astar()
	{
		this.foundPath = new List<AStarNode>();
		//We need two lists, one for the nodes we need to check and one for the nodes we've already checked
		this.openSet = new List<AStarNode>();
		this.closedSet_Test = new List<AStarNode>();
		this.closedSet = new HashSet<AStarNode>();
		this.currentNode=null;  

	}



	public List<AStarNode> FindPathActual(AStarNode start, AStarNode target)
	{

		foundPath.Clear ();
		openSet.Clear ();
		closedSet.Clear ();
		closedSet_Test.Clear ();

		start.Color_Flag = 1;
		target.Color_Flag = 2;

		Game_Controller.Instance.data.rows [start.I].col [start.J].Color_Flag = 1;
		Game_Controller.Instance.data.rows [target.I].col [target.J].Color_Flag = 2;

		Debug.Log ("Start I,J"+start.I+","+start.J);
		Debug.Log ("target I,J"+target.I+","+target.J);
		int COUNT_TEST = 0;
		//We start adding to the open set
		openSet.Add(start);
		#if !BLOCK_CODE
		while (openSet.Count > 0)
		{

			    Debug.Log("Loop1 Count"+openSet.Count);
				currentNode = openSet[0];
			  //   Debug.Log ("TEST A");
				for (int i = 0; i < openSet.Count; i++)
				{
				     
				    // Debug.Log ("TEST B");
					//We check the costs for the current node
					//You can have more opt. here but that's not important now
					if (openSet[i].fCost < currentNode.fCost ||
						(openSet[i].fCost == currentNode.fCost &&
							openSet[i].hCost < currentNode.hCost))
					{
					   //  Debug.Log ("TEST C");
						//and then we assign a new current node
						//if (!currentNode.Equals(openSet[i]))
					    if(currentNode.I==openSet[i].I && currentNode.J==openSet[i].J)
						{
							currentNode = openSet[i];
					     	//Debug.Log ("TEST D");
						}
					}
				    Debug.Log("Loop2 Count"+openSet.Count);
				    
				}


			 
				  currentNode.Color_Flag = 5;
				  Game_Controller.Instance.data.rows [currentNode.I].col [currentNode.J].Color_Flag = 5;
			 

			 



				


				//we remove the current node from the open set and add to the closed set
				openSet.Remove(currentNode);
				closedSet.Add(currentNode);
			    closedSet_Test.Add(currentNode);


				//if the current node is the target node
			if (currentNode.I==target.I && currentNode.J==target.J)
				{
				
					//that means we reached our destination, so we are ready to retrace our path
					 foundPath = RetracePath(start, currentNode);
			    	Debug.Log ("TEST E Break @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

				     currentNode.Color_Flag = 5;
				     Game_Controller.Instance.data.rows [currentNode.I].col [currentNode.J].Color_Flag = 5;
				Thread.Sleep(2000);
					 break;
					//return foundPath;

				}
				Debug.Log ("######################################");
				Debug.Log ("Current Node" + currentNode.I + "," + currentNode.J);

			  

				//if we haven't reached our target, then we need to start looking the neighbours
				foreach (AStarNode neighbour in GetNeighbours(currentNode))
				{

				    Debug.Log("Loop3 Count"+openSet.Count);
				      //  Debug.Log ("TEST F");
				    Debug.Log ("NeighBour Node" + neighbour.I + "," + neighbour.J);
					if (!closedSet.Contains (neighbour))
					{
					   // Debug.Log ("TEST G");
						//we create a new movement cost for our neighbours
						float newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
						//and if it's lower than the neighbour's cost
						if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains (neighbour)) 
						{
						  //  Debug.Log ("TEST H");
							//we calculate the new costs
							neighbour.gCost = newMovementCostToNeighbour;
							neighbour.hCost = GetDistance(neighbour, target);
							//Assign the parent node
							neighbour.parentNode = currentNode;
							//And add the neighbour node to the open set
							if (!openSet.Contains(neighbour))
							{
								openSet.Add(neighbour);
							    //  Debug.Log ("TEST I");
							}

						}

					}
					
				}

			Debug.Log ("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

			for (int i = 0; i < openSet.Count; i++) 
			{
				  
				  openSet[i].Color_Flag = 4;
				  Game_Controller.Instance.data.rows [openSet[i].I].col [openSet[i].J].Color_Flag = 4;
				 
			}
			Debug.Log ("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
			for (int i = 0; i < closedSet.Count; i++) 
			{
				if(   closedSet_Test[i].Color_Flag != 5)
				{
				  closedSet_Test[i].Color_Flag = 3;
				  Game_Controller.Instance.data.rows [closedSet_Test[i].I].col [closedSet_Test[i].J].Color_Flag = 3;
				}
			}

			Debug.Log ("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");

			//Thread.Sleep(8000);
		//	Debug.Log("Done");
			//break;
			//
			if(Game_Controller.Instance.z_bool_show_Astar)
			   Thread.Sleep(2000);
		 
			Game_Controller.Instance.data.rows [currentNode.I].col [currentNode.J].Color_Flag = 3;
			// break;
			 
			 
		}

		#endif 
		//Thread.Sleep(20000);
		return foundPath;
	}

	private List<AStarNode> RetracePath(AStarNode startNode, AStarNode endNode)
	{
		//Retrace the path, is basically going from the endNode to the startNode
		List<AStarNode> path = new List<AStarNode>();
		AStarNode currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode.Color_Flag = 3;

			//by taking the parentNodes we assigned
			currentNode = currentNode.parentNode;
		}

		//then we simply reverse the list
		path.Reverse();

		return path;
	}

	private List<AStarNode> GetNeighbours(AStarNode node)
	{
		//This is were we start taking our neighbours
		List<AStarNode> retList = new List<AStarNode>();

		int current_row         = node.I;
		int current_column      = node.J;


		//Bottom
		int BottomNodeRow = current_row - 1;
		int BottomNodeColumn = current_column;
		Debug.Log ("Bottom ::" +BottomNodeRow+","+BottomNodeColumn);

		if (Check_valid_Index (BottomNodeRow, BottomNodeColumn))
		{
			retList.Add (Game_Controller.Instance.data.rows[BottomNodeRow].col[BottomNodeColumn]);
		}
		 


		//Top
		int TopNodeRow = current_row + 1;
		int TopNodeColumn = current_column;
		Debug.Log ("Top ::" +TopNodeRow+","+TopNodeColumn);

		if (Check_valid_Index (TopNodeRow, TopNodeColumn))
		{
			retList.Add (Game_Controller.Instance.data.rows[TopNodeRow].col[TopNodeColumn]);
		}

		 

		//Right
		int RightNodeRow = current_row;
		int RightNodeColumn = current_column + 1;
		Debug.Log ("Right ::" +RightNodeRow+","+RightNodeColumn);
		 
		if (Check_valid_Index (RightNodeRow, RightNodeColumn))
		{
			retList.Add (Game_Controller.Instance.data.rows[RightNodeRow].col[RightNodeColumn]);
		}


		 
		//Left
		int leftNodeRow = current_row;
		int leftNodeColumn = current_column - 1;
		Debug.Log ("Left ::" +leftNodeRow+","+leftNodeColumn);
		if (Check_valid_Index (leftNodeRow, leftNodeColumn))
		{
			retList.Add (Game_Controller.Instance.data.rows[leftNodeRow].col[leftNodeColumn]);
		}

		if (retList.Count == 0) 
		{
			Debug.LogError ("Logic Error in Neighbour I,J"+current_row+","+current_column);
		}
			 
			
		 

		return retList;

	}

	bool Check_valid_Index(int I,int J)
	{


		


		 

	 
		bool boarder_check_flag = (                      (I == -1) ||
		                                                  (J == -1) ||
		                          I ==Game_Controller.Instance.ROWS ||
			                      J == Game_Controller.Instance.COLUMNS);

		if (boarder_check_flag)
			return false;


		if (Game_Controller.Instance.data.rows [I].col [J].obstacle)
			return false;
		else
			return true;


			                         

			   

		//return  boarder_check_flag && !Game_Controller.Instance.data.rows[I].col[J].obstacle;
		 
	}

	private float GetDistance(AStarNode posA, AStarNode posB)
	{
		//return Vector3.Distance (posA.Position, posB.Position)*10.0f;
		//Vector3.Distance (

		return (float)(Mathf.Abs (posA.I - posB.I) + Mathf.Abs(posA.J - posB.I));
	}



}
