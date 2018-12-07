using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {
	public Vector3 targetPos;
	public Vector3 startPos;
	public GameObject spawner;
	public bool deliver;
	// Use this for initialization
	int powerUpLane;
	void Start () {
		deliver = false;
	}
	public void StartDelivery(Transform refPlane, int xScale){
		targetPos = PlayerMover.GetPosInPlane((xScale*0.25f)+0.25f, 1,1,refPlane);
		deliver = true;
		powerUpLane =xScale;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(deliver){
			transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.5f);

		}else if(transform.position != startPos){
			transform.position = Vector3.MoveTowards(transform.position, startPos, 1f);
		}
		if(transform.position == targetPos){
			SpawnPowerUp();
			
		}
	}
	void SpawnPowerUp(){
		spawner.GetComponent<spawner>().powerUp = true;
		spawner.GetComponent<spawner>().powerUpLane = powerUpLane-2;
	}
	
}