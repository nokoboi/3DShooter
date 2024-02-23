using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject efectoExplosion;
    public EnemyShoot enemy;
    //private void OnCollisionEnter(Collision collision)
    //{
    //   if (collision.gameObject.CompareTag("Enemigo"))
    //    {
    //        if(enemy.healthPoints > 0)
    //        {
    //            enemy.healthPoints--;
    //        }
    //        else
    //        {
    //            Instantiate(efectoExplosion, transform.position, transform.rotation);

    //            Destroy(collision.gameObject);
    //            Destroy(this, 1);
    //        }

    //    }
    //}
}
