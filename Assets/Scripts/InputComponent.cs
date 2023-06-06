using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks
{
    [RequireComponent(typeof(MoveComponent),typeof(ShootComponent))]
    public class InputComponent : MonoBehaviour
    {
        private DirectionType _lastType;
        private MoveComponent _moveComponent;
        private ShootComponent _shootComponent;
        private PlayerCondition _playerCondition;
        private MoveAnimator _animator;
        [SerializeField]
        private InputAction _move;
        [SerializeField]
        private InputAction _shoot;
        [SerializeField]
        private InputAction _rage;
        private bool _colisionSteel;

        private void Start()
        {
            _animator = GetComponent<MoveAnimator>();
            _moveComponent = GetComponent<MoveComponent>();
            _shootComponent = GetComponent<ShootComponent>();
            _playerCondition = GetComponent<PlayerCondition>();
            _move.Enable();
            _shoot.Enable();
            _rage.Enable();
            _move.performed += MoveAudio;
            _move.canceled += StopAudio;
            _rage.canceled += Rage;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var cell = collision.gameObject.GetComponent<CellComponent>();
            if (cell == null) return;
            if(!cell.DestroyCell&&cell.DestroyProjectile)
            {
                _colisionSteel = true;
                _move.Dispose();
            }
            else
            {
                _colisionSteel = false;
            }
            _move.Enable();
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            _move.Dispose();
            _colisionSteel = false;
            _move.Enable();
        }
        private void MoveAudio(InputAction.CallbackContext obj)
        {
            if(_colisionSteel==true)
            AudioManager.Instance.PlayOnMove("Stacked");
            else
            AudioManager.Instance.PlayOnMove("Move");
            _animator.AnimationOnMove();
        }
        private void StopAudio(InputAction.CallbackContext obj)
        {
            AudioManager.Instance.StopOnMove();
            _animator.StopAnimationOnMove();
        }
        private void Rage(InputAction.CallbackContext obj)
        {
            _playerCondition.RageMode();
        }
        private void Update()
        {
            var shoot = _shoot.ReadValue<float>();
            if (shoot == 1f)
            {
                _shootComponent.OnShoot();
            }
            var direction = _move.ReadValue<Vector2>();
            DirectionType type;
            if(direction.x!=0f && direction.y!=0f)
            {
                type = _lastType; 
            }
            else if (direction.x == 0f && direction.y == 0f)
            {
                return;
            }
            else
            {
                type = _lastType = direction.ConvertDirectionFromType();
            }
            _moveComponent.OnMove(type);
        } 
        private void OnDestroy()
        {
            _move.Dispose();
            _shoot.Dispose();
            _rage.Dispose();
        }
    }
}