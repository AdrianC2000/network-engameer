using UnityEngine;

public class QuestInputHandler : MonoBehaviour
{
    [SerializeField] 
    private GameObject questInputUI;

    public static QuestInput questInput;
    
    private void Update()
    {
        if (questInputUI.active)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                questInput.CheckInput();
            }
        }
    }
}
