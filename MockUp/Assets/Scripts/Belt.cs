using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour {

	
	public float speed;
    [SerializeField]
    float mattSpeedSlower = 75f;
	private float currentScroll;

    
	private void Update()
    {

		//Texture Scroller (Main texture) Scrolling at the same speed as the current scroll speed
		currentScroll = currentScroll + Time.deltaTime * speed/ mattSpeedSlower;
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(-currentScroll,0);

        if (Input.GetKey(KeyCode.W))
            {
                speed += Time.deltaTime;
            }
        if (Input.GetKey(KeyCode.S)){
            speed -= Time.deltaTime*2;
        }
        
    }

    
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.position += transform.right * speed * Time.deltaTime;
        rb.MovePosition(rb.position - transform.right * speed * Time.deltaTime);
    }

}


