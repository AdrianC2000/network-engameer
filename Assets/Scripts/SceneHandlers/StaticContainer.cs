using UnityEngine;

namespace SceneHandlers
{
    // Class used for passing the static values between the scenes
    public class StaticContainer
    {
        public static int DeathsCounter;
        public static int CorrectAnswersCounter;
        public static int TotalAnswersCounter;
        public static bool WereStatisticsLoaded;
        public static GameObject Music; 
    }
}