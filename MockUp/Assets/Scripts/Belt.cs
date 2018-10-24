﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour {

	
	public float speed;

	private float currentScroll;

    
	private void Update()
    {

		//Texture Scroller (Main texture)
		currentScroll = currentScroll + Time.deltaTime * speed;
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, currentScroll);
    }

    
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.position += transform.right * speed * Time.deltaTime;
        rb.MovePosition(rb.position - transform.right * speed * Time.deltaTime);
    }

}

