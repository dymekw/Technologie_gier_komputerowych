using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class timer : MonoBehaviour {

    public static timer instance;
    public TMP_Text text;
    private float startTime;
    private float maxTime = 120;
    private float hourglassAdditionalTime = 15;
    private int collectedHourglasses;

    void Awake()
    {
        instance = this;
        collectedHourglasses = 0;
    }

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        float timeLeft = maxTime - Time.time + startTime + hourglassAdditionalTime * collectedHourglasses;
        text.text = Mathf.Max(0,timeLeft).ToString("0.0");
	}

    public void collectedHourglass()
    {
        collectedHourglasses++;
    }
}
