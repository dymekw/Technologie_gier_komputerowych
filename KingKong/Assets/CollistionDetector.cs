using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollistionDetector : MonoBehaviour {

    public GameObject character;
    public GameObject destroyedWall;
    public GameObject wall;
    private Vector3 offset = new Vector3(0, 0.01f, 0);
    private Vector3 rotation = new Vector3(1f, 1f, 1f);

    public Renderer rend;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != character.name)
            return;

        if (shouldBeWholeDestroyed())
        {
            GameObject clone = Instantiate(destroyedWall, transform.position + offset, transform.rotation);
            clone.transform.localScale = gameObject.transform.localScale;
            clone.transform.Rotate(rotation);
        }
        else
        {
            float holeWidth = 1f / Mathf.Max(transform.lossyScale.x, transform.lossyScale.z);

            float collisionPoint = getCollisionPoint(col);
            float normalizedCollisionPoint = collisionPoint;

            bool axis = splitVertically(col);
            Debug.Log("Vertical: " + axis);

            if (collisionPoint - holeWidth > -0.5f)
            {
                instansiateWall(-0.5f, collisionPoint - holeWidth, axis);
            } else
            {
                normalizedCollisionPoint = -0.5f + holeWidth;
            }
            if (collisionPoint + holeWidth < 0.5f)
            {
                instansiateWall(collisionPoint + holeWidth, 0.5f, axis);
            } else
            {
                normalizedCollisionPoint = 0.5f - holeWidth;
            }

            float holeBegin = normalizedCollisionPoint - holeWidth;
            float holeEnd = normalizedCollisionPoint + holeWidth;

            instansiateDestroyedWall(holeBegin, holeEnd, axis);
        }

        Destroy(gameObject);       
    }

    bool shouldBeWholeDestroyed()
    {
        return transform.lossyScale.x * transform.lossyScale.z < 1;
    }

    float getCollisionPoint(Collision col)
    {
        Vector3 v = transform.InverseTransformPoint(col.contacts[0].point);
        if (v.z == 0.5f || v.z == -0.5f)
        {
            return v.x;
        }
        return v.z;
    }

    bool splitVertically(Collision col)
    {
        Vector3 v = transform.InverseTransformPoint(col.contacts[0].point);

        if ((v.z == 0.5f || v.z == -0.5f) && transform.lossyScale.z <= transform.lossyScale.x)
        {
            return true;
        }
        if ((v.x == 0.5f || v.x == -0.5f) && transform.lossyScale.z <= transform.lossyScale.x)
        {
            return true;
        }
        return false;
    }

    GameObject instansiateWall(float begin, float end, bool splitVer)
    {
        Vector3 position;
        Vector3 scale;

        if (splitVer)
        {
            position = new Vector3(gameObject.transform.lossyScale.x * (end + begin) / 2, 0, 0);
            scale = new Vector3((end - begin) * gameObject.transform.lossyScale.x, gameObject.transform.lossyScale.y, gameObject.transform.lossyScale.z);
        } else
        {
            position = new Vector3(0,0, gameObject.transform.lossyScale.z * (end + begin) / 2);
            scale = new Vector3(gameObject.transform.lossyScale.x, gameObject.transform.lossyScale.y, (end - begin) * gameObject.transform.lossyScale.z);
        }

        GameObject newWall = Instantiate(wall, transform.position + position, transform.rotation);
        newWall.transform.localScale = scale;

        rend.material.SetTextureScale("_MainTex", scale);

        return newWall;
    }

    GameObject instansiateDestroyedWall(float begin, float end, bool splitVer)
    {
        Vector3 position;
        Vector3 scale;

        if (splitVer)
        {
            position = new Vector3(gameObject.transform.lossyScale.x * (end + begin) / 2, 0, 0);
            scale = new Vector3((end - begin) * gameObject.transform.lossyScale.x, gameObject.transform.lossyScale.y, gameObject.transform.lossyScale.z);
        }
        else
        {
            position = new Vector3(0, 0, gameObject.transform.lossyScale.z * (end + begin) / 2);
            scale = new Vector3(gameObject.transform.lossyScale.x, gameObject.transform.lossyScale.y, (end - begin) * gameObject.transform.lossyScale.z);
        }

        GameObject newDestroyedWall = Instantiate(destroyedWall, transform.position + position + offset, transform.rotation);
        newDestroyedWall.transform.localScale = scale;
        newDestroyedWall.transform.Rotate(rotation);
        return newDestroyedWall;
    }
}
