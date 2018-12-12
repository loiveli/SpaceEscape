using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMover : MonoBehaviour
{
    public Vector3 PlayerPos;
    public GameObject Hand;
    public float leftRightScale;
    public float leftRightScaleDelay;
    public float depthScale;
    public float jumpScale;
    public float jumptime;
    public float jumpSpeed;
    public float strafeSpeed;
    public float jumpHeight;
    public int lanes;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float minSpeed;
    [SerializeField]
    GameObject breakStuff;
    public bool giant;
    public bool Jump;
	public Vector3 targetScale;
    public int airtime;
    [SerializeField]
    GameObject puff;
    public int collected;
    [SerializeField]
    int totalObstaclesSmashed;
    [SerializeField]
    float playersNewSize;
    public int obstaclesSmashed;
    public Transform MovePlane;
    public Transform LeftB, RightB, BackB, FwdB, UpB, DownB;
    public Belt belt;
    // Use this for initialization
    public static float xDistance;
    public static float yDistance;
    public static float zDistance;
    [SerializeField]
    float puTime;
    [SerializeField]
    float speedFactor;
    void Start()
    {
        strafeSpeed = 0.2f;
        jumpSpeed = Mathf.PI / 1.5f;
        lanes = 5;
        leftRightScale = 0.5f;
        leftRightScaleDelay = leftRightScale;
        depthScale = 0.5f;
        PlayerPos = MovePlayer();
        Jump = false;
        airtime = -1;
        jumpHeight = 0.5f;
		targetScale = transform.localScale;
        xDistance = (RightB.position - LeftB.position).magnitude;
        yDistance = (UpB.position - DownB.position).magnitude;
        zDistance = (FwdB.position - BackB.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpScale < 0.1f)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveHorizontal(-1);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveHorizontal(1);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump = true;
                jumptime = 0;
            }
        }

        if(transform.position.z > 13.3f)
        {
            belt.speed = 0;
            StartCoroutine(PlayerAtStart());
        }

    }

    void FixedUpdate()
    {

        if (collected >= 20 &&Hand.GetComponent<HandScript>().deliver == false)
        {
            Hand.GetComponent<HandScript>().StartDelivery(MovePlane,Random.Range(0,5));
            collected = 0;
        }
        if (Jump)
        {
            jumptime += Time.fixedDeltaTime * jumpSpeed;
            jumpScale = Mathf.Abs(Mathf.Sin(jumptime)) * jumpHeight;

        }
        if (jumptime >= Mathf.PI)
        {
            Jump = false;
            jumpScale = 0;
        }



        if (depthScale < 0.5f)
        {
            depthScale += 0.0001f;
        }

        PlayerPos = MovePlayer();
        transform.position = Vector3.MoveTowards(transform.position, MovePlane.position + PlayerPos, .25f);
        leftRightScale = Mathf.MoveTowards(leftRightScale, leftRightScaleDelay, strafeSpeed);
		transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, 0.1f*playersNewSize);
    }

    void MoveHorizontal(int lanesToRight)
    {


        leftRightScaleDelay += 1f / (lanes - 1) * lanesToRight;
        Debug.Log("moved " + 1f / (lanes - 1) * lanesToRight);

        if (leftRightScaleDelay > 1)
        {
            leftRightScaleDelay = 1;
        }
        else if (leftRightScaleDelay < 0)
        {
            leftRightScaleDelay = 0;
        }


    }
    Vector3 MovePlayer()
    {
        
        float xcom = (xDistance * leftRightScale) - (xDistance / 2);
        
        float zcom = (zDistance * depthScale) - (zDistance / 2);
        
        float ycom = (yDistance * jumpScale);
        return MovePlane.right * xcom + MovePlane.forward * zcom + MovePlane.up * ycom;


    }

    public static Vector3 GetPosInPlane( float xScale,float yScale, float zScale,Transform refPlane){
        float xcom = (xDistance * xScale) - (xDistance / 2);
        
        float zcom = (zDistance * zScale) - (zDistance / 2);
        
        float ycom = (yDistance * yScale);
        return refPlane.right * xcom + refPlane.forward * zcom + refPlane.up * ycom;
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag.ToString());
        if (other.gameObject.tag == "Box" && !giant)
        {
            Debug.Log("Hit box");
            depthScale -= 0.1f;
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 500 + transform.forward * 500 - transform.right * 1000 * (leftRightScale - 0.5f));
            if(belt.speed> 2) belt.speed -=1;
        }
        Debug.Log(other.gameObject.tag.ToString());
        if (other.gameObject.tag == "Collectible")
        {
            collected++;
            Debug.Log("Collected collectible");
            GameObject.Destroy(other.gameObject);
            belt.speed += .1f;
        }
        if (other.gameObject.tag == "giantPowerUp")
        {
            StartCoroutine(Giant());
        }
        if(other.gameObject.tag == "Box" && giant == true){
            Instantiate(breakStuff, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            obstaclesSmashed++;
            totalObstaclesSmashed++;
        }
        if(other.gameObject.tag == "BootPU")
        {
            StartCoroutine(Boot());
        }
        if(other.gameObject.tag == "SlowBomb")
        {
            StartCoroutine(Slow());
        }
    }




    IEnumerator Giant()
    {
        Instantiate(puff, transform.position, Quaternion.identity);
        giant = true;
        targetScale *= playersNewSize;//Make the player a giant
        yield return new WaitForSeconds(8f); // Wait for 8 seconds
        giant = false;
        Instantiate(puff, transform.position, Quaternion.identity);
        targetScale *= 1/playersNewSize ; // shrink back to small
        obstaclesSmashed = 0;
    }

    IEnumerator Boot()
    {
        strafeSpeed *=2;
        belt.speed *= speedFactor;
        yield return new WaitForSeconds(puTime);
        belt.speed /= speedFactor;
        strafeSpeed /=2;
    }

    IEnumerator Slow()
    {
        float startSpeed = belt.speed;
        jumpHeight = 0.1f;
        jumpSpeed *= 2;
        strafeSpeed /=2;
        if (belt.speed > 15)
        {
            belt.speed /= 2;
        } else
        {
            belt.speed = 10;
        }
        yield return new WaitForSeconds(puTime);
        strafeSpeed *=2;
        jumpSpeed = Mathf.PI / 1.5f;
        jumpHeight = .5f;
        belt.speed = startSpeed;
    }
    IEnumerator PlayerAtStart()
    {
        GetComponent<Collider>().enabled = false;
        PlayerPos = new Vector3(-5.4f,0.27f,-6.92f);
        yield return new WaitForSeconds(4f);
        depthScale = .5f;
        GetComponent<Collider>().enabled = true;
    }

}
