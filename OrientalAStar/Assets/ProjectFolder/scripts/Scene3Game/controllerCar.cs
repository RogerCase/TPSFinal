using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerCar : MonoBehaviour 
{

	[SerializeField]
   public bool player = true;

   [SerializeField]
    Vector3                                       a_centerOfGravity = Vector3.zero;

    [SerializeField]
	WheelCollider   []                                               c_wheels=null;

	[SerializeField]
	float                                                             d_maxTorque=0;

	[SerializeField]
	float                                                             e_maxSteer=0;

	[SerializeField]
	float                                                         f_handBrakeTorque=0;


	[SerializeField]
	GameObject   []                                                        g_tires=null;
	[SerializeField]
	float                                                             h_current_speed;
	[SerializeField]
	public float                                                                i_maxSpeed =0;
	[SerializeField]
	float                                                           j_maxReverseTorque;

	[SerializeField]
	bool  k_reversing =false;

	[SerializeField]
	int                                                                            k_gear;
     [SerializeField]
	float []                                                                  l_GearRatio;
	[SerializeField]
	float                                                                     m_Rot_speed; 
 

	void EngineSound()
	{
	   int i;

		for ( i = 0; i < l_GearRatio.Length; i++)
		{
			if (l_GearRatio[i] > h_current_speed)
			{
			  //break this value
			  break;
			}


		}


		float  minGearValue = 0.00f;
		float  maxGearValue = 0.00f;
		if (i == 0)
		{
		   minGearValue = 0;
		}
		else
		{
		   minGearValue = l_GearRatio[i-1];
		}
		maxGearValue = l_GearRatio[i];



		float pitch    = ((h_current_speed - minGearValue)/(maxGearValue - minGearValue)+0.8f);
		GetComponent<AudioSource>().pitch = pitch;

		k_gear = i;




	 
	}
	// Use this for initialization
	void Start ()
	{
		this.GetComponent<Rigidbody>().centerOfMass = a_centerOfGravity;

	}

	void  FixedUpdate ()
	 {
 
			 if(player)
			 {
				Drive();
				 EngineSound();
				AllignWheels();
				CalculateAndCapSpeed();
				}
				//GUIButtonControl();
	  }

	void Drive()
	{
					//the car will be 4 wheel drive or else it will be slow or feel a little sluggish
			//no matter how much you increase the max torque.
		//c_wheels[0].motorTorque = d_maxTorque * Input.GetAxis("Vertical");
		//c_wheels[1].motorTorque = d_maxTorque * Input.GetAxis("Vertical");
		c_wheels[2].motorTorque = d_maxTorque * Input.GetAxis("Vertical");
		c_wheels[3].motorTorque = d_maxTorque * Input.GetAxis("Vertical");

		c_wheels[0].steerAngle = e_maxSteer * Input.GetAxis("Horizontal")*m_Rot_speed*Time.deltaTime;
		c_wheels[1].steerAngle = e_maxSteer * Input.GetAxis("Horizontal")*m_Rot_speed*Time.deltaTime;


			if (Input.GetButton("Jump"))//pressing space triggers the car's handbrake
			{
				c_wheels[0].brakeTorque = f_handBrakeTorque*5;
				c_wheels[1].brakeTorque = f_handBrakeTorque*5;
				c_wheels[2].brakeTorque = f_handBrakeTorque*5;
				c_wheels[3].brakeTorque = f_handBrakeTorque*5;
			}
			else//letting go of space disables the handbrake
			{
				c_wheels[0].brakeTorque = 0;
				c_wheels[1].brakeTorque = 0;
				c_wheels[2].brakeTorque = 0;
				c_wheels[3].brakeTorque = 0;
			}
	}

	void AllignWheels()
	{
		//allign the wheel transforms to their wheelcollider's position
         for (int i = 0; i < 4; i++)
            {
                Quaternion quat;
                Vector3 position;
			    c_wheels[i].GetWorldPose(out position,out quat);
                g_tires[i].transform.position = position;
                g_tires[i].transform.rotation = quat;
                
            }

	}

	void CalculateAndCapSpeed()
	{
	    

		h_current_speed = GetComponent<Rigidbody>().velocity.magnitude * 2.23693629f;//convert currentspeed into MPH


		float speed = GetComponent<Rigidbody>().velocity.magnitude;
		Vector3	  LocalCurrentSpeed;
		LocalCurrentSpeed = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);//convert velocity of the rigid body from world space to local space

		speed *= 2.23693629f;


		if (h_current_speed > i_maxSpeed || (LocalCurrentSpeed.z*2.23693629f) < -j_maxReverseTorque)
		{
			//we calculate the float value of the Rigid body's magnitude in local space. 
			//If it's speed going backwards is bigger than the reverse speed var, 
			//or if it's reached it's top drive speed, we simply multiply
			//the x,y, and z velocities by .99, so it stays just below the desired speed.
			//why the hell didn't I think of that before?
			GetComponent<Rigidbody>().velocity *= 0.99f; 
		}


		if (LocalCurrentSpeed.z<-0.1f)//in local space, if the car is travelling in the direction of the -z axis, (or in reverse), reversing will be true
		{
			k_reversing = true;
		}
		else
		{
			k_reversing = false;
		}	   
	



	 
	}
	// Update is called once per frame
	void Update () 
	{
		
	}

	void  OnGUI()
	{
 
		//show the GUI for the speed and gear we are on.
		//GUI.Box(Rect(10,10,70,30),"MPH: " + Mathf.Round(GetComponent<Rigidbody>().velocity.magnitude * 2.23693629f));
		/*
		if (!k_reversing)
		GUI.Box(Rect(10,70,70,30),"Gear: " + gear);
		if (k_reversing)//if the car is going backwards display the gear as R
		GUI.Box(Rect(10,70,70,30),"Gear: R"); */

		 

		if(player)
			 {
         
		GUI.Box(new Rect(10,10,70,30), new GUIContent("MPH: " + Mathf.Round(GetComponent<Rigidbody>().velocity.magnitude * 2.23693629f)) );

		if (!k_reversing)
		 
		   GUI.Box(new Rect(10,70,70,30), new GUIContent( "Gear: " + k_gear)) ;

		if (k_reversing)//if the car is going backwards display the gear as R
		 
		   GUI.Box(new Rect(10,70,70,30), new GUIContent("Gear: R")) ;
		   }
	}
}
