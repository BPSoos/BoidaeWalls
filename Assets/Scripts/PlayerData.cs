using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static List<GamePlayer> GamePlayers = new List<GamePlayer>();

    public static void CleanUpPlayerData()
    {
        GamePlayers.Clear();
        GameInputManager.axisMap.Clear();
    }

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

    public static string convertKeyCode(KeyCode controlKey)
    {
        if (controlsMap.ContainsKey(controlKey))
            return controlsMap[controlKey];
        return controlKey.ToString();
    }

    public static Dictionary<KeyCode, string> controlsMap = new Dictionary<KeyCode, string>
    {
        {KeyCode.K, "K"},
        {KeyCode.Alpha0, "0"}, {KeyCode.Alpha1, "1"}, {KeyCode.Alpha2, "2"}, {KeyCode.Alpha3, "3"},
        {KeyCode.Alpha4, "4"}, {KeyCode.Alpha5, "5"}, {KeyCode.Alpha6, "6"}, {KeyCode.Alpha7, "7"},
        {KeyCode.Alpha8, "8"}, {KeyCode.Alpha9, "9"}, {KeyCode.Mouse0, "M0"}, {KeyCode.Mouse1, "M1"},
        {KeyCode.Mouse2, "M2"}, {KeyCode.Mouse3, "M3"}, {KeyCode.Mouse4, "M4"}, {KeyCode.Mouse5, "M5"},
        {KeyCode.Asterisk, "*"}, {KeyCode.Keypad0, "N0"}, {KeyCode.Keypad1, "N1"}, {KeyCode.Keypad2, "N2"},
        {KeyCode.Keypad3, "N3"}, {KeyCode.Keypad4, "N4"}, {KeyCode.Keypad5, "N5"}, {KeyCode.Keypad6, "N6"},
        {KeyCode.Keypad7, "N7"}, {KeyCode.Keypad8, "N8"}, {KeyCode.Keypad9, "N9"}, {KeyCode.RightArrow, "→"},
        {KeyCode.LeftArrow, "←"}, {KeyCode.UpArrow, "↑"}, {KeyCode.DownArrow, "↓"}, {KeyCode.Ampersand, "`"},
        {KeyCode.Backslash, "\\"}, {KeyCode.Comma, ","}, {KeyCode.Semicolon, ";"}, {KeyCode.Quote, "'"},
        {KeyCode.LeftBracket, "["}, {KeyCode.RightBracket, "]"}, {KeyCode.Equals, "="}, {KeyCode.Minus, "-"}
    };
}

