using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float fallDelay;


	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();	
	}
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2d.isKinematic = false;

        GetComponent<Collider2D>().isTrigger = true; //para que caida al infinito
        yield return (0);
    }
}