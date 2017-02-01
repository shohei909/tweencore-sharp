
using System;

namespace TweenCore
{
	public class FloatChangePart:FloatChange
	{
		public FloatChangePart(float previous, float current)
			: base(previous, current)
		{
		}
		
		public bool IsEntrance()
		{
			return (Previous <= 0 && 0 < Current) || (Current < 1.0 && 1.0 <= Previous);
		}
	
		public bool IsExit()
		{
			return (Current <= 0.0 && 0.0 < Previous) || (Previous < 1.0 && 1.0 <= Current);
		}
	}
}
