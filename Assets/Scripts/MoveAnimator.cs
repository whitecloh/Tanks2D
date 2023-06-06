using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class MoveAnimator : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] _tanks;
        [SerializeField]
        private SpriteRenderer _render;
        private int _lastTank=0;

        public void AnimationOnMove()
        {
            StartCoroutine(OnMove(_lastTank));
        }
        public void StopAnimationOnMove()
        {
            StopAllCoroutines();
        }
        private IEnumerator OnMove(int last)
        {
            for ( int i = last; i < _tanks.Length; i++)
            {
                _lastTank = i;
                _render.sprite = _tanks[i];
                yield return new WaitForSeconds(0.3f);
            }
            StartCoroutine(OnMove(0));
        }
    }
}
