using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //Destruir los proyectiles cuando toquen algo.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true)
        {
            //Si es el jugador le hacemos daño.
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Player>().Damage(1);
            }
            Destroy(gameObject);
        }
    }
}