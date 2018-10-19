using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
	float leftRightScale;
	float upDownScale;
	public int lanes;
	public List<Transform> borderPoints = new List<Transform>();
	// Use this for initialization
	void Start () {
		lanes = 4;
		leftRightScale = 0;
		upDownScale = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
		leftRightScale -= 1f/lanes;
			
		}
		if(Input.GetKeyDown(KeyCode.D)){
			leftRightScale +=1f/lanes;
		}
	}
	
	void FixedUpdate()
	{
		transform.position =	 Vector3.MoveTowards(transform.position, PlayerInPlane(), 0.1f);	
	}
	Vector3 PlayerInPlane(){
		Vector3 leftRight = borderPoints[0].position-borderPoints[2].position;
		Vector3 upDown = borderPoints[1].position-borderPoints[3].position;
		
		return leftRight * leftRightScale + upDown*upDownScale;
	}
}
