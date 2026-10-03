using MGSC;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QM_Template.Mcm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_Template
{
    public class ModConfig : PersistentConfig<ModConfig>
    {
        public ModConfig()
        {
        }

        public ModConfig(string configPath) : base(configPath)
        {

        }


        // ---- Example of an enum config

        ///// <summary>
        ///// The list of items that will not be recycled.
        ///// </summary>
        //[JsonProperty(ItemConverterType =typeof(StringEnumConverter))]
        //public HashSet<ItemClass> DoNotRecycleItems { get; set; } = new HashSet<ItemClass>()
        //{
        //    ItemClass.RepairKit,
        //    ItemClass.Ammo,
        //    ItemClass.Mine,
        //    ItemClass.Grenade,
        //    //Most of the repair kits.  Such as armor or firearm repair kits.
        //    ItemClass.RepairKit,
        //    //Contains some repair kits that are not tagged as RepairKit.  For example, hi-tech repair kit or quasimorph repair kit.
        //    //  Shouldn't affect anything as regular parts should not break down.
        //    ItemClass.Parts,        

        //};


        // ---- Example of a boolean config

        ///// <summary>
        ///// If true, will not automatically close the corpse window if there is more than one tab.  Including a new tab 
        ///// created by dropping items.
        ///// </summary>
        //public bool FooBool { get; set; } = false;


        // ---- Example of a key binding config.  Uses the 1 key.

        //[JsonConverter(typeof(StringEnumConverter))]
        //public KeyCode FooXKey { get; set; } = KeyCode.Alpha1;

        public void Save(string configPath)
        {
            string json = JsonConvert.SerializeObject(this, SerializerSettings);
            File.WriteAllText(Plugin.ConfigDirectories.ConfigPath, json);
        }
    }
}
