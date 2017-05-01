using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera_M : MonoBehaviour 
{
	
	[SerializeField]
	float distanceAway;
	[SerializeField]
	float distanceUp;

	[SerializeField]
	float smooth;

	[SerializeField]
	public  Transform follow;

	[SerializeField]
	Vector3 target_position;

	public bool start_Follow = false;


	// Use this for initialization
	void Start () 
	{
		follow = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void LateUpdate()
	{
		 if (start_Follow) {
			
			target_position = follow.position + follow.up * distanceUp - follow.forward * distanceAway;


			transform.position = Vector3.Lerp (transform.position, target_position, Time.deltaTime * smooth);

			Debug.DrawRay (follow.position, follow.up * distanceUp, Color.red);
			Debug.DrawRay (follow.position, -follow.forward * distanceAway, Color.blue);
			Debug.DrawLine (follow.position, target_position, Color.magenta);


			transform.LookAt (follow);
	 	}
	}


}
