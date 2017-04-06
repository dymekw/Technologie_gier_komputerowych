using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour {

    public Text text;
    private float startTime;
    private float maxTime = 120;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        float timeLeft = maxTime - Time.time + startTime;
        text.text = Mathf.Max(0,timeLeft).ToString("0.0");
	}
}
