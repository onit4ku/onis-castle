using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour {

	//Stats
	public int currentHealth;
	public int maxHealth = 2;

	public bool isDead = false;
 //   bool invul = false;
	private gameMaster gm;

	private void Awake() {
		gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
	}


	// Use this for initialization
	void Start () {
		currentHealth = maxHealth; //Vida a tope al inicio
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		// Si el enemigo tiene 0 de vida o menos puntos y no se ha muerto aún...
		if (currentHealth < 1 && !isDead) {
			// ... llama a la función para que muera
			Death ();
		}
		if (isDead) {
			Destroy(gameObject);
		}
	}

	//Funcion para recibir el daño
	public void Damage(int dmg){
		currentHealth -= dmg; //le restamos el damage a la vida actual
		gameObject.GetComponent<Animation>().Play("Dmg_Hero");

      //  invul = true; //is invulnerable
        StartCoroutine(DmgWait());
    }


	void Death()
	{
		// Increase the score by 150 points
		gm.souls += 150;

		// Set dead to true.
		isDead = true;

	}
   
    private IEnumerator DmgWait()
    {
        gameObject.GetComponent<Animation>().Play("Dmg_Hero"); //play hit animation
        //animator.SetTrigger("Hit"); 

        yield return new WaitForSeconds(1); //invul time
       // invul = false;
    }
}