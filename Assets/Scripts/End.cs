using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colision con el final");
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Asylum");
    }
}
