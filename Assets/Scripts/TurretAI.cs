using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {

    //Integers
    public int currentHealth;
    public int maxHealth = 4;

    //Floats
    public float distance;
    public float wakeRange = 5;
    public float shootInterval = 0.7f;
    public float bulletSpeed = 6;
    public float bulletTimer;

    //Booleans
    public bool awake = false;
    public bool pointing = true; //lookingright
	public bool isDead = false;

    public GameObject bullet;
    public Transform target;

    public Animator anim;

    public Transform shootPointLeft;
    public Transform shootPointRight;

    private gameMaster gm;

    private void Awake() {
        anim = gameObject.GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
    }

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("Awake", awake);
        anim.SetBool("Pointing", pointing);

        RangeCheck();

        if(target.transform.position.x > transform.position.x)
        {
            pointing = true;
        }
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


    void RangeCheck(){
        distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < wakeRange)
        {
            awake = true;
            pointing = true;
        }

        if (distance > wakeRange)
        {
            awake = false;
            pointing = false;
           
        }
    }

    public void Attack(bool attackingRight){
        bulletTimer += Time.deltaTime;
        if (bulletTimer >= shootInterval){
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();

            if (!attackingRight){
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;
            }
     
            if (attackingRight){
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;
            }
        }
    }

	void Death()
	{
		// Increase the score by 150 points
		gm.souls += 150;

		// Set dead to true.
		isDead = true;
	}

    public void Damage(int damage)
    {
       currentHealth -= damage;
       gameObject.GetComponent<Animation>().Play("Dmg_Hero");
    }
}