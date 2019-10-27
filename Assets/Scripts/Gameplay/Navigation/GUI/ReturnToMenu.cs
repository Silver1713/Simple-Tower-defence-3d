using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour, IPointerClickHandler
{
    GameManager manager = GameManager.gameManager; //Gets game manager.
    
    
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) // Onclick
    {
        Time.timeScale = 1; //Set timescale to normal.
        SceneManager.LoadScene("Main Menu"); //Go back to main menu
        
        transform.parent.parent.gameObject.SetActive(false); //Hide the UI
        
        return;
    }

   
}
