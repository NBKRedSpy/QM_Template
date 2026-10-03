using HarmonyLib;
using ModConfigMenu;
using ModConfigMenu.Contracts;
using ModConfigMenu.Objects;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;
using QM_Template.Mcm;

namespace QM_Template
{
    /// <summary>
    /// Responsible for mapping the ModConfig to the MCM configuration menu.  
    /// </summary>
    internal class McmConfiguration : McmConfigurationBase
    {

        public McmConfiguration(ModConfig config) : base (config) { }

        /// <summary>
        /// Registers the mod configuration with the Mod Config Menu API. This method is called when the mod is loaded and allows you to define the configuration options that will be available to users in the mod configuration menu.
        /// </summary>
        public override void Configure()
        {

            ////---------- Important!  Uncomment this code if to add MCM configuration support.

            //ModConfig defaults = new ModConfig();

            //ModConfigMenuAPI.RegisterModConfig("QM_Template", new List<IConfigValue>()
            //{
            //    //Note that MCM does support color codes, as shown below.

            //    //-- Restart note for any settings that cannot be immediately applied without restarting the game.
            //    //new ConfigValue("__RestartNote", @"<color=#FF0000>The game must be restarted if any changes are made.</color>" , "Restart"),

            //    //--Example of using a boolean
            //    //CreateConfigProperty(nameof(ModConfig.FooBool),
            //    //    "When enabled, does X",
            //    //    "Enable X"),
                

            //    //Example of using a dropdown for a key.  MCM does not have a "press to bind" option.
            //    //  If sort is false, the keys are grouped by area instead of alphabetical. 

            //    //CreateEnumDropdown<KeyCode>(nameof(ModConfig.FooXKey),
            //    //    "The key to press to amputate the selected body part.",
            //    //    "Amputate Key", sort: true),

            //    //Used when MCM doesn't have support for some value.  Currently this includes text.
            //    //CreateReadOnly(nameof(ModConfig.SomeText))

            //}, OnSave);
        }
         
    }
}
