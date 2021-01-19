using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Managers.Interfaces;

namespace Managers
{
    [Serializable]
    public class Score
    {
        public int highScore;
    }
    
    public class ScoreManager : IGameService
    {
        #region Fields
        private int m_currentScore;
        private int m_hiScore;
        #endregion Fields
        
        #region Properties
        public int CurrentScore => m_currentScore;
        public int HiScore => m_hiScore;
        #endregion Properties
        
        
        #region Methods
        public void AddScore(int score)
        {
            m_currentScore += score;
            if (m_currentScore > m_hiScore)
                m_hiScore = m_currentScore;
            
            Debug.Log($"current score: {m_currentScore} | hi-score: {m_hiScore}");
            SaveHiScore();
        }

        private void SaveHiScore()
        {
            Score newHiScore = new Score();
            newHiScore.highScore = m_hiScore;
            string json = JsonConvert.SerializeObject(newHiScore, Formatting.Indented);

            string jsonFilePath = Path.Combine(Application.persistentDataPath + "\\score", "hiscore.json");
            File.WriteAllText(jsonFilePath, json);
        }
        #endregion Methods
        
        
        #region Service Methods
        public void Initialize()
        {
            string scoreDirectory = Path.Combine(Application.persistentDataPath, "score");
            if (!Directory.Exists(scoreDirectory))
            {
                Directory.CreateDirectory(scoreDirectory);
            }

            string hiScoreFilePath = Path.Combine(scoreDirectory, "hiscore.json");
            if (File.Exists(hiScoreFilePath))
            {
                string json = File.ReadAllText(hiScoreFilePath);
                Score score = JsonConvert.DeserializeObject<Score>(json);
                m_hiScore = score.highScore;
            }
            else
            {
                m_hiScore = 0;
            }

            m_currentScore = 0;
        }
        #endregion Service Methods
    }
}