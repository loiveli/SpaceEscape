using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {

    [SerializeField]
    Transform dropPoint;
    [SerializeField]
    float handSpeed;
    [SerializeField]
    float step;
    [SerializeField]
    Vector3 startPoint;
    [SerializeField]
    float waitTime;
    [SerializeField]
    private int collectables;
    [SerializeField]
    bool done;
	// Use this for initialization
	void Start () {
        startPoint = transform.position;
        step = handSpeed * Time.deltaTime;

	}
	
	// Update is called once per frame
	void Update () {

        
        if(!done){
                StartCoroutine( MoveIn());
        } else
        {
            StartCoroutine(MoveOut());
        }
        
       

    }

    IEnumerator MoveIn()
    {
            done = false;
            transform.position = Vector3.MoveTowards(transform.position, dropPoint.position, step);
            yield return new WaitForSeconds(waitTime);
            done = true;
            
    }

    IEnumerator MoveOut(){
        transform.position = Vector3.MoveTowards(transform.position, startPoint, step);
        yield return new WaitForSeconds(waitTime);

    }


}
