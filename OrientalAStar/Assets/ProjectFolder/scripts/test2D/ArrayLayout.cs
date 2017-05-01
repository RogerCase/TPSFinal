using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArrayLayout
{
    
    public static int COLUMNS;
	public static int ID;


     [System.Serializable]
	 public class rowData
	 {
	   
		public floor[] col;
			
	 }
	public rowData[] rows;

	public void  make_columns( )
    {       
	 
		for(int i =0;i<rows.Length;i++)
		{
			 
			rows[i]  = new  rowData();
			rows[i].col = new floor[COLUMNS];
		}
			
    }

	 
}
