using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerScoreRecord
{
    public string PlayerID;      // Unique identifier for the player
    public int HighScore;        // Player's high score
    public DateTime DateAchieved; // Date and time when the score was recorded

    // Constructor for easy record creation.
    public PlayerScoreRecord(string playerID, int highScore, DateTime dateAchieved)
    {
        PlayerID = playerID;
        HighScore = highScore;
        DateAchieved = dateAchieved;
    }

    /// <summary>
    /// Converts the record into a CSV-formatted string.
    /// Date is stored in a round-trip ("o") format for precision.
    /// </summary>
    public string ToCSVString()
    {
        return $"{PlayerID},{HighScore},{DateAchieved.ToString("o", CultureInfo.InvariantCulture)}";
    }

    /// <summary>
    /// Parses a CSV string (one record) into a PlayerScoreRecord object.
    /// </summary>
    public static PlayerScoreRecord FromCSVString(string csvLine)
    {
        string[] parts = csvLine.Split(',');
        if (parts.Length != 3)
            throw new Exception("Invalid CSV line format.");

        string playerID = parts[0];
        int highScore = int.Parse(parts[1]);
        DateTime dateAchieved = DateTime.Parse(parts[2], null, DateTimeStyles.RoundtripKind);
        return new PlayerScoreRecord(playerID, highScore, dateAchieved);
    }
}

public class PlayerDatabase : MonoBehaviour
{
    private string filePath;

    private void Awake()
    {
        // Define the CSV file path in Unity's persistent data directory.
        filePath = Path.Combine(Application.persistentDataPath, "PlayerDatabase.csv");

        // If the file does not exist, create it with a header line.
        if (!File.Exists(filePath))
        {
            CreateDatabaseFile();
        }
    }

    /// <summary>
    /// Creates the CSV file and writes the header.
    /// </summary>
    private void CreateDatabaseFile()
    {
        try
        {
            // Header format: PlayerID,HighScore,Date
            File.WriteAllText(filePath, "PlayerID,HighScore,Date\n");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error creating player database file: " + ex.Message);
        }
    }

    /// <summary>
    /// Logs a new score record by appending it to the CSV file.
    /// </summary>
    /// <param name="playerID">The unique player identifier</param>
    /// <param name="highScore">The player's high score</param>
    public void LogScore(string playerID, int highScore)
    {
        PlayerScoreRecord record = new PlayerScoreRecord(playerID, highScore, DateTime.UtcNow);
        try
        {
            // Append the CSV string representation of the record to the file.
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(record.ToCSVString());
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error logging score: " + ex.Message);
        }
    }

    /// <summary>
    /// Reads the entire CSV file and returns a list of all score records.
    /// </summary>
    public List<PlayerScoreRecord> GetAllScores()
    {
        List<PlayerScoreRecord> records = new List<PlayerScoreRecord>();

        try
        {
            // Read all lines from the file.
            string[] lines = File.ReadAllLines(filePath);
            // Start at index 1 to skip the header line.
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                try
                {
                    PlayerScoreRecord record = PlayerScoreRecord.FromCSVString(lines[i]);
                    records.Add(record);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error parsing record on line {i + 1}: " + ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error reading player database file: " + ex.Message);
        }

        return records;
    }

    /// <summary>
    /// Retrieves the top score records sorted in descending order by HighScore.
    /// </summary>
    /// <param name="topCount">Maximum number of top records to return (default is 10).</param>
    public List<PlayerScoreRecord> GetTopScores(int topCount = 10)
    {
        List<PlayerScoreRecord> records = GetAllScores();
        // Sort the records by high score in descending order.
        records.Sort((a, b) => b.HighScore.CompareTo(a.HighScore));

        // Return only the top 'topCount' records.
        if (records.Count > topCount)
            records = records.GetRange(0, topCount);

        return records;
    }
}