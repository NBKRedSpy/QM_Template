using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_Template
{
    [HarmonyPatch(typeof(MainMenuScreen), nameof(MainMenuScreen.Awake))]
    public static class ExamplePatch
    {
        public static void Prefix(MainMenuScreen __instance)
        {
            Debug.Log("--- main menu awake");
        }
    }
}
