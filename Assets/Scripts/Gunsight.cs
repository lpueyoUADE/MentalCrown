using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunsight : MonoBehaviour
{
    private Vector3 posicionPuntero;
    
    void Update()
    {
        posicionPuntero = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicionPuntero.z = 0;
        this.transform.position = posicionPuntero;
    }
}
