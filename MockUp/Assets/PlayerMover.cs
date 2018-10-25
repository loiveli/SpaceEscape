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
	public Transform MovePlane;
    public Transform LeftB, RightB, BackB, FwdB, UpB, DownB;
    // Use this for initialization
    void Start()
    {
        lanes = 5;
        leftRightScale = 0.5f;
        depthScale = 0.5f;
        PlayerPos = MovePlayer();
		Jump = false;
	}

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.A)){
			MoveHorizontal(-1);
		}if(Input.GetKeyDown(KeyCode.D)){
			MoveHorizontal(1);
		}if(Input.GetKeyDown(KeyCode.Space)&&!Jump){
			Jump = true;
		}
    }

    void FixedUpdate()
    {
        if(jumpScale >=1){
			Jump= false;
		}
		if(Jump&&jumpScale <1){
			jumpScale += 0.05f;
		}else if(jumpScale >0){
			jumpScale -= 0.1f;
		}
		if(depthScale <0.5f){
			depthScale += 0.001f;
		}
		PlayerPos = MovePlayer();
		transform.position = MovePlane.position + PlayerPos;
		transform.rotation = MovePlane.rotation;
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
	Vector3 MovePlayer()
    {
        float xDistance = (RightB.position - LeftB.position).magnitude;
		float xcom = (xDistance * leftRightScale)-(xDistance/2);
        float zDistance = (FwdB.position-BackB.position).magnitude;
		float zcom = (zDistance*depthScale)-(zDistance/2);
		float yDistance = (UpB.position-DownB.position).magnitude;
		float ycom = (yDistance*jumpScale);
		return MovePlane.right*xcom+MovePlane.forward*zcom+MovePlane.up*ycom;


    }
}
