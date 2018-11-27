using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
	[SerializeField]
	float fullSpeed;
	float lightRange;
	GameObject mill;
	public Light changingLight;
    [SerializeField]
    float regularSpeed;

	// Use this for initialization
	void Start () {
		mill = GameObject.Find("Matto");

		changingLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		Belt speedo = mill.GetComponent<Belt>();
		speedo.speed = fullSpeed;
		fullSpeed += (Time.deltaTime/2);
		lightRange = fullSpeed * 35;
		if(lightRange < 80){
			changingLight.range = 80;
		}else{
			changingLight.range = lightRange;
		}

		

        if(fullSpeed >= 6)
        {
            fullSpeed = regularSpeed;
        }


       	}
}
