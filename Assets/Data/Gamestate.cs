using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Gamestate : Singleton<Gamestate>
{
    public TimePeriod Environment => Environments[Index.Value];
    public Observed<int> Index { get; private set; }
    public void MoveIndex(int value)
    {
        Index = Index + value;
        if (Index.Value < 0) Index = Environments.Count - 1;
        if (Index.Value >= Environments.Count) Index = 0;
    }
    public Observed<bool> InGame { get; private set; }
    public Observed<bool> Paused { get; private set; }

    public PlayerData Player { get; private set; }
    public List<TimePeriod> Environments {get; private set; }
    public Counter<string, int> Kills { get; private set; }
    public Counter<string, int> Props { get; private set; }

    public Gamestate()
    {
        Index = 0;
        Environments = new List<TimePeriod>();

        Player = new PlayerData();
        Kills = new Counter<string, int>();
        Props = new Counter<string, int>();

        Paused = new Observed<bool>();
        InGame = new Observed<bool>();
    }
    public void Initialize(float life, float energy, float energyRegen, float switchSpeed, float moveSpeed, float damage, float attackSpeed)
    {
        Paused.Value = false;
        
        Index = 0;

        Kills.Reset();
        Props.Reset();

        Player = new PlayerData();

        Player.Life.Value = Player.MaxLife.Value = life;
        Player.Energy.Value = Player.MaxEnergy.Value = energy;

        Player.Damage.Value = damage;

        Player.EnergyRegen.Value = energyRegen;
        Player.SwitchSpeed.Value = switchSpeed;
        Player.MoveSpeed.Value = moveSpeed;
        Player.AttackSpeed.Value = attackSpeed;
    }
    
    
}