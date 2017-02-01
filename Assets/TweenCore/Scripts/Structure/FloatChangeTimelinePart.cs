
using System;

namespace TweenCore
{
	public class FloatChangeTimelinePart: FloatChangePart
	{
		public bool IsMinorChange { get; private set; }
		public int Index { get; private set; }
		public float RangeLeft { get; private set; }
		public float RangeRight { get; private set; }
	
		public FloatChangeTimelinePart(float previousValue, float currentValue, int index, float rangeLeft, float rangeRight, bool isMinorChange)
			: base(previousValue, currentValue)
		{
			this.Index = index;
			this.RangeLeft = rangeLeft;
			this.RangeRight = rangeRight;
			this.IsMinorChange = isMinorChange;
		}
	}
}
