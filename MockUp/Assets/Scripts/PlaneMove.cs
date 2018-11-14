using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour {

	// Use this for initialization
	public static Transform MovePlane;
    public static List<Transform> boundaries = new List<Transform>();
	public static float xDistance;
	void Start()
	{
		foreach(Transform item in transform){
			if(item.tag == "Boundary"){
				boundaries.Add(item);
			}
			Debug.Log(boundaries.Count);
		}
		xDistance =(boundaries[1].position - boundaries[0].position).magnitude;
	}

	// Update is called once per frame
	public static Vector3 MovePlayer( float leftRightScale, float depthScale, float jumpScale, Transform refrence)
    {
        
		float xcom = (xDistance * leftRightScale)-(xDistance/2);
        float zDistance = (boundaries[3].position-boundaries[2].position).magnitude;
		float zcom = (zDistance*depthScale)-(zDistance/2);
		float yDistance = (boundaries[4].position-boundaries[5].position).magnitude;
		float ycom = (yDistance*jumpScale);
		return refrence.right*xcom+refrence.forward*zcom+refrence.up*ycom;


    }
}
