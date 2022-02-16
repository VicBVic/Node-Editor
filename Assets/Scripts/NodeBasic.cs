using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NodeBasic : MonoBehaviour, INode
{

    public const int maxConnections = 30;

    public GameObject ConnectionLine;

    public Connection[] connections;
    private GameObject[] connectionLines = new GameObject[maxConnections];
    public float maxSpeed = 10.0f;
    public float maxSteeringSpeed = 10.0f;


    private bool dragging;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        var goodConnections = from element in connections
                      where (element.target != null)
                      select element; 
        connections= goodConnections.ToArray();
        for(int i=0;i<connections.Length; i++)
        {
            connectionLines[i] = Instantiate(ConnectionLine);
            print(connectionLines[i]);
            ConnectionRenderer cr = connectionLines[i].GetComponent<ConnectionRenderer>();
            cr.connection = connections[i];
            cr.origin = gameObject;
            print("herE");
            //print(cr);
        }


    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (connections.Length > 0 && !dragging)
        {
            Vector3 wantedPositionChange = Vector3.zero;
            foreach (var connection in connections)
            {
                Vector3 distance = connection.target.transform.position - transform.position;
                Vector3 wantedDis = Vector3.zero;

                if (distance.magnitude < connection.minDistance)
                {
                    wantedDis = distance.normalized * (distance.magnitude - connection.minDistance);
                }
                if (distance.magnitude > connection.maxDistance)
                {
                    wantedDis = distance.normalized * (distance.magnitude - connection.maxDistance);
                }
                Vector3 wantedDisBiased = wantedDis.normalized * Mathf.Pow(wantedDis.magnitude, Mathf.Log(connection.attraction, 2));
                wantedPositionChange += wantedDisBiased;
            }
            wantedPositionChange/= connections.Length;
            Vector3 wantedVelocity = wantedPositionChange;
            Vector3 wantedSteering = wantedVelocity - velocity;
            Vector3 acceleration = Vector3.ClampMagnitude(wantedSteering, maxSteeringSpeed);
            acceleration.z = 0;

            velocity += acceleration * Time.fixedDeltaTime;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            
            transform.position += velocity * Time.fixedDeltaTime;
        }
        else if (dragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }

    private void OnMouseDown()
    {
        dragging = true;

    }

    private void OnMouseUp()
    {
        dragging=false;
    }
}
