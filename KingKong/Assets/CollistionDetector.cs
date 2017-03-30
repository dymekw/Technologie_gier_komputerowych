using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollistionDetector : MonoBehaviour {

    public GameObject character;
    public GameObject destroyedWall;
    public GameObject wall;

    private Vector3 offset = new Vector3(0, 0.01f, 0);
    private Vector3 rotation = new Vector3(1f, 1f, 1f);
    private Renderer rend;
    private const float HOLE_WIDTH = 2.5f;

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
            float holeWidthLocal = HOLE_WIDTH / Mathf.Max(transform.lossyScale.x, transform.lossyScale.z);

            float collisionPoint = getCollisionPoint(col);
            float normalizedCollisionPoint = collisionPoint;

            if (collisionPoint - holeWidthLocal/2 <= -0.5f)
            {
                normalizedCollisionPoint = -0.5f + holeWidthLocal/2;
            }
            else if (collisionPoint + holeWidthLocal/2 >= 0.5f)
            {
                normalizedCollisionPoint = 0.5f - holeWidthLocal/2;
            }

            float holeBegin = normalizedCollisionPoint - holeWidthLocal/2;
            float holeEnd = normalizedCollisionPoint + holeWidthLocal/2;

            instansiateDestroyedWall(holeBegin, holeEnd);

            if (holeBegin > -0.5f)
            {
                instantiateWall(-0.5f, holeBegin);
            }
            if (holeEnd < 0.5f)
            {
                instantiateWall(holeEnd, 0.5f);
            }
        }

        Destroy(gameObject);       
    }

    bool shouldBeWholeDestroyed()
    {
        return transform.lossyScale.x * transform.lossyScale.z <= HOLE_WIDTH * 1f;
    }

    float getCollisionPoint(Collision col)
    {
        Vector3 mean = new Vector3();
        foreach (ContactPoint p in col.contacts)
        {
            mean += p.point;
        }

        mean /= col.contacts.Length;
        mean = transform.InverseTransformPoint(mean);

        return mean.x;
    }

    GameObject instantiateWall(float begin, float end)
    {
        Vector3 position = getNewPosition(begin, end);
        Vector3 scale = getNewScale(begin, end);

        GameObject newWall = Instantiate(wall, transform.position + position, transform.rotation);
        newWall.transform.localScale = scale;

        rend.material.SetTextureScale("_MainTex", scale);

        return newWall;
    }

    GameObject instansiateDestroyedWall(float begin, float end)
    {
        Vector3 position = getNewPosition(begin, end);
        Vector3 scale = getNewScale(begin, end);

        GameObject newDestroyedWall = Instantiate(destroyedWall, transform.position + position + offset, transform.rotation);
        newDestroyedWall.transform.localScale = scale;
        newDestroyedWall.transform.Rotate(rotation);
        return newDestroyedWall;
    }

    Vector3 getNewPosition(float begin, float end)
    {
        float coef = (end + begin) / 2;
        float angle = (360-transform.rotation.eulerAngles.y) * Mathf.Deg2Rad;
        Vector3 rot = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));

        return Vector3.Scale(new Vector3(transform.lossyScale.x * coef, 0, transform.lossyScale.x * coef), rot);
    }

    Vector3 getNewScale(float begin, float end)
    {
        Vector3 coef = new Vector3(gameObject.transform.lossyScale.x, gameObject.transform.lossyScale.y, gameObject.transform.lossyScale.z);
        return Vector3.Scale(new Vector3(end - begin, 1, 1), coef);
    }
}
