using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.1f, 0.1f, 0, Space.Self);
    }
}
