using System.Collections;
using UnityEngine;

namespace Tanks
{
	[RequireComponent(typeof(MoveComponent))]
	public class EnemyController : MonoBehaviour
	{
		private MoveComponent _moveComponent;
		private PlayerCondition _player;
		private MoveAnimator _animator;
		private DirectionType _direction=DirectionType.Down;
		private int _weight=100;
		public float _moveTime = 2f;

        void Start()
		{
			_animator = GetComponent<MoveAnimator>();
			_player = FindObjectOfType<PlayerCondition>();
			_moveComponent = GetComponent<MoveComponent>();
			StartCoroutine(Moving(_moveTime));
		}

		private void Rotating()
		{
				if (_weight >= 70) _direction = DirectionType.Down;
				else if (_weight < 70 && _weight >= 50) _direction = DirectionType.Left;
				else if (_weight < 50 && _weight >= 20) _direction = DirectionType.Right;
				else if (_weight < 20) _direction = DirectionType.Up;

			StartCoroutine(Moving(_moveTime));
		}
		IEnumerator Moving(float time)
		{
			yield return null;
			_animator.AnimationOnMove();
			var _time = time;

			while (_time > 0)
			{
				_moveComponent.OnMove(_direction);
				_time -= Time.deltaTime;
				yield return new WaitForFixedUpdate();
			}

			_weight = Random.Range(0, 100);
			Rotating();
		}

        private void OnCollisionEnter2D(Collision2D collision) // изменение направления при столкновении с другими обьектами
        {
			var cell = collision.gameObject.GetComponent<CellComponent>();
			var baseComp = collision.gameObject.GetComponent<BaseComponent>();
			var tank = collision.gameObject.GetComponent<ShootComponent>();

			if((baseComp!=null)||(cell != null && !cell.DestroyCell)||(tank != null))
            {
				StopAllCoroutines();
				_weight = Random.Range(109 - _weight,91-_weight);
				Rotating();
			}
			if(tank!=null)
            {
				gameObject.GetComponent<Rigidbody2D>().mass = 50f;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
			var tank = collision.gameObject.GetComponent<ShootComponent>();
			if (tank == null) return;
				gameObject.GetComponent<Rigidbody2D>().mass = 1f;
		}

        public void OnPlayerShooting() // направление ботов на игрока во время его стрельбы
        {
				StopAllCoroutines();
				float xDif = transform.position.x - _player.transform.position.x;
				float yDif = transform.position.y - _player.transform.position.y;
				if (xDif>0 && Mathf.Abs(xDif) >= Mathf.Abs(yDif))
				{
					_weight = 60;
				}
				if (xDif<=0 && Mathf.Abs(xDif) >= Mathf.Abs(yDif))
				{
					_weight = 40;
				}
				if (yDif>0&& Mathf.Abs(xDif)< Mathf.Abs(yDif))
				{
					_weight = 80;
				}
				if (yDif<=0 && Mathf.Abs(xDif) < Mathf.Abs(yDif))
				{
					_weight = 10;
				}
				Rotating();			
        }
    }
	}