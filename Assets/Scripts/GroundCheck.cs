using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour
{
    //Referencias
    private Player player;

    //Inicialización
    private void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }
    
    //Comprobar la colision con el suelo
    void OnTriggerEnter2D(Collider2D col)
    {
        if (null == player)
        {
            Start();
        }

        player.grounded = true;
    }

    //Mientras esté tocando una colision con la base
    void OnTriggerStay2D(Collider2D col)
    {
        player.grounded =  true;
    }
    //Dejar de estar en el suelo
    void OnTriggerExit2D(Collider2D col)
    {
        player.grounded = false;
    }
}