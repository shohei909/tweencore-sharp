
namespace TweenCore
{
	public class TimelineSearchResult<T>
	{
		public T Data{ get; private set; }
		public int Index{ get; private set; }
		public float RangeLeft{ get; private set; }
		public float RangeRight{ get; private set; }

		public TimelineSearchResult(T data, int index, float rangeLeft, float rangeRight)
		{
			this.Data = data;
			this.Index = index;
			this.RangeLeft = rangeLeft;
			this.RangeRight = rangeRight;
		}
	

		public float InnerRate(float rate)
		{
			return rate.InverseLerp(RangeLeft, RangeRight);
		}
	}
}
