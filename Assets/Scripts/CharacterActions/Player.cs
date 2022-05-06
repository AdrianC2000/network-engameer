using UnityEngine;

namespace CharacterActions
{
    public class Player
    {
        private CharacterController _character;
        private Vector3 _startingPosition;
        private int _deathsCounter;
        private int _correctAnswersCounter;
        private int _totalAnswersCounter; 
        
        public Player(CharacterController character, Vector3 startingPosition)
        {
            _character = character;
            _startingPosition = startingPosition;
            _deathsCounter = 0;
            _correctAnswersCounter = 0;
        }

        public CharacterController GetCharacter()
        {
            return _character;
        }
        
        public Vector3 GetStartingPosition()
        {
            return _startingPosition;
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
        
    }
}