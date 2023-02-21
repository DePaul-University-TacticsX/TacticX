using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TacticsX.TeamBuilder;

namespace TacticsX.Data
{
    public static class SaveManager {
        public static string teamPath = Application.persistentDataPath + "/team.bin";

        public static void SaveTeamData(CharacterSelectButton[] team) {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(teamPath, FileMode.Create)) {
                TeamData teamData = new TeamData(team);
                formatter.Serialize(stream, teamData);
                stream.Close();
            }

            Debug.Log("Saved team to " + teamPath);
        }

        public static TeamData LoadTeamData() {
            TeamData teamData = null;
            
            if (File.Exists(teamPath)) {
                BinaryFormatter formatter = new BinaryFormatter();
                
                using (FileStream stream = new FileStream(teamPath, FileMode.Open)) {
                    teamData = formatter.Deserialize(stream) as TeamData;
                }
            }
            else {
                Debug.LogError("Save file not found for team in " + teamPath);
            }
            
            return teamData;
        }
    }
}
