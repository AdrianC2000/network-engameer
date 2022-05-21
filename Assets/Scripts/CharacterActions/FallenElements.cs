using UnityEngine;

namespace CharacterActions
{
    public class FallenElements : MonoBehaviour
    {
        [SerializeField] 
        private GameObject fallenElement;

        private Vector3 startingPosition; 

        private void Start()
        {
            startingPosition = fallenElement.transform.position;
        }
        private void Update()
        {
            if (fallenElement.transform.position.y <= -10)
            {
                Rigidbody rb = fallenElement.gameObject.GetComponent<Rigidbody>();
                fallenElement.transform.position = startingPosition;
                Destroy(rb);
            }
        }
    }
}