using System;

public abstract class Status : MonoBehaviour
{
	protected Damageable damageable;
	protected Vector3 direction;
	protected int ticksSinceStart;
	protected int durationTicks;
	protected float value;

	protected StatusContainer container;

	public virtual bool NeedsRefresh => false;
	public virtual bool HasLifetime => false;

	public event Action<Status> OnDestroyed;

	protected void Awake()
	{
		damageable = GetComponent<Damageable>();
	}

	protected void FixedUpdate()
	{
		if (ticksSinceStart++ >= durationTicks)
			DestroyStatus();
	}

	public virtual void InitStatus(IStatusData statusData)
    {
		durationTicks = statusData.DurationTicks;
		value = statusData.Value;
		direction = statusData.direction;
		ApplyEffects();
    }

	protected abstract void ApplyEffects();

	protected abstract void RemoveEffects();

	public virtual void RefreshEffects()
	{
		RemoveEffects();
		ApplyEffects();
	}

	public void DestroyStatus()
	{
		RemoveEffects();
		OnDestroyed?.Invoke(this);
		Destroy(this);
	}
}
