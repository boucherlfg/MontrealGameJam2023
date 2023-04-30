using System.Collections.Generic;
using UnityEngine;

public class Gamestate
{
    public class TutorialData
    {
        private Dictionary<TutorialScript.TutorialType, bool> tutorials;
        public TutorialData()
        {
            tutorials = new Dictionary<TutorialScript.TutorialType, bool>();
        }
        public bool this[TutorialScript.TutorialType key]
        {
            get => tutorials.ContainsKey(key) && tutorials[key];
            set => tutorials[key] = value;
        }
    }
    private static Gamestate _instance;
    public static Gamestate Instance
    {
        get
        {
            if (_instance == null) _instance = new Gamestate();
            return _instance;
        }
    }

    private Gamestate()
    {
        Environments = new Environment[]{
            new Environment(), new Environment(), new Environment(), new Environment()
        };
    }
    public void Initialize(float life, float energy, float energyRegen, float switchSpeed, float moveSpeed, float damage, float attackSpeed)
    {
        Potions = 0;
        Paused = false;
        Position = Vector2.zero;
        EnvironmentIndex = 0;

        foreach (var env in Environments)
        {
            var player = env.player;
            player.Life = player.MaxLife = life;
            player.Energy = player.MaxEnergy = energy;
            player.EnergyRegen = energyRegen;
            player.SwitchSpeed = switchSpeed;
            player.MoveSpeed = moveSpeed;
            player.Damage = damage;
            player.AttackSpeed = attackSpeed;
            player.Score = 0;
        }
    }
    public PlayerData Player
    {
        get => Environments[EnvironmentIndex].player;
    }

    public int totalKillCount;
    public int EnvironmentIndex;
    public string Epoch;
    public float SwitchCooldown;
    public float AttackCooldown;
    public int Potions;
    public Vector2 Position;
    public bool Paused;
    public Environment[] Environments
    {
        get;
        private set;
    }
    public TutorialData Tutorial { get => Environments[EnvironmentIndex].tutorial; }
    public class Environment
    {
        public TutorialData tutorial;
        public PlayerData player;
        public Environment()
        {
            player = new PlayerData();
            tutorial = new TutorialData();
        }
    }

    public class PlayerData
    {
        public float Life;
        public float MaxLife;
        
        public float Energy;
        public float EnergyRegen;
        public float MaxEnergy;

        public float MoveSpeed;
        public float Damage;

        public float AttackSpeed;

        public float SwitchSpeed;
        public int Score;
    }

}