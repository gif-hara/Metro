using System.Collections.Generic;
using Metro.ObjectPool;
using UnityEngine;

namespace Metro.CharacterController
{
	/// <summary>
	/// 銃口クラス
	/// </summary>
	public class Muzzle : MonoBehaviour
	{
		[SerializeField]
		private Bullet bullet;

		[SerializeField]
		private float speed;

		private Transform cachedTransform;

		void Awake()
		{
			this.cachedTransform = this.transform;
		}

		public void Fire()
		{
			this.bullet.Fire(this.cachedTransform.position, this.cachedTransform.right * this.speed);
		}
	}
}
