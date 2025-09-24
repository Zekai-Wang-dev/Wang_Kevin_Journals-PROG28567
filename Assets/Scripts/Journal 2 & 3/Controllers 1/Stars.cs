using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;

    public Vector3 drawPoint; 

    public float time; 
    private int count; 

    void Start() {

        time = 0f; 
        count = 0; 

    }

    // Update is called once per frame
    void Update()
    {

        DrawConstellation();

    }

    public void DrawConstellation() {

        time += Time.deltaTime / drawingTime; 

        drawPoint = Vector3.Lerp(starTransforms[count].position, starTransforms[count+1].position, time);
        
        if (time > 1) {
               
            count += 1; 

            if (count >= starTransforms.Count - 1) {

                count = 0; 

            }

            time = 0f; 

        }

        Debug.DrawLine(starTransforms[count].position, drawPoint);

        for (int i = 0; i < count; i++) {

            Debug.DrawLine(starTransforms[i].position, starTransforms[i+1].position); 

        }

    }

}
