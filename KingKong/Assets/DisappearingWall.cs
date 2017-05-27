using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingWall : MonoBehaviour {

    public GameObject wall;

    private float SPEED = 0.1f;
    private bool isVisible;
    private bool changingState = false;
    
    void Start () {
        InvokeRepeating("ChangeState", Random.Range(5, 15), Random.Range(5,20));

        if (Random.Range(0,1) == 0)
        {
            isVisible = false;
            wall.transform.Translate(new Vector3(0, -2.5f, 0));
        }
    }
	
	
	void Update () {
		if (changingState)
        {
            float dy = isVisible ? SPEED : -SPEED;

            wall.transform.Translate(new Vector3(0, dy, 0));

            if (wall.transform.position.y >= 1f || wall.transform.position.y <= -2.5f)
            {
                changingState = false;
            }
        }
	}

    void ChangeState()
    {
        changingState = true;
        isVisible = !isVisible;
    }
}
