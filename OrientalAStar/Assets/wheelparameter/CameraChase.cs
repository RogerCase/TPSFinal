using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChase : MonoBehaviour
 {
	public GameObject m_Car;
	public GameObject m_enemy;
	public bool rotate=false; 
	public Vector3 rotationVector;
	public float zoomRacio = 0.5f;
	public float DefaultFOV = 60f;
	public float rotationDamping;
	public float heightDamping;
	public float distance;


	public float height;

	controllerCar enemy;

	public bool chase_camera = false;

	void Start()
	{
		m_Car = GameObject.FindWithTag ("player");
		m_enemy = GameObject.FindWithTag ("enemy");

		m_enemy.transform.GetComponent<AI> ().run_Ai = true;

		//m_Car.transform.gameObject.transform.GetChild (3).gameObject.SetActive (true);
		//m_Car.transform.gameObject.transform.GetChild (4).gameObject.SetActive (true);
		//m_Car.transform.GetComponent<AudioSource> ().enabled = true;
		chase_camera = true;

			
	/*		
			.GetComponent<AI> ().run_Ai = true;
		 m_Car = .transform.GetComponent<controllerCar> ();

		m_Car.transform.gameObject.transform.GetChild (3).gameObject.SetActive (true);
		m_Car.transform.gameObject.transform.GetChild (4).gameObject.SetActive (true);
		m_Car.transform.GetComponent<AudioSource> ().enabled = true;
		chase_camera = true;*/

		Debug.Log ("Hai");

	}

 
		void LateUpdate () 
		{

		if(chase_camera)
		{
		    float wantedAngel = rotationVector.y;
		    float wantedHeight = m_Car.transform.position.y + height;
			var myAngel = transform.eulerAngles.y;
			var myHeight = transform.position.y;
			myAngel = Mathf.LerpAngle(myAngel,wantedAngel,rotationDamping*Time.deltaTime);
			myHeight = Mathf.Lerp(myHeight,wantedHeight,heightDamping*Time.deltaTime);


		     Quaternion currentRotation = Quaternion.Euler(0,myAngel,0);
		    transform.position = m_Car.transform.position;
			transform.position -= currentRotation*Vector3.forward*distance;
		    transform.position = new Vector3(transform.position.x,myHeight,transform.position.z);
			//transform.position.y = myHeight;
		     transform.LookAt(m_Car.transform); 

		   }
		      
		}

	void FixedUpdate ()
	{

		if(chase_camera)
		{


			Vector3 localVilocity = m_Car.transform.InverseTransformDirection(m_Car.GetComponent<Rigidbody>().velocity);

			if (localVilocity.z<-0.5 && rotate)
			{
				rotationVector.y = m_Car.transform.eulerAngles.y + 180;
			}
			else 
			{
				rotationVector.y = m_Car.transform.eulerAngles.y;
			}

			float acc = m_Car.GetComponent<Rigidbody>().velocity.magnitude;
			GetComponent<Camera>().fieldOfView = DefaultFOV + acc*zoomRacio;


		}
	 
	}


}
