using HarmonyLib;
using MGSC;
using ModConfigMenu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_Template
{
    public static class Plugin
    {
        public static ConfigDirectories ConfigDirectories = new ConfigDirectories();

        public static ModConfig Config { get; private set; }

        public static Logger Logger { get; private set; } = new Logger();

        private static McmConfiguration McmConfiguration { get; set; }

        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {

            Directory.CreateDirectory(ConfigDirectories.AllModsConfigFolder);
            ConfigDirectories = new ConfigDirectories();
            Directory.CreateDirectory(ConfigDirectories.ModPersistenceFolder);

            Config = new ModConfig(ConfigDirectories.ConfigPath).LoadConfig();

            new Harmony("nbk_RedSpy_" + ConfigDirectories.ModAssemblyName).PatchAll();
        }

        /// <summary>
        /// Configures MCM if it is available.
        /// </summary>
        /// <param name="modContext"></param>
        [Hook(ModHookType.MainMenuStarted)]
        public static void MainMenuStarted(ModContext modContext)
        {
            //Note - this is a workaround as the game does not support an "optional dependency"
            //  and we cannot guarantee that MCM will be loaded before this mod.

            //If MCM is not loaded, this will be run every time the main menu is started, but this is not 
            //  an issue.
            if (McmConfiguration != null) return;

            McmConfiguration = McmConfiguration.TryConfigure(modContext.State, Config);

        }
    }
