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
    private bool texturesEnabled;
    [SerializeField]
    private int flickerBool;
    public Transform MovePlane;
    public Transform LeftB, RightB, BackB, FwdB, UpB, DownB;
    [SerializeField]
    float invicibilityTime;
    [SerializeField]
    GameObject Alien;
    Animator m_Animator;
    bool isBlinkin;

    // Use this for initialization
    void Start()
    {
        lanes = 5;
        leftRightScale = 0.5f;
        depthScale = 0.5f;
        PlayerPos = MovePlayer();
		Jump = false;
		airtime = -1;
        m_Animator = gameObject.GetComponent<Animator>();

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
        flickerBool = Random.Range(0, 2);
    }

    void FixedUpdate()
    {
		
		PlayerPos = MovePlayer();
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
			airtime = 15;
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
            StartCoroutine(Invicibity());
			//other.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 1000-transform.forward*1000+transform.right*2000*(leftRightScale-0.5f));
		}
	}

    IEnumerator Invicibity()
    {
        GetComponent<Animation>().
        GetComponent<CapsuleCollider>().enabled = false;
        m_Animator.SetBool("isBlinking", true);
        //Alien.GetComponent<SkinnedMeshRenderer>().enabled = false;
        yield return new WaitForSeconds(invicibilityTime);
        m_Animator.SetBool("isBlinking", false);
        GetComponent<CapsuleCollider>().enabled = true;
        //Alien.GetComponent<SkinnedMeshRenderer>().enabled = true;

    }

}
