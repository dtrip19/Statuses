using System;

public interface IStatusData<T> where T : struct
{
	public int DurationTicks { get; }
	public float Value { get; }
	public Vector3 Direction { get; }
}

public struct BasicStatusData : IStatusData
{
	private int durationTicks;
	private float value;
	private Vector3 direction;

	public int DurationTicks => durationTicks;
	public float Value => value;
	public Vector3 Direction => direction;

	public BasicStatusData(int durationTicks, float value, Vector3 direction)
    {
		this.durationTicks = durationTicks;
		this.value = value;
		this.direction = direction;
    }

	public BasicStatusData(int durationTicks)
    {
		this.durationTicks = durationTicks;
		this.value = 0;
		this.direction = Vector3.zero;
    }
}
