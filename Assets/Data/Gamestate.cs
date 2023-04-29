using UnityEngine;

public class Gamestate
{
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
    public void Initialize(float life, float energy, float switchSpeed, float moveSpeed, float damage, float attackSpeed)
    {
        foreach (var env in Environments)
        {
            var player = env.player;
            player.Life = player.MaxLife = life;
            player.Energy = player.MaxEnergy = energy;
            player.SwitchSpeed = switchSpeed;
            player.MoveSpeed = moveSpeed;
            player.Damage = damage;
            player.AttackSpeed = attackSpeed;
        }
    }
    public PlayerData Player
    {
        get => Environments[Index].player;
    }

    public int Index
    {
        get;
        set;
    }
    public string Epoch;
    public int Score;
    public float SwitchCooldown;
    public float AttackCooldown;
    public Environment[] Environments
    {
        get;
        private set;
    }

    public class Environment
    {
        public PlayerData player;
        public int killCount;
        public Environment()
        {
            player = new PlayerData();
        }
    }

    public class PlayerData
    {
        public Vector2 Position;
        public float Life;
        public float MaxLife;
        
        public float Energy;
        public float MaxEnergy;

        public float MoveSpeed;
        public float Damage;

        public float AttackSpeed;

        public float SwitchSpeed;
    }

}