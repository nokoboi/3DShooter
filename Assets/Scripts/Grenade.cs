using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Grenade : MonoBehaviour
{
    public float delay = 2f;
    private float countdown;
    public float radio = 5f;
    public float fuerzaExplosion = 70f;
    private bool exploded = false;

    //Sonido
    public AudioSource shotAudioSource;
    public AudioClip shotShound;


    //Efectos de explosion
    public GameObject efectoExplosion;
    private GameObject explosion;

    private void Awake()
    {
        //cargar sonido
        shotAudioSource=GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown<=0 && !exploded)
        {
            Exploded();
            exploded = true;
        }
        if(exploded && !shotAudioSource.isPlaying)
        {
            Destroy(explosion);
            Destroy(gameObject);
        }
    }

    private void Exploded()
    {
        shotAudioSource.PlayOneShot(shotShound);
        explosion= Instantiate(efectoExplosion, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radio);
        foreach(Collider objeto in colliders)
        {
            Rigidbody rb = objeto.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(fuerzaExplosion,transform.position,radio);
            }
        }

        //Destruye la granada pero el sonido sigue porque es el padre
        Destroy(transform.GetChild(0).gameObject);

    }
}
