using UnityEngine;

namespace Tanks
{
    [RequireComponent(typeof(MoveComponent))]
    public class Projectile : MonoBehaviour
    {
        public SideType _side;
        private DirectionType _direction;

        private MoveComponent _moveComponent;
        [SerializeField]
        private int _damage = 1;
        [SerializeField]
        private float _lifeTime =3f;

        private void Start()
        {
            _moveComponent = GetComponent<MoveComponent>();
            Destroy(gameObject, _lifeTime);
        }

        public void SetParams(DirectionType direction , SideType side)
        {
            (_direction, _side) = (direction, side);
        }
        private void Update()
        {
            _moveComponent.OnMove(_direction);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var shoot = collision.GetComponent<ShootComponent>();
            if(shoot!=null)
            {
                if (shoot.GetSide == _side) return;

                var condition = shoot.GetComponent<ConditionComponent>();
                condition.SetDamage(_damage);
                AudioManager.Instance.PlaySFX("BrickHit");
                Destroy(gameObject);
                return;
            }

            var cell = collision.GetComponent<CellComponent>();
            if (cell != null)
            {
                if (cell.DestroyCell)
                {
                    AudioManager.Instance.PlaySFX("BrickHit");
                    Destroy(collision.gameObject);
                }
                if (cell.DestroyProjectile)
                { 
                    Destroy(gameObject);
                }
                if(!cell.DestroyCell&&cell.DestroyProjectile)
                {
                    AudioManager.Instance.PlaySFX("SteelHit");
                }
                return;
            }

            var playerBase = collision.GetComponent<BaseComponent>();
            if(playerBase != null)
            {
                if(playerBase.GetSide!=_side)
                {
                    playerBase.ImageResult();
                    Destroy(collision.gameObject);
                    Destroy(playerBase.gameObject);
                }
            }
        }
    }
}
