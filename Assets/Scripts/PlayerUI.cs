using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour
{   
    public GameObject[] playerPrefabs = new GameObject[10];
    public GameObject PlayerExitButton;
    public int PlayerYSlot {get; private set;}
    public int serialNumber {get; set;}
    public InputField playerName;
    public string currentColor = "";
    public KeyCode leftControl;
    public KeyCode rigthControl;
    private bool controlSettingOn = false;
    private string controlSettingDirection;
    public TextMeshProUGUI playerLeftControl;
    public TextMeshProUGUI playerRightControl;
   
    
    void Start()
    {
        InitializeRemoveButton();
    }

    void Update() 
    {
        /* setting control, listening to keypress*/
        if (controlSettingOn)
        {
            foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    SetControls(controlSettingDirection, kcode);
                }
            }
        }
    }

    public void UpdatePosition(int newSlot)
    {
        /* sets position to a given Y slot
         args:
            newSlot: the given Y slot*/
        foreach (Transform playerItem in transform)
        {
            var newPosition = new Vector3(playerItem.transform.position.x, 250 - newSlot * 40 , 0);
            playerItem.position = newPosition;
            PlayerYSlot = newSlot;
        }
    }

    public void RemoveButton()
    {
        transform.gameObject.SetActive(false);
        transform.parent.GetComponent<MainMenu>().RemovePlayer(PlayerYSlot, serialNumber);
    }

    private void InitializeRemoveButton()
    {
        /* adds a listener to the remove button*/
        foreach (var t in playerPrefabs)
        {
            if (!t.tag.Contains("ExitButton")) continue;
            var button = t.GetComponent<Button>();
            button.onClick.AddListener( RemoveButton );
        }
    }

    public void SetColorAvailable(bool setState, string colorOfButton){
        /* enables or disables a button of given color for each player
         args:
            setState: true to enable, false to disable
            colorOfButton: color of buttons to set*/
        foreach (Transform playerItem in transform)
        {
            if (playerItem.gameObject.name.Contains(colorOfButton))
                playerItem.gameObject.SetActive(setState);
        }
    }

    public void ColorSelected(string colorOfButton)
    {
        void SetControlColors()
        {
            /* sets the color of the control buttons, also updates currentColor as needed*/
            if (currentColor == colorOfButton)
            {
                transform.Find("PlayerLeftControlImage").GetComponent<Image>().color = Color.grey;
                transform.Find("PlayerRightControlImage").GetComponent<Image>().color = Color.grey;
                currentColor = "";
            }
            else
            {
                transform.Find("PlayerLeftControlImage").GetComponent<Image>().color = PlayerData.BasicColorDict[colorOfButton];
                transform.Find("PlayerRightControlImage").GetComponent<Image>().color = PlayerData.BasicColorDict[colorOfButton];
                currentColor = colorOfButton;
            }
        }
        /*      updates currentColor and modifies all needed buttons accordingly
            args:
                colorOfButton: the color button that was pressed */
        foreach (Transform playerItem in transform)
        {
            if (!playerItem.gameObject.activeSelf)
                continue;
            if (playerItem.gameObject.name.Contains(colorOfButton))
            {
                bool buttonSelectionOriginalState = (playerItem.GetComponent<Image>().color == Color.grey);
                if (buttonSelectionOriginalState)
                    playerItem.GetComponent<Image>().color = PlayerData.BasicColorDict[colorOfButton];
                else
                    playerItem.GetComponent<Image>().color = Color.grey;
                
                transform.parent.GetComponent<MainMenu>().PlayerColorSelected(buttonSelectionOriginalState, colorOfButton, serialNumber);
            }else if (currentColor != "" && playerItem.gameObject.name.Contains(currentColor))
            {
                playerItem.GetComponent<Image>().color = PlayerData.BasicColorDict[currentColor];
                transform.parent.GetComponent<MainMenu>().PlayerColorSelected(true, currentColor, serialNumber);
            }
        }
        SetControlColors();
    }

    public void ControlSelected(string direction)
    {
        /* sets the state of this object to read a keypress for control selection
         args:
            direction: "Left" or "Right" depending on which control button was pressed*/
        transform.Find("ColorSettingText" + direction).gameObject.SetActive(true);
        controlSettingOn = true;
        controlSettingDirection = direction;
    }

    public void SetControls(string direction, KeyCode controlKey)
    {
        /*args:
            direction: "Left" or "Right" depending on which control button was pressed
            controlKey: the captured keypress to set the control with*/
        Debug.Log("Player " + direction + " ControlText set to : " + controlKey);
        if (direction == "Left")
        {
            playerLeftControl.text = PlayerData.convertKeyCode(controlKey);
            leftControl = controlKey;
        }
        else
        {
            playerRightControl.text = PlayerData.convertKeyCode(controlKey);
            rigthControl = controlKey;
        }
        controlSettingOn = false;
        transform.Find("ColorSettingText" + direction).gameObject.SetActive(false);
        // ideally, we should check here  or (somewhere) if the pressed key is among the acceptable ones and it is not used currently
        // we could also initialize a few control keys to start with
    }
}
