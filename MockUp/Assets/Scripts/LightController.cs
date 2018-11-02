using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
	[SerializeField]
	float fullSpeedo;
	GameObject mill;
	public Light lighthos;
    [SerializeField]
    float regularSpeed;

	// Use this for initialization
	void Start () {
		mill = GameObject.Find("TreadMill");

		lighthos = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		Belt speedo = mill.GetComponent<Belt>();
		speedo.speed = fullSpeedo;
		lighthos.range = fullSpeedo * -1;

		fullSpeedo += (Time.deltaTime/2);

        if(fullSpeedo >= 6)
        {
            fullSpeedo = regularSpeed;
        }


       	}
}
