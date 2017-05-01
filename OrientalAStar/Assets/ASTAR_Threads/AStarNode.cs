using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AStarNode  
{




	public Vector3                                              Position;
	public int                                                      I=100,J=100;

	//Node's costs for pathfinding purposes
	public float                                           hCost;
	public float                                           gCost;

	public int Color_Flag=0;

	//public Transform Tile_transform;

	public float fCost
	{
		get //the fCost is the gCost+hCost so we can get it directly this way
		{
			return gCost + hCost;
		}
	}

	public AStarNode parentNode;
	public bool obstacle = false;


	public void setAStarNode(int I,int J,Vector3 Position,bool obstacle,int Color_Flag)
	{
		this.I = I;
		this.J = J;
		this.Position = Position;
		this.obstacle = obstacle;
		this.Color_Flag = Color_Flag;
	}
	public void setColorFlag(int c)
	{
		this.Color_Flag = c;
	}

	 
	 
}
