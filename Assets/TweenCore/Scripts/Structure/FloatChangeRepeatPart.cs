
using System;

namespace TweenCore
{
	public class FloatChangeRepeatPart:FloatChangePart
	{
		
		public bool IsMinorChange { get; private set; }
		public int RepeatIndex { get; private set; }
		public int RepeatLength { get; private set; }
	
		public FloatChangeRepeatPart(float previousValue, float currentValue, int repeatIndex, int repeatLength, bool isMinorChange)
			: base(previousValue, currentValue)
		{
			this.RepeatIndex = repeatIndex;
			this.RepeatLength = repeatLength;
			this.IsMinorChange = isMinorChange;
		}
	}
}
