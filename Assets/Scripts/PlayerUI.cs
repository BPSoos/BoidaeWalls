using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    public int player_id { get; set; }
    public Color32 player_color { get; set; }
    public Player(int id, Color32 color)
    {
        player_id = id;
        player_color = color;
    }
}

public static class PlayerData
    {
        public static List<Player> game_players = new List<Player>();

        public static Dictionary<string, Color32> basic_color_dict = new Dictionary<string, Color32>
        {
            { "Army", new Color32(55, 169, 64, 255)},
            { "Blue", new Color32(64, 68, 226, 255)},
            { "Cyan", new Color32(0, 255, 248, 255)},
            { "Green", new Color32(108, 254, 49, 255)},
            { "Orange", new Color32(255, 127, 0, 255)},
            { "Pink", new Color32(223, 169, 177, 255)},
            { "Red", new Color32(254, 69, 68, 255)},
            { "Yellow", new Color32(238, 227, 64, 255)},
            {"", new Color32(255, 255, 255, 255)}
        };

    }


public class PlayerUI : MonoBehaviour
{   
    public GameObject[] playerPrefabs = new GameObject[4];
    public int player_y_slot {get; set;}
    public int player_count {get; set;}
    public InputField player_name;

    public string current_color = "";
   
    
    void Start()
    {
        transform.gameObject.SetActive(false);
        InitializeButtons();
    }

    public void UpdatePosition(int new_slot)
    {
        foreach (Transform player_item in transform)
        {
            Vector3 new_position = new Vector3(player_item.transform.position.x, 250 - new_slot * 40 , 0);
            player_item.position = new_position;
            player_y_slot = new_slot;
        }
    }

    public void RemoveButton()
    {
        transform.gameObject.SetActive(false);
        transform.parent.GetComponent<MainMenu>().PlayerRemoved(player_y_slot, player_count);
    }

    public void ChangeInputFieldText(string message)
    { 
         var textscript = transform.GetChild(1).Find("Text").GetComponent<Text>();
         if(textscript == null){Debug.LogError("Script not found");return;}
         textscript.text = message;

    }

    public void InitializeButtons()
    {
        for (int i = 0; i < playerPrefabs.Length; i++)
        {
            if (playerPrefabs[i].tag.Contains("ExitButton"))
            {
                Button button = playerPrefabs[i].GetComponent<Button>();
                button.onClick.AddListener( ()=> RemoveButton() );
            }
        }
    }

    public void SetColorAvailable(bool set_state, string color_of_button){
        foreach (Transform player_item in transform)
        {
            if (player_item.gameObject.name.Contains(color_of_button))            
                player_item.gameObject.SetActive(set_state);
        }
    }

    public void ColorSelected(string color_of_button)
    {
        foreach (Transform player_item in transform)
        {
            if (player_item.gameObject.name.Contains(color_of_button))
            {
                bool button_currently_selected = (player_item.GetComponent<Image>().color == Color.grey);
                if(button_currently_selected)                
                    player_item.GetComponent<Image>().color = PlayerData.basic_color_dict[color_of_button];
                else
                    player_item.GetComponent<Image>().color = Color.grey;                
                transform.parent.GetComponent<MainMenu>().PlayerColorSelected(button_currently_selected, color_of_button, player_count);
            }else if (current_color != "" && player_item.gameObject.name.Contains(current_color))
            {
                player_item.GetComponent<Image>().color = PlayerData.basic_color_dict[current_color];
                transform.parent.GetComponent<MainMenu>().PlayerColorSelected(true, current_color, player_count);
            }
        }
        current_color = color_of_button;
    }
}
