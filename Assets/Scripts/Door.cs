using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour {

    //Nivel que queremos cambiar en el inspector de la puerta.
    public int LevelToLoad;
    //Referencia al gameMaster
    private gameMaster gm;
    //Detectar si el jugador entra en el campo de la puerta, para mostrar el mensaje en el HUD.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SavedSouls(); //guardar las almas antes de que cambiemos de nivel
            gm.InputText.text = ("[E] to enter.");
            if (Input.GetKeyDown("e"))
            {
                SceneManager.LoadScene(LevelToLoad); //cargar la escena deseada 
                //la siguiente escena tiene que tener un gamemaster para que que cargue las almas obtenidas en el anterior
            }
        }
    }
    //Que se muestre el mensaje mientras seguimos en el campo de la puerta.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown("e"))
            {
                SavedSouls();
                SceneManager.LoadScene(LevelToLoad);
            }
        }
    }
    //Borrar el texto una vez el player salga de la puerta
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gm.InputText.text = ("");
        }
    }

    void SavedSouls()
    {
        PlayerPrefs.SetInt("Souls", gm.souls);
    }

    // Use this for initialization
    void Start () {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
	}
}