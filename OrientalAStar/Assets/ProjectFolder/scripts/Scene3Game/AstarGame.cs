using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarGame : MonoBehaviour
{

	#region List fields

    public static PriorityQueue closedList, openList;
    public static int rows=0;
    public static int columns =0;
 
 


    #endregion

    /// <summary>
    /// Calculate the final path in the path finding
    /// </summary>
    private static ArrayList CalculatePath(Node node)
    {
        ArrayList list = new ArrayList();
        while (node != null)
        {
            list.Add(node);
            node = node.parent;
        }
        list.Reverse();
        return list;
    }

    /// <summary>
    /// Calculate the estimated Heuristic cost to the goal
    /// </summary>
    private static float HeuristicEstimateCost(Node curNode, Node goalNode)
    {
        Vector3 vecCost = curNode.position - goalNode.position;
        return vecCost.magnitude;
    }

    /// <summary>
    /// Find the path between start node and goal node using AStar Algorithm
    /// </summary>
	public static ArrayList FindPath(Node start, Node goal,ref Floorbase floorbase)
    {

        
		AstarGame.rows =floorbase.b_ROWS;
		AstarGame.columns=floorbase.c_COLUMNS;


#if !ASTAR_TEST_A
		//Calculate the path based on the final node
		goal.parent=start;
		return CalculatePath(goal);
#endif

		openList = new PriorityQueue();
        openList.Push(start);
        start.nodeTotalCost = 0.0f;
		start.estimatedCost = HeuristicEstimateCost(start, goal);

		closedList = new PriorityQueue();
        Node node = null;


		while (openList.Length != 0)
        {
			node = openList.First();

            if (node.position == goal.position)
            {
                return CalculatePath(node);
            }


			 ArrayList neighbours = new ArrayList();
     		 AstarGame.getNeighbours(node,neighbours,ref floorbase);


			#region CheckNeighbours

            //Get the Neighbours
            for (int i = 0; i < neighbours.Count; i++)
            {
                //Cost between neighbour nodes
                Node neighbourNode = (Node)neighbours[i];

                if (!closedList.Contains(neighbourNode))
                {					
					//Cost from current node to this neighbour node
	                float cost = HeuristicEstimateCost(node, neighbourNode);	
	                
					//Total Cost So Far from start to this neighbour node
	                float totalCost = node.nodeTotalCost + cost;
					
					//Estimated cost for neighbour node to the goal
	                float neighbourNodeEstCost = HeuristicEstimateCost(neighbourNode, goal);					
					
					//Assign neighbour node properties
	                neighbourNode.nodeTotalCost = totalCost;
	                neighbourNode.parent = node;
	                neighbourNode.estimatedCost = totalCost + neighbourNodeEstCost;
	
	                //Add the neighbour node to the list if not already existed in the list
	                if (!openList.Contains(neighbourNode))
	                {
	                    openList.Push(neighbourNode);
	                }
                }
            }
			
            #endregion
            
            closedList.Push(node);
            openList.Remove(node);



        }
		//If finished looping and cannot find the goal then return null
        if (node.position != goal.position)
        {
            Debug.LogError("Goal Not Found");
            return null;
        }

        //Calculate the path based on the final node
        return CalculatePath(node);





    #if !BLOCK_COMMEND
        //Start Finding the path
        openList = new PriorityQueue();
        openList.Push(start);
        start.nodeTotalCost = 0.0f;


        start.estimatedCost = HeuristicEstimateCost(start, goal);

        closedList = new PriorityQueue();
        

        while (openList.Length != 0)
        {
            node = openList.First();

            if (node.position == goal.position)
            {
                return CalculatePath(node);
            }
			
            ArrayList neighbours = new ArrayList();
            GridManager.instance.GetNeighbours(node, neighbours);

            #region CheckNeighbours

            //Get the Neighbours
            for (int i = 0; i < neighbours.Count; i++)
            {
                //Cost between neighbour nodes
                Node neighbourNode = (Node)neighbours[i];

                if (!closedList.Contains(neighbourNode))
                {					
					//Cost from current node to this neighbour node
	                float cost = HeuristicEstimateCost(node, neighbourNode);	
	                
					//Total Cost So Far from start to this neighbour node
	                float totalCost = node.nodeTotalCost + cost;
					
					//Estimated cost for neighbour node to the goal
	                float neighbourNodeEstCost = HeuristicEstimateCost(neighbourNode, goal);					
					
					//Assign neighbour node properties
	                neighbourNode.nodeTotalCost = totalCost;
	                neighbourNode.parent = node;
	                neighbourNode.estimatedCost = totalCost + neighbourNodeEstCost;
	
	                //Add the neighbour node to the list if not already existed in the list
	                if (!openList.Contains(neighbourNode))
	                {
	                    openList.Push(neighbourNode);
	                }
                }
            }
			
            #endregion
            
            closedList.Push(node);
            openList.Remove(node);
        }

        //If finished looping and cannot find the goal then return null
        if (node.position != goal.position)
        {
            Debug.LogError("Goal Not Found");
            return null;
        }

        //Calculate the path based on the final node
        return CalculatePath(node);

#endif
    }


	public static void getNeighbours(Node node, ArrayList neighbors,ref Floorbase floorbase)
	{
		int row = node.tile.I;
		int column = node.tile.J;
		bool obstacle =false;


		//Bottom
		obstacle = node.tile.gameObject.GetComponent<RandomScene>().obstacle;
        int leftNodeRow = row - 1;
        int leftNodeColumn = column;
		AstarGame.AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors,ref  floorbase);


		//Top
        leftNodeRow = row + 1;
        leftNodeColumn = column;
		AstarGame.AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors,ref  floorbase);



		//Right
        leftNodeRow = row;
        leftNodeColumn = column + 1;
		AstarGame.AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors,ref  floorbase);


		//Left
        leftNodeRow = row;
        leftNodeColumn = column - 1;
		AstarGame.AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors,ref  floorbase);



	/*
		Vector3 neighborPos = node.position;
        int neighborIndex = GetGridIndex(neighborPos);

        int row = GetRow(neighborIndex);
        int column = GetColumn(neighborIndex);

        //Bottom
        int leftNodeRow = row - 1;
        int leftNodeColumn = column;
        AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);

        //Top
        leftNodeRow = row + 1;
        leftNodeColumn = column;
        AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);

        //Right
        leftNodeRow = row;
        leftNodeColumn = column + 1;
        AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);

        //Left
        leftNodeRow = row;
        leftNodeColumn = column - 1;
        AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);
        */

	}

 #if  !BLOCK_COMMEND
	public static void AssignNeighbour(int I, int J, ArrayList neighbors,ref Floorbase floorbase)
    {
        if (I != -1 && J != -1 && I < AstarGame.rows && J < AstarGame.columns)
        {


			if(!floorbase.data.rows[I].col[J].GetComponent<RandomScene>().obstacle)
			{
			    Node nodeToAdd = new Node();


				Vector3 Start_AStar_pos = floorbase.data.rows[I].col[J].transform.GetChild(
					floorbase.data.rows[I].col[J].GetComponent<RandomScene>().g_FinalSelectionChild).transform.position;
				neighbors.Add(Start_AStar_pos);
			} 
          

         } 
    }
   
#endif
 
}
