using System;
using UnityEngine;

namespace TweenCore
{
	public class FloatChange
	{
		// ===============
		// Properties
		// ===============
	
		public float Previous {
			get;
			private set;
		}
		public float Current {
			get;
			private set;
		}
	
		public FloatChange(float PreviousValue, float CurrentValue)
		{
			this.Previous = PreviousValue;
			this.Current = CurrentValue;
		}
	
		// ===============
		// Methods
		// ===============
	
		public Direction Direction()
		{
			if (Previous < Current) {
				return TweenCore.Direction.Forward;
			} else if (Current < Previous) {
				return TweenCore.Direction.Backward;
			} else {
				return TweenCore.Direction.Stopped;
			}
		}
	
		public FloatChange MapFloatChange(Func<float, float>func)
		{
			return new FloatChange(func(Previous), func(Current));
		}
	
		public bool IsCrossOver(float threshold, BoundaryMode boundaryMode = BoundaryMode.High)
		{
			switch (boundaryMode) {
				case BoundaryMode.Low:
					return (Previous < threshold && threshold <= Current) || (Current < threshold && threshold <= Previous);
	        
				case BoundaryMode.High:
					return (Previous <= threshold && threshold < Current) || (Current <= threshold && threshold < Previous);
			}
	    	
			throw new Exception("BoundaryMode must be High or Low.");
		}
	
		public void HandlePart(float from, float to, Action<FloatChangePart> updatePart)
		{
			if (
				(
				    (from < Previous && Current < to) ||
				    (from < Current && Previous < to) ||
				    (to < Previous && Current < from) ||
				    (to < Current && Previous < from)
				) &&
				(Previous != Current)) {
				updatePart(
					new FloatChangePart(
						Previous.InverseLerp(from, to).Clamp(),
						Current.InverseLerp(from, to).Clamp()
					)
				);
			}
		}
	
		public void HandleRepeatPart(
			float firstPartFrom,
			float firstPartTo,
			int repeatLimit,
			Action<FloatChangeRepeatPart> updateRepeatPart
		)
		{
			if (firstPartFrom != firstPartTo) {
				var p = Previous.InverseLerp(firstPartFrom, firstPartTo);
				var c = Current.InverseLerp(firstPartFrom, firstPartTo);
	
				if ((0 < c && p < repeatLimit) || (0 < p && c < repeatLimit)) {
					p = p.Clamp(0, repeatLimit);
					c = c.Clamp(0, repeatLimit);
	
					var pCount = Mathf.FloorToInt(p);
					var cCount = Mathf.FloorToInt(c);
					var hasNext = true;
	
					if (p < c) {
						do {
							if (cCount == pCount) {
								hasNext = false;
								if (p != c) {
									updateRepeatPart(new FloatChangeRepeatPart(p - pCount, c - pCount, pCount, repeatLimit, hasNext));
								}
							} else {
								hasNext = (pCount + 1 != c);
								if (p - pCount != 1) {
									updateRepeatPart(new FloatChangeRepeatPart(p - pCount, 1, pCount, repeatLimit, hasNext));
								}
							}
							p = (pCount += 1);
						} while (hasNext);
					} else {
						do {
							if (pCount == cCount) {
								hasNext = false;
								if (p != c) {
									updateRepeatPart(new FloatChangeRepeatPart(p - pCount, c - pCount, pCount, repeatLimit, hasNext));
								}
							} else {
								hasNext = (pCount - 1 != c);
								if (p - pCount != 0) {
									updateRepeatPart(new FloatChangeRepeatPart(p - pCount, 0, pCount, repeatLimit, hasNext));
								}
							}
							p = pCount;
							pCount -= 1;
						} while (hasNext);
					}
				}
			}
		}

		public void HandleTimelinePart(
			float timelineFrom,
			float timelineTo,
			Timeline<Action<FloatChangeTimelinePart>> updatePartTimeline)
		{
			if (timelineFrom != timelineTo) {
				var p = Previous.InverseLerp(timelineFrom, timelineTo);
				var c = Current.InverseLerp(timelineFrom, timelineTo);
				if ((0 < p && c < 1) || (0 < c && p < 1)) {
					var length = updatePartTimeline.Count;
					p = p.Clamp(0, 1);
					c = c.Clamp(0, 1);

					var pResult = updatePartTimeline.Search(p);
					var cResult = updatePartTimeline.Search(c);
					var pCount = pResult.Index;
					var cCount = cResult.Index;
					var pRate = pResult.InnerRate(p);
					var cRate = cResult.InnerRate(c);
					var hasNext = false;

					if (p < c) {
						do {
							if (pCount == cCount) {
								hasNext = false;
								var part = new FloatChangeTimelinePart(
									           pRate,
									           cRate,
									           pCount,
									           updatePartTimeline.RangeLeft(pCount),
									           updatePartTimeline.RangeRight(pCount),
									           hasNext
								           );
								updatePartTimeline.DataAt(pCount)(part);
							} else {
								hasNext = (pCount + 1 != cCount) || (cRate != 0);
								var part = new FloatChangeTimelinePart(
									           pRate,
									           1,
									           pCount,
									           updatePartTimeline.RangeLeft(pCount),
									           updatePartTimeline.RangeRight(pCount),
									           hasNext
								           );
								updatePartTimeline.DataAt(pCount)(part);
							}
							pRate = 0;
							pCount += 1;
						} while (hasNext);
					} else {
						do {
							if (pCount == cCount) {
                            
								hasNext = false;
								var part = new FloatChangeTimelinePart(
									           pRate,
									           cRate,
									           pCount,
									           updatePartTimeline.RangeLeft(pCount),
									           updatePartTimeline.RangeRight(pCount),
									           hasNext
								           );
								updatePartTimeline.DataAt(pCount)(part);
							} else {
								hasNext = (pCount - 1 != cCount) || (cRate != 1);
								var part = new FloatChangeTimelinePart(
									           pRate,
									           0,
									           pCount,
									           updatePartTimeline.RangeLeft(pCount),
									           updatePartTimeline.RangeRight(pCount),
									           hasNext
								           );
								updatePartTimeline.DataAt(pCount)(part);
							}
							pRate = 1;
							pCount -= 1;
						} while (hasNext);
					}
				}
			}
		}
	}
}
