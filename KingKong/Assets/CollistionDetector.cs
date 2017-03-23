using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollistionDetector : MonoBehaviour {

    public GameObject character;
    public GameObject destroyedWall;
    private Mesh mesh;

    // Use this for initialization
    void Start () {
        mesh = GetComponent<MeshFilter>().mesh;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != character.name)
            return;

        Debug.Log(howShouldBeSplitted(col));
        if (shouldBeWholeDestroyed())
        {
            Vector3 offset = new Vector3(0, 0.5f, 0);
            Vector3 rotation = new Vector3(1f, 1f, 1f);
            GameObject clone = Instantiate(destroyedWall, transform.position + offset, transform.rotation);
            clone.transform.localScale = gameObject.transform.localScale;
            clone.transform.Rotate(rotation);
            Destroy(gameObject);
        }
        else
        {

        }
        Debug.Log("Kolizja(global): " + col.contacts[0].point);
        Debug.Log("Kolizja(local): " + transform.InverseTransformPoint(col.contacts[0].point));
        /*foreach (Vector3 v in mesh.vertices)
        {
            Debug.Log("Mesh point: " + v.ToString());
        }
        Destroy(gameObject);*/        
    }

    bool shouldBeWholeDestroyed()
    {
        return true;
    }

    char howShouldBeSplitted(Collision col)
    {
        Vector3 v = transform.InverseTransformPoint(col.contacts[0].point);

        if (v.z == 0.5f || v.z == -0.5f)
        {
            return 'V';
        }
        return 'H';
    }
}
