using UnityEngine;

namespace Tanks
{
    public class CellComponent : MonoBehaviour
    {
        [SerializeField]
        private bool _destroyProjectile;
        [SerializeField]
        private bool _destroyCell;

        public bool DestroyProjectile => _destroyProjectile;
        public bool DestroyCell => _destroyCell;
    }
}
