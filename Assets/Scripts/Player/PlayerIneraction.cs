using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerIneraction : MonoBehaviour
{
    public TextMeshProUGUI textAmmo;
    public TextMeshProUGUI textVidas;
    public TextMeshProUGUI textGrenades;
    public bool vulnerable = true;
    // Start is called before the first frame update
    void Start()
    {

        textVidas.text = GameManager.instance.vidas.ToString();
        textGrenades.text = GameManager.instance.grenades.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AmmoBox"))
        {
            GameManager.instance.gunAmmo += other.GetComponent<AmmoBox>().ammo;
            textAmmo.text = GameManager.instance.gunAmmo.ToString();
            Destroy(other.gameObject);
        }
        //if (other.gameObject.CompareTag("Enemigo") && vulnerable)
        //{
        //    Debug.Log("Colision con enemigo");
        //    PerderVida();
        //}
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemigo") && vulnerable)
        {
            Debug.Log("Colision con enemigo");
            PerderVida();
        }

        if (hit.gameObject.CompareTag("final"))
        {
            Debug.Log("Collision con el final");
            SceneManager.LoadScene("MenuScene");
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BulletEnemigo"))
        {
            Debug.Log("perdiendo vidas");
            PerderVida();
        }

        if (collision.gameObject.CompareTag("final"))
        {
            Debug.Log("Collision con el final");
            SceneManager.LoadScene("MenuScene");

        }
    }

    void PerderVida()
    {
        vulnerable = false;
        GameManager.instance.vidas -= 1;
        if (GameManager.instance.vidas <= 0)
        {
            Muerto();
        }
        else
        {
            textVidas.text = GameManager.instance.vidas.ToString();
            StartCoroutine(VulnerableCorutine());

        }
    }

    private void Muerto()
    {
        GameManager.instance.muerto = true;
        textVidas.text = "0";
        Transform transformacion = transform;
        float anguloX = -129f;
        transformacion.eulerAngles = new Vector3(anguloX, transformacion.eulerAngles.y, transformacion.eulerAngles.z);
        StartCoroutine(Reiniciar());
    }

    IEnumerator Reiniciar()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator VulnerableCorutine()
    {
        yield return new WaitForSeconds(3);
        vulnerable = true;
    }



}
