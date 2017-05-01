using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarCarRace : MonoBehaviour 
{

    public PriorityQueue closedList, openList;
   
	  

	    public rootScene  floorbase=null;

	    void Awake()
	    {
		    floorbase  = GameObject.FindGameObjectWithTag("rootScene").GetComponent<rootScene>(); 
	    }
 


	public  void AssignNeighbour(int I, int J, ArrayList neighbors)
    {

         //int rows = this.GetComponent<Floorbase>().b_ROWS;
		 //int columns = this.GetComponent<Floorbase>().c_COLUMNS;


		  int rows =  floorbase.b_ROWS;
		  int columns = floorbase.c_COLUMNS;

		
        if (I != -1 && J != -1 && I < rows && J < columns)
        {

        /*
			//bool obstalce = this.GetComponent<Floorbase>().data.rows[I].col[J].GetComponent<RandomScene>().obstacle;
			bool obstalce = this.GetComponent<Floorbase>().data.rows[I].col[J].GetComponent<RandomScene>().obstacle;

			if(!obstalce)
			{
			    

				int index_of_empty = this.GetComponent<Floorbase>().data.rows[I].col[J].GetComponent<RandomScene>().g_FinalSelectionChild;
				Vector3 position_of_empty_object =this.GetComponent<Floorbase>().data.rows[I].col[J].transform.GetChild(index_of_empty ).transform.position;
				Node nodeToAdd = new Node(position_of_empty_object);
				nodeToAdd.tile=this.GetComponent<Floorbase>().data.rows[I].col[J];
				neighbors.Add(nodeToAdd);
			} 
          */

			//bool obstalce = this.GetComponent<Floorbase>().data.rows[I].col[J].GetComponent<RandomScene>().obstacle;
			bool obstalce = floorbase.data.rows[I].col[J].obstacle;

			if(!obstalce)
			{
			    

			//	int index_of_empty = floorbase.data.rows[I].col[J].GetComponent<RandomScene>().g_FinalSelectionChild;
				//Vector3 position_of_empty_object =floorbase.data.rows[I].col[J].transform.GetChild(index_of_empty ).transform.position;
				int pos = floorbase.data.rows[I].col[J].ChildIDSelected;
				Node nodeToAdd = new Node(floorbase.data.rows[I].col[J].transform.GetChild(pos).transform.position);
				nodeToAdd.tile=floorbase.data.rows[I].col[J];
				neighbors.Add(nodeToAdd);
			} 


         } 
    }

	public void getNeighbours(Node node, ArrayList neighbors)
	{
		int row = node.tile.I;
		int column = node.tile.J;
		bool obstacle =false;


		//Bottom
		obstacle = node.tile.obstacle;
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



	 

	}


	private float HeuristicEstimateCost(Node curNode, Node goalNode)
    {
        Vector3 vecCost = curNode.position - goalNode.position;
        return vecCost.magnitude;
    }

	private ArrayList CalculatePath(Node node)
    {
        ArrayList list = new ArrayList();
        while (node != null)
        {
#if ASTAR_TEST
            Debug.Log("node names inside list inside Calculate path"+node.tile.name);
#endif
            list.Add(node);
            node = node.parent;
 
        }
        list.Reverse();
        return list;
    }


	public   ArrayList FindPath(Node start, Node goal)
    {

 
		openList = new PriorityQueue();
        openList.Push(start);
        start.nodeTotalCost = 0.0f;
		start.estimatedCost = HeuristicEstimateCost(start, goal);

		closedList = new PriorityQueue();
        Node node = null;

#if ASTAR_TEST
       Debug.Log("Test B ");
#endif

int I =0;

		while (openList.Length != 0)
        {
			node = openList.First();

#if ASTAR_TEST
       Debug.Log("Test C "+I++);
#endif


            if (node.position == goal.position)
            {
                return CalculatePath(node);
            }


			 ArrayList neighbours = new ArrayList();
     	     getNeighbours(node,neighbours);


			 
#if !BLOCK_COMMEND
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
			
             
            
            closedList.Push(node);
            openList.Remove(node);

#endif

        }
		//If finished looping and cannot find the goal then return null
        if (node.position != goal.position)
        {
            Debug.LogError("Goal Not Found");
            return null;
        }


#if ASTAR_TEST_A
		//Calculate the path based on the final node
		goal.parent=start;
		return CalculatePath(goal);
 #else
        //Calculate the path based on the final node
        return CalculatePath(node);
#endif
		




    } 

	 

}
