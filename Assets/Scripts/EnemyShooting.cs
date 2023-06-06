using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    [RequireComponent(typeof(ShootComponent))]

       public class EnemyShooting : MonoBehaviour
    {
        private ShootComponent _shootComponent;

        private void Start()
        {
            _shootComponent = GetComponent<ShootComponent>();
            StartCoroutine(Shooting());
        }

        private IEnumerator Shooting()
        {
            _shootComponent.OnShoot();
            yield return null;
            StartCoroutine(Shooting());
        }
    }
}
