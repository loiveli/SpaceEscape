using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {

    [SerializeField]
    Transform dropPoint;
    [SerializeField]
    float handSpeed;
    [SerializeField]
    Vector3 startPoint;
    [SerializeField]
    float waitTime;
	// Use this for initialization
	void Start () {
        startPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine( Mover());
       

    }

    IEnumerator Mover()
    {
            float step = handSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, dropPoint.position, step);
            yield return new WaitForSeconds(waitTime);
            transform.position = Vector3.MoveTowards(transform.position, startPoint, step);
    }


}
