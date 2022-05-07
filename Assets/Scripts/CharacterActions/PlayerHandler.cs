using CharacterActions;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField]
    private CharacterController character;
    private Player _player;

    private void Start()
    {
        Quest.ReloadQuestsFile();
        _player = new Player(character, character.transform.position);
        Collider.Player = _player;
        PauseMenu.Player = _player; 
        StatisticsMenu.Player = _player;
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
        }
    }

    public static void Respawn(Player player)
    {
        player.GetCharacter().enabled = false;
        player.GetCharacter().transform.position = player.GetStartingPosition();
        player.GetCharacter().enabled = true;
    }
}
