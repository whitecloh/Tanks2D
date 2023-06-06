using System.Collections;
using UnityEngine;

namespace Tanks
{
    public class ShootComponent : MonoBehaviour
    {
        private bool _canShoot = true;
        [SerializeField]
        private float _delayShoot = 0.5f;
        [SerializeField]
        private Projectile _bulletPrefab;
        [SerializeField]
        private SideType _side;
        [SerializeField]
        private Transform _shootPosition;

        public SideType GetSide => _side;
            
        public void OnShoot()
        {
            if (_canShoot==false) return;
            var bullet = Instantiate(_bulletPrefab, _shootPosition.position, transform.rotation);
            bullet.SetParams(transform.eulerAngles.ConvertRotationFromType(), _side);
            StartCoroutine(Shoot());

            if(_side==SideType.Player)
            {
                var enemies = FindObjectsOfType<EnemyController>();
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].OnPlayerShooting();
                }
            }
            if(_side==SideType.Enemy)
            {
                _delayShoot = Random.Range(0.5f, 2f);
            }
        }
        private IEnumerator Shoot()
        {
            _canShoot = false;
            AudioManager.Instance.PlaySFX("Shoot");
            yield return new WaitForSeconds(_delayShoot);
            _canShoot = true;
        }
    }
}