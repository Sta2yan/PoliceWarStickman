using UnityEngine.Events;

public interface IEnergyCollectable
{
    public event UnityAction<IEnergyCollectable> EnergyCollected;
    public int EnergyCostBonus { get; }
}