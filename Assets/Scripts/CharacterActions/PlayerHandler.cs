using CharacterActions;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    private Player _player;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip falling; 
    [SerializeField] private AudioClip correctAnswer; 
    [SerializeField] private AudioClip wrongAnswer; 
    
    private void Start()
    {
        Quest.ReloadQuestsFile(); // TODO -> move those reloads into the finish of the game, not the start
        QuestInput.ReloadQuestsFile();
        _player = new Player(character, character.transform.position);
        Collider.Player = _player;
        PauseMenu.Player = _player;
        StatisticsMenu.Player = _player;
        Quest.audiosource = audioSource;
        QuestInput.audiosource = audioSource;
        Quest.correctAnswer = correctAnswer;
        QuestInput.correctAnswer = correctAnswer;
        Quest.wrongAnswer = wrongAnswer;
        QuestInput.wrongAnswer = wrongAnswer;
    }

    private void Update()
    {
        RespawnIfFallen();
    }

    private void RespawnIfFallen()
    {
        if (character.transform.position.y <= -10)
        {
            Respawn(_player);
            _player.IncreaseDeathsCounter();
            audioSource.clip = falling;
            audioSource.Play();
        }
    }

    public static void Respawn(Player player)
    {
        player.GetCharacter().enabled = false;
        player.GetCharacter().transform.position = player.GetRespawnPosition();
        player.GetCharacter().enabled = true;
    }
}