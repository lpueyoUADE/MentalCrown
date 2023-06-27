using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemigo : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    public GameObject bullet;

    public Personaje personaje;

    public Transform aim;

    private Animator anim;

    private LayerMask layerMask;

    public float velocidad;
    public int vida;
    private float time;
    private int nextAction;
    private float x, y;

    string[] estados = { "pasivo", "agresivo", "amistoso" };
    int estado = 0;

    public float umbral;

    AudioSource source;
    public AudioClip clip;
    public AudioClip die;

    public static UnityEvent onDieEnemy = new UnityEvent();

    private int generateRandRange()
    {
        return 30 + Random.Range(-5, 10);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        source = GetComponent<AudioSource>();

        x = 0;
        y = 0;
        time = 0;
        nextAction = generateRandRange();

        // Hago que ignore la propia layer del enemigo.
        layerMask = 1 << gameObject.layer;
        layerMask = ~layerMask;
    }

    private void moverse()
    {
        x = Random.Range(-1, 2);
        y = Random.Range(-1, 2);
    }

    private void atacar()
    {
        x = 0f;
        y = 0f;

        Vector3 diff = personaje.transform.position - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, diff, Mathf.Infinity, layerMask);

        if (hit.collider.gameObject.GetComponent<Personaje>()) {

            diff.z = 0;
            diff.Normalize();

            aim.right = diff;
            Instantiate(bullet, transform.position + diff * 1.5f, aim.rotation);

            source.pitch = 1 + Random.Range(-0.1f, 0f);
            source.volume = 0.6f + Random.Range(-0.1f, 0.1f);
            source.PlayOneShot(clip);
            Debug.DrawRay(transform.position, diff, Color.green);
        } else
        {
            Debug.DrawRay(transform.position, diff, Color.red);
        }

    }

    private void esperar()
    {
        x = 0f;
        y = 0f;
    }

    public bool entre(int valor, int desde, int hasta){
        return desde <= valor && valor < hasta;
    }
    private void FixedUpdate()
    {
        if (estaMuerto())
        {
            return;
        }

        time += 1;

        updateEstado();

        if (time == nextAction)
        {
            int nuevaAccion = Random.Range(0, 100);

            if(estado == 0) // pasivo
            {
                if (entre(nuevaAccion, 1, 80))
                {
                    moverse();
                }

                if (entre(nuevaAccion, 80, 85))
                {
                    atacar();
                }

                if (entre(nuevaAccion, 85, 100))
                {
                    esperar();
                }
            }

            if (estado == 1) // Agresivo
            {
                if (entre(nuevaAccion, 1, 20))
                {
                    moverse();
                }

                if (entre(nuevaAccion, 20, 60))
                {
                    atacar();
                }

                if (entre(nuevaAccion, 60, 100))
                {
                    esperar();
                }
            }

            if (estado == 2) // amistoso
            {
                if (entre(nuevaAccion, 1, 30))
                {
                    moverse();
                }

                if (entre(nuevaAccion, 30, 100))
                {
                    esperar();
                }
            }
            time = 0;
            nextAction = generateRandRange();
        }

        mover();
    }

    public void dañar(int daño)
    {
        if (estaMuerto())
        {
            return;
        }
        
        vida -= daño;

        if (vida <= 0)
        {
            matar();
        } else
        {
            anim.SetTrigger("DañadoTrigger");
        }
    }
    public void matar()
    {
        x = 0f;
        y = 0f;

        mover();

        anim.SetTrigger("MuertoTrigger");
        bc.enabled = false;

        source.PlayOneShot(die);

        onDieEnemy.Invoke();
    }

    public void mover()
    {
        Vector2 direccionVelocidad = new Vector2(x, y).normalized * velocidad;

        //rb.velocity = direccionVelocidad;
        rb.AddForce(direccionVelocidad);
        anim.SetFloat("Movimiento", rb.velocity.magnitude);
    }
    public bool estaMuerto()
    {
        return vida <= 0;
    }
    public bool estaVivo()
    {
        return !estaMuerto();
    }

    public void updateEstado()
    {
        if (personaje.estaVivo())
        {
            estado = Vector3.Distance(transform.position, personaje.transform.position) > umbral ? 0 : 1;

        } else {
            estado = 2;
        }
    }
}
