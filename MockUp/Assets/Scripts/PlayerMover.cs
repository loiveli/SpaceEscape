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
    public Transform LeftB, RightB, BackB, FwdB, UpB, DownB;
    // Use this for initialization
    void Start()
    {
        lanes = 5;
        leftRightScale = 0.5f;
        depthScale = 0.5f;
        PlayerPos = MovePlayer();
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
		
		PlayerPos = MovePlayer();
		transform.position = Vector3.MoveTowards(transform.position, MovePlane.position + PlayerPos,.25f);
		transform.rotation = MovePlane.rotation;
		//jumpScale = Mathf.Abs(Mathf.Sin(Time.time));
	if(Jump&&jumpScale <1){
			jumpScale += 0.03f;
		}if(jumpScale >1){
			Jump = false;

		}if (jumpScale>0&&!Jump){
			jumpScale -=0.025f;
		}
		if(jumpScale <0){
			jumpScale = 0;
		}
		
		if(depthScale <0.5f){
			depthScale += 0.0001f;
		}
		
		PlayerPos = MovePlayer();
		transform.position = Vector3.MoveTowards(transform.position, MovePlane.position + PlayerPos,.25f);
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
    
	
	
	void OnCollisionEnter(Collision other)
	{
		Debug.Log(other.gameObject.tag.ToString());
		if(other.gameObject.tag == "Box"){
			Debug.Log("Hit box");
			depthScale -=0.1f;
			other.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 1000-transform.forward*1000+transform.right*2000*(leftRightScale-0.5f));
		}
		Debug.Log(other.gameObject.tag.ToString());
		if(other.gameObject.tag == "Collectible"){
			Debug.Log("Collected collectible");
			GameObject.Destroy(other.gameObject);
		}
	}
	
}
