using TMPro;
using UnityEngine;
namespace FPControllerspace
{
    public class Playerview : BasicUnit
    {
        private Camera camera;
        public TextMeshProUGUI textMesh;
        private IInteractable interactableObject;
        private FPController _player;
        private FPInputManager _input;
        

        private void Awake()
        {
            _player = GetComponent<FPController>();
            _input = GetComponent<FPInputManager>();
        }
        private void Start()
        {
            camera = Camera.main;
            
            currenthealth = baseStats.health;
            currentarmor = baseStats.armor;
        }

        void Update()
        {
            //int layerMask = 1 << 10;
            Ray ray = new Ray(camera.transform.position, camera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3, layerMask))
            {
                interactableObject = hit.collider.gameObject.GetComponent<IInteractable>();

                if (interactableObject != null && interactableObject.getinteracted() == false)
                {
                    if (_input.Actioninput)
                    {
                        interactableObject.Interact();
                    }
                    
                    textMesh.text = interactableObject.GetInteractionText();


                }
                else if(interactableObject != null && interactableObject.getinteracted() == true)
                {
                    if (_input.Actioninput)
                    {
                        interactableObject.functioncall();
                    }
                    textMesh.text = interactableObject.GetInteractedText();
                }
                else
                {
                    textMesh.text = "";
                }
            }
            else
            {
                textMesh.text = "";
            }
         
        }
        private void LateUpdate()
        {
            HandleHealth();
        }

        public interface IInteractable
        {
            void Interact();
            string GetInteractionText();
            bool getinteracted();

            string GetInteractedText();

            void functioncall();
        }
    }
}