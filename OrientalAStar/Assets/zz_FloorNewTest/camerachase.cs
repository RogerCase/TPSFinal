﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerachase : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.rotation = Quaternion.Euler(90, 0, 0);
		
	}
}
