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

            //Optionally support MCM if the user has MCM installed.  Otherwise the user must edit the config
            //  file directly as needed.
            McmConfiguration = new McmConfiguration(Config);
            McmConfiguration.TryConfigure();

            new Harmony("nbk_RedSpy_" + ConfigDirectories.ModAssemblyName).PatchAll();
        }

    }
}
