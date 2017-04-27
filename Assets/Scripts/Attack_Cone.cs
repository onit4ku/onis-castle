using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Cone : MonoBehaviour {
    
    //Referencia
    public TurretAI turretAI;

    //Si el cono es hacia la izquierda o la derecha
    public bool isLeft = false; //lo marcamos en el inspector del collider

    private void Awake(){
        turretAI = gameObject.GetComponentInParent<TurretAI>();
    }
    //Detectar si el jugador entra se encuentra en la izquierda o derecha
    private void OnTriggerStay2D(Collider2D col){
        
        if (col.CompareTag("Player"))
        {
            if (isLeft){
                turretAI.Attack(false);
            }
            else{
                turretAI.Attack(true);
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}