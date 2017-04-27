using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaMuerte : MonoBehaviour {

    //Referencias
    private Player player;

    // Use this for initialization
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    
    //Comprobar que el Jugador entra en la zona.
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.currentHealth = 0;
        }
    }
}