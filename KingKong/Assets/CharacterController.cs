using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    private float speed = 10.0F;
    private float rotationSpeed = 100.0F;
    public Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Mouse X") * rotationSpeed;
        rotation *= Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }
    void FixedUpdate()
    {
        float translationV = Input.GetAxis("Vertical") * speed;
        float translationH = Input.GetAxis("Horizontal") * speed;
        
        translationV *= Time.deltaTime;
        translationH *= Time.deltaTime;
        
        transform.Translate(translationH, 0, translationV);
    }

    void OnCollisionEnter(Collision col)
    {
        ExampleGUIAspectsController.health_bar.IncrimentBar(-1);
    }
}
