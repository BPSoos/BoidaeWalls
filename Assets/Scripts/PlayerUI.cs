using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{   
    public GameObject[] playerPrefabs = new GameObject[4];
    public int player_y_slot {get; set;}
    public int player_count {get; set;}

    // Start is called before the first frame update

    void Start(){
        transform.gameObject.SetActive(false);
        InitializeButtons();
    }

    public void CreateUIElements()
    {
        Debug.Log("a");
    }

    public void UpdatePosition(int new_slot){
        foreach (Transform player_item in transform){
            Vector3 new_position = new Vector3(player_item.transform.position.x, 500 - new_slot * 40 , 0);
            player_item.position = new_position;
            player_y_slot = new_slot;
        }
    }

    public void RemoveButton(){
        transform.gameObject.SetActive(false);
        transform.parent.GetComponent<MainMenu>().PlayerRemoved(player_y_slot, player_count);
    }

    public void InitializeButtons(){
        for (int i = 0; i < playerPrefabs.Length; i++){
            if (playerPrefabs[i].tag.Contains("ExitButton")){
                Button button = playerPrefabs[i].GetComponent<Button>();
                button.onClick.AddListener( ()=> RemoveButton() );
            }
        }
    }
}
