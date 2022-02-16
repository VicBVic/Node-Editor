using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{

    public GameObject Node;

    private void Start()
    {
        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
        if(boxCollider != null)
        {
            Vector2 newSize;
            newSize.y=Camera.main.orthographicSize * 2;
            newSize.x = newSize.y * Camera.main.aspect;
            boxCollider.size = newSize;
        }
    }

    private void OnMouseDown()
    {
        print("yey");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(Node, mousePos, Quaternion.identity); 
    }
}
