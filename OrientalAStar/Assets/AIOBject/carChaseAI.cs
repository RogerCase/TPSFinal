using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carChaseAI : MonoBehaviour 
{

    public GameObject chaser_object = null;
    public Transform target=null;
    public float adjust = 2.0f;
    public GameObject  target_C;
     
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}

	public void Chaser_Object(GameObject obj,Material mat )
	{
		
		GameObject sample = Instantiate(chaser_object);
		target_C = sample;
		sample.GetComponent<AIchaser>().Car=obj.transform.parent.gameObject;
		//sample.transform.position =obj.transform.position ;
		//sample.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(sample.transform.position);
	    this.target = sample.transform;
		sample.GetComponent<AIchaser>().SetPosition(obj.transform.position);

		Debug.Log("Position set "+ obj.transform.position);
		Debug.Log("Position got "+ sample.transform.position);


	//	sample.transform.GetComponent<UnityEngine.AI.NavMeshAgent>().updatePosition = true;
		//sample.GetComponent<UnityEngine.AI.NavMeshAgent>().nextPosition=obj.transform.position;

	//	Physics.IgnoreCollision(obj.GetComponent<Collider>(), sample.GetComponent<UnityEngine.AI.NavMeshAgent>());
		//sample.GetComponent<AIchaser>().navmesh_enabled=true;
		sample.GetComponent<Renderer>().material=mat;


	}
}
