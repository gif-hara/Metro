using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Framework.Extensions
{
	/// <summary>
	/// 計算系処理拡張クラス.
	/// </summary>
	public static partial class Extensions
	{
		/// <summary>
		/// 浮動小数点型の等値比較を行う.
		/// </summary>
		/// <returns><c>true</c> if is equal the specified a b threshold; otherwise, <c>false</c>.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		/// <param name="threshold">Threshold.</param>
		public static bool IsEqual(float a, float b, float threshold = 0.0001f)
		{
			a = a - b;
			a = a < 0 ? -a : a;
			return a <= threshold;
		}

		/// <summary>
		/// 右回りであるか返す.
		/// </summary>
		/// <returns><c>true</c> if is clockwise the specified current target; otherwise, <c>false</c>.</returns>
		/// <param name="currentAngle">Current angle.</param>
		/// <param name="targetAngle">Target angle.</param>
		public static bool IsClockwise(float currentAngle, float targetAngle)
		{
			return targetAngle > currentAngle ? !(targetAngle - currentAngle > 180.0f) : currentAngle - targetAngle > 180.0f;
		}

		/// <summary>
		/// 角度の正規化したものを返す.
		/// </summary>
		/// <returns>The angle.</returns>
		/// <param name="angle">Angle.</param>
		/// <param name="currentAngle">Current angle.</param>
		/// <param name="targetAngle">Target angle.</param>
		public static float NormalizeAngle(float angle, float currentAngle, float targetAngle)
		{
			return (angle + (Extensions.IsClockwise(currentAngle, targetAngle) ? -360.0f : 360.0f)) % 360.0f;
		}

		/// <summary>
		/// 0を含む符号値を返す.
		/// a = 0なら0を返す.
		/// a > 0なら1を返す.
		/// a < 0なら-1を返す.
		/// </summary>
		/// <returns>The sign.</returns>
		/// <param name="a">The alpha component.</param>
		public static float ZSign(float a)
		{
			return IsEqual(a, 0) ? 0 : Mathf.Sign(a);
		}

		/// <summary>
		/// <paramref name="angle"/>を<paramref name="number"/>で分割した値を返す
		/// </summary>
		/// <remarks>
		/// <paramref name="applyFixedAngle"/>が<c>true</c>の場合は上下方向が軸になります
		/// </remarks>
		public static int Digitalize(this float angle, int number, bool applyFixedAngle = true)
		{
			Assert.AreNotEqual(number, 0);
			if (number == 1)
			{
				return 0;
			}
			
			var splitAngle = 360 / number;
			var fixedAngle = applyFixedAngle ? 0.0f : (splitAngle / 2.0f);
			for (var i = 1; i < number; i++)
			{
				var min = (splitAngle * i) - (fixedAngle * i);
				var max = min + splitAngle;
				if (angle >= min && angle < max)
				{
					return i;
				}
			}

			return 0;
		}
	}
}