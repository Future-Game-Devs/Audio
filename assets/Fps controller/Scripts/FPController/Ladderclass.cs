using UnityEngine;


namespace FPControllerspace
{
    public class Ladderclass : MonoBehaviour, Playerview.IInteractable
    {
        public FPController _player;
        public string myString;
        public string interacted;
        public Transform snapbuttom;
        public Transform snaptop;
        public bool snaped = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 6) 
            {
               // _player = other.gameObject.GetComponent<FPController>();
                _player.state = FPController.State.climbing;
                snaped = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 6) 
            {
               // _player = other.gameObject.GetComponent<FPController>();
                _player.state = FPController.State.walking;
                snaped = false;
            }
        }

        private void SnapToBottom()
        {
            _player.transform.position = snapbuttom.position;
            Physics.SyncTransforms();
            _player.state = FPController.State.climbing;
            snaped = true;
        }

        private void SnapToTop()
        {
            _player.transform.position = snaptop.position;
            Physics.SyncTransforms();
            _player.state = FPController.State.climbing;
            
            snaped = true;
            
        }

        public void Interact()
        {
            if (_player.transform.position.y > snaptop.position.y)
            {
                SnapToTop();
            }
            else
            {
                SnapToBottom();
            }
        }
        public void functioncall()
        {
            if (_player.transform.position.y > snapbuttom.position.y)
            {
                _player.state = FPController.State.laddersliding;
            }
        }
        public string GetInteractionText()
        {
            return myString; // Return the defined interaction text
        }
        public bool getinteracted()
        {
            return snaped;
        }
        public string GetInteractedText()
        {
            return interacted;
        }
    }
}