using UnityEngine;

namespace Tanks
{
    public class ConditionComponent : MonoBehaviour
    {
        [SerializeField]
        protected int _healts;

        public virtual void SetDamage(int damage)
        {
            _healts -= damage;
            if(_healts<=0)
            {
                Destroy(gameObject);
            }
        }
    }
}