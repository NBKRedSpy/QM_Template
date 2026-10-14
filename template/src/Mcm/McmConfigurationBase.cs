using HarmonyLib;
using MGSC;
using ModConfigMenu.Contracts;
using ModConfigMenu.Implementations;
using ModConfigMenu.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace QM_Template.Mcm
{


    /// <summary>
    /// The collection of helper utilities and methods to make it easier to create a ModConfigMenu configuration.  This is an abstract class 
    /// that is inherited by the McmConfiguration class.
    /// 
    /// This includes helper methods to create dropdowns for enums, read-only settings, and numeric settings.
    /// By default, this simplifies the configuration by using reflection to interact with the ModConfig.
    /// </summary>
    /// <param name="config">The configuration object to use for this mod.</param>
    /// 
    internal abstract class McmConfigurationBase(ModConfig config)
    {
        // NOTE - This class could be improved to move the ModConfig type specific logic to the derived class, but this works.


        protected const string WarningColorElement = "<color=#FBE343>";

        /// <summary>
        /// The common warning about the number keys being prefixed with Alpha.
        /// </summary>
        protected const string KeyCodeAlphaNote = $"{WarningColorElement}Note: The keys 1 through 0 are named Alpha1 through Alpha0.</color>";

        /// <summary>
        /// The configuration object to use for this mod.
        /// </summary>
        /// <remarks></remarks>
        public ModConfig Config { get; set; } = config;

        /// <summary>
        /// Used to set the defaults established by the ModConfig class.  Needed for the MCM's Restore Defaults button.
        /// </summary>
        private ModConfig Defaults { get; set; } = new ModConfig();

        /// <summary>
        /// Used to make the keys for read only entries unique.
        /// </summary>
        private static int UniqueId = 0;



        /// <summary>
        /// The ModConfig specific configuration.  Use the Create* and OnSave helper functions.
        /// </summary>
        public abstract void Configure();

        /// <summary>
        /// Attempts to configure the MCM support if MCM is available.  Returns null if MCM is not available or if an error occurs.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="config"></param>
        /// <returns>Null if MCM is not available or if an error occurs.</returns>
        public static McmConfiguration TryConfigure(State state, ModConfig config)
        {
            if (!state.Get<UserMods>().Values
                .Any(x => x.UniqueModName == "Crynano_ModConfigMenu"))
            {
                return null;
            }

            try
            {
                McmConfiguration mcmConfig = new McmConfiguration(config);
                mcmConfig.Configure();
                return mcmConfig;
            }
            catch (Exception ex)
            {
                Plugin.Logger.LogError(ex, "An error occurred when configuring MCM");
            }
            return null;
        }

        /// <summary>
        /// Creates a dropdown config for an enum type.
        /// </summary>
        /// <typeparam name="T">The object type to extract the data from</typeparam>
        /// <typeparam name="TEnum">The target enum.</typeparam>
        /// <param name="propertyName">The name of the property to use for the value.</param>
        /// <param name="tooltip">Tooltip to show when the item is hovered over.</param>
        /// <param name="label">The label to display for the dropdown.</param>
        /// <param name="header">The header under which this item is grouped by in the MCM UI.</param>
        /// <returns>The created IConfigValue representing the dropdown.</returns>
        internal IConfigValue CreateEnumDropdown<TEnum>(string propertyName, string tooltip, string label, string header = "General", bool sort = false)
            where TEnum : Enum
        {

            object defaultValue = AccessTools.Property(typeof(ModConfig), propertyName).GetValue(Defaults);
            object propertyValue = AccessTools.Property(typeof(ModConfig), propertyName).GetValue(Config);

            List<object> enumNames;

            if (sort)
            {
                enumNames = Enum.GetNames(typeof(TEnum)).OrderBy(x => x).ToList<object>();
            }
            else
            {
                enumNames = Enum.GetNames(typeof(TEnum)).ToList<object>();
            }

            var dropDown = new DropdownConfig(propertyName, propertyValue.ToString(), header, defaultValue.ToString(),
                 tooltip, label, enumNames);

            return dropDown;
        }

        /// <summary>
        /// Creates the common "must restart" message that many mods need.
        /// </summary>
        /// <returns></returns>
        protected ConfigValue CreateRestartMessage()
        {
            return new ConfigValue("__Restart", "<color=#FF0000>The game must be restarted for any changes to take effect.", "Restart");

        }

        /// <summary>
        /// Creates a setting that is only available in the config file due to lack of MCM support.
        /// Creates a unique ID for the key to avoid the Save from picking it up.
        /// </summary>
        /// <param name="propertyName">The name of the property to create a read-only setting for.</param>
        /// <param name="header">The header under which this item is grouped by in the MCM UI.</param>
        /// <returns>The created ConfigValue representing the read-only setting.</returns>
        protected ConfigValue CreateReadOnly(string propertyName, string header = "Only available in config file")
        {
            int key = UniqueId++;  //Used to make the keys unique so they do not match a property.

            object value = AccessTools.Property(typeof(ModConfig), propertyName)?.GetValue(Config);

            if (value == null)
            {
                //Try field
                value = AccessTools.Field(typeof(ModConfig), propertyName)?.GetValue(Config);
            }

            string formattedValue;

            if (value == null)
            {
                value = "Null";
            }
            if (value is IEnumerable enumList)
            {
                List<string> list = new();

                foreach (var item in enumList)
                {
                    list.Add(item.ToString());
                }

                formattedValue = string.Join(",", list);
            }
            else
            {
                formattedValue = value.ToString();
            }

            string formattedPropertyName = FormatUpperCaseSpaces(propertyName);

            return new ConfigValue(key.ToString(), $@"{formattedPropertyName}: {WarningColorElement}{formattedValue}</color>", header);

        }

        /// <summary>
        /// Formats a string with no spaces to having spaces before each uppercase letter.
        /// Used to make a property name easier to read.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static string FormatUpperCaseSpaces(string propertyName)
        {
            //Since the UI uppercases the text, add spaces to make it easier to read.
            Regex regex = new Regex(@"([A-Z0-9])");
            string formattedPropertyName = regex.Replace(propertyName.ToString(), " $1").TrimStart();
            return formattedPropertyName;
        }
        
        /// <summary>
        /// Creates a configuration value for a numeric property.
        /// </summary>
        /// <typeparam name="T">The type of the numeric property (int or float).</typeparam>
        /// <param name="propertyName">The name of the property to create a configuration value for.</param>
        /// <param name="tooltip">The tooltip text to display in the MCM UI.</param>
        /// <param name="label">The label text to display in the MCM UI.</param>
        /// <param name="min">The minimum value for the numeric property.</param>
        /// <param name="max">The maximum value for the numeric property.</param>
        /// <param name="header">The header under which this item is grouped by in the MCM UI.</param>
        /// <returns>The created ConfigValue representing the numeric setting.</returns>
        protected ConfigValue CreateConfigProperty<T>(string propertyName,
            string tooltip, string label, T min, T max, string header = "General") where T : struct
        {
            T defaultValue = (T)AccessTools.Property(typeof(ModConfig), propertyName).GetValue(Defaults);
            T propertyValue = (T)AccessTools.Property(typeof(ModConfig), propertyName).GetValue(Config);

            switch (typeof(T))
            {
                case Type floatType when floatType == typeof(float):

                    return new ConfigValue(propertyName, propertyValue, header, defaultValue,
                        tooltip, label, Convert.ToSingle(min), Convert.ToSingle(max));
                case Type intType when intType == typeof(int):
                    return new ConfigValue(propertyName, propertyValue, header, defaultValue,
                        tooltip, label, Convert.ToInt32(min), Convert.ToInt32(max));
                default:
                    throw new ApplicationException($"Unexpected numeric type '{typeof(T).Name}'");
            }
        }


        /// <summary>
        /// Creates a configuration value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="tooltip"></param>
        /// <param name="label">If not set, will use the property name, adding spaced before each capital letter.</param>
        /// <param name="header"></param>
        /// <returns></returns>
        protected ConfigValue CreateConfigProperty(string propertyName,
            string tooltip, string label = "", string header = "General")
        {
            object defaultValue = AccessTools.Property(typeof(ModConfig), propertyName).GetValue(Defaults);
            object propertyValue = AccessTools.Property(typeof(ModConfig), propertyName).GetValue(Config);

            string formattedLabel = label == "" ? FormatUpperCaseSpaces(propertyName) : label;

            return new ConfigValue(propertyName, propertyValue, header, defaultValue, tooltip, formattedLabel);
        }

        /// <summary>
        /// Uses reflection to set the ModConfig's property that matches the ConfigValue key.
        /// Returns false if the property could not be found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        protected bool SetModConfigValue(string key, object value)
        {
            MethodInfo propertyMethod = AccessTools.Property(typeof(ModConfig), key)?.GetSetMethod();

            Action<object> setMethod = null;   //The action to set the property or field's value.


            Type propertyType = null;

            //Try property
            if (propertyMethod != null)
            {
                setMethod = (value) => propertyMethod.Invoke(Config, new object[] { value });
                propertyType = propertyMethod.GetParameters()[0].ParameterType;
            }
            //Try field
            else
            {
                FieldInfo field = AccessTools.Field(typeof(ModConfig), key);
                if (field == null)
                {
                    return false;
                }

                setMethod = (value) => { field.SetValue(Config, value); };
                propertyType = field.FieldType;
            }


            if (propertyType.IsEnum)
            {
                //Convert from string to enum
                object enumValue = Enum.Parse(propertyType, value.ToString());
                setMethod(enumValue);
            }
            else
            {
                ////Change type if needed
                //object convertedValue = Convert.ChangeType(value, propertyType);
                setMethod(value);
            }

            return true;
        }

        
        /// <summary>
        /// Calls save when the user clicks the MCM's save button.
        /// </summary>
        /// <param name="currentConfig"></param>
        /// <param name="feedbackMessage"></param>
        /// <returns></returns>
        protected virtual bool OnSave(Dictionary<string, object> currentConfig, out string feedbackMessage)
        {
            feedbackMessage = "";

            foreach (var entry in currentConfig)
            {
                SetModConfigValue(entry.Key, entry.Value);
            }

            Config.Save();

            return true;
        }
    }
}
