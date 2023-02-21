using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Michsky.UI.Freebie;

namespace TacticsX.Data
{
    public static class SaveManager {
        static string teamPath = Application.persistentDataPath + "/team.bin";

        public static void SaveTeam(CharacterSelectButton[] team) {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(teamPath, FileMode.Create)) {
                TeamData teamData = new TeamData(team);
                formatter.Serialize(stream, teamData);
                stream.Close();
            }
        }

        public static TeamData LoadTeam() {
            TeamData teamData = null;
            
            if (File.Exists(teamPath)) {
                BinaryFormatter formatter = new BinaryFormatter();
                
                using (FileStream stream = new FileStream(teamPath, FileMode.Open)) {
                    teamData = formatter.Deserialize(stream) as TeamData;
                }
            }
            else {
                Debug.LogError("Save file not found for player team in " + teamPath);
            }
            
            return teamData;
        }
    }
}
