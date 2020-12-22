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
    private int _firstEmptySlot = 0;
    private int _maxPlayerCount = 8;
    public int TestPlayerAmount;
    public void PlayGame(){
        for (int i =0; i < TestPlayerAmount; i++){
            var addMe = new Player(addedPlayersList[i].serialNumber,
                            PlayerData.BasicColorDict[addedPlayersList[i].currentColor]);
        PlayerData.GamePlayers.Add(addMe);
        
        Debug.Log(PlayerData.GamePlayers[i].PlayerID.ToString() + " player added with color " +
                            PlayerData.GamePlayers[i].PlayerColor.ToString());
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex  + 1);
    }

    void Update() {
        
    }
    void Start()
    {
        initPlayerObjects(2);
    }

    public void QuitGame(){
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void AddPlayer(){
        PlayerUI newPlayerObject = Instantiate(playerClass, this.transform, false);
        newPlayerObject.UpdatePosition(addedPlayersList.Count);
        newPlayerObject.serialNumber = addedPlayersList.Count;
        newPlayerObject.gameObject.SetActive(false);
        addedPlayersList.Add(newPlayerObject);
    }

    public void AddPlayerButtonPress(){
        if (_firstEmptySlot < _maxPlayerCount){
            AddPlayer();
            foreach (var addedPlayer in addedPlayersList)
            {
                if ((addedPlayer.PlayerYSlot == _firstEmptySlot)){
                    Debug.Log("I'm in");
                    break;
                }
            }
            if (_firstEmptySlot == 0)
                transform.Find("AddButton").gameObject.SetActive(true);
            _firstEmptySlot ++;
        }else{
            Debug.Log("Max Players reached");
        }
    }

    public void initPlayerObjects(int activePlayerCount = 0)
    {
        for (int i = 0; i < _maxPlayerCount; i++)
        {
            AddPlayer();
            if (i < activePlayerCount)
                addedPlayersList[i].gameObject.SetActive(true);
        }
    }

    public void PlayerColorSelected(bool setState, string colorOfButton, int playerIndexWhoChoose){
        for (int i = 0; i < _maxPlayerCount; i++){
            if (i != playerIndexWhoChoose){
                addedPlayersList[i].SetColorAvailable(setState, colorOfButton);
            }
        }
    }

    public void PlayerRemoved(int playerSlot, int playerCount){
        int lastSlot = playerSlot;
        for (int i = 0; i < _maxPlayerCount; i++){
            if (addedPlayersList[i].PlayerYSlot > playerSlot){
                 addedPlayersList[i].UpdatePosition(addedPlayersList[i].PlayerYSlot - 1);
            }
        }
        addedPlayersList[playerCount].UpdatePosition(7);
        _firstEmptySlot --;
        if (_firstEmptySlot == 0)
            transform.Find("PlayButton").gameObject.SetActive(false);
    }
}
