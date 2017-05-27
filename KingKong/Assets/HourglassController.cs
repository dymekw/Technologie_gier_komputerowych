using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourglassController : MonoBehaviour
{

    public GameObject hourglass;
    public GameObject character;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hourglass.transform.Rotate(1.1f, 0.1f, -0.9f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != character.name)
            return;

        timer.instance.collectedHourglass();
        Destroy(hourglass);
    }
}
