using System;
using System.Collections;
using System.Collections.Generic;
using Metro.ObjectPool;
using UniRx;
using UnityEngine;


namespace Metro.CharacterController
{
	public class Bullet : MonoBehaviour
	{
		[SerializeField]
		private float destroyDuration;
		
		private Vector3 velocity;

		private Transform cachedTransform;

		private static Dictionary<string, PoolableBullet> pools = new Dictionary<string, PoolableBullet>();

		void Awake()
		{
			this.cachedTransform = this.transform;
		}

		void Update()
		{
			this.cachedTransform.localPosition += this.velocity * Time.deltaTime;
		}
		
		public void Fire(Vector3 position, Vector2 velocity)
		{
			var pool = GetPool(this);
			var instance = pool.Rent();
			instance.cachedTransform.localPosition = position;
			instance.velocity = velocity;

			Observable.Timer(TimeSpan.FromSeconds(this.destroyDuration))
				.TakeUntilDisable(instance)
				.SubscribeWithState2(instance, pool, (l, _instance, _pool) =>
				{
					_pool.Return(_instance);
				});
		}

		private static PoolableBullet GetPool(Bullet bullet)
		{
			PoolableBullet result;
			if (!pools.TryGetValue(bullet.name, out result))
			{
				result = new PoolableBullet(bullet);
				pools.Add(bullet.name, result);
			}

			return result;
		}
	}
}
