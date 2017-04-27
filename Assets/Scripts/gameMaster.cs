using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameMaster : MonoBehaviour {

    //Moneda del juego
    public int souls = 0;

    //Texto de la moneda
    public Text soulsText;

    //Texto de entrada en el HUD
    public Text InputText;

	// Inicialización
	void Start () {
        if (PlayerPrefs.HasKey("Souls"))
        {
			if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0)){

				//if(Application.loadedLevel == 0){
						
				
                PlayerPrefs.DeleteKey("Souls");
                souls = 0; //reiniciar las almas si es el primer nivel
            }
            else
            {
                souls = PlayerPrefs.GetInt("Souls");
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        soulsText.text = ("" + souls);
	}
}