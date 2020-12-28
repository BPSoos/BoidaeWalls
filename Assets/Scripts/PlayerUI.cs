using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player
{
    private int _serialNumber;

    public int serialNumber
    {
        get
        {
            return _serialNumber;
        }
        set
        {
            _serialNumber = value;
            Debug.Log("serialNumber set to: " + _serialNumber);
        }
        
    }

    private Color32 _selfColor;
    
    public Color32 selfColor
    {
        get
        {
            return _selfColor;
        }
        set
        {
            _selfColor = value;
            Debug.Log("Color set to: " + _selfColor);
        }
    }

    private string _left;
    public string left 
    {
        get
        {
            return _left;
        } 
        set
        {
            _left = value;
            Debug.Log("left control set: " + _left);
        }
    }
    
    private string _right;
    public string right 
    {
        get
        {
            return _right;
        } 
        set
        {
            _right = value;
            Debug.Log("right control set: " + _right);
        }
    }
    
    private string _name;
    public string name 
    {
        get
        {
            return _name;
        } 
        set
        {
            _name = value;
            Debug.Log("name set: " + _name);
        }
    }

    public Player(int id, Color32 color, string leftControl, string rightControl, string playerName)
    {
        serialNumber = id;
        selfColor = color;
        left = leftControl;
        right = rightControl;
        name = playerName;

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
    public GameObject[] playerPrefabs = new GameObject[10];

    public GameObject PlayerExitButton;
    public int PlayerYSlot {get; private set;}
    public int serialNumber {get; set;}
    [FormerlySerializedAs("player_name")] public InputField playerName;

    [FormerlySerializedAs("current_color")] public string currentColor = "";
    private bool controlSettingOn = false;
    private string controlSettingDirection;
    public TextMeshProUGUI playerLeftControl;
    public TextMeshProUGUI playerRightControl;
   
    
    void Start()
    {
        InitializeButtons();
    }

    void Update() 
    {
        if (controlSettingOn)
        {
            foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    Debug.Log("KeyCode down: " + kcode);
                    SetControls(controlSettingDirection, kcode);
                }
            }
        }
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
            if (!playerItem.gameObject.activeSelf)
                continue;
            if (playerItem.gameObject.name.Contains(colorOfButton))
            {
                bool buttonCurrentlySelected = (playerItem.GetComponent<Image>().color == Color.grey);
                if (buttonCurrentlySelected)
                {
                    playerItem.GetComponent<Image>().color = PlayerData.BasicColorDict[colorOfButton];
                }
                else
                    playerItem.GetComponent<Image>().color = Color.grey;
                transform.parent.GetComponent<MainMenu>().PlayerColorSelected(buttonCurrentlySelected, colorOfButton, serialNumber);
            }else if (currentColor != "" && playerItem.gameObject.name.Contains(currentColor))
            {
                playerItem.GetComponent<Image>().color = PlayerData.BasicColorDict[currentColor];
                transform.parent.GetComponent<MainMenu>().PlayerColorSelected(true, currentColor, serialNumber);
            }
        }
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

    public void ControlSelected(string direction)
    {
        transform.Find("ColorSettingText" + direction).gameObject.SetActive(true);
        controlSettingOn = true;
        controlSettingDirection = direction;
        //transform.Find("ColorSettingText" + direction).gameObject.SetActive(!(transform.Find("ColorSettingText" + direction).gameObject.activeSelf));
    }

    public void SetControls(string direction, KeyCode controlKey)
    {
        Debug.Log("Player" + direction + "ControlText");
        Debug.Log(controlKey);
        if (direction == "Left")
            playerLeftControl.text = controlKey.ToString();
        else
            playerRightControl.text = controlKey.ToString();
        controlSettingOn = false;
        transform.Find("ColorSettingText" + direction).gameObject.SetActive(false);
        // ideally, we should check here if the pressed key is among the acceptable ones and it is not used currently
        // we could also initialise a few control keys to start with
    }
}
