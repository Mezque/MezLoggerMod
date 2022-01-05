

using MelonLoader;

namespace MezLoggerMod
    {
    public class Prefs
        {
            public static MelonPreferences_Category main;
            public static MelonPreferences_Entry<string> LogName;
            public static MelonPreferences_Entry<string> c1;
            public static MelonPreferences_Entry<string> c2;
            public static MelonPreferences_Entry<float> Spacing;
        public static void InitPref()
            {
                main = MelonPreferences.CreateCategory("MezLoggerMod");
                LogName = main.CreateEntry<string>("Logger Name", "MezLogger", "Logger Name Default = MezLogger");
                c1 = main.CreateEntry<string>("Logger Colour", "#6A329F", "Logger Colour Default = #6A329F");
                c2 = main.CreateEntry<string>("Text Colour", "#6A329F", "Text Colour Default = ##a1dcff");
                Spacing = main.CreateEntry<float>("Text Spacing", 25f, "Text Spacing Default = 25f");
            }
        }
    }
