using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestFallingController : MonoBehaviour
{
    private float rotation;
    private float startingPosX;
    private float startingPosY;
    private Rigidbody2D rd;
    private void Start()
    {
        recalcValues();
        rd = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (transform.position.y < -15)
        {
            transform.position = new Vector3(startingPosX, startingPosY, 0);
            recalcValues();
            rd.velocity = Vector2.zero;
        }
    }

    private void recalcValues()
    {
        rotation = Random.Range(0.1f, 0.5f);
        startingPosX = Random.Range(-5, 5);
        startingPosY = Random.Range(10, 5);
    }
}
