using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {
    [SerializeField]
    Text metersText;
    [SerializeField]
    float meters;
    // Use this for initialization
    void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
        meters += Time.deltaTime;
        metersText.text = meters + "m";
	}
}
