using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigurationUtils
{
    static ConfigurationData data = null;

    public static List<ConfigurationData.Table> Tables => data.Tables;

    public static bool Initialized { get; private set; }

    public static void Initialize()
    {
        data = new ConfigurationData();
        Initialized = true;
    }
}
