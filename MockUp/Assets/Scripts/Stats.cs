using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Stats : MonoBehaviour {
    [SerializeField]
    TextMeshPro timeRunned;
    [SerializeField]
    PlayerMover player;
    [SerializeField]
    TextMeshPro collectedItemsText;
    [SerializeField]
    TextMeshPro giantPU;
    private float puTime = 8;
    [SerializeField]
    TextMeshProUGUI doSomething;
    [SerializeField]
    TextMeshPro speed;
    [SerializeField]
    Belt belt;
    bool gameEnded;
    private float totalRunTime;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        totalRunTime += Time.deltaTime;
        if(player.transform.position.z > 13.3 && !gameEnded)
        {
            PlayerPrefs.SetFloat("stat", totalRunTime);
            gameEnded = true;
        }
        if (!gameEnded)
        {
            timeRunned.text = "Runned " + totalRunTime.ToString("0.00");
        }
        
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
