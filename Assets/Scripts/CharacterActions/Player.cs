using System;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterActions
{
    public class Player
    {
        private CharacterController _character;
        private Vector3 _respawnPosition;
        private int _deathsCounter;
        private int _correctAnswersCounter;
        private int _totalAnswersCounter; 
        private bool _firstQuestCall; // True if the quest was loaded, false if it needs to be loaded
        private List<string> _usedQuestDevices;
        private bool _wasStatisticsMenuOn; 
        
        public Player(CharacterController character, Vector3 respawnPosition)
        {
            _character = character;
            _respawnPosition = respawnPosition;
            _deathsCounter = 0;
            _correctAnswersCounter = 0;
            _firstQuestCall = false;
            _usedQuestDevices = new List<string>();
            _wasStatisticsMenuOn = false;
        }

        public CharacterController GetCharacter()
        {
            return _character;
        }
        
        public Vector3 GetRespawnPosition()
        {
            return _respawnPosition;
        }
        
        public void SetRespawnPosition(Vector3 respawnPosition)
        {
            _respawnPosition = respawnPosition;
        }
        
        public int GetDeathsCounter()
        {
            return _deathsCounter;
        }
        
        public int GetCorrectAnswersCounter()
        {
            return _correctAnswersCounter;
        }
        
        public int GetTotalAnswersCounter()
        {
            return _totalAnswersCounter;
        }

        public void IncreaseDeathsCounter()
        {
            _deathsCounter = _deathsCounter+=1;
        }
        
        public void IncreaseCorrectAnswersCounter()
        {
            _correctAnswersCounter = _correctAnswersCounter+=1;
        }
        
        public void IncreaseTotalAnswersCounter()
        {
            _totalAnswersCounter = _totalAnswersCounter+=1;
        }
        
        public bool GetFirstQuestCall()
        {
            return _firstQuestCall;
        }
        
        public void SetFirstQuestCall(bool firstQuestCall)
        {
            _firstQuestCall = firstQuestCall;
        }

        public List<string> GetUsedQuestDevices()
        {
            return _usedQuestDevices;
        }
        
        public void AddUsedDevicesWithQuest(string deviceName)
        {
            _usedQuestDevices.Add(deviceName);
        }

        public void setWasStatisticsMenuOn(bool wasStatisticsMenuOn)
        {
            _wasStatisticsMenuOn = wasStatisticsMenuOn;
        }
        
        public bool wasStatisticsMenuOn()
        {
            return _wasStatisticsMenuOn;
        }
        
    }
}