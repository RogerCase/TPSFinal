using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIchaser : MonoBehaviour 
{
	public GameObject  Car;
     public bool navmesh_enabled = false;
    
	void Start () 
	{

	   

	 	//this.transform.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
		//this.transform.GetComponent<UnityEngine.AI.NavMeshAgent>().updatePosition = false;

		/*public void SetPosition(Vector3 position)
 {
     _navMeshAgent.enabled = false;
 
     transform.position = position;
 
     _navMeshAgent.enabled = true;
 }

	*/
	 	 // Update the transform position explicitly in the OnAnimatorMove callback
		//this.transform.GetComponent<UnityEngine.AI.NavMeshAgent>().updatePosition = false;
	}

	void OnCollisionEnter(Collision collision) 
	{
         // Debug.Log ("xxx " + collision.other.name);

	 


   }

	public void SetPosition(Vector3 position)
    {
     
		      this.transform.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
		 
		     transform.position = position;
		 
		this.transform.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true; 
   }

   void Update()
   {
      if(navmesh_enabled)
					enable_nav();
   }

   public void enable_nav()
   {
		this.transform.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
   }



}
