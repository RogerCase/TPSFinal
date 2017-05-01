using UnityEngine;
using System.Collections;

/**
 * main game manager
 * 
 * place from where we start
 * 
 * */

public class Game : MonoBehaviour
{
	
	// long, but simple example
	private Example1Manager m_Example1Manager;
	
 
	
	void Start () 
	{
		m_Example1Manager = new Example1Manager();
	 
		
		// activate example 1
		m_Example1Manager.GoGoGo();
		
 ;
	}
	
	void Update () 
	{
		//
	}
}
