using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioArma : MonoBehaviour
{
    public GameObject[] arma;


    // Update is called once per frame
    void Update()
    {
        CambiarArma();
    }

    private void CambiarArma()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < arma.Length; i++)
            {
                arma[i].SetActive(false);
            }
            arma[0].SetActive(true);
            GameManager.instance.tipoDeArma = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i < arma.Length; i++)
            {
                arma[i].SetActive(false);
            }
            arma[1].SetActive(true);
            GameManager.instance.tipoDeArma = 2;
        }
    }
}
