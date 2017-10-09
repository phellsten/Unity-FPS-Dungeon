using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public int startingHealth = 100;
    private int currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = startingHealth;
	}
	
	public int GetHealth()
    {
        return currentHealth;
    }

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
    }
}
