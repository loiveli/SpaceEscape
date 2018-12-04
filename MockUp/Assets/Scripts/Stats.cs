using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {
    [SerializeField]
    Text timeRunned;
    [SerializeField]
    PlayerMover player;
    [SerializeField]
    Text collectedItemsText;
    [SerializeField]
    Text giantPU;
    private float puTime = 8;
    [SerializeField]
    Text doSomething;
    [SerializeField]
    Text speed;
    [SerializeField]
    Belt belt;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        timeRunned.text = "Runned " + Time.time.ToString("0.00") + " sec";
        collectedItemsText.text = player.collected.ToString() + " Collected";
        speed.text = "Speed "+ belt.speed.ToString("0");
        if (player.giant)
        {
            puTime -= Time.deltaTime;
            giantPU.text = "Giant powerup activated " + puTime.ToString("0.00");
            doSomething.text = "Smahsing time!\n"+player.obstaclesSmashed + "\n Obstacles smashed!";
        }
        else {
            puTime = 8;
            giantPU.text = "";
            doSomething.text = "";
        }

	}
}
