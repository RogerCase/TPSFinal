using UnityEngine;
using System.Collections;

public class shoot : MonoBehaviour 
{
  [SerializeField]
  float last_fire_time=0;
  
  [SerializeField]
  Transform GUN_TIP;
  
	[SerializeField]
	float TimeInterval = .5f;
	Animator animator;
 
	[SerializeField]
	float current_fire_time=0;
	
	[SerializeField]
	float interval_fire_time=0;
	[SerializeField]
	GameObject bullet;

	[SerializeField]
	bool repeatFire = false;

	[SerializeField]
	float Bullet_adjust_forward=1.0f;
	[SerializeField]
	float Bullet_adjust_upward=1.0f;
	void Awake () {
		 
		animator = GetComponent<Animator> ();

		//if(repeatFire)	

		//StartCoroutine( RepeatingFunction() );
	}

	IEnumerator RepeatingFunction () 
	{
		while(true) 
		{
			Fire_Bullet();
			//execute code here.
			yield return new WaitForSeconds(TimeInterval);
		}
	}
	void Update()
	{
		 
		  
	}	
	
	
 public void Fire_Bullet()
 {
		current_fire_time=Time.time;
		
		//if(current_fire_time-last_fire_time>interval_fire_time)
		//{
		//	last_fire_time= current_fire_time;
			
			
			/*
			GameObject bulletI= 	Instantiate(bullet,
			transform.position+transform.forward*Bullet_adjust_forward+
			            transform.up*Bullet_adjust_upward,
			Quaternion.identity) as GameObject;*/
			
				GameObject bulletI= 	Instantiate(bullet,
			                                 GUN_TIP.position+transform.forward*Bullet_adjust_forward+
				                                 transform.up*Bullet_adjust_upward,
				                                 this.transform.rotation) as GameObject;
			
			
			 bulletI.GetComponent<MoveBullet>().bullet_direction= transform.forward;
			 
		//}
   
 }
	public void ShootWithDelay(float time_delay)
	{
		Invoke("Fire_Bullet", time_delay);
	 
	}

	  
 
 
}
