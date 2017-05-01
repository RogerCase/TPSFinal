using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIforTPS : MonoBehaviour 
{
	[SerializeField]
	bool                                              ag_test_gizmo_waypoint_sphere = false;
	public int playerIndex = 1;
	[SerializeField]
	int                                                              ad_currentPathIndex=1;	
	[SerializeField]
	float ag_distanceFromWayPoint= 80.0f;

	public Transform target;

	private UnityEngine.AI.NavMeshAgent agent;

	[SerializeField]
	private Animator animator;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.speed = Random.Range(4.0f, 10.0f);
		agent.SetDestination(target.position);
	}
	
	// Update is called once per frame
	void Update () {



		animator.SetFloat ("speed",  agent.velocity.magnitude);

		//GetSteer ();
		
	}

	void GetSteer()
	{

		/*
		if(ad_currentPathIndex == this.GetComponent<PathFinding>()._wayPoints.Length)
		  return;
		Vector3 referenceCurrentPathPoint = new Vector3(
			
			this.GetComponent<PathFinding>()._wayPoints[ad_currentPathIndex].x,
			this.transform.GetChild(0).transform.position.y,
			this.GetComponent<PathFinding>()._wayPoints[ad_currentPathIndex].z
			
			);*/

		if(ad_currentPathIndex == Game_Controller.Instance.indexingEnemies[playerIndex-1].foundPath.Count)
			return;
		Vector3 referenceCurrentPathPoint = new Vector3(

			Game_Controller.Instance.indexingEnemies[playerIndex-1].foundPath[ad_currentPathIndex].Position.x,
			this.transform.GetChild(0).transform.position.y,
			Game_Controller.Instance.indexingEnemies[playerIndex-1].foundPath[ad_currentPathIndex].Position.z

		);


		 

		float Check_Val = Vector3.Distance(referenceCurrentPathPoint,this.transform.GetChild(0).transform.position);


		if(Check_Val<= ag_distanceFromWayPoint)
		{
			ad_currentPathIndex++;
			//Debug.Log("Way Point Changed"+ad_currentPathIndex);
		}


	}

	void OnDrawGizmos() 
	{

		//if(run_Ai && ad_currentPathIndex<this.GetComponent<PathFinding>()._wayPoints.Length)
		if(  Game_Controller.Instance.indexingEnemies[playerIndex-1].foundPath.Count>0)
		{
			if(!ag_test_gizmo_waypoint_sphere)
				return;

			/*
			Vector3 referenceCurrentPathPoint = new Vector3(
				
				this.GetComponent<PathFinding>()._wayPoints[ad_currentPathIndex].x,
				this.transform.GetChild(0).transform.position.y,
				this.GetComponent<PathFinding>()._wayPoints[ad_currentPathIndex].z
				
				);*/
			Vector3 referenceCurrentPathPoint=Vector3.zero;
			for (int i = 0; i < Game_Controller.Instance.indexingEnemies [playerIndex - 1].foundPath.Count; i++) {

				  referenceCurrentPathPoint = new Vector3 (

					                                   Game_Controller.Instance.indexingEnemies [playerIndex - 1].foundPath [i].Position.x,
					                                   this.transform.GetChild (0).transform.position.y,
					                                   Game_Controller.Instance.indexingEnemies [playerIndex - 1].foundPath [i].Position.z

				                                   );
				if(ad_currentPathIndex==i)
					Gizmos.color = Color.red;
				else
				Gizmos.color = Color.blue;
				Gizmos.DrawWireSphere(referenceCurrentPathPoint,ag_distanceFromWayPoint);
			}



		}

	}
}
