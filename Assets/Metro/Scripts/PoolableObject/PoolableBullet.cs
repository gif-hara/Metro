using System.Collections;
using System.Collections.Generic;
using Metro.CharacterController;
using UniRx.Toolkit;
using UnityEngine;
using UnityEngine.Assertions;

namespace Metro.ObjectPool
{
	[RequireComponent(typeof(Bullet))]
	public class PoolableBullet : ObjectPool<Bullet>
	{
		private Bullet prefab;
		
		public PoolableBullet(Bullet prefab)
		{
			this.prefab = prefab;
		}

		protected override Bullet CreateInstance()
		{
			return Object.Instantiate(this.prefab);
		}
	}
}
