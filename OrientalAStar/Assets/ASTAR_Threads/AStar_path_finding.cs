using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar_path_finding 
{

	 

	public void ApplyAStar(int I)
	{

		/*
		pathArray = new ArrayList();
		Start_AStar_pos = Start_AStar.transform.GetChild(0).transform.position;
		End_AStar_pos   = End_AStar.transform.GetChild(2).transform.position;

		//Assign StartNode and Goal Node
		startNode = new Node(Start_AStar_pos);
		startNode.tile = Start_AStar;

		goalNode  = new Node(End_AStar_pos );
		goalNode.tile = End_AStar;

		#if ASTAR_TEST

		Debug.Log("Start Position"+Start_AStar_pos);
		Debug.Log("End Position"+End_AStar_pos);


		#endif


		//pathArray = floorbase.GetComponent<AstarCarRace>().FindPath(startNode, goalNode);
		pathArray = this.GetComponent<AstarCarRace>().FindPath(startNode, goalNode);


		if(pathArray.Count ==0)
		{

			#if ASTAR_TEST

			Debug.Log("Size is zero");
			#endif
			return;
		}



		#if ASTAR_TEST
		Debug.Log("#######################");
		Debug.Log("Way point Size ::" + pathArray.Count);
		Debug.Log("@@@@@@@@@@@@@@@@@@@@@@@");
		#endif
		_wayPoints = new Vector3[pathArray.Count];
		PathTiles = new floor[ pathArray.Count];
		for(int i=0;i<pathArray.Count;i++)
		{
			Node node = (Node)pathArray[i];
			_wayPoints[i]=node.position;
			PathTiles[i]= node.tile;
			int tile_Child = PathTiles[i].ChildIDSelected;
			PathTiles[i].transform.GetChild(tile_Child).GetComponent<Renderer>().material=path_material;

			#if ASTAR_TEST
			Debug.Log(" TILE  "+PathTiles[i]);
			Debug.Log(" Position Empty "+_wayPoints[i]);
			Debug.Log("@@@@@@@@@@@@@@@@@@@@@@@" );
			#endif
			//node.tile.gameObject.GetComponent<Renderer>().material=path_material;
		}

		Start_AStar.GetComponent<Renderer>().material= startMaterial;
		End_AStar.GetComponent<Renderer>().material= endMaterial;  


		this.GetComponent<AI>().run_Ai=true;

		Game_Controller.Instance.playerA_Star = Game_Controller.Instance.playerB_Star = true;
*/


	}

}
