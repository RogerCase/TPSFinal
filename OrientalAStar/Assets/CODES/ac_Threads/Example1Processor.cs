using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
 
 
public class Example1Processor :MonoBehaviour
{

	private ThreadProcessor  Thread_maker;

	void Start()
	{
		Thread_maker = this.GetComponent<ThreadProcessor>();
	}
		
	
	 


	public void Process() 
 	{ 
		Thread_maker.Process(true);
		 
	}
}
