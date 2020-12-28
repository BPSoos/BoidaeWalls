using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer
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

    public GamePlayer(int id, Color32 color, string leftControl, string rightControl, string playerName)
    {
        serialNumber = id;
        selfColor = color;
        left = leftControl;
        right = rightControl;
        name = playerName;

    }
}
