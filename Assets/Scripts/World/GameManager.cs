using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance
    {
        get;
        private set;
    }

    public int gunAmmo = 10;
    public int vidas = 10;
    public int grenades = 5;
    public int tipoDeArma = 2; //1 escopeta, 2 granada
    public bool muerto= false;

    private void Awake()
    {
        instance = this;
    }
}
