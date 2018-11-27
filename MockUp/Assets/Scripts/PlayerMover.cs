using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public Vector3 PlayerPos;
    public float leftRightScale;
    public float leftRightScaleDelay;
	public float depthScale;
    public float jumpScale;
    public float jumptime;
	public float jumpSpeed;
	public float strafeSpeed;
	public float jumpHeight;
	public int lanes;
    public bool Jump;
	public int airtime;
	[SerializeField]
	GameObject puff;
	public int collected;
	[SerializeField]
	float playersNewSize;
	public Transform MovePlane;
    public Transform LeftB, RightB, BackB, FwdB, UpB, DownB;
    // Use this for initialization
    void Start()
    {
        jumpSpeed = Mathf.PI/1.5f;
		lanes = 5;
        leftRightScale = 0.5f;
        leftRightScaleDelay = leftRightScale;
		depthScale = 0.5f;
        PlayerPos = MovePlayer();
		Jump = false;
		airtime = -1;
		jumpHeight = 0.5f;
	}

    // Update is called once per frame
    void Update()
    {
		if(jumpScale <0.1f){
			if(Input.GetKeyDown(KeyCode.A)){
			MoveHorizontal(-1);
		}if(Input.GetKeyDown(KeyCode.D)){
			MoveHorizontal(1);
		}if(Input.GetKeyDown(KeyCode.Space)){
			Jump = true;
			jumptime = 0;
		}
		}
		
    }

    void FixedUpdate()
    {
		
		if(collected >=50){
			spawner.SpawnPowerUp();
		}
		if(Jump){
			jumptime += Time.fixedDeltaTime*jumpSpeed;
			jumpScale = Mathf.Abs(Mathf.Sin(jumptime))*jumpHeight;

		}
		if(jumptime >= Mathf.PI){
			Jump = false;	
			jumpScale = 0;
		}
		
	
		
		if(depthScale <0.5f){
			depthScale += 0.0001f;
		}
		
		PlayerPos = MovePlayer();
		transform.position = Vector3.MoveTowards(transform.position, MovePlane.position + PlayerPos,.25f);
		leftRightScale = Mathf.MoveTowards(leftRightScale,leftRightScaleDelay,0.1f);
	}
    
	void MoveHorizontal(int lanesToRight){
		
		
		leftRightScaleDelay += 1f/(lanes-1)*lanesToRight;
		Debug.Log("moved "+1f/(lanes-1)*lanesToRight);
		
		if(leftRightScaleDelay>1){
			leftRightScaleDelay = 1;
		}else if(leftRightScaleDelay<0){
			leftRightScaleDelay = 0;
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
			collected++;
			Debug.Log("Collected collectible");
			GameObject.Destroy(other.gameObject);
		}
		if(other.gameObject.tag == "giantPowerUp"){
			StartCoroutine(Giant());
		}
	}

		IEnumerator Giant()
	{
		Instantiate(puff,transform.position,Quaternion.identity);
        transform.localScale *= playersNewSize;//Make the player a giant
		yield return new WaitForSeconds(4f); // Wait for 4 secodns
		Instantiate(puff,transform.position,Quaternion.identity);
		transform.localScale /= playersNewSize; // shrink back to small
	}
	
}
