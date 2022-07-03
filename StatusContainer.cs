using System;
using System.Collections.Generic;

public class StatusContainer : Monobehavior
{
	private List<Status> statuses = new List<Status>();

    public IReadonlyCollection<Status> Statuses => statuses.AsReadOnly();

    public void AddStatus<T>(int durationTicks) where T : Status
    {
        var statusData = new BasicStatusData(durationTicks);
        AddStatus<T>(statusData);
    }

    public void AddStatus(Type type, int durationTicks)
    {
        var statusData = new BasicStatusData(durationTicks);
        AddStatus(type, statusData);
    }

    public void AddStatus<T>(IStatusData statusData)
    {
        var status = gameObject.AddComponent<T>();
        statuses.Add(status);
        status.OnDestroyed += RemoveStatus;
        status.InitStatus(statusData);
    }

    public void AddStatus(Type type, IStatusData statusData)
    {
        var status = gameObject.AddComponent(type) as Status;
        statuses.Add(status);
        status.OnDestroyed += RemoveStatus;
        status.InitStatus(statusData);
    }

    private void RemoveStatus(Status status)
    {
        statuses.Remove(status);
        RefreshStatuses();
    }

    private void RefreshStatuses()
    {
        foreach (var status in statuses)
        {
            if (status.NeedsRefresh)
                status.RefreshEffects();
        }
    }
}
