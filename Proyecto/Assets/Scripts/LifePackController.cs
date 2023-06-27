using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifePackController : MonoBehaviour
{
    public int salud;
    public bool usado;
    AudioSource source;
    public AudioClip clip;

    private Animator anim;
    public static UnityEvent OnUsedMedikit = new UnityEvent();

    private void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Personaje personaje = collision.gameObject.GetComponent<Personaje>();
        if (personaje && personaje.estaVivo() && personaje.vida + salud <= Personaje.maxVida && !usado)
        {
           personaje.Curar(salud);

            salud = 0;
            usado = true;

            anim.SetBool("usado", usado);
            source.PlayOneShot(clip);
            OnUsedMedikit.Invoke();
        }
    }
}
