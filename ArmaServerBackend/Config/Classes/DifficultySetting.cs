using System.Collections.Generic;
using System.Globalization;

namespace ArmaServerBackend
{
    public class DifficultySetting
    {
        public MissionDifficulty missionDifficulty { get; set; }
        public decimal SkillAI { get; set; }
        public decimal PrecisionAI { get; set; }
        public int AILevelPreset { get; set; }
        public List<ConfigSetting> DifficultyItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>        
        private string GetProfileSkills()
        {
            var profileSkills = "";

            profileSkills +=
                "\t\taiLevelPreset=" +
                AILevelPreset.ToString(CultureInfo.InvariantCulture) +
                ";" + Helpers.NewLine() + Helpers.NewLine() +
                "\t\tclass CustomAILevel" + Helpers.NewLine() +
                "\t\t{" + Helpers.NewLine() +
                "\t\t\tskillAI=" + SkillAI.ToString(CultureInfo.InvariantCulture) + ";" + Helpers.NewLine() +
                "\t\t\tprecisionAI=" + PrecisionAI.ToString(CultureInfo.InvariantCulture) + ";" +
                Helpers.NewLine() +
                "\t\t};" + Helpers.NewLine();

            return profileSkills;
        }

        /// <summary>
        /// Get difficulty flags string.
        /// </summary>
        /// <returns></returns>
        private string GetProfileOptions()
        {
            var profileOptions = Helpers.NewTab(2) + "class Options" + Helpers.NewLine() + Helpers.NewTab(2) + "{" + Helpers.NewLine();
            foreach (var diffItem in DifficultyItems) profileOptions += Helpers.NewTab(3) + diffItem.ToString() + Helpers.NewLine();
            profileOptions += Helpers.NewTab(2) + "};" + Helpers.NewLine();
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
                "difficulty=\"" + difficulty + "\";" + Helpers.NewLine() +
                "class DifficultyPresets" + Helpers.NewLine() +
                "{" + Helpers.NewLine() +
                Helpers.NewTab() + "class CustomDifficulty" + Helpers.NewLine() +
                Helpers.NewTab() + "{" + Helpers.NewLine() +
                GetProfileOptions() +
                GetProfileSkills() +
                 Helpers.NewTab() + "};" + Helpers.NewLine(2) +
                "};";

            return profileString;
        }
    }
}
