using UnityEngine;

namespace AU
{

    public static class Constants
    {
#if UNITY_EDITOR && !UNITY_WEBPLAYER

		public static string DEFAULT_STRINGS_PERSITANCE_PATH { get { return "/LevelData/Resources/STRINGS.json"; } }

#else

        public static string DEFAULT_STRINGS_PERSITANCE_PATH { get { return "STRINGS"; } }

#endif


    }
}

