using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Personaje : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private Animator anim;

    public static int maxVida = 30;
    public int vida;
    public float velocidad;

    AudioSource source;
    public AudioClip step;
    public AudioClip die;

    public static UnityEvent OnHit = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (estaMuerto())
        {
            return;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direccionVelocidad = new Vector2(x, y).normalized * velocidad;
        rb.AddForce(direccionVelocidad);

        if (rb.velocity.magnitude > 0.5)
        {
            if (!source.isPlaying && (x != 0 || y != 0))
            {
                source.PlayOneShot(step);
            }
        }
        
        anim.SetFloat("Movimiento", rb.velocity.magnitude);

        // espejo el personaje en funcion del mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int flip = transform.position.x > mousePos.x ? 180 : 0;
        transform.localRotation = Quaternion.Euler(0, flip, 0);
    }

    IEnumerator DieCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("GameOverMenu");
    }
    public void dañar(int daño)
    {
        if (estaMuerto())
        {
            return;
        }

        vida -= daño;

        string hitTrigger;

        if (estaVivo())
        {
            hitTrigger = "DañadoTrigger";
        } else
        {
            hitTrigger = "MuertoTrigger";
            cc.enabled = false;
            rb.velocity = Vector2.zero;
            source.PlayOneShot(die);

            StartCoroutine(DieCoroutine(die.length + 0.3f));
        }

        anim.SetTrigger(hitTrigger);
        OnHit.Invoke();
    }

    public void Curar(int salud)
    {
        vida += salud;
    }
    public bool estaMuerto()
    {
        return vida <= 0;
    }

    public bool estaVivo()
    {
        return !estaMuerto();
    }
}
