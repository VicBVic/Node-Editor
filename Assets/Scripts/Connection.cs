using System;
using UnityEngine;

[Serializable] 
public struct Connection
{
    public GameObject target;
    public float attraction;
    public float minDistance;
    public float maxDistance;
}
