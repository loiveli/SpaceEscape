using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour {

	
	public float speed;

	private float currentScroll;

    
	private void Update()
    {

		//Texture Scroller (Main texture) Scrolling at the same speed as the current scroll speed
		currentScroll = currentScroll + Time.deltaTime * speed;
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, currentScroll);
    }

    
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.position -= transform.forward * speed * Time.deltaTime;
        rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
    }

}


