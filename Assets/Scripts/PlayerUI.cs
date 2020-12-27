using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player
{
    public int PlayerID { get; private set; }
    public Color32 PlayerColor { get; private set; }
    public Player(int id, Color32 color)
    {
        PlayerID = id;
        PlayerColor = color;
    }
}

public static class PlayerData
    {
        public static List<Player> GamePlayers = new List<Player>();

        public static readonly Dictionary<string, Color32> BasicColorDict = new Dictionary<string, Color32>
        {
            // this is currently ordered as they appear on the UI from left to right
            // it sounds like a good idea to make it so that if the order changes here, so does the order on the UI
            { "Orange", new Color32(255, 127, 0, 255)},
            { "Cyan", new Color32(0, 255, 248, 255)},
            { "Yellow", new Color32(238, 227, 64, 255)},
            { "Red", new Color32(254, 69, 68, 255)},
            { "Green", new Color32(108, 254, 49, 255)},
            { "Blue", new Color32(64, 68, 226, 255)},
            { "Pink", new Color32(223, 169, 177, 255)},
            { "Army", new Color32(55, 169, 64, 255)},
            {"", new Color32(255, 255, 255, 255)}
        };

    }


public class PlayerUI : MonoBehaviour
{   
    public GameObject[] playerPrefabs = new GameObject[4];
    public int PlayerYSlot {get; private set;}
    public int serialNumber {get; set;}
    [FormerlySerializedAs("player_name")] public InputField playerName;

    [FormerlySerializedAs("current_color")] public string currentColor = "";
   
    
    void Start()
    {
        InitializeButtons();
    }

    public void UpdatePosition(int newSlot)
    {
        foreach (Transform playerItem in transform)
        {
            Vector3 newPosition = new Vector3(playerItem.transform.position.x, 250 - newSlot * 40 , 0);
            playerItem.position = newPosition;
            PlayerYSlot = newSlot;
        }
    }

    public void RemoveButton()
    {
        transform.gameObject.SetActive(false);
        transform.parent.GetComponent<MainMenu>().PlayerRemoved(PlayerYSlot, serialNumber);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void ChangeInputFieldText(string message)
    { 
         var textscript = transform.GetChild(1).Find("Text").GetComponent<Text>();
         if(textscript == null){Debug.LogError("Script not found");return;}
         textscript.text = message;

    }

    private void InitializeButtons()
    {
        foreach (var t in playerPrefabs)
        {
            if (!t.tag.Contains("ExitButton")) continue;
            var button = t.GetComponent<Button>();
            button.onClick.AddListener( RemoveButton );
        }
    }

    public void SetColorAvailable(bool setState, string colorOfButton){
        foreach (Transform playerItem in transform)
        {
            if (playerItem.gameObject.name.Contains(colorOfButton))            
                playerItem.gameObject.SetActive(setState);
        }
    }

    public void ColorSelected(string colorOfButton)
    {
        foreach (Transform playerItem in transform)
        {
            if (playerItem.gameObject.name.Contains(colorOfButton))
            {
                bool buttonCurrentlySelected = (playerItem.GetComponent<Image>().color == Color.grey);
                if(buttonCurrentlySelected)                
                    playerItem.GetComponent<Image>().color = PlayerData.BasicColorDict[colorOfButton];
                else
                    playerItem.GetComponent<Image>().color = Color.grey;
                transform.parent.GetComponent<MainMenu>().PlayerColorSelected(buttonCurrentlySelected, colorOfButton, serialNumber);
            }else if (currentColor != "" && playerItem.gameObject.name.Contains(currentColor))
            {
                playerItem.GetComponent<Image>().color = PlayerData.BasicColorDict[currentColor];
                transform.parent.GetComponent<MainMenu>().PlayerColorSelected(true, currentColor, serialNumber);
            }
        }
        currentColor = colorOfButton;
    }
}
