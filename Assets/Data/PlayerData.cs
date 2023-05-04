using UnityEngine;

public class PlayerData
{
    public Observed<int> Potions { get; private set; }
    public Observed<Vector2> Position { get; private set; }
    public Observed<float> SwitchCooldown { get; private set; }
    public Observed<float> AttackCooldown { get; private set; }
    public Observed<bool> IsExhausted { get; private set; }
    public Observed<bool> IsSprinting { get; private set; }


    public Observed<float> Life { get; private set; }
    public Observed<float> MaxLife { get; private set; }

    public Observed<float> Energy { get; private set; }
    public Observed<float> MaxEnergy { get; private set; }


    public Observed<float> Damage { get; private set; }

    public Observed<float> EnergyRegen { get; private set; }
    public Observed<float> MoveSpeed { get; private set; }
    public Observed<float> AttackSpeed { get; private set; }
    public Observed<float> SwitchSpeed { get; private set; }
    public PlayerData()
    {
        IsExhausted = new Observed<bool>();
        IsSprinting = new Observed<bool>();
        Life = new Observed<float>();
        MaxLife = new Observed<float>();
        Energy = new Observed<float>();
        EnergyRegen = new Observed<float>();
        MaxEnergy = new Observed<float>();
        MoveSpeed = new Observed<float>();
        Damage = new Observed<float>();
        AttackSpeed = new Observed<float>();
        SwitchSpeed = new Observed<float>();
        SwitchCooldown = new Observed<float>();
        Potions = new Observed<int>();
        Position = new Observed<Vector2>();
        AttackCooldown = new Observed<float>();
    }
}