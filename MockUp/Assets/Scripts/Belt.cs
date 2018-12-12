using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour {

	
	public float speed;
    [SerializeField]
    float mattSpeedSlower = 75f;
	private float currentScroll;
    PlayerMover player;
    [SerializeField]
    float minSpeed;
    [SerializeField]
    float maxSpeed;



    private void Update()
    {

		//Texture Scroller (Main texture) Scrolling at the same speed as the current scroll speed
		currentScroll = currentScroll + Time.deltaTime * speed/ mattSpeedSlower;
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(-currentScroll,0);

        if (speed <= minSpeed)
        {
            speed = minSpeed;
        }
        else if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }
        

    }

    
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.position += transform.right * speed * Time.deltaTime;
        rb.MovePosition(rb.position - transform.right * speed * Time.deltaTime);
    }

    

}


