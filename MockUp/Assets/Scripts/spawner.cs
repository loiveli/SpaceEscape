using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class spawner : MonoBehaviour {

    public GameObject trash;
	public float spawntime;
	public float spawnrate;
	public int laneblocks;
	public List<int> blockLanes;
	public float laneDist;
	// Use this for initialization
	void Start () {
		spawntime = Time.fixedTime;
		spawnrate = 5;
		laneblocks = 1;
		laneDist = PlaneMove.xDistance/4f;
		}
	
	// Update is called once per frame
	void FixedUpdate () {
        
		if(Time.fixedTime-spawntime >spawnrate){
			laneblocks = Random.Range(1,6);
			for(int blocks = 0; blocks<laneblocks; blocks++){
				blockLanes.Add(Random.Range(-2,3));

			}
			blockLanes = blockLanes.Distinct().ToList();
			if(blockLanes.Count == 5){
				blockLanes.Remove(0);
			}
			foreach(int lane in blockLanes ){
				GameObject tempTrash = Instantiate(trash,transform.position+transform.right*lane*laneDist,Quaternion.identity);
				tempTrash.GetComponent<TrashMove>().LanePos = (lane+2)*0.25f;
				tempTrash.GetComponent<TrashMove>().refrencePlane = transform.parent;
				tempTrash.GetComponent<TrashMove>().speed = 2;
			}
			spawntime = Time.fixedTime;
			if(spawnrate >1) spawnrate -= 0.05f;
			blockLanes.Clear();
		}
	}
}
