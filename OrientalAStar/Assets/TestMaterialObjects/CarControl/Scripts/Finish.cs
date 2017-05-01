using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Finish : MonoBehaviour {

	void OnCollisionEnter(Collision collision) 
	{
       //Debug.Log(collision.gameObject.tag=="player");



		if(collision.gameObject.tag=="player"||collision.gameObject.tag=="enemy")
		{
			collision.gameObject.transform.GetComponent<Timer>().timer_end=true;

			if( collision.gameObject.tag=="enemy")
			{
				collision.gameObject.transform.GetComponent<AI>().stop_race=true;
			}



		}
        if(collision.gameObject.tag=="player")
        {
			Invoke("Launch_database", 2);
			 
			
        } 


       
    }

	void Launch_database()
	{
		SceneManager.LoadScene ("6_dataBaseScene");
	}


}
