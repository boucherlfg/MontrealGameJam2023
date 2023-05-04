using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float life = 5;
    public float moveSpeed = 4;
    public float damage = 1;
    public float attackSpeed = 1;
    public float energy = 3;
    public float energyRegen = 1;
    public float switchSpeed = 5;


    
    // Start is called before the first frame update
    void Start()
    {
        Gamestate.Instance.Initialize(life, energy, energyRegen, switchSpeed, moveSpeed, damage, attackSpeed);
    }
}
