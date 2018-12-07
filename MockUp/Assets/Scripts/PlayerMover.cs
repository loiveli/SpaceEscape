﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMover : MonoBehaviour
{
    public Vector3 PlayerPos;
    public GameObject Hand;
    public GameObject alien;
    public float leftRightScale;
    public float leftRightScaleDelay;
    public float depthScale;
    public float jumpScale;
    public float jumptime;
    public float jumpSpeed;
    public float strafeSpeed;
    public float recoverySpeed;
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
    private bool invincible;

    
    public int invistimer;

    

    void Start()
    {
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
        recoverySpeed = 0.00002f;
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

    }

    void FixedUpdate()
    {

        if (collected >= 25 && Hand.GetComponent<HandScript>().deliver == false)
        {
            Hand.GetComponent<HandScript>().StartDelivery(MovePlane, Random.Range(0, 5));
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
        if (invistimer >= 0)
        {
            invistimer--;
            
        }
        if (invistimer == 0)
        {
            gameObject.GetComponent<Collider>().enabled = true;
            invincible = false;
        }


        if (depthScale < 0.5f&& depthScale > 0)
        {
            depthScale += recoverySpeed;
        }
        if(depthScale <0){
            jumpScale-=0.1f;
        }
        if(jumpScale <= -1){
            leftRightScale = 0.5f;
        leftRightScaleDelay = leftRightScale;
        depthScale = 0.5f;
        jumpScale = 0;
        }
        PlayerPos = MovePlayer();
        transform.position = Vector3.MoveTowards(transform.position, MovePlane.position + PlayerPos, .25f);
        leftRightScale = Mathf.MoveTowards(leftRightScale, leftRightScaleDelay, 0.1f);
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, 0.1f * playersNewSize);
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

    public static Vector3 GetPosInPlane(float xScale, float yScale, float zScale, Transform refPlane)
    {
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
            InvisRun();
            if(belt.speed >2){
                belt.speed -= 0.25f;
            }
            
        }
        Debug.Log(other.gameObject.tag.ToString());
        if (other.gameObject.tag == "Collectible")
        {
            collected++;
            Debug.Log("Collected collectible");
            GameObject.Destroy(other.gameObject);
            belt.speed += 0.25f;
        }
        if (other.gameObject.tag == "giantPowerUp")
        {
            StartCoroutine(Giant());
        }

        if (other.gameObject.tag == "Box" && giant == true)
        {
            Instantiate(breakStuff, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            obstaclesSmashed++;
            totalObstaclesSmashed++;
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
        targetScale *= 1 / playersNewSize; // shrink back to small
        obstaclesSmashed = 0;
    }
    void InvisRun()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        invincible = true;
        
        invistimer = 90;
    }

}
