using System.Collections;
using UnityEngine;

namespace Tanks
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerCondition : ConditionComponent
    {

        private Vector3 _startPosition;
        [SerializeField]
        private float _immortalTime =3f;
        [SerializeField]
        private float _immortalSwitchVisual = 0.5f;

        private UImanager _ui;
        private bool _immortal;
        private SpriteRenderer _render;

        private void Start()
        {
            AudioManager.Instance.PlaySFX("Start");
            _ui = FindObjectOfType<UImanager>();
            _startPosition = transform.position;
            _render = GetComponent<SpriteRenderer>();
        }

        public override void SetDamage(int damage)
        {
            if (_immortal) return;
            _healts -= damage;

            var exp = Instantiate(_ui.GetDeadAnimation, transform.parent);
            exp.transform.position = transform.position;
            AudioManager.Instance.PlaySFX("Explosion");

            transform.position = _startPosition;
            StartCoroutine(OnImmotrality(_immortalTime));

            if (_healts <= 0)
            {
                _ui.OnEndGame();
                Destroy(gameObject);
            }
        }
        public void RageMode()
        {
            if (_immortal == false)
            {
                AudioManager.Instance.sfxSource.Stop();
                AudioManager.Instance.PlaySFX("Bonus");
                StartCoroutine(OnImmotrality(1000f));
            }
            else
            {
                StopAllCoroutines();
                _immortal = false;
                _render.enabled = true;
            }
        }
        private IEnumerator OnImmotrality(float _time)
        {
            _immortal = true;
            var time = _time;
            while(time>=0f)
            {
                _render.enabled = !_render.enabled;
                time-=1*_immortalSwitchVisual;
                yield return new WaitForSeconds(_immortalSwitchVisual);
            }
            _immortal = false;
            _render.enabled = true;
        }
    }
}
