using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class IndexingEnemies 
{
	public bool finished_AStar; 
	public int Start_E_I,Start_E_J;
	public int End_E_I,End_E_J;
	public float aStarFinish_Time;

	public List<AStarNode> foundPath;
}
