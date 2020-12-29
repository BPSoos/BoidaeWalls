using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    
    public int deadPlayers = 0;
    private float timeAfterEnd = 5f;
    public BoaPlayer1 boaPlayerClass;
    public TextMeshProUGUI winText;
    public List<BoaPlayer1> activePlayers;

    public void EndGame (){
        StartCoroutine(PlayEndGameAnimation ());
    }

    public void Start()
    {
        Debug.Log("here");
        foreach (var gamePlayer in PlayerData.GamePlayers)
        {
            Debug.Log("instantiating player");
            var newBoaPlayer = Instantiate(boaPlayerClass, this.transform, false);
            newBoaPlayer.body.line.startColor = gamePlayer.selfColor;
            newBoaPlayer.body.line.endColor = gamePlayer.selfColor;
            newBoaPlayer.head.GetComponent<SpriteRenderer>().color = gamePlayer.selfColor;
            newBoaPlayer.head.inputAxis += gamePlayer.serialNumber;
            newBoaPlayer.name = gamePlayer.name;
            newBoaPlayer.myManager = this;
            activePlayers.Add(newBoaPlayer);
        }
    }

    IEnumerator PlayEndGameAnimation ()
    {
        foreach (var activePlayer in activePlayers)
        {
            if (!activePlayer.lost)
            {
                winText.text = activePlayer.name + " Wins This Round!";
                deadPlayers = 0;
                break;
            }
        }
        winText.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeAfterEnd);
        winText.gameObject.SetActive(false);
        winText.text = "";
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
    }

    public void MenuButton()
    {
        PlayerData.CleanUpPlayerData();
        SceneManager.LoadScene(0);
    }
}
