using UnityEngine;

namespace TweenCore
{
	public static class Easing
	{
		const float PI = 3.1415926535897932384626433832795f;
		const float PI_H = PI / 2;
		const float LN_2 = 0.6931471805599453f;
		const float LN_2_10 = 6.931471805599453f;
		/*
	     * LINEAR
	     */
		///<summary>1-order</summary>
		public static float Linear(this float t)
		{
			return t;
		}
	
		/*
	     * SINE
	     */
		public static float SineIn(this float t)
		{
			if (t == 0f) {
				return 0f;
			} else if (t == 1f) {
				return 1f;
			} else {
				return 1f - Mathf.Cos(t * PI_H);
			}
		}
		public static float SineOut(this float t)
		{
			if (t == 0) {
				return 0;
			} else if (t == 1) {
				return 1;
			} else {
				return Mathf.Sin(t * PI_H);
			}
		}
		public static float SineInOut(this float t)
		{
			if (t == 0) {
				return 0;
			} else if (t == 1) {
				return 1;
			} else {
				return -0.5f * (Mathf.Cos(PI * t) - 1);
			}
		}
		public static float SineOutIn(this float t)
		{
			if (t == 0f) {
				return 0f;
			} else if (t == 1) {
				return 1f;
			} else if (t < 0.5f) {
				return 0.5f * Mathf.Sin((t * 2) * PI_H);
			} else {
				return -0.5f * Mathf.Cos((t * 2 - 1) * PI_H) + 1;
			}
		}
	
	
		/*
	     * QUAD EASING
	     */
		///<summary>2-order</summary>
		public static float QuadIn(this float t)
		{
			return t * t;
		}
		///<summary>2-order</summary>
		public static float QuadOut(this float t)
		{
			return -t * (t - 2);
		}
		///<summary>2-order</summary>
		public static float QuadInOut(this float t)
		{
			return (t < 0.5f) ? 2 * t * t : -2 * ((t -= 1) * t) + 1;
		}
		///<summary>2-order</summary>
		public static float QuadOutIn(this float t)
		{
			return (t < 0.5f) ? -0.5f * (t = (t * 2)) * (t - 2) : 0.5f * (t = (t * 2 - 1)) * t + 0.5f;
		}
	
	
		/*
	     * CUBIC EASING
	     */
		///<summary>3-order</summary>
		public static float CubicIn(this float t)
		{
			return t * t * t;
		}
		///<summary>3-order</summary>
		public static float CubicOut(this float t)
		{
			return (t = t - 1) * t * t + 1;
		}
		///<summary>3-order</summary>
		public static float CubicInOut(this float t)
		{
			return ((t *= 2) < 1) ?
	            0.5f * t * t * t :
	            0.5f * ((t -= 2) * t * t + 2);
		}
		///<summary>3-order</summary>
		public static float CubicOutIn(this float t)
		{
			return 0.5f * ((t = t * 2 - 1) * t * t + 1);
		}
	
	
		/*
	     * QUART EASING
	     */
		///<summary>4-order</summary>
		public static float QuartIn(this float t)
		{
			return (t *= t) * t;
		}
		///<summary>4-order</summary>
		public static float QuartOut(this float t)
		{
			return 1 - (t = (t = t - 1) * t) * t;
		}
		///<summary>4-order</summary>
		public static float QuartInOut(this float t)
		{
			return ((t *= 2) < 1) ? 0.5f * (t *= t) * t : -0.5f * ((t = (t -= 2) * t) * t - 2);
		}
		///<summary>4-order</summary>
		public static float QuartOutIn(this float t)
		{
			return (t < 0.5f) ? -0.5f * (t = (t = t * 2 - 1) * t) * t + 0.5f : 0.5f * (t = (t = t * 2 - 1) * t) * t + 0.5f;
		}
	
	
		/*
	     * QUINT EASING
	     */
		///<summary>5-order</summary>
		public static float QuintIn(this float t)
		{
			return t * (t *= t) * t;
		}
		///<summary>5-order</summary>
		public static float QuintOut(this float t)
		{
			return (t = t - 1) * (t *= t) * t + 1;
		}
		///<summary>5-order</summary>
		public static float QuintInOut(this float t)
		{
			return ((t *= 2) < 1) ? 0.5f * t * (t *= t) * t : 0.5f * (t -= 2) * (t *= t) * t + 1;
		}
		///<summary>5-order</summary>
		public static float QuintOutIn(this float t)
		{
			return 0.5f * ((t = t * 2 - 1) * (t *= t) * t + 1);
		}
	
	
		/*
	     * EXPO EASING
	     */
		public static float ExpoIn(this float t)
		{
			return t == 0 ? 0 : Mathf.Exp(LN_2_10 * (t - 1));
		}
		public static float ExpoOut(this float t)
		{
			return t == 1 ? 1 : (1 - Mathf.Exp(-LN_2_10 * t));
		}
		public static float ExpoInOut(this float t)
		{
			if (t == 0) {
				return 0;
			} else if (t == 1) {
				return 1;
			} else if ((t *= 2) < 1) {
				return 0.5f * Mathf.Exp(LN_2_10 * (t - 1));
			} else {
				return 0.5f * (2 - Mathf.Exp(-LN_2_10 * (t - 1)));
			}
		}
		public static float ExpoOutIn(this float t)
		{
			if (t < 0.5f) {
				return 0.5f * (1 - Mathf.Exp(-20 * LN_2 * t));
			} else if (t == 0.5f) {
				return 0.5f;
			} else {
				return 0.5f * (Mathf.Exp(20 * LN_2 * (t - 1)) + 1);
			}
		}
	
	    
		/*
	     * CIRC EASING
	     */
		public static float CircIn(this float t)
		{
			return (t < -1 || 1 < t) ? 0 : 1 - Mathf.Sqrt(1 - t * t);
		}
		public static float CircOut(this float t)
		{
			return (t < 0 || 2 < t) ? 0 : Mathf.Sqrt(t * (2 - t));
		}
		public static float CircInOut(this float t)
		{
			if (t < -0.5f || 1.5f < t) {
				return 0.5f;
			} else if ((t *= 2) < 1) {
				return -0.5f * (Mathf.Sqrt(1 - t * t) - 1);
			} else {
				return 0.5f * (Mathf.Sqrt(1 - (t -= 2) * t) + 1);
			}
		}
		public static float CircOutIn(this float t)
		{
			if (t < 0) {
				return 0;
			} else if (1 < t) {
				return 1;
			} else if (t < 0.5f) {
				return 0.5f * Mathf.Sqrt(1 - (t = t * 2 - 1) * t);
			} else {
				return -0.5f * ((Mathf.Sqrt(1 - (t = t * 2 - 1) * t) - 1) - 1);
			}
		}
	
	
		/*
	     * BOUNCE EASING
	     */
		public static float BounceIn(this float t)
		{
			if ((t = 1 - t) < (1 / 2.75f)) {
				return 1 - ((7.5625f * t * t));
			} else if (t < (2 / 2.75f)) {
				return 1 - ((7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f));
			} else if (t < (2.5f / 2.75f)) {
				return 1 - ((7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f));
			} else {
				return 1 - ((7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f));
			}
		}
		public static float BounceOut(this float t)
		{
			if (t < (1 / 2.75f)) {
				return (7.5625f * t * t);
			} else if (t < (2 / 2.75f)) {
				return (7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f);
			} else if (t < (2.5f / 2.75f)) {
				return (7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f);
			} else {
				return (7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f);
			}
		}
		public static float BounceInOut(this float t)
		{
			if (t < 0.5f) {
				if ((t = (1 - t * 2)) < (1 / 2.75f)) {
					return (1 - ((7.5625f * t * t))) * 0.5f;
				} else if (t < (2 / 2.75f)) {
					return (1 - ((7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f))) * 0.5f;
				} else if (t < (2.5f / 2.75f)) {
					return (1 - ((7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f))) * 0.5f;
				} else {
					return (1 - ((7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f))) * 0.5f;
				}
			} else {
				if ((t = (t * 2 - 1)) < (1 / 2.75f)) {
					return ((7.5625f * t * t)) * 0.5f + 0.5f;
				} else if (t < (2 / 2.75f)) {
					return ((7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f)) * 0.5f + 0.5f;
				} else if (t < (2.5f / 2.75f)) {
					return ((7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f)) * 0.5f + 0.5f;
				} else {
					return ((7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f)) * 0.5f + 0.5f;
				}
			}
		}
		public static float BounceOutIn(this float t)
		{
			if (t < 0.5f) {
				if ((t = (t * 2)) < (1 / 2.75f)) {
					return 0.5f * (7.5625f * t * t);
				} else if (t < (2 / 2.75f)) {
					return 0.5f * (7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f);
				} else if (t < (2.5f / 2.75f)) {
					return 0.5f * (7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f);
				} else {
					return 0.5f * (7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f);
				}
			} else {
				if ((t = (1 - (t * 2 - 1))) < (1 / 2.75f)) {
					return 0.5f - (0.5f * (7.5625f * t * t)) + 0.5f;
				} else if (t < (2 / 2.75f)) {
					return 0.5f - (0.5f * (7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f)) + 0.5f;
				} else if (t < (2.5f / 2.75f)) {
					return 0.5f - (0.5f * (7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f)) + 0.5f;
				} else {
					return 0.5f - (0.5f * (7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f)) + 0.5f;
				}
			}
		}
	
		private const float overshoot = 1.70158f;
	
		/*
	     * BACK EASING
	     */
		public static float BackIn(this float t)
		{
			if (t == 0) {
				return 0;
			} else if (t == 1) {
				return 1;
			} else {
				return t * t * ((overshoot + 1) * t - overshoot);
			}
		}
		public static float BackOut(this float t)
		{
			if (t == 0) {
				return 0;
			} else if (t == 1) {
				return 1;
			} else {
				return ((t = t - 1) * t * ((overshoot + 1) * t + overshoot) + 1);
			}
		}
		public static float BackInOut(this float t)
		{
			if (t == 0) {
				return 0;
			} else if (t == 1) {
				return 1;
			} else if ((t *= 2) < 1) {
				return 0.5f * (t * t * (((overshoot * 0.984375f) + 1) * t - overshoot * 1.525f));
			} else {
				return 0.5f * ((t -= 2) * t * (((overshoot * 1.525f) + 1) * t + overshoot * 1.525f) + 2);
			}
		}
		public static float BackOutIn(this float t)
		{
			if (t == 0) {
				return 0;
			} else if (t == 1) {
				return 1;
			} else if (t < 0.5f) {
				return 0.5f * ((t = t * 2 - 1) * t * ((overshoot + 1) * t + overshoot) + 1);
			} else {
				return 0.5f * (t = t * 2 - 1) * t * ((overshoot + 1) * t - overshoot) + 0.5f;
			}
		}
	
	
		/*
	     * ELASTIC EASING
	     */
		private const float amplitude = 1f;
		private const float period = 0.0003f;
	    
		public static float ElasticIn(this float t)
		{
			if (t == 0) {
				return 0;
			} else if (t == 1) {
				return 1;
			} else {
				var s = period / 4;
				return -(amplitude * Mathf.Exp(LN_2_10 * (t -= 1)) * Mathf.Sin((t * 0.001f - s) * (2 * PI) / period));
			}
		}
		public static float ElasticOut(this float t)
		{
			if (t == 0) {
				return 0;
			} else if (t == 1) {
				return 1;
			} else {
				var s = period / 4;
				return Mathf.Exp(-LN_2_10 * t) * Mathf.Sin((t * 0.001f - s) * (2 * PI) / period) + 1;
			}
		}
		public static float ElasticInOut(this float t)
		{
			if (t == 0) {
				return 0;
			} else if (t == 1) {
				return 1;
			} else {
				var s = period / 4;
	
				if ((t *= 2) < 1) {
					return -0.5f * (amplitude * Mathf.Exp(LN_2_10 * (t -= 1)) * Mathf.Sin((t * 0.001f - s) * (2 * PI) / period));
				} else {
					return amplitude * Mathf.Exp(-LN_2_10 * (t -= 1)) * Mathf.Sin((t * 0.001f - s) * (2 * PI) / period) * 0.5f + 1;
				}
			}
		}
		public static float ElasticOutIn(this float t)
		{
			if (t < 0.5f) {
				if ((t *= 2) == 0) {
					return 0;
				} else {
					var s = period / 4;
					return (amplitude / 2) * Mathf.Exp(-LN_2_10 * t) * Mathf.Sin((t * 0.001f - s) * (2 * PI) / period) + 0.5f;
				}
			} else {
				if (t == 0.5f) {
					return 0.5f;
				} else if (t == 1) {
					return 1;
				} else {
					t = t * 2 - 1;
					var s = period / 4;
					return -((amplitude / 2) * Mathf.Exp(LN_2_10 * (t -= 1)) * Mathf.Sin((t * 0.001f - s) * (2 * PI) / period)) + 0.5f;
				}
			}
		}
	
		/*
	     * WARP EASING
	     */
		public static float WarpOut(this float t)
		{
			return t <= 0 ? 0 : 1;
		}
		public static float WarpIn(this float t)
		{
			return t < 1 ? 0 : 1;
		}
		public static float WarpInOut(this float t)
		{
			return t < 0.5f ? 0 : 1;
		}
		public static float WarpOutIn(this float t)
		{
			if (t <= 0) {
				return 0; 
			} else if (t < 1) {
				return 0.5f;
			} else {
				return 1;
			}
		}
	}
}
