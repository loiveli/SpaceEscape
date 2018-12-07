using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class spawner : MonoBehaviour {

    public List<GameObject> trash;
	public GameObject collectible;
	public GameObject Hand;
	public List<GameObject> PowerUpObj;
	public float spawntime;
	public float spawnrate;
	public int laneblocks;
	public int lastLane;
	public List<int> blockLanes;
	public bool powerUp;
	public int powerUpLane;
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
	public void SpawnPowerUp( int lane){
		
	}
	void FixedUpdate () {
        
		if(Time.fixedTime-spawntime >spawnrate){
			blockLanes.Clear();
			laneblocks = Random.Range(1,5);
			for(int blocks = 0; blocks<laneblocks; blocks++){
				blockLanes.Add(Random.Range(0,5)-2);

			}
			blockLanes = blockLanes.Distinct().ToList();
			blockLanes.Sort();
			
			int RNGlane = Random.Range(0,6-blockLanes.Count-1);
				List<int> tempList = refrenceList.Except(blockLanes).ToList();
				Debug.Log(RNGlane + " RNGLANE");
				lastLane = tempList[RNGlane];
				if(!powerUp){
					Instantiate(collectible,transform.position+transform.right*lastLane*PlayerMover.xDistance/4,transform.rotation);
				}else{
					blockLanes.Remove(powerUpLane);
					Instantiate(PowerUpObj[Random.Range(0,PowerUpObj.Count)], transform.position + transform.right*powerUpLane*PlayerMover.xDistance/4, transform.rotation);
					powerUp = false;
					Hand.GetComponent<HandScript>().deliver = false;
				}
			if(blockLanes.Count == 1){
				Instantiate(trash[0],transform.position+transform.right*lastLane*PlayerMover.xDistance/4,transform.rotation);
			}else{
				foreach(int lane in blockLanes ){
				Instantiate(trash[Random.Range(1,trash.Count)],transform.position+transform.right*lane*PlayerMover.xDistance/4,transform.rotation);
			}
			}
			
			
			
				
				
				Debug.Log(lastLane + " was chosen as new lane");
				
			
			spawntime = Time.fixedTime;
			if(spawnrate >1) spawnrate -= 0.05f;
			
		}
		
	}
}
