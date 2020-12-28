using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenu : MonoBehaviour
{
    public PlayerUI playerClass;
    public List<PlayerUI> addedPlayersList;
    private int _firstEmptySlot = 0;
    private int _maxPlayerCount = 8;
    public int TestPlayerAmount;
    public void PlayGame(){
        foreach (var addedPlayer in addedPlayersList)
        {
            if (addedPlayer.gameObject.activeSelf)
            {
                GamePlayer addMe = new GamePlayer(id: addedPlayer.serialNumber,
                                        color: PlayerData.BasicColorDict[addedPlayer.currentColor],
                                        addedPlayer.playerLeftControl.GetComponent<TextMeshProUGUI>().text,
                                        addedPlayer.playerRightControl.GetComponent<TextMeshProUGUI>().text,
                                        addedPlayer.playerName.GetComponent<InputField>().text);
                PlayerData.GamePlayers.Add(addMe);
            }
        }
    }

    void Update() 
    {
    }
    void Start()
    {
        initPlayerObjects(2);
    }

    public void QuitGame(){
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void AddPlayerObject(){
        PlayerUI newPlayerObject = Instantiate(playerClass, this.transform, false);
        newPlayerObject.UpdatePosition(addedPlayersList.Count);
        newPlayerObject.serialNumber = addedPlayersList.Count;
        newPlayerObject.gameObject.SetActive(false);
        addedPlayersList.Add(newPlayerObject);
    }

    public void AddPlayerButtonPress(){
        if (_firstEmptySlot < _maxPlayerCount){
            foreach (var addedPlayer in addedPlayersList)
            {
                if ((addedPlayer.PlayerYSlot == _firstEmptySlot))
                {
                    addedPlayer.gameObject.SetActive(true);
                    break;
                }
            }
            _firstEmptySlot ++;
            if(_firstEmptySlot == 3)
                RemoveButtonsSetActive(true);
        }else{
            Debug.Log("Max Players reached");
        }
    }

    private void initPlayerObjects(int activePlayerCount = 0)
    /* Instantiates all the Player1 objects needed, only activates a given amount*/
    {
        for (int i = 0; i < _maxPlayerCount; i++)
        {
            AddPlayerObject();
        }
        for (int i = 0; i < activePlayerCount; i++)
        {
            {
                AddPlayerButtonPress();
                addedPlayersList[i].ColorSelected(PlayerData.BasicColorDict.Keys.ElementAt(i));
            }
        }
        RemoveButtonsSetActive(false);
    }

    public void PlayerColorSelected(bool setState, string colorOfButton, int playerIndexWhoChoose){
        for (int i = 0; i < _maxPlayerCount; i++){
            if (i != playerIndexWhoChoose){
                addedPlayersList[i].SetColorAvailable(setState, colorOfButton);
            }
        }
    }

    public void RemovePlayer(int playerSlot, int playerCount){
        /*  Disables the given Player1 object and organizes the UI correctly (positions, colors)
         args:
            playerSlot: given Player1's current Y slot
            playerCount: given Player1's serial number */
        for (int i = 0; i < _maxPlayerCount; i++){
            if (addedPlayersList[i].PlayerYSlot > playerSlot){
                 addedPlayersList[i].UpdatePosition(addedPlayersList[i].PlayerYSlot - 1);
            }
        }
        addedPlayersList[playerCount].UpdatePosition(7);
        if (addedPlayersList[playerCount].currentColor != "")
            addedPlayersList[playerCount].ColorSelected(addedPlayersList[playerCount].currentColor);
        _firstEmptySlot --;
        if(_firstEmptySlot == 2)
            RemoveButtonsSetActive(false);
    }

    private void RemoveButtonsSetActive(bool state)
    /* Disables Remove Button for every Player1*/
    {
        foreach (var addedPlayer in addedPlayersList)
        {
            addedPlayer.PlayerExitButton.SetActive(state);
        }
    }
}
