using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	private bool attacking = false;

	private float attackTimer = 0;
	private float attackCd = 0.8f; //Cooldown

	public Collider2D attackTrigger;   

	private Animator anim;
     
	void Awake() {
		anim = gameObject.GetComponent<Animator>();

		attackTrigger.enabled = false;
	}
	
	void Update () {
        //Tecla para el ataque
		if (Input.GetKeyDown("f") && !attacking) { 
 			attacking = true;
			attackTimer = attackCd;

			attackTrigger.enabled = true; //se activa el collider del AttackTrigger
		}

		if (attacking) {
		
			if (attackTimer > 0) {
				attackTimer -= Time.deltaTime;
			} else {
				attacking = false;
				attackTrigger.enabled = false;
			}
			anim.SetBool ("Attacking", attacking); //animación del ataque, para que se active en el animator
		}
	}
}