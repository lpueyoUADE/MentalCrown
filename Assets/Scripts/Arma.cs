using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Arma : MonoBehaviour
{
    public GameObject bullet;
    public int balas;
    public float explosionRadius;
    public float explosionPower;
    public float explosionCoolDown;
    private float explosionTimeStamp;
    public LayerMask explosionLayerMask;

    private SpriteRenderer sprite;

    AudioSource source;
    public AudioClip clip;
    public AudioClip clipEmpty;
    public AudioClip explosionClip;

    public ParticleSystem shotPs;
    public ParticleSystem blowPs;

    Personaje personaje;

    public static int maxBalas = 255;

    public static UnityEvent OnShot = new UnityEvent();

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        personaje = GetComponentInParent<Personaje>();
        explosionTimeStamp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(personaje.estaMuerto())
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 diff = mousePos - transform.position;

        diff.z = 0;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        int flip = 0;
        int flipSign = 1;

        // Espejo y roto el arma
        if (transform.position.x > mousePos.x)
        {
            flip = 180;
            flipSign = -1;
        }

        transform.rotation = Quaternion.Euler(0f, flip, flipSign * rot_z + flip);

        // Si el arma está apuntando hacia arriba, lo mando al fondo
        int soritngOrder = transform.rotation.z > 0.1 && transform.rotation.z < 1 ? -1 : 1;
        sprite.sortingOrder = soritngOrder;

        if (Input.GetButtonDown("Fire1"))
        {
            if (tieneBalas())
            {
                // diff lo uso para instanciar la bala fuera del centro del arma.
                // el 2 está harcodeado a pleno.
                balas--;
                Instantiate(bullet, transform.position + diff * 1.5f, transform.rotation);
            
                source.pitch = 1 + Random.Range(-0.1f, 0.1f);
                source.volume = 1 + Random.Range(-0.1f, 0.1f);
                source.PlayOneShot(clip);
                source.PlayDelayed(0.5f);
                
                shotPs.Play();
            
                OnShot.Invoke();

            } else {
                source.PlayOneShot(clipEmpty);
            }
        }

        if (Input.GetButtonDown("Fire2") && canShootFire2())
        {
            setExplosionTimeStamp();
            source.PlayOneShot(explosionClip);
            blowPs.Play();

            Vector3 explosionPos = transform.position;
            var colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosionLayerMask);

            foreach (Collider2D hit in colliders)
            {
                Rigidbody2D rb = hit.gameObject.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    Vector3 dir = rb.transform.position - transform.position;
                    rb.AddForce(dir.normalized * explosionPower, ForceMode2D.Impulse);
                }
            }
        }
    }

    public bool tieneBalas()
    {
        return balas > 0;
    }

    public void agregarBalas(int cantidad)
    {
        balas += cantidad;
    }

    private void setExplosionTimeStamp()
    {
        explosionTimeStamp = Time.time + explosionCoolDown;
    }

    private bool canShootFire2()
    {
        return explosionTimeStamp <= Time.time;
    }
}
