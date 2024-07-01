
using UnityEngine;


namespace FPControllerspace
{
    [RequireComponent(typeof(FPController))]
    public class Playercrouching : MonoBehaviour
    {
        [Header("crouchheight")]
        [SerializeField] private float crouchheight = 1.5f;
        [SerializeField] private float crouchtransitionspeed = 10f;

        float standingheight;
        float currentheight;

        FPController _player;
        FPInputManager _input;
        
        private void Awake()
        {
            _player = GetComponent<FPController>();
            _input = GetComponent<FPInputManager>();
         
        }
        private void Start()
        {
            currentheight = standingheight = _player.Height;
        }
        private void OnEnable() => _player.OnBeforeMove += OnBeforeMove;
        private void OnDisable() => _player.OnBeforeMove -= OnBeforeMove;
       void  OnBeforeMove()
        {
            var heightTarget= _input.crouchInput ? crouchheight:standingheight;
            float deltacrouch = Time.deltaTime * crouchtransitionspeed;
            currentheight = Mathf.Lerp(currentheight, heightTarget, deltacrouch);
            _player.Height = currentheight;
        }
    }
}
