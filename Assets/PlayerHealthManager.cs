using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
	public Image damageImage;
    public AudioClip deathClip;
	public AudioClip deathTune;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	private AudioSource playerAudio, playerAudio2;
    private charController charControl;

    private bool isDead;
    private bool damaged;


    void Awake()
    {
        playerAudio = GetComponents<AudioSource>()[0];
		playerAudio2 = GetComponents<AudioSource>()[1];
        charControl = GetComponent<charController>();
        currentHealth = startingHealth;
		GameObject.Find ("Dead").GetComponent<RawImage> ().enabled = false;

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
        charControl.StopMovement();
        playerAudio.clip = deathClip;
        playerAudio.Play();
		playerAudio2.clip = deathTune;
		playerAudio2.Play();
		GameObject.Find ("Dead").GetComponent<RawImage> ().enabled = true;


    }


    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
