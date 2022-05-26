using UnityEngine;

namespace CharacterActions
{
    public class FallenElements : MonoBehaviour
    {
        [SerializeField] 
        private GameObject fallenElement;

        private Vector3 startingPosition; 
        private Quaternion startingRotation;

        private void Start()
        {
            startingPosition = fallenElement.transform.position;
            startingRotation = fallenElement.transform.rotation;
        }
        private void Update()
        {
            if (fallenElement.transform.position.y <= -10)
            {
                Rigidbody rb = fallenElement.gameObject.GetComponent<Rigidbody>();
                fallenElement.transform.position = startingPosition;
                fallenElement.transform.rotation = startingRotation;
                Destroy(rb);
            }
        }
    }
}