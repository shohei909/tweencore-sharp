
using System;
using System.Collections.Generic;

namespace TweenCore
{
	public class Timeline<T>
	{
		public float TotalWeight { get; private set; }
		public int Count { get { return dataArray.Count; } }
		protected List<T> dataArray;
		protected List<float> weightArray;
		
		public Timeline()
		{
			this.dataArray = new List<T>();
			this.weightArray = new List<float>();
			this.TotalWeight = 0;
		}
	
		public Timeline<T> Add(T data, float weight = 1.0f)
		{
			if (weight <= 0) {
				throw new Exception("weight must be positive number");
			}
			if (dataArray.Count == 0) {
				TotalWeight = weight;
			} else {
				weightArray.Add(TotalWeight);
				TotalWeight += weight;
			}
			dataArray.Add(data);
			return this;
		}
	
		public TimelineSearchResult<T> Search(float rate, BoundaryMode boundaryMode = BoundaryMode.High)
		{
			if (dataArray.Count == 0) {
				throw new Exception("timeline is not initialized");
			}
	
			var searchResult = weightArray.FloatBinarySearch(rate * TotalWeight, boundaryMode);
			float baseWeight;
			if (searchResult == 0) {
				baseWeight = 0;
			} else {
				baseWeight = weightArray[searchResult - 1] / TotalWeight;
			}
	
			float nextWeight;
			if (searchResult == dataArray.Count - 1) {
				nextWeight = 1;
			} else {
				nextWeight = weightArray[searchResult] / TotalWeight;
			}
	
			return new TimelineSearchResult<T>(
				dataArray[searchResult],
				searchResult,
				baseWeight,
				nextWeight
			);
		}
	
		public T DataAt(int index)
		{
			if (dataArray.Count == 0) {
				throw new Exception("timeline is not initialized");
			}
			return dataArray[index];
		}
	
		public float RangeLeft(int index)
		{
			if (index == 0) {
				return 0.0f;
			}
	
			return weightArray[index - 1] / TotalWeight;
		}
	
		public float RangeRight(int index)
		{
			if (index == dataArray.Count) {
				return 1.0f;
			}
	
			return weightArray[index] / TotalWeight;
		}
	}
}
