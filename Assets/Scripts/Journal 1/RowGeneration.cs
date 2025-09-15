using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RowGeneration : MonoBehaviour
{
    public Button generate;
    public TMP_InputField input;
    int rows;
    bool activator = false; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        generate.onClick.AddListener(Listener);

    }

    // Update is called once per frame
    void Update()
    {

        int.TryParse(input.text, out rows);

        if (activator == true)
        {

            DrawSquare(rows); 

        }

    }

    void DrawSquare(int rows)
    {

        for (int i = 0; i < rows; i++)
        {
            
            int r = i*2;

            Debug.DrawLine(new Vector2(-1f + r, 1f), new Vector2(-1f + r, -1f), new Color(255, 255, 255));
            Debug.DrawLine(new Vector2(-1f + r, -1f), new Vector2(1f + r, -1f), new Color(255, 255, 255));
            Debug.DrawLine(new Vector2(1f + r, -1f), new Vector2(1f + r, 1f), new Color(255, 255, 255));
            Debug.DrawLine(new Vector2(1f + r, 1f), new Vector2(-1f + r, 1f), new Color(255, 255, 255));
        }
 
    }

    void Listener()
    {
        activator = true; 
    }
}
