using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorTest : MonoBehaviour 
{   
	
	public int I,J;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	#if UNDEFINE
	public void print_node()
	{

		Debug.Log ("I::" + I + "J::" + J);
		Debug.Log ("Pos"+Game_Controller.Instance.scene_Vec[I,J]);
		Debug.Log ("obstacle"+Game_Controller.Instance.Obstacle[I,J]);
		Debug.Log ("#########");


	}
	#endif 
}
