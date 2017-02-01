using System;
using System.Collections.Generic;
using UnityEngine;

namespace TweenCore
{
	public static class FloatTools
	{
		///<summary>
		/// same as `1 - rate`
		///</summary>
		public static float Revert(this float rate)
		{
			return 1 - rate;
		}
		
		///<summary>
		/// Clamps a `value` between `min` and `max`.
		///</summary>
		public static float Clamp(this float value, float min = 0.0f, float max = 1.0f)
		{
			if (value <= min) {
				return min;
			} else if (max <= value) {
				return max;
			} else {
				return value;
			}
		}
		
		///<summary>
		/// Linear interpolation between `from` and `to` by `rate`
		///</summary>
		public static float Lerp(this float rate, float from, float to)
		{
			return from * (1 - rate) + to * rate;
		}
		
		///<summary>
		/// Normalizes a `value` within the range between `from` and `to` into a value between 0 and 1
		///</summary>
		public static float InverseLerp(this float value, float from, float to)
		{
			return (value - from) / (to - from);
		}
		
		public static float Repeat(this float value, float from = 0.0f, float to = 1.0f)
		{
			var p = InverseLerp(value, from, to);
			return p - Mathf.Floor(p);
		}
		
		public static float Shake(this float rate, float center, Func<float> randomFunc)
		{
			if (randomFunc == null)
				randomFunc = (() => UnityEngine.Random.value);
			return center + Spread(randomFunc(), rate);
		}
		
		///<summary>
		/// same as `FloatTools.Lerp(rate, -scale, scale)`
		///</summary>
		public static float Spread(this float rate, float scale)
		{
			return Lerp(rate, -scale, scale);
		}
		
		public static float SinByRate(this float rate)
		{
			return Mathf.Sin(rate * 2 * Mathf.PI);
		}
		
		public static float CosByRate(this float rate)
		{
			return Mathf.Cos(rate * 2 * Mathf.PI);
		}
		
		// =================================================
		// Round Trip
		// =================================================
		///<summary>
		/// Round trip motion that goes from 0.0 to 1.0 and returns to 0.0 in the reverse playback movement.
		///</summary>
		public static float Yoyo(this float rate, Func<float, float> easing)
		{
			return easing(((rate < 0.5f) ? rate : (1 - rate)) * 2);
		}
		///<summary>
		/// Round trip motion that goes from 0.0 to 1.0 and returns to 0.0 with the movement in which the moving direction is reversed.
		///</summary>
		public static float Zigzag(this float rate, Func<float, float> easing)
		{
			return (rate < 0.5f) ? easing(rate * 2) : 1 - easing((rate - 0.5f) * 2);
		}
		
		
		// =================================================
		// Complex Easing
		// =================================================
		///<summary>
		/// Intermediate easing between the two easings
		///</summary>
		public static float MixEasing(
			float rate,
			Func<float, float> easing1,
			Func<float, float> easing2,
			float easing2Strength)
		{
			return easing2Strength.Lerp(
				easing1(rate),
				easing2(rate)
			);
		}
		///<summary>
		/// Gradually changes to another easing at the beginning and at the end
		///</summary>
		public static float CrossfadeEasing(
			float rate,
			Func<float, float> easing1,
			Func<float, float> easing2,
			Func<float, float> easing2StrengthEasing,
			float easing2StrengthStart = 0.0f,
			float easing2StrengthEnd = 1.0f)
		{
			return easing2StrengthEasing(rate).Lerp(
				easing2StrengthStart,
				easing2StrengthEnd
			).Lerp(
				easing1(rate),
				easing2(rate)
			);
		}
		
		public static float ConnectEasing(
			float time,
			Func<float, float> easing1,
			Func<float, float> easing2,
			float switchTime = 0.5f,
			float switchValue = 0.5f
		)
		{
			if (time < switchTime) {
				return easing1(time.InverseLerp(0, switchTime)).Lerp(0, switchValue);
			} else {
				return easing2(time.InverseLerp(switchTime, 1)).Lerp(switchValue, 1);
			}
		}
		
		public static float oneTwoEasing(
			float time,
			Func<float, float> easing1,
			Func<float, float> easing2,
			float switchTime = 0.5f)
		{
			if (time < switchTime) {
				return easing1(time.InverseLerp(0, switchTime));
			} else {
				return easing2(time.InverseLerp(switchTime, 1));
			}
		}
		
		// =================================================
		// Float Array
		// =================================================
		
		///<summary>
		///<returns>0 to sortedValues.Count integer</returns>
		///</summary>
		public static int FloatBinarySearch(this IList<float> sortedValues, float value, BoundaryMode boundaryMode = BoundaryMode.Low)
		{
			var min = 0;
			var max = sortedValues.Count;
			if (boundaryMode == BoundaryMode.Low) {
				while (true) {
					var next = (max - min) / 2 + min;
					var dv = sortedValues[next];
					if (dv <= value) {
						min = next + 1;
					} else {
						max = next;
					}
					if (min == max) break;
				}
			} else {
				while (true) {
					var next = (max - min) / 2 + min;
					var dv = sortedValues[next];
					if (dv < value) {
						min = next + 1;
					} else {
						max = next;
					}
					if (min == max) break;
				}
			}
			return min;
		}
		
		
		// =================================================
		// Polyline
		// =================================================
		public static float Polyline(this float rate, IList<float> values)
		{
			if (values.Count < 2) {
				throw new Exception("points Count must be more than 2");
			} else {
				var max = values.Count - 1;
				var scaledRate = rate * max;
				var index = Mathf.FloorToInt(Clamp(scaledRate, 0, max - 1));
				var innerRate = scaledRate - index;
				return Lerp(innerRate, values[index], values[index + 1]);
			}
		}
		
		
		// =================================================
		// Bernstein Polynomial
		// =================================================
		///<summary>
		/// Quadratic Bernstein polynomial
		///</summary>
		public static float Bezier2(this float rate, float from, float control, float to)
		{
			return Lerp(rate, Lerp(rate, from, control), Lerp(rate, control, to));
		}
		///<summary>
		/// Cubic Bernstein polynomial
		///</summary>
		public static float Bezier3(this float rate, float from, float control1, float control2, float to)
		{
			return Bezier2(rate, Lerp(rate, from, control1), Lerp(rate, control1, control2), Lerp(rate, control2, to));
		}
		///<summary>
		/// Bernstein polynomial, which is the mathematical basis for Bézier curve
		///</summary>
		public static float Bezier(this float rate, IList<float> values)
		{
			if (values.Count < 2) {
				throw new Exception("points Count must be more than 2");
			} else if (values.Count == 2) {
				return Lerp(rate, values[0], values[1]);
			} else if (values.Count == 3) {
				return Bezier2(rate, values[0], values[1], values[2]);
			} else {
				return _Bezier(rate, values);
			}
		}
		
		private static float _Bezier(this float rate, IList<float> values)
		{
			if (values.Count == 4) {
				return Bezier3(rate, values[0], values[1], values[2], values[3]);
			}
			var list = new List<float>();
			for (var i = 0; i < values.Count - 1; i++)
			{
				list.Add(Lerp(rate, values[i], values[i + 1]));
			}
			return _Bezier(rate, list);
		}
		
		///<summary>
		///Uniform Quadratic B-spline
		///</summary>
		public static float UniformQuadraticBSpline(this float rate, IList<float> values)
		{
			if (values.Count < 2) {
				throw new Exception("points Count must be more than 2");
			} else if (values.Count == 2) {
				return Lerp(rate, values[0], values[1]);
			} else {
				var max = values.Count - 2;
				var scaledRate = rate * max;
				var index = Mathf.FloorToInt(Clamp(scaledRate, 0, max - 1));
				var innerRate = scaledRate - index;
				var p0 = values[index];
				var p1 = values[index + 1];
				var p2 = values[index + 2];
				return innerRate * innerRate * (p0 / 2 - p1 + p2 / 2) + innerRate * (-p0 + p1) + p0 / 2 + p1 / 2;
			}
		}
		
		
		// =================================================
		// Converter
		// =================================================
		public static float FrameToSecond(this float frame, float fps)
		{
			return frame / fps;
		}
		public static float SecondToFrame(this float second, float fps)
		{
			return second * fps;
		}
		public static float DegreeToRate(this float degree)
		{
			return degree / 360;
		}
		public static float RateToDegree(this float rate)
		{
			return rate * 360;
		}
		public static float RadianToRate(this float radian)
		{
			return radian / (2 * Mathf.PI);
		}
		public static float RateToRadian(this float rate)
		{
			return rate * 2 * Mathf.PI;
		}
		public static float MillisecondToBeat(this float millisecond, float bpm)
		{
			return millisecond * bpm / 60000;
		}
		public static float BeatToMillisecond(this float beat, float bpm)
		{
			return beat * 60000 / bpm;
		}
	}
}
