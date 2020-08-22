using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenu : MonoBehaviour
{
    public PlayerUI playerClass;
    public List<PlayerUI> addedPlayersList;
    private int first_empty_slot = 0;
    private bool initialized_first_player = false;
    public static int test_player_amount = 4;
    public void PlayGame(){
        for (int i =0; i < test_player_amount; i++){
        Player add_me = new Player(addedPlayersList[i].player_count, PlayerData.basic_color_dict[addedPlayersList[i].current_color]);
        PlayerData.game_players.Add(add_me);
        Debug.Log(PlayerData.game_players[i].player_id.ToString() + " player added with color " + PlayerData.game_players[i].player_color.ToString());
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex  + 1);
    }

    void Update() {
        if (initialized_first_player)
            return;
        for (int i = 0; i < test_player_amount; i++)
        {
            AddPlayerButton();
            //PlayerData.game_players.Add(new PlayerData.Player(i, new Color32(55, 169, 64, 255)));
        }
        initialized_first_player = true;
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
        if (first_empty_slot < 8){
            for (int i = 0; i < 8; i++){
                if (!(addedPlayersList[i].gameObject.activeSelf) &&
                                (addedPlayersList[i].player_y_slot == first_empty_slot)){
                    addedPlayersList[i].gameObject.SetActive(true);
                    addedPlayersList[i].ChangeInputFieldText("New Player " + (first_empty_slot + 1).ToString());
                }
            }
            if (first_empty_slot == 0)
                transform.Find("PlayButton").gameObject.SetActive(true);
            first_empty_slot ++;
        }else{
            Debug.Log("Max Players reached");
        }
    }

    public void PlayerColorSelected(bool set_state, string color_of_button, int player_index_who_choose){
        for (int i = 0; i < 8; i++){
            if (i != player_index_who_choose){
                addedPlayersList[i].SetColorAvailable(set_state, color_of_button);
            }
        }
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
        if (first_empty_slot == 0)
            transform.Find("PlayButton").gameObject.SetActive(false);
    }
}
