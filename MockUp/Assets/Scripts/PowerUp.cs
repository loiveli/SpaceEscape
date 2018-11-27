using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField]
    Transform dropPoint;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Destroy(gameObject);
		}
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DropPoint")
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
