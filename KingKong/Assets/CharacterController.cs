using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	Animator anim;
    private float speed = 10.0F;
    private float rotationSpeed = 100.0F;
    public Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
		
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) )
        {
            anim.Play("Walk");
        }
        else if (Input.GetMouseButton(0) && !ExampleGUIAspectsController.isBlocked())
        {
            anim.Play("Attack");
        }
        else if (Input.GetKeyDown(KeyCode.Space)){
            anim.Play("jump");
        }
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
