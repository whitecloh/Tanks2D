using System.Collections;
using UnityEngine;
namespace Tanks
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyCondition : ConditionComponent
    {
        [SerializeField]
        private float _immortalTime = 3f;
        [SerializeField]
        private float _immortalSwitchVisual = 0.5f;
        private MoveAnimator _animator;
        private bool _immortal;
        private SpriteRenderer _render;

        private UImanager _ui;

        private void Start()
        {
            _ui = FindObjectOfType<UImanager>();
            _animator = GetComponent<MoveAnimator>();
            _render = GetComponent<SpriteRenderer>();
        }

        public override void SetDamage(int damage)  //при получении урона
        {
            if (_immortal) return;

            _healts -= damage;
            AudioManager.Instance.PlaySFX("BrickHit");
            StartCoroutine(OnImmotrality());

            if (_healts <= 0)
            {
                var spawner = FindObjectOfType<Spawner>();
                AudioManager.Instance.PlaySFX("EnemyExplosion");
                var exp = Instantiate(_ui.GetDeadAnimation, transform.parent);
                exp.transform.position = transform.position;
                _animator.StopAnimationOnMove();
                Destroy(gameObject);
                spawner.StartCoroutine(spawner.SpawnAfterDead());              
            }
        }

        private IEnumerator OnImmotrality() // неу€звимость
        {
            _immortal = true;
            var time = _immortalTime;

            while (time >= 0f)
            {
                _render.enabled = !_render.enabled;
                time -= 1 * _immortalSwitchVisual;
                yield return new WaitForSeconds(_immortalSwitchVisual);
            }

            _immortal = false;
            _render.enabled = true;
        }
    }
}
