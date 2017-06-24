using System.Collections;
using System.Collections.Generic;
using Metro.ObjectPool;
using UnityEngine;


namespace Metro.CharacterController
{
	public class Bullet : MonoBehaviour
	{
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
			var instance = GetPool(this).Rent();
			instance.cachedTransform.localPosition = position;
			instance.velocity = velocity;
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
