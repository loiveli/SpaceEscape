using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
	[SerializeField]
	float fullSpeed;
	float lightRange;
	GameObject mill;
	public Light light;
    [SerializeField]
    float regularSpeed;

	// Use this for initialization
	void Start () {
		mill = GameObject.Find("Matto");

		light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		Belt speedo = mill.GetComponent<Belt>();
		speedo.speed = fullSpeed;
		fullSpeed += (Time.deltaTime/2);

		lightRange = fullSpeed * 35;
		if(lightRange < 120){
			light.range = 120;
		}else{
			light.range = lightRange;
		}

		

        if(fullSpeed >= 6)
        {
            fullSpeed = regularSpeed;
        }


       	}
}
