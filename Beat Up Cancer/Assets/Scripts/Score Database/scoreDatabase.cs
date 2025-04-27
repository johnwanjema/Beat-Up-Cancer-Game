using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerScoreRecord
{
    public string PlayerID;       // Unique identifier for the player
    public string PlayerName;     // The player's display name
    public int HighScore;         // Player's high score
    public DateTime DateAchieved; // Timestamp when the score was recorded

    // Constructor for easy record creation.
    public PlayerScoreRecord(string playerID, string playerName, int highScore, DateTime dateAchieved)
    {
        PlayerID = playerID;
        PlayerName = playerName;
        HighScore = highScore;
        DateAchieved = dateAchieved;
    }

    /// <summary>
    /// Converts the record into a CSV-formatted string.
    /// Fields: PlayerID,PlayerName,HighScore,DateAchieved
    /// Date is stored in round-trip ("o") format for precision.
    /// </summary>
    public string ToCSVString()
    {
        return $"{PlayerID},{PlayerName},{HighScore},{DateAchieved.ToString("o", CultureInfo.InvariantCulture)}";
    }

    /// <summary>
    /// Parses a CSV string into a PlayerScoreRecord object.
    /// </summary>
    public static PlayerScoreRecord FromCSVString(string csvLine)
    {
        string[] parts = csvLine.Split(',');
        if (parts.Length != 4)
            throw new Exception("Invalid CSV line format.");

        string playerID      = parts[0];
        string playerName    = parts[1];
        int    highScore     = int.Parse(parts[2]);
        DateTime dateAchieved = DateTime.Parse(parts[3], null, DateTimeStyles.RoundtripKind);

        return new PlayerScoreRecord(playerID, playerName, highScore, dateAchieved);
    }
}

public class PlayerDatabase : MonoBehaviour
{
    private string filePath;

    private void Awake()
    {
        // CSV file path in Unity's persistent data directory
        filePath = Path.Combine(Application.persistentDataPath, "PlayerDatabase.csv");

        // Create file with header if it doesn't exist
        if (!File.Exists(filePath))
            CreateDatabaseFile();
    }

    /// <summary>
    /// Creates the CSV file and writes the header line.
    /// </summary>
    private void CreateDatabaseFile()
    {
        try
        {
            // Header: PlayerID,PlayerName,HighScore,Date
            File.WriteAllText(filePath, "PlayerID,PlayerName,HighScore,Date\n");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error creating player database file: " + ex.Message);
        }
    }

    /// <summary>
    /// Logs a new score record by appending it to the CSV file.
    /// </summary>
    /// <param name="playerID">Unique player identifier</param>
    /// <param name="playerName">Player's display name</param>
    /// <param name="highScore">The player's high score</param>
    public void LogScore(string playerID, string playerName, int highScore)
    {
        var record = new PlayerScoreRecord(playerID, playerName, highScore, DateTime.UtcNow);

        try
        {
            using (var writer = new StreamWriter(filePath, true))
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
    /// Reads all records from the CSV and returns them.
    /// </summary>
    public List<PlayerScoreRecord> GetAllScores()
    {
        var records = new List<PlayerScoreRecord>();

        try
        {
            var lines = File.ReadAllLines(filePath);
            for (int i = 1; i < lines.Length; i++) // skip header
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                try
                {
                    records.Add(PlayerScoreRecord.FromCSVString(lines[i]));
                }
                catch (Exception parseEx)
                {
                    Debug.LogError($"Error parsing line {i+1}: {parseEx.Message}");
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
    /// Returns the top N records sorted by HighScore descending.
    /// </summary>
    /// <param name="topCount">Max number of top records to return</param>
    public List<PlayerScoreRecord> GetTopScores(int topCount = 10)
    {
        var records = GetAllScores();
        records.Sort((a, b) => b.HighScore.CompareTo(a.HighScore));

        if (records.Count > topCount)
            records = records.GetRange(0, topCount);

        return records;
    }
}


