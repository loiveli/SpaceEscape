﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 15f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}