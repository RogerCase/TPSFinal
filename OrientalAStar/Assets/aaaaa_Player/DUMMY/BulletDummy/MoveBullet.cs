using UnityEngine;
using System.Collections;

public class MoveBullet : MonoBehaviour
{

     [SerializeField]
     float VELOCITY =10.0f;
     
	[SerializeField]
	float DESTORY_TIME =1.0f;
	Rigidbody rb;
	 public Vector3 bullet_direction;

	
	 
	void Start ()	
 	{   
		 
		rb= this.GetComponent<Rigidbody>();

		Destroy(gameObject,DESTORY_TIME);
	}
	
	void FixedUpdate()
	{
		rb.velocity = bullet_direction*VELOCITY*Time.deltaTime ;
	    
	}
	
	
	 
	
}
