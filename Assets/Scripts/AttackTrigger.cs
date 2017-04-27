using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    //Daño base del personaje
	public int Damage = 2;

	private void OnTriggerEnter2D(Collider2D collision)
	{
        //Haremos daño al que tenga el tag "Enemigo"
		if (collision.isTrigger == true &&  collision.CompareTag("Enemigo")){
            StartCoroutine(DmgWait());
            collision.SendMessageUpwards ("Damage", Damage);
        }
	}

    private IEnumerator DmgWait()
    {
        yield return new WaitForSeconds(3);
    }
}