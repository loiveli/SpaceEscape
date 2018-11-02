using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {

    public GameObject trash;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Instantiate(trash, transform.position+transform.right*Random.Range(-2,3)*4.5f, Quaternion.identity);
        }
	}
}
