using System.Collections.Generic;

public class Gamestate : Singleton<Gamestate>
{
    public TimePeriod Environment {
        get
        {
            if (Index < 0) return FinalBossLevel;
            return Environments[Index.Value];
        }
    }

    public Observed<int> Index { get; private set; }
    public void TriggerFinalBoss()
    {
        Index.Value = -1;
    }
    public void MoveIndex(int value)
    {
        var index = Index;
        if (index == -1) return;
        index = index + value;
        if (index < 0) index = Environments.Count - 1;
        if (index >= Environments.Count) index = 0;
        Index.Value = index;
    }

    public Observed<bool> InFinalLevel { get; private set; }
    public Observed<bool> InGame { get; private set; }
    public Observed<bool> Paused { get; private set; }

    public PlayerData Player { get; private set; }
    public List<TimePeriod> Environments {get; private set; }
    public TimePeriod FinalBossLevel { get; set; }

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
        InFinalLevel = new Observed<bool>();
    }
    public void Initialize(float life, float energy, float energyRegen, float switchSpeed, float moveSpeed, float damage, float attackSpeed)
    {

        Paused = new Observed<bool>();
        InFinalLevel = new Observed<bool>();

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