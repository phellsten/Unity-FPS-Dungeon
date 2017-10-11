using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    // public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    // AudioSource playerAudio;
    bool isDead;
    bool damaged;


    void Awake()
    {
        // playerAudio = GetComponent<AudioSource>();
        currentHealth = startingHealth;
    }


    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void ApplyDamage(int damageAmount)
    {
        damaged = true;
        currentHealth -= damageAmount;

        healthSlider.value = currentHealth;

        //playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        // playerAudio.clip = deathClip;
        // playerAudio.Play();

    }


    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
