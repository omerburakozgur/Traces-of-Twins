using System;
using UnityEngine;

public static class StaticVariables
{
    // Global Variables 

    [Serializable]
    public class Character
    {
        [Header("Character")]
        public static int selectedCharacter = 0;
    }

    [Serializable]
    public class Variables
    {
        [Header("Variable")]
        public static float minigameEndWaitTime = 1f;
    }

    [Serializable]
    public class Sound
    {
        [Header("Sound")]
        public static float musicVolume = 0.5f;
        public static float effectVolume = 0.5f;
        public static float ambientVolume = 0.5f;
        public static float masterVolume = 0.5f;
        public static float UIVolume = 1f;


    }

    [Serializable]
    public class Objects
    {
        [Header("Items")]
        public static bool itemCrowbar = false;
        public static bool itemFlashlight = false;
        public static bool itemPasswordNote = false;
        public static bool itemCarKeys = false;
        public static bool itemFuelCan = false;
        public static bool itemTire = false;
        public static bool itemLockpick = false;
        public static bool itemShovel = false;
        public static bool itemLetter = false;
        public static bool itemDNABags = false;
        public static bool itemFingerprintBags = false;
        public static bool itemElectricSwitches = false;
        public static bool itemComputerCable = false;


    }

    // this can be used for dialogue localisation instead of duplicating objects and scenes
    [Serializable]
    public static class Dialogues
    {
        public static string[] dialog1 = { "Dialog 1 Dialog 1 Dialog 1 Dialog 1 Dialog 1", "Dialog 1.2 Dialog 2 Dialog 2 Dialog 2 Dialog 2", "Dialog 1.3 Dialog 3 Dialog 3 Dialog 3 Dialog 3" };
        public static string dialog1Character = "";

        public static string[] dialog2 = { "Dialog 2.1 Dialog 2.1 Dialog 2.1 Dialog 2.1 Dialog 2.1", "Dialog 2.2 Dialog 2.2 Dialog 2.2 Dialog 2.2 Dialog 2.2 ", "Dialog 2.3 Dialog 2.3 Dialog 2.3 Dialog 2.3 Dialog 2.3" };
        public static string dialog2Character = "";
    }


}
