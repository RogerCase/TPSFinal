using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour 
{

   [SerializeField]
   float                                                              aa_MAX_STEER =15.0f;

	
	[SerializeField]
	WheelCollider                                                         ab_WheelFL=null;
	[SerializeField]
	WheelCollider                                                         ac_WheelFR=null;
	[SerializeField]
	int                                                              ad_currentPathIndex=0;	
	
	[SerializeField]
	float                                                                 ae_newSteer=0.0f;	

	[SerializeField]
	Vector3                                                    af_centerOfmass=Vector3.zero;	

	[SerializeField]
	float                                                    ag_distanceFromWayPoint =20.0f;	
	[SerializeField]
	float                                                 af_currentDistanceFromWayPoint =0;
	
	[SerializeField]
	bool                                              ag_test_gizmo_waypoint_sphere = false;
	
	 
	public WheelCollider                                                         ah_WheelRL=null;
	 
	public WheelCollider                                                         ai_WheelRR=null;
	
	
 
	public float                                                              aj_MAX_TORQUE =50.0f;
	[SerializeField]
	 float                                                              aja_CURRENT_TORQUE =0.0f; 
	[SerializeField]
	float                                                              ajb_BREAK_TORQUE =0.0f; 
	
	public float                                                            ak_CURRENT_SPEED =0.0f;
	[SerializeField]
	float                                                              al_TOP_SPEED =150.0f;
	[SerializeField]
	float                                                      am_DEACCELERATED_SPEED =10.0f;
	
	   
	public bool                                                            aq_IsBreaking=false;	
	
	
	[SerializeField]
	float                                                                ar_SensorLength =5.0f;
	[SerializeField]
	float                                                                ar_breakSensorLength =10.0f;
	[SerializeField]
	float                                                             as_Front_sensor_Star=0.0f;
 
	
	[SerializeField]
	Transform                                                                  au_Sensor_Middle=null;


	[SerializeField]
	Transform                                                                    av_Sensor_Right=null;
	
	[SerializeField]
	Transform                                                                     aw_Sensor_Left=null;
	
	[SerializeField]
	Transform                                                                          ax_Center=null;
	
	[SerializeField]
	float                                                                   ay_frontSensorAngle=30.0f;
	
	[SerializeField]
	float                                                                   az_SideWaySensorLength=5.0f;
	
	[SerializeField]
	float                                                                        SENSITIVITY_FACTOR =0.5f;
	
	
 
	[SerializeField]
	int                                                                   ba_Flag=0;
	
	
	[SerializeField]
	float                                                                   bb_AvoidSensitivity=0.0f;
	
	[SerializeField]
	float                                                                   bc_direction=0.0f;
	[SerializeField]
	float                                                                   bd_avoidSpeed=10.0f;
	
	[SerializeField]
	bool                                                                       be_reversing=false;
	[SerializeField]
	float                                                                   bf_aRCounter=0.0f;
	[SerializeField]
	float                                                                   bg_Waitforreverse=2.0f;
	
	[SerializeField]
	float                                                                   bg_reverFor=1.5f;
	
	[SerializeField]
	float                                                                   bc_REVERSELIMITVELOCITY=2.0f;
	
	[SerializeField]
	bool BREAK_SENSOR = false;
	
	
	[SerializeField]
	bool LEFT_SENSOR = false;
	
	
	[SerializeField]
	bool RIGHT_SENSOR = false;
	
	
	[SerializeField]
	bool SIDE_SENSOR = false;
	 
	public bool FINALREVERSE = false;

	public bool run_Ai = false;

	public bool stop_race = false;

	public int playerIndex = 1;


	void Start () 
	{
	 
		GetComponent<Rigidbody>().centerOfMass= af_centerOfmass;
		
		 
		
	}
	
	
	
	void Update()
	{
		if(!stop_race)
		{
			if(run_Ai)
			{

				 
			 
				    if(ba_Flag==0)
				    GetSteer();


				    Move_car();
				 
					 AdjustCar_Script();
					  
			}
		}
		else
		{
			ai_WheelRR.brakeTorque= ah_WheelRL.brakeTorque = am_DEACCELERATED_SPEED*10;
			 
		}
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
		

		Vector3 localSteerVector = this.transform.GetChild(0).transform.InverseTransformPoint(referenceCurrentPathPoint);


		//It decides left or right movement
		ae_newSteer = aa_MAX_STEER*(localSteerVector.x/localSteerVector.magnitude);
		ab_WheelFL.steerAngle=ac_WheelFR.steerAngle=ae_newSteer;
		af_currentDistanceFromWayPoint = localSteerVector.magnitude;
		// Debug.Log("######");
		// Debug.Log("Steer "+ae_newSteer);
		// Debug.Log("Mag to way point "+af_currentDistanceFromWayPoint);
		// Debug.Log("Euclidian"+Vector3.Distance(referenceCurrentPathPoint,this.transform.GetChild(0).transform.position));
		 Debug.Log("Index "+ad_currentPathIndex);

		float Check_Val = Vector3.Distance(referenceCurrentPathPoint,this.transform.GetChild(0).transform.position);
		

		if(Check_Val<= ag_distanceFromWayPoint)
		{
			ad_currentPathIndex++;
			//Debug.Log("Way Point Changed"+ad_currentPathIndex);
		}
		
		
	}
	
	void Move_car()
	{   //current speed kilometer/hour
		ak_CURRENT_SPEED = 2.0f*( 22.0f /7.0f) * ah_WheelRL.radius * ah_WheelRL.rpm * (60.0f / 1000.0f);
		ak_CURRENT_SPEED = Mathf.Round(ak_CURRENT_SPEED);
		if(ak_CURRENT_SPEED<=al_TOP_SPEED )
		{   
		    if(!be_reversing)
		    {
			  ai_WheelRR.motorTorque= ah_WheelRL.motorTorque = aj_MAX_TORQUE;
			}
			else
			{
				ai_WheelRR.motorTorque= ah_WheelRL.motorTorque = -aj_MAX_TORQUE*4.0f;
			}  
			ai_WheelRR.brakeTorque= ah_WheelRL.brakeTorque = 0.0f;
		}
		else  
		{
			ai_WheelRR.motorTorque= ah_WheelRL.motorTorque = 0.0f;
			ai_WheelRR.brakeTorque= ah_WheelRL.brakeTorque = am_DEACCELERATED_SPEED;
		}
		
		aja_CURRENT_TORQUE= ac_WheelFR.motorTorque;
		ajb_BREAK_TORQUE=ab_WheelFL.brakeTorque;
	  
	}
	
	
	
 
	void OnDrawGizmos() 
	{

		//if(run_Ai && ad_currentPathIndex<this.GetComponent<PathFinding>()._wayPoints.Length)
		if(run_Ai && Game_Controller.Instance.indexingEnemies[playerIndex-1].foundPath.Count>0)
		{
			if(!ag_test_gizmo_waypoint_sphere)
			return;
	
			/*
			Vector3 referenceCurrentPathPoint = new Vector3(
				
				this.GetComponent<PathFinding>()._wayPoints[ad_currentPathIndex].x,
				this.transform.GetChild(0).transform.position.y,
				this.GetComponent<PathFinding>()._wayPoints[ad_currentPathIndex].z
				
				);*/

			Vector3 referenceCurrentPathPoint = new Vector3(

				Game_Controller.Instance.indexingEnemies[playerIndex-1].foundPath[ad_currentPathIndex].Position.x,
				this.transform.GetChild(0).transform.position.y,
				Game_Controller.Instance.indexingEnemies[playerIndex-1].foundPath[ad_currentPathIndex].Position.z

			);
			
			
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(referenceCurrentPathPoint,ag_distanceFromWayPoint);
		}
		 		
	}
 
	
	
	 
	
	
	
	void AdjustCar_Script()
	{
		RaycastHit hit;
		
		ba_Flag =0;
		bb_AvoidSensitivity=0.0f;
 	/*	
		Vector3 rightAngle = Quaternion.AngleAxis (ay_frontSensorAngle, au_Sensor_Middle.forward) * au_Sensor_Middle.right;
		Vector3 leftAngle = Quaternion.AngleAxis (-ay_frontSensorAngle, au_Sensor_Middle.forward) * au_Sensor_Middle.right;
		*/
		Vector3 rightAngle = Quaternion.AngleAxis (ay_frontSensorAngle, au_Sensor_Middle.up) * au_Sensor_Middle.right;
		Vector3 leftAngle = Quaternion.AngleAxis (-ay_frontSensorAngle, au_Sensor_Middle.up) * au_Sensor_Middle.right;

		if(BREAK_SENSOR && !be_reversing)
		{
	 
				//Breaking Sensor
				
			if (Physics.Raycast (au_Sensor_Middle.position, au_Sensor_Middle.right,out hit,ar_breakSensorLength)) 
				{ 
					if(hit.transform.tag!= "Tiles" )
					{
				       ba_Flag++;
					   ai_WheelRR.brakeTorque= ah_WheelRL.brakeTorque = am_DEACCELERATED_SPEED;
					    Debug.DrawLine(au_Sensor_Middle.position,hit.point,Color.red);
					   Debug.Log("Breaking Sensor Acted "+am_DEACCELERATED_SPEED+"::"+hit.transform.tag);
					   
					}
				}
				else
				{
					ai_WheelRR.brakeTorque= ah_WheelRL.brakeTorque = 0.0f;
					 Debug.Log("Breaking Sensor Just Released ");
				}
	    }
	    
	    
	    if(RIGHT_SENSOR)
	    {
			 
			//Straight Right 
			if (Physics.Raycast (av_Sensor_Right.position, av_Sensor_Right.right,out hit,ar_SensorLength)) 
			{ 
				if(hit.transform.tag!= "Tiles" )
				{
				    ba_Flag++;
				 	bb_AvoidSensitivity-=SENSITIVITY_FACTOR;
					 Debug.DrawLine(av_Sensor_Right.position,hit.point,Color.green);				
					Debug.Log("Straight Right Inside"+bb_AvoidSensitivity);
			    }
			   
			 		
			} 
			 else if (Physics.Raycast (av_Sensor_Right.position, rightAngle,out hit,ar_SensorLength)) 
			//if (Physics.Raycast (av_Sensor_Right.position, rightAngle,out hit,ar_SensorLength)) 
			{   //Angled right
				if(hit.transform.tag!= "Tiles" )
				{
					
					bb_AvoidSensitivity-=(SENSITIVITY_FACTOR/1.1f);
					Debug.Log("Angled right Inside"+bb_AvoidSensitivity);
					ba_Flag++;
					Debug.DrawLine(av_Sensor_Right.position,hit.point,Color.white);
				}	
			 		
			}
	 		
       }
		
	  if(LEFT_SENSOR)
	  {	
			 
				//Straight Left 
				if (Physics.Raycast (aw_Sensor_Left.position, aw_Sensor_Left.right,out hit,ar_SensorLength)) 
				{ 
				   if(hit.transform.tag!= "Tiles" )
					{
						ba_Flag++;
						bb_AvoidSensitivity+=SENSITIVITY_FACTOR;
					 	Debug.DrawLine(aw_Sensor_Left.position,hit.point,Color.blue);
					    Debug.Log("Straight LeftInside");
					}
					
					 			
				}  

			 	else if (Physics.Raycast (aw_Sensor_Left.position, leftAngle,out hit,ar_SensorLength)) 
			  // if (Physics.Raycast (aw_Sensor_Left.position, leftAngle,out hit,ar_SensorLength))
				{ 
				    if(hit.transform.tag!= "Tiles" )
					{
						
						bb_AvoidSensitivity+=(SENSITIVITY_FACTOR/1.1f);
						ba_Flag++;
					     Debug.DrawLine(aw_Sensor_Left.position,hit.point,Color.green);
					     Debug.Log("Angled Left Inside"+bb_AvoidSensitivity);
					}
				 			
				}
		}
		
 
	 		
		
		if(SIDE_SENSOR)
		{ 
			//Center side way right
			if (Physics.Raycast (ax_Center.position, -ax_Center.forward,out hit,az_SideWaySensorLength)) 
			{ 
				if(hit.transform.tag!= "Tiles" )
				{
					
					ba_Flag++;
					bb_AvoidSensitivity-=(SENSITIVITY_FACTOR/1.1f);
					 Debug.DrawLine(ax_Center.position,hit.point,Color.yellow);
					  Debug.Log("Side Right Inside"+bb_AvoidSensitivity);
				}	
			   
			  			
			}
 	
		
		
			//Center side way left
			if (Physics.Raycast (ax_Center.position, ax_Center.forward,out hit,az_SideWaySensorLength)) 
			{ 
				if(hit.transform.tag!= "Tiles" )
				{
					
					ba_Flag++;
					bb_AvoidSensitivity+=(SENSITIVITY_FACTOR/1.1f);
					 Debug.DrawLine(ax_Center.position,hit.point,Color.magenta);
					Debug.Log("Side Left  Inside"+bb_AvoidSensitivity);
				}
		 			
			}
		
		}
		
		if(BREAK_SENSOR)
		{
				//Middle Sensor // No way to move all sensors can celled out
				if(bb_AvoidSensitivity==0)
				{
					if (Physics.Raycast (au_Sensor_Middle.position, au_Sensor_Middle.right,out hit,ar_SensorLength)) 
					{ 
					    if(hit.transform.tag!= "Tiles" )
						{
							
							
						 Debug.DrawLine(hit.point,hit.point+hit.normal*10.0f,Color.magenta);
//						Debug.Log(hit.normal);
						

				 			   
						//Vector3 crossFN = Vector3.Cross(hit.normal, -this.transform.forward); 
						Vector3 crossFN = Vector3.Cross(hit.normal, this.transform.GetChild(0).transform.up);
						float direction = Mathf.Sign(Vector3.Dot( crossFN, this.transform.GetChild(0).transform.forward));
						//float direction = Mathf.Sign(Vector3.Dot( crossFN, this.transform.up));
						
						if(direction<0)
						{
							bb_AvoidSensitivity=-SENSITIVITY_FACTOR;
						}
						else
						{
							bb_AvoidSensitivity=SENSITIVITY_FACTOR;
						}  

						Debug.Log("Break Balance"+bb_AvoidSensitivity);
							
						
						}  
				 
					
					}
				}
		
		
		}
		if(this.GetComponent<Rigidbody>().velocity.magnitude<bc_REVERSELIMITVELOCITY && !be_reversing && !FINALREVERSE)
		{
		   bf_aRCounter+=Time.deltaTime;
		   if(bf_aRCounter>=bg_Waitforreverse)
		   {
				bf_aRCounter=0.0f;
				be_reversing=true;
		   }
		}
		else if(!be_reversing)
		{
			bf_aRCounter =0;
		}
     
        if(be_reversing)
        {
			bb_AvoidSensitivity*=-1.0f;
			bf_aRCounter+=Time.deltaTime;
			
			if(bf_aRCounter>=bg_reverFor)
			{
				bf_aRCounter=0.0f;
				be_reversing=false;
			   
			}
		 
        }
		if(ba_Flag!=0)
		{
			
			Avoid_Steer(bb_AvoidSensitivity);
		}
		
		 
	}
	
	void Avoid_Steer(float sensitivity)
	{
	  
		ab_WheelFL.steerAngle=ac_WheelFR.steerAngle= bd_avoidSpeed*sensitivity;
		//Debug.Log(ab_WheelFL.steerAngle);
	}
}
