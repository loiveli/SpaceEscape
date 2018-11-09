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
	// Use this for initialization
	void Start () {
		spawntime = Time.fixedTime;
		spawnrate = 2;
		laneblocks = 1;
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
				Instantiate(trash,transform.position+transform.right*lane*4.5f,transform.rotation);
			}
			spawntime = Time.fixedTime;
			if(spawnrate >1) spawnrate -= 0.05f;
			blockLanes.Clear();
		}
	}
}
