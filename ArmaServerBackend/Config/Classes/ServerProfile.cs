using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ArmaServerBackend
{
    /// <summary>
    /// ServerProfile
    /// </summary>
    public class ServerProfile : Helpers
    {
        private MissionDifficulty getMissionDifficulty(PBOFile pbo) => (pbo.IsEnabled && pbo.ModType == PboModType.Mission) ? pbo.MissionDifficulty : MissionDifficulty.recruit;
        private MissionDifficulty getMissionDifficulty()
        {
            MissionDifficulty missionDifficulty = MissionDifficulty.unselected;
            foreach (var pbo in DLL.ConfigValues.Pbos)
            {
                var dif = getMissionDifficulty(pbo);
                if ((int)dif > (int)missionDifficulty) missionDifficulty = dif;
            };
            return missionDifficulty;
        }


        public MissionDifficulty missionDifficulty => getMissionDifficulty(); 
        public decimal SkillAI { get; set; }
        public decimal PrecisionAI { get; set; }
        public int AILevelPreset { get; set; }
        public List<ConfigSetting> DifficultyItems { get; set; }


        /// <summary>
        /// Get difficulty options.
        /// </summary>
        /// <returns></returns>
        private string GetDifficultOptions()
        {
            var profileOptions = 
                NewTab(2) + "class Options" + NewLine() + 
                NewTab(2) + "{" + NewLine();
            foreach (var diffItem in DifficultyItems) profileOptions += 
                    NewTab(3) + diffItem.ToString() + NewLine();
            profileOptions += 
                NewTab(2) + "};" + NewLine();
            return profileOptions;
        }

        /// <summary>
        /// Convent to user friendly string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var profileString = "";
            string difficulty = DLL.HelperFunctions.Capitalize(System.Enum.GetName(typeof(MissionDifficulty), (int)missionDifficulty));
            profileString +=
                "difficulty=\"" + difficulty + "\";" + NewLine() +
                "class DifficultyPresets" + NewLine() +
                "{" + NewLine() +
                NewTab() + "class CustomDifficulty" + NewLine() +
                NewTab() + "{" + NewLine() + GetDifficultOptions() +
                NewTab(2) + "aiLevelPreset=" + AILevelPreset.ToString(CultureInfo.InvariantCulture) + ";" + NewLine() + NewLine() +
                NewTab(2) + "class CustomAILevel" + NewLine() +
                NewTab(2) + "{" + NewLine() +
                NewTab(3) + "skillAI=" + SkillAI.ToString(CultureInfo.InvariantCulture) + ";" + NewLine() +
                NewTab(3) + "precisionAI=" + PrecisionAI.ToString(CultureInfo.InvariantCulture) + ";" + NewLine() +
                NewTab(2) + "};" + NewLine() +
                NewTab() +  "};" + NewLine(2) +
                "};";
            return profileString;
        }

        /// <summary>
        /// Write Saved Server Profile
        /// </summary>
        /// <param name="file"></param>
        internal override void WriteFile(string file, string _ = null) => DLL.HelperFunctions.WriteFile(file, DLL.ConfigValues.serverProfile.ToString());
    }
}
