using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	//Array de corazones
	public Sprite[] HeartSprites;
	//Imagen que asocia los corazones en la UI
	public Image HeartUI;

	//Referencia al jugador
	private Player player;

	 void Start(){
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

	}

	 void Update(){
		HeartUI.sprite = HeartSprites [player.currentHealth];
	}
}