using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public int heartValue = 5;
    public Slider healthSlider;
	public Image damageImage;
    public Image healthImage;
    public AudioClip deathClip;
	public AudioClip deathTune;
	public AudioClip winTune;

	public GameObject cursor;

	public bool playedtune = false;

    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	public bool gameWon = false;

	private AudioSource playerAudio, playerAudio2, playerAudio3;
    private charController charControl;

    public bool isDead;
    private bool damaged;


    void Awake()
    {
        playerAudio = GetComponents<AudioSource>()[0];
		playerAudio2 = GetComponents<AudioSource>()[1];
		playerAudio3 = GetComponents<AudioSource>()[2];

        charControl = GetComponent<charController>();
        currentHealth = startingHealth;
		GameObject.Find ("Dead").GetComponent<RawImage> ().enabled = false;
        updateHealthBar();

    }


    void Update()
    {
		if (gameWon) {
			
			playerAudio.Stop ();
			playerAudio2.Stop ();
			return;
		}

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

        updateHealthBar();

        //playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    private void updateHealthBar()
    {
        int h = (int)(currentHealth / heartValue);
        float rem = (currentHealth % heartValue) / (float)heartValue;

        if (rem != 0)
        {
            h++;
        }
        
        GameObject healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        List<GameObject> hearts = new List<GameObject>();

        foreach (Transform heart in healthBar.transform)
        {
            hearts.Add(heart.gameObject);
        }

        int heartsNum = hearts.Count;

        Debug.Log("h=" + currentHealth + ", n=" + h + ", l="+heartsNum+", r="+rem);

        if (heartsNum != h)
        {
            while (heartsNum >= h)
            {
                hearts[heartsNum - 1].transform.SetParent(null);
                Destroy(hearts[heartsNum-1]);
                heartsNum--;
            }

            while (heartsNum < h)
            {
                var newHeart = GameObject.Instantiate(healthImage);
                newHeart.transform.SetParent(healthBar.transform, false);
                heartsNum++;
            }
        }

        if (rem != 0)
        {
            Debug.Log("heart cull: " + heartsNum);
            var heart = healthBar.transform.GetChild(heartsNum-1);
            heart.GetComponent<Image>().fillAmount = 1.0f - rem;
        }
        else
        {
            var heart = healthBar.transform.GetChild(heartsNum - 1);
            heart.GetComponent<Image>().fillAmount = 1.0f;
        }
    }

    public void Death()
    {
		cursor.GetComponentInChildren<Canvas> ().enabled = false;
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

	public void win() {
		playerAudio3.clip = winTune;
		playerAudio3.Play ();
		cursor.GetComponentInChildren<Canvas> ().enabled = false;

	}
}
