using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    [SerializeField]
    Transform playerPos;
    [SerializeField]
    Transform endPos;
    [SerializeField]
    float step;
    [SerializeField]
    Transform startPos;
    bool theEnd;
	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
		if(playerPos.position.z > 13.3f)
        {
            StartCoroutine(EndMovement());
        }
	}

    IEnumerator EndMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPos.transform.position, step);
        yield return new WaitForSeconds(5);
        transform.position = Vector3.MoveTowards(transform.position, startPos.transform.position, step);
        
    }
}
