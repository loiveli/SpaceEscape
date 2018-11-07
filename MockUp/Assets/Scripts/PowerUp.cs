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


	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		StartCoroutine(PickUp(collision)) ;
	}

	IEnumerator PickUp(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
        {
			GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
			Instantiate(puff, transform.position, Quaternion.identity);
            player.transform.localScale *= playersNewSize;
			yield return new WaitForSeconds(4f);
			player.transform.localScale /= playersNewSize;
            Destroy(gameObject);
        }
	}
}
