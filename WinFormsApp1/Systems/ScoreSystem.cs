using System;
using System.IO;

namespace GameProjectOop.Systems
{
    public class ScoreSystem
    {
        private string filePath = "kills_history.txt";

        public void RecordKills(string level, int kills)
        {
            try
            {
                // Format: Date Time - Level: [LevelName], Kills: [Count]
                string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Level: {level}, Kills: {kills}{Environment.NewLine}";
                File.AppendAllText(filePath, entry);
            }
            catch (Exception)
            {
                // Silently fail to avoid crashing the game if file I/O fails
            }
        }
    }
}
