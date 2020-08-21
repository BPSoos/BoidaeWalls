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
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex  + 1);
    }

    private bool all_colors_selected = false;
    private List<PlayerData> active_players_data = new List<PlayerData>();

    private string GetActivePlayersData(){
        string data_as_string = "";
        foreach(PlayerData player_data in active_players_data){
            data_as_string += player_data.id; 
            data_as_string += " player with name ";
            data_as_string += player_data.Player_name;
            data_as_string += " has color: ";
            data_as_string += player_data.Player_color;
            data_as_string +="    ";
        }
        return data_as_string;
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
        int id = 0;
        if (first_empty_slot < 8){
            for (int i = 0; i < 8; i++){
                if (!(addedPlayersList[i].gameObject.activeSelf) &&
                                (addedPlayersList[i].player_y_slot == first_empty_slot)){
                    addedPlayersList[i].gameObject.SetActive(true);
                    addedPlayersList[i].ChangeInputFieldText("New Player " + (first_empty_slot + 1).ToString());
                    id = i;
                }
            }
            active_players_data.Add(new PlayerData("", ("New Player " + (first_empty_slot + 1).ToString()), id));
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
        for (int i = 0; i < active_players_data.Count; i++){
            if (active_players_data[i].id == player_index_who_choose){       
                active_players_data[player_index_who_choose].SetColorBasedOnButtonClick(set_state, color_of_button);
                Debug.Log(GetActivePlayersData());
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
        for (int i = 0; i < active_players_data.Count; i++){
            if (active_players_data[i].id == i){
                Debug.Log("Removing player: " + i.ToString());
                active_players_data.RemoveAt(i);
                Debug.Log(GetActivePlayersData());
            }
        }
        first_empty_slot --;
    }
}

public class PlayerData
{   
    public int id;
    private string player_color;
    public string Player_color 
    {
        get { return player_color; }
        set { player_color = value;}
    }
    private string player_name;
    public string Player_name
    {
        get { return player_name; }
        set { player_name = value; }
    }

    public PlayerData(string color, string name, int id){
        Player_color = color;
        Player_name = name;
        Debug.Log(name + " added with id: " + id);
    }

    public void SetColorBasedOnButtonClick(bool set_state, string color){
        if (set_state){
            Player_color = "";
        }else{
            Player_color = color;
        }
    }

}
