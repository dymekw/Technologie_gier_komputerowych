using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollController : MonoBehaviour {

    private float speed = 10.0F;
    private float rotationSpeed = 100.0F;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float translationV = Input.GetAxis("Vertical") * speed;
        float translationH = Input.GetAxis("Horizontal") * speed;
        float rotation = Input.GetAxis("Mouse X") * rotationSpeed;
        translationV *= Time.deltaTime;
        translationH *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(translationH, 0, translationV);
        transform.Rotate(0, rotation, 0);
    }
}
