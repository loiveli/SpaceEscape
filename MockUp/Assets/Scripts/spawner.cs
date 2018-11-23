using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class spawner : MonoBehaviour {

    public GameObject trash;
	public GameObject collectible;
	public float spawntime;
	public float spawnrate;
	public int laneblocks;
	public int lastLane;
	public List<int> blockLanes;
	public List<int> refrenceList;
	// Use this for initialization
	void Start () {
		spawntime = Time.fixedTime;
		spawnrate = 2;
		laneblocks = 1;
		lastLane =-1;
		for(int k =-2;k<=2;k++){
			refrenceList.Add(k);
		}
		}
	
	// Update is called once per frame
	void FixedUpdate () {
        
		if(Time.fixedTime-spawntime >spawnrate){
			blockLanes.Clear();
			laneblocks = Random.Range(1,5);
			for(int blocks = 0; blocks<laneblocks; blocks++){
				blockLanes.Add(Random.Range(0,5)-2);

			}
			blockLanes = blockLanes.Distinct().ToList();
			blockLanes.Sort();
			
			foreach(int lane in blockLanes ){
				Instantiate(trash,transform.position+transform.right*lane*4.5f,transform.rotation);
			}
			
			
				int RNGlane = Random.Range(0,6-blockLanes.Count-1);
				List<int> tempList = refrenceList.Except(blockLanes).ToList();
				Debug.Log(RNGlane + " RNGLANE");
				lastLane = tempList[RNGlane];
				Instantiate(collectible,transform.position+transform.right*lastLane*4.5f,transform.rotation);
				Debug.Log(lastLane + " was chosen as new lane");
				
			
			spawntime = Time.fixedTime;
			if(spawnrate >1) spawnrate -= 0.05f;
			
		}
	}
}
