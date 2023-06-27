using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 0.1f, Space.Self);
    }
}
