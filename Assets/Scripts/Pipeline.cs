using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour
{

    public Vector3 mousePos;
    public List<Vector3> mousePositions = new List<Vector3>();
    public float magnitude;
    public float totalMagnitude; 

    public float time;
    public float formattedTime; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            totalMagnitude = 0;

            time += Time.deltaTime;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float.TryParse(time.ToString("0.00"), out formattedTime);

            if ((formattedTime*200)%20f == 0)
            {

                mousePositions.Add(mousePos);

            }

            for (int i = 0; i < mousePositions.Count - 1; i++)
            {
                Debug.DrawLine(mousePositions[i], mousePositions[i+1]);
                magnitude = Mathf.Sqrt(Mathf.Pow(mousePositions[i].x + mousePositions[i+1].x, 2) + Mathf.Pow(mousePositions[i].y + mousePositions[i + 1].y, 2));
                totalMagnitude += magnitude; 

            }

        }
        else
        {
            if (totalMagnitude > 0)
            {
                print(totalMagnitude);

            }
            time = 0;
            mousePositions.Clear();
            totalMagnitude = 0;

        }
    }

}
