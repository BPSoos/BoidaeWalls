using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenu : MonoBehaviour
{
    public PlayerUI playerClass;
    public List<PlayerUI> addedPlayersList;
    private int first_empty_slot = 0;
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex  + 1);
    }

    void Start(){
        for (int i = 0; i < 8; i++){        
        AddPlayer();
        }
    }

    public void QuitGame(){
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void AddPlayer(){
        PlayerUI new_player_object = Instantiate(playerClass);
        new_player_object.transform.SetParent(this.transform, false);
        new_player_object.UpdatePosition(addedPlayersList.Count);
        new_player_object.player_count = addedPlayersList.Count;
        addedPlayersList.Add(new_player_object);
    }

    public void AddPlayerButton(){
        for (int i = 0; i < 8; i++){
            if (!(addedPlayersList[i].gameObject.activeSelf) &&
                            (addedPlayersList[i].player_y_slot == first_empty_slot)){
                addedPlayersList[i].gameObject.SetActive(true);
            }
        }        
        first_empty_slot ++;
        Debug.Log("first_empty_slot: " + first_empty_slot.ToString());
    }

    public void ChooseColor(string color_chosen){
    Debug.Log(color_chosen);
    }

    public void PlayerRemoved(int player_slot, int player_count){
        int last_slot = player_slot;
        for (int i = 0; i < 8; i++){
            if (addedPlayersList[i].player_y_slot > player_slot){
                 addedPlayersList[i].UpdatePosition(addedPlayersList[i].player_y_slot - 1);
            }
        }
        addedPlayersList[player_count].UpdatePosition(7);
        first_empty_slot --;
        Debug.Log("first_empty_slot: " + first_empty_slot.ToString());

    }
}
