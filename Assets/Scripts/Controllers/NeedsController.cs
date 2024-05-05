using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsController : MonoBehaviour
{
    public int food, happiness, energy;
    public int foodTickRate, happinessTickRate, energyTickRate;
    public DateTime lastTimeFed, lastTimeHappy, lastTimeEnergized;
   
    private void Awake()
    {
        Initialize(100, 100, 100, 10, 10, 10);
    }

    public void Initialize(int food, int happiness, int energy, int foodTickRate, int happinessTickRate, int energyTickRate)
    {
        lastTimeFed = DateTime.Now;
        lastTimeHappy = DateTime.Now;
        lastTimeEnergized = DateTime.Now;
        this.food = food;
        this.happiness = happiness;
        this.energy = energy;
        this.foodTickRate = foodTickRate;
        this.happinessTickRate = happinessTickRate;
        this.energyTickRate = energyTickRate;
    }

    public void Initialize(int food, int happiness, int energy, int foodTickRate, int happinessTickRate, int energyTickRate, DateTime lastTimeFed, DateTime lastTimeHappy, DateTime lastTimeEnergized) 
    {
        this.lastTimeFed = lastTimeFed;
        this.lastTimeHappy = lastTimeHappy;
        this.lastTimeEnergized = lastTimeEnergized;
        this.food = food;
        this.happiness = happiness;
        this.energy = energy;
        this.foodTickRate = foodTickRate;
        this.happinessTickRate = happinessTickRate;
        this.energyTickRate = energyTickRate;
    }

    private void Update()
    {
        if(TimingManager.gameHourTimer < 0) 
        {
            ChangeFood(-foodTickRate);
            ChangeHappiness(-foodTickRate);
            ChangeEnergy(-foodTickRate);
        }
    }

    public void ChangeFood(int amount)
    {
        food += amount;
        if(amount  > 0) 
        {
            lastTimeFed = DateTime.Now;
        } 
        if (food < 0)
        {
            PetManager.instance.Die();
        }
        else if (food > 100) food = 100;
    }
    public void ChangeHappiness(int amount)
    {
        happiness += amount;
        if (amount > 0)
        {
            lastTimeHappy = DateTime.Now;
        }
        if (happiness < 0)
        {
            PetManager.instance.Die();
        }
        else if (happiness > 100) happiness = 100;
    }

    public void ChangeEnergy(int amount)
    {
        energy += amount;
        if (amount > 0)
        {
            lastTimeEnergized = DateTime.Now;
        }
        if (energy < 0)
        {
            PetManager.instance.Die();
        }
        else if (energy > 100) energy = 100;
    }
}


