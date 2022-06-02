using System;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterActions
{
    public class Player
    {
        private CharacterController _character;
        private Vector3 _respawnPosition;
        private bool _firstQuestCall; // True if the quest was loaded, false if it needs to be loaded
        private List<string> _usedQuestDevices;
        
        public Player(CharacterController character, Vector3 respawnPosition)
        {
            _character = character;
            _respawnPosition = respawnPosition;
            _firstQuestCall = false;
            _usedQuestDevices = new List<string>();
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
    }
}