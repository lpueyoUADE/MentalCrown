using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public GameObject personaje;
    public float offsetX, offsetY;

    // Update is called once per frame
    void Update()
    {
        Vector2 posicionMario = personaje.transform.position;
        transform.position = new Vector3(posicionMario.x + offsetX, posicionMario.y + offsetY, transform.position.z);
    }
}
