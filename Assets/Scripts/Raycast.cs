using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Vector2 MousePosition;

    void Update()
    {
        Vector2 startPoint = transform.position;
        Vector2 direction = MousePosition - startPoint;
        //RaycastHit2D hit = Physics2D.Raycast(startPoint, direction);
        Debug.DrawRay(startPoint, direction, Color.red, 0.2f);
    }
}
