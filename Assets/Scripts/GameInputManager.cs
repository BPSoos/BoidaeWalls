using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
 
public static class GameInputManager
{
    public class AxisKeys
    {
        public KeyCode positive { get; set; }
        public KeyCode negative { get; set; }
    }
    static Dictionary<string, KeyCode> keyMapping;
    static string[] keyMaps = new string[6]
    {
        "Attack",
        "Block",
        "Forward",
        "Backward",
        "Left",
        "Right"
    };
    static KeyCode[] defaults = new KeyCode[6]
    {
        KeyCode.Q,
        KeyCode.E,
        KeyCode.W,
        KeyCode.S,
        KeyCode.A,
        KeyCode.D
    };

    public static Dictionary<string, AxisKeys> axisMap = new Dictionary<string, AxisKeys>
    {
        { "Horizontal",new AxisKeys{positive = KeyCode.Alpha0, negative = KeyCode.Alpha1}}      
    };

 
    static GameInputManager()
    {
        InitializeDictionary();
    }
 
    private static void InitializeDictionary()
    {
        keyMapping = new Dictionary<string, KeyCode>();
        for(int i=0;i<keyMaps.Length;++i)
        {
            keyMapping.Add(keyMaps[i], defaults[i]);
        }
    }
 
    public static void SetKeyMap(string keyMap,KeyCode key)
    {
        if (!keyMapping.ContainsKey(keyMap))
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + keyMap);
        keyMapping[keyMap] = key;
    }
 
    public static bool GetKeyDown(string keyMap)
    {
        return Input.GetKeyDown(keyMapping[keyMap]);
    }

    public static float GetAxisRaw(string axisName)
    {
        if (Input.GetKey(axisMap[axisName].positive))
            return 1;
        if (Input.GetKey(axisMap[axisName].negative))
            return -1;
        return 0;
    }
}