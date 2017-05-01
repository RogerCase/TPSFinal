using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Example1Manager :  MonoBehaviour
{
	


	private Example1Processor Processor;
	


	void Start()
	{
		Processor = this.GetComponent<Example1Processor>();
	}

	
	 
	

	public void GoGoGo() 
	{
		 
		Processor.Process();
	}
	

	 

}
