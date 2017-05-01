using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour 
{
    public bool timer_end = false;
	public bool start_timer = false;
    public int Player_Index =0;
    
    public float start=0.0f;

	public GameObject Camera = null;
    
    
     
	// Use this for initialization
	void Start ()
	{
	
	

			
	}

	public void SetIndex_properties()
	{
		start=0.0f;
		if(Player_Index==0)

			Game_Controller.Instance.Timer_Player= 0;

		if (Player_Index == 1)
		{

			Game_Controller.Instance.Timer_A = 0;

			Camera.gameObject.SetActive (false);
		}

		start_timer = true;


		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		if(!timer_end && start_timer)
		{
			 if(Player_Index==0)
				Game_Controller.Instance.Timer_Player+= Time.deltaTime;
					
				if(Player_Index==1)
				Game_Controller.Instance.Timer_A+= Time.deltaTime;
				//if(Player_Index==2)
				//Game_Controller.Instance.Timer_B+= Time.deltaTime;
		}
	} 
}
