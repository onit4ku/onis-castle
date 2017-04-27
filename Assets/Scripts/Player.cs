using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    //Floats

    [SerializeField]
    private float maxSpeed = 4f;
    [SerializeField]
    private float speed = 60f;
    [SerializeField]
    private float jumpPower = 400f;

    //Booleans
    public bool grounded;
    public bool canDoubleJump;
    public bool facingRight = true;
    public bool wallSliding;
    public bool wallCheck;

    //Referencias
    private Rigidbody2D rb2d;
    private Animator anim;
    private gameMaster gm;
    public Transform wallCheckPoint;
    public LayerMask wallLayerMask;

    //Stats
    [SerializeField]
    public int currentHealth;
    [SerializeField]
    public int maxHealth = 5;

	// Inicialización
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        currentHealth = maxHealth; //Vida a tope al inicio
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
    }
	
	// Se llama una vez por frame
	void Update () {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        //Control del sprite
        //Movimiento Horizontal izquierda
        if (Input.GetAxis("Horizontal") < -0.1f) {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }

        //Movimiento Horizontal derecha
        if (Input.GetAxis("Horizontal") > 0.1f) {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        
        //Control del salto
        if (Input.GetButtonDown("Jump")) {
            if (grounded) {
                rb2d.AddForce(Vector2.up * jumpPower);
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce((Vector2.up * jumpPower));
                    //rb2d.AddForce((Vector2.up * jumpPower) / 1.5f); //la potencia del segundo salto será reducida  1.5f
                }
            }
        }

        //Control vida
        if (currentHealth < 1)
        {
            Die();
        }

        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }

        //paredes
        if (!grounded)
        {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);
            if(facingRight && Input.GetAxis("Horizontal") > 0.1f || !facingRight && Input.GetAxis("Horizontal") < 0.1f)
            {
                if (wallCheck)
                {
                    HandleWallSliding();
                }
            }
        }

        if(!wallCheck || grounded)
        {
            wallSliding = false;
        }

    }
    //Control del wall slide
    void HandleWallSliding()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.7f);
        wallSliding = true;

        if (Input.GetButtonDown("Jump"))
        {
            if (facingRight)
            {
                rb2d.AddForce(new Vector2(-1, 1) * jumpPower);
            }
            else
            {
                rb2d.AddForce(new Vector2(1, 1) * jumpPower);
            }
        }
    }

    private void FixedUpdate() {

        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        float h = Input.GetAxis("Horizontal");

        //Fake friction, ease velocity x del jugador
        if (grounded) { 
            rb2d.velocity = easeVelocity;
        }
        //Mueve al jugador
        if (grounded)
        {
            rb2d.AddForce((Vector2.right * speed) * h);
        }
        else
        {
            rb2d.AddForce((Vector2.right * speed / 2) * h);
        }

        //Limite de la velocidad del jugador
        if (rb2d.velocity.x > maxSpeed) {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed) {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }

	//Cuando la vida llega a 0, reiniciaremos la escena actual
    void Die()
    {
        //Reiniciar el nivel
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	//Funcion para recibir el daño
	public void Damage(int dmg){
		currentHealth -= dmg; //le restamos el damage a la vida actual
		gameObject.GetComponent<Animation>().Play("Dmg_Hero");
	}

	//golpe al recibir el daño
	public IEnumerator KnockBack(float knockDuration, float knockBackPower, Vector3 knockBackDirection){
		float timer = 0;
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);

		while (knockDuration > timer) {
			timer += Time.deltaTime;
			rb2d.velocity = new Vector2 (0, 0); 
			rb2d.AddForce(new Vector3(knockBackDirection.x * - 100, knockBackPower, transform.position.z));
		}
		yield return 0; //cuando la condición del while termine, detenemos el IEnumerator
	}

    //Coleccionar puntos
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Soul"))
        {
            Destroy(collision.gameObject);
            gm.souls += 100;
        }
    }
}