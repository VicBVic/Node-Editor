using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionRenderer : MonoBehaviour
{
    public Connection connection;
    public GameObject origin;
    public float baseWidth = 1.0f;
    public float maxWidth = 2.0f;
    public float z = 5.0f;

    public Gradient contractColor;
    public Gradient expandColor;
    public Gradient chillColor;


    // Start is called before the first frame update
    void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            lineRenderer.startWidth = 0.5f;
            lineRenderer.endWidth = 1.0f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        if(lineRenderer != null)
        {
            Vector3 a = origin.transform.position;
            a.z = z;
            Vector3 b = connection.target.transform.position;
            b.z = z;
            lineRenderer.SetPosition(0, a);
            lineRenderer.SetPosition(1, (a+b)/2);
            float distance = Vector3.Distance(a, b);
            
            lineRenderer.widthMultiplier = baseWidth;
            lineRenderer.colorGradient = chillColor;


            if (distance < connection.minDistance)
            {
                lineRenderer.widthMultiplier -= (maxWidth - baseWidth) * (connection.minDistance-distance)/connection.minDistance;
                lineRenderer.colorGradient = expandColor;

            }
            if (distance > connection.maxDistance)
            {
                lineRenderer.widthMultiplier += (maxWidth - baseWidth) *  (distance -connection.maxDistance)/ distance;
                lineRenderer.colorGradient = contractColor;
            }

            lineRenderer.widthMultiplier *= connection.attraction;

        }
    }
}
