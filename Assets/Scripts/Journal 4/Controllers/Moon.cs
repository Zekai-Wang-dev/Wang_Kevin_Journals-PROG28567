using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;
    public Transform moonTransform;

    public Transform target;
    public float speed;
    public float radius;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {

        angle = 0; 
        
    }

    // Update is called once per frame
    void Update()
    {

        OrbitalMotion(radius, speed, target);

    }

    public void OrbitalMotion(float radius, float speed, Transform target)
    {

        if (angle < 360)
        {

            angle += Time.deltaTime * speed; 

        }
        else
        {

            angle = 0; 

        }

        float x = Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = Mathf.Sin(angle * Mathf.Deg2Rad);

        moonTransform.position = new Vector3(x * radius, y * radius, 0) + target.position;

    }

}
