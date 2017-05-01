using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerLogic_T : MonoBehaviour
{


	// Inspector serialized
	[SerializeField]
	private Animator animator;
	//public Transform follow;


	//[SerializeField]
	//private float directionDampTime = 0.25f;
	[SerializeField]
	private float speed = 0.0f;
	[SerializeField]
	private float v = 0.0f;
	[SerializeField]
	private float h = 0.0f;
	public float ROTATION_SPEED =10.0f;
	public float FORWARD_SPEED =10.0f;

	public bool fire_flag=true;

	[SerializeField]
	bool player = false;

	// Use this for initialization
	void Start ()
	{

		animator = GetComponent<Animator>();

		/*
		if(animator.layerCount >= 2)
		{
			animator.SetLayerWeight(1, 1);
		}	*/	

	}

	// Update is called once per frame
	void Update () 
	{

		if (player) {
			
			if (animator) {

				if (Input.GetKeyDown (KeyCode.F) && fire_flag) {   

					 
					animator.SetBool ("fire", true);
					fire_flag = false;

				} else {
					// Pull values from controller/keyboard
					h = Input.GetAxis ("Horizontal");
					v = Input.GetAxis ("Vertical");	

					speed = new Vector2 (h, v).magnitude;

					animator.SetFloat ("speed", speed);
					//animator.SetFloat("direction", h, directionDampTime, Time.deltaTime);

					transform.Rotate (ROTATION_SPEED * this.transform.transform.up * Time.deltaTime * h);
					transform.position += (FORWARD_SPEED * this.transform.forward * Time.deltaTime * v);
				}




			}
		}
	}
}
