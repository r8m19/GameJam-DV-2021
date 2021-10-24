using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    public LineRenderer line;
    public float vanishRate;

    Vector2 _startPos;
    Vector2 _endPos;
    Vector3 fullVector;
    Vector3 vector;

    private void Awake()
    {
        if(!line)
        line = GetComponent<LineRenderer>();
        Display(Vector2.left * 5.6f, Vector2.zero);
    }
    
    public void Display(Vector2 startPos, Vector2 endPos)
    {
        line.SetPositions(new Vector3[2] {startPos, endPos });
        _startPos = startPos;
        _endPos = endPos;
        fullVector = _endPos - _startPos;
        vector = fullVector;
    }

    private void Update()
    {
        vector -= fullVector * vanishRate/100;
        
        line.SetPosition(1, (Vector3)_startPos + vector);
        
        if (vector.magnitude <= 0.15f)
        {
            Destroy(gameObject);
        }  
    }
}
