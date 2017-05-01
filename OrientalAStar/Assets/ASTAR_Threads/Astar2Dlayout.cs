using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Astar2Dlayout  
{

	public static int COLUMNS;
	public static int ID;


	[System.Serializable]
	public class rowData
	{

		public AStarNode[] col;

	}
	public rowData[] rows;

	public void  make_columns( )
	{       

		for(int i =0;i<rows.Length;i++)
		{

			rows[i]  = new  rowData();
			rows[i].col = new AStarNode[COLUMNS];
			for(int j =0;j<rows[i].col.Length;j++)
			{
				rows [i].col [j] = new AStarNode ();
			}
		}

	}

}
