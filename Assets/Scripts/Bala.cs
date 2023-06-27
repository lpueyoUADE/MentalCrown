using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad;
    public int da�o;

    public GameObject bulletDie;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemigo enemigo = collision.gameObject.GetComponent<Enemigo>();
        Personaje personaje = collision.gameObject.GetComponent<Personaje>();

        if (enemigo)
        {
            enemigo.da�ar(da�o);
        }

        if (personaje)
        {
            personaje.da�ar(da�o);
        }

        Instantiate(bulletDie, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        transform.position += transform.right * Time.deltaTime * velocidad;
    }
}
