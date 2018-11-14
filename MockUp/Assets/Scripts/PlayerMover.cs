using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public Vector3 PlayerPos;
    public float leftRightScale;
    public float depthScale;
    public float jumpScale;
    public int lanes;
    public bool Jump;
	public int airtime;
	public Transform MovePlane;
    // Use this for initialization
    void Start()
    {
        jumpScale = 0;
		lanes = 5;
        leftRightScale = 0.5f;
        depthScale = 0.5f;
        PlayerPos = PlaneMove.MovePlayer(leftRightScale,depthScale,jumpScale,MovePlane);
		Jump = false;
		airtime = -1;
	}

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.A)){
			MoveHorizontal(-1);
		}if(Input.GetKeyDown(KeyCode.D)){
			MoveHorizontal(1);
		}if(Input.GetKeyDown(KeyCode.Space)&&jumpScale == 0){
			Jump = true;
		}
    }

    void FixedUpdate()
    {
		
		PlayerPos = PlaneMove.MovePlayer(leftRightScale,depthScale,jumpScale,MovePlane);
		transform.position = Vector3.MoveTowards(transform.position, MovePlane.position + PlayerPos,.25f);
		transform.rotation = MovePlane.rotation;
		//jumpScale = Mathf.Abs(Mathf.Sin(Time.time));
	if(Jump&&jumpScale <1){
			jumpScale += 0.05f;
		}else if(jumpScale >0){
			jumpScale -= 0.075f;
		}
		if(jumpScale <0){
			jumpScale = 0;
		}
		if(jumpScale >=1&&airtime == -1){
			airtime = 1;
		}
		if(depthScale <0.5f){
			depthScale += 0.0001f;
		}
		if(airtime >=0)
		{
			airtime--;
		}if(airtime == 0){
			Jump= false;
		} 
		/*		PlayerPos = PlaneMove.MovePlayer(leftRightScale,depthScale,jumpScale);
		transform.position = Vector3.MoveTowards(transform.position, MovePlane.position + PlayerPos,.25f);
		transform.rotation = MovePlane.rotation;
		 */

	}
    
	void MoveHorizontal(int lanesToRight){
		
		
		leftRightScale += 1f/(lanes-1)*lanesToRight;
		Debug.Log("moved "+1f/(lanes-1)*lanesToRight);
		
		if(leftRightScale>1){
			leftRightScale = 1;
		}else if(leftRightScale<0){
			leftRightScale = 0;
		}


	}
	
    
	
	
	void OnCollisionEnter(Collision other)
	{
		
		if(other.gameObject.tag == "Box"){
			
			depthScale -=0.1f;
			
		}
	}
	
}
