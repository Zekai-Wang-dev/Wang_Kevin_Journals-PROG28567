using UnityEngine;

public class SquareSpawner : MonoBehaviour
{

    Vector3 mousePos;
    Vector3 newMousePos;
    float mouseScrollData;
    float newMouseScrollData; 
    public float scale; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scale = 0.1f;
        mouseScrollData = 0; 
    }

    // Update is called once per frame
    void Update()
    {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        mouseScrollData += Input.mouseScrollDelta.y * scale; 

        if (Input.GetMouseButton(0))
        {
            print("Spawn");
            newMousePos = mousePos;
            newMouseScrollData = mouseScrollData; 

        }

        DrawSqaure(newMousePos, newMouseScrollData, 1);
        DrawSqaure(mousePos, mouseScrollData, 0.5f);

        print(mouseScrollData);

    }

    void DrawSqaure(Vector3 mousePos, float mouseScrollData, float transparency)
    {

        Debug.DrawLine(new Vector2(mousePos.x - 1f - mouseScrollData, mousePos.y + 1f + mouseScrollData), new Vector2(mousePos.x - 1f - mouseScrollData, mousePos.y - 1f - mouseScrollData), new Color(255, 255, 255, transparency));
        Debug.DrawLine(new Vector2(mousePos.x - 1f - mouseScrollData, mousePos.y + 1f + mouseScrollData), new Vector2(mousePos.x + 1f + mouseScrollData, mousePos.y + 1f + mouseScrollData), new Color(255, 255, 255, transparency));
        Debug.DrawLine(new Vector2(mousePos.x + 1f + mouseScrollData, mousePos.y + 1f + mouseScrollData) , new Vector2(mousePos.x + 1f + mouseScrollData, mousePos.y - 1f - mouseScrollData), new Color(255, 255, 255, transparency));
        Debug.DrawLine(new Vector2(mousePos.x + 1f + mouseScrollData, mousePos.y - 1f - mouseScrollData), new Vector2(mousePos.x - 1f - mouseScrollData, mousePos.y - 1f - mouseScrollData), new Color(255, 255, 255, transparency));


    }

}
