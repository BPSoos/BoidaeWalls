using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static List<GamePlayer> GamePlayers = new List<GamePlayer>();

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

