using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletChest : MonoBehaviour
{
    public int balas;
    public bool usado;
    AudioSource source;
    public AudioClip clip;

    private Animator anim;
    public static UnityEvent OnUsedChest = new UnityEvent();

    private void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Personaje personaje = collision.gameObject.GetComponent<Personaje>();
        if (personaje && !usado)
        {
            Arma arma = personaje.GetComponentInChildren<Arma>();
            arma.agregarBalas(balas);

            balas = 0;
            usado = true;

            anim.SetBool("usado", usado);
            source.PlayOneShot(clip);
            OnUsedChest.Invoke();
        }
    }
}
