﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour {

    public GameObject heart;
    public GameObject character;
    public static int addHealth = 20;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        heart.transform.Rotate(new Vector3(0,1,0));
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != character.name)
            return;
        ExampleGUIAspectsController.health_bar.IncrimentBar(addHealth);
        Destroy(heart);
    }
}
