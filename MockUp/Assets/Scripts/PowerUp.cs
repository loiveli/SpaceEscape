using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {


	[SerializeField]
	GameObject puff;
	[SerializeField]
	GameObject player;
	[SerializeField]
	float playersNewSize;
    [SerializeField]
    Transform dropPoint;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void OnCollisionEnter(Collision collision)
	{
		StartCoroutine(PickUp(collision));
	}

	IEnumerator PickUp(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
        {
            GetComponent<Collider>().enabled = false; //Removes the collider of the powerup, so that player can't pick it up again
            GetComponent<MeshRenderer>().enabled = false; // Removes the Mesh so it seems that its destroyed.
            Instantiate(puff, transform.position, Quaternion.identity); // makes a particle effect when power up is picked up
            player.transform.localScale *= playersNewSize;//Make the player a giant
			yield return new WaitForSeconds(4f); // Wait for 4 secodns
			player.transform.localScale /= playersNewSize; // shrink back to small
            Destroy(gameObject); // Destroys the powerup
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
