using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollistionDetector : MonoBehaviour {

    public GameObject character;
    public GameObject destroyedWall;
    public GameObject wall;
    public bool isDestroyable = true;
    private float wallLife;
    private Vector3 offset = new Vector3(0, 0.05f, 0);
    private Vector3 rotation = new Vector3(5f, 5f, 5f);
    private const float HOLE_WIDTH = 1.5f;

    // Use this for initialization
    void Start () {
        wallLife = (int)Random.Range(1.0f, 6.0f);
        //Vector3 scale = wall.transform.localScale;
        //scale.z += (wallLife/1000) ;
        // //wall.transform.localScale = scale;
        // Renderer rend = GetComponent<Renderer>();
        // rend.material.shader = Shader.Find("Specular");
        // rend.material.SetColor("_SpecColor", Color.red);
        if (!isDestroyable)
        {
            wallLife = 0;
            wall.GetComponent<Renderer>().material.color = Color.black;
            return;
        }
        else if (wallLife == 1){
            wall.GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (wallLife == 2)
        {
            wall.GetComponent<Renderer>().material.color = Color.green;

        }
        else if (wallLife == 3)
        {
            wall.GetComponent<Renderer>().material.color = Color.cyan;
        }
        else if (wallLife == 4)
        {
            wall.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (wallLife == 5)
        {
            wall.GetComponent<Renderer>().material.color = Color.red;
        }
     }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionStay(Collision col)
    {
        if (Input.GetMouseButton(0) && wallLife > 0.0)
        {
            wallLife -= 0.05f;
        }
        else if(wallLife <= 0)
        {

            if (col.gameObject.name != character.name || !isDestroyable)
                return;

            if (shouldBeWholeDestroyed())
            {
                GameObject clone = Instantiate(destroyedWall, transform.position + offset, transform.rotation);
                clone.transform.localScale = gameObject.transform.localScale;
                clone.transform.Rotate(rotation);
            }
            else
            {
                float holeWidthLocal = 0.95f * HOLE_WIDTH / Mathf.Max(transform.lossyScale.x, transform.lossyScale.z);

                float collisionPoint = getCollisionPoint(col);
                float normalizedCollisionPoint = collisionPoint;

                if (collisionPoint - holeWidthLocal / 2 <= -0.5f)
                {
                    normalizedCollisionPoint = -0.5f + holeWidthLocal / 2;
                }
                else if (collisionPoint + holeWidthLocal / 2 >= 0.5f)
                {
                    normalizedCollisionPoint = 0.5f - holeWidthLocal / 2;
                }

                float holeBegin = normalizedCollisionPoint - holeWidthLocal / 2;
                float holeEnd = normalizedCollisionPoint + holeWidthLocal / 2;

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

        return newWall;
    }

    GameObject instansiateDestroyedWall(float begin, float end)
    {
        Vector3 position = getNewPosition(begin, end);
        Vector3 scale = getNewScale(begin, end) * 0.99f;

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
