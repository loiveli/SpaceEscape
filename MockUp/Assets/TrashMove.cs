using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMove : MonoBehaviour
{

    float depthPos;
    public float LanePos;
    float VertPos;
    public Transform refrencePlane;
    public bool grounded;
    public Vector2 TrashMovePattern;
	public float speed;
	void Start()
    {
        VertPos = 1f;
		depthPos = 1f;
    }
    
    void FixedUpdate()
    {
		
		if(grounded){
			depthPos -= TrashMovePattern.y/1000f*speed;
			LanePos += TrashMovePattern.x/1000f*speed;
		}else{
			VertPos-= speed/50;
		}
		transform.position = Vector3.MoveTowards(transform.position, PlaneMove.MovePlayer(LanePos,depthPos,VertPos,refrencePlane), 1);
		if(depthPos<0.1f){
			grounded =false;
		}
	}
    
		/// <summary>
		/// OnCollisionEnter is called when this collider/rigidbody has begun
		/// touching another rigidbody/collider.
		/// </summary>
		/// <param name="other">The Collision data associated with this collision.</param>
		void OnCollisionEnter(Collision other)
		{
			
			if(other.gameObject.tag == "Matto"){
			grounded = true;
		}
	
			if(other.gameObject.tag == "Player"){
			grounded = false;
			VertPos = 2;
			depthPos -= 0.15f;
			Debug.Log(grounded+ " grounded");
			Debug.Log("hit Box");
		}
		
		
			//Debug.Log("grounded");
		
		
	}
   
    // Update is called once per frame

}
