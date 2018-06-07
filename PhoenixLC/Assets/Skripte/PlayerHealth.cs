using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public float maxHealth = 100f;
	public float maxShield = 100f;
	public float shieldRegenTime = 2f;
	public float shieldRegenAmount = 10f;
	public GameObject deathExplosion;
	public RectTransform healthBar;
	public RectTransform shieldBar;

	GameObject shield;
	[SerializeField]
	GameObject gameOverScreen;

	float currentHealth;
	float currentShield;
	bool isDead = false;

	float maxWidthHealth;
	float maxWidthShield;

	float shieldBeforeHit;

    int lives = 1;
    Vector3 respawnPosition;
    Quaternion respawnRotation;
    public TextMeshProUGUI brojZivota;

    void Awake(){
		shield = GameObject.Find("Shield");
		//izbacuje neki error ali radi
		//shield = transform.Find("Shield").gameObject;

		maxWidthHealth = healthBar.rect.width;
		maxWidthShield = shieldBar.rect.width;
	}

	void Start () {
		currentHealth = maxHealth;
		currentShield = maxShield;
        respawnPosition = transform.position;
        respawnRotation = transform.rotation;

		shield.SetActive(false);
		InvokeRepeating ("RegenerateShield", shieldRegenTime, shieldRegenTime);
	}

	void RegenerateShield(){

		if (currentShield < maxShield) {
			currentShield += shieldRegenAmount;
			UpdateShieldBar (currentShield / maxShield);
		}
		if (currentShield > maxShield) {
			currentShield = maxShield;
			UpdateShieldBar (currentShield / maxShield);
		}

	}

	public void TakeDamage(float dmg){

		CancelInvoke ("RegenerateShield");
		InvokeRepeating ("RegenerateShield", shieldRegenTime, shieldRegenTime);

		shieldBeforeHit = currentShield;

		if (currentShield > 0) {
			currentShield -= dmg;

			shield.SetActive(true);
			Invoke("DeactivateShield", 1f);


			if (currentShield < 0) {
				currentShield = 0;
				dmg -= shieldBeforeHit;
				currentHealth -= dmg;
				UpdateHealthBar (currentHealth / maxHealth);

			}

			UpdateShieldBar (currentShield / maxShield);

		} else {
			currentHealth -= dmg;
			if (currentHealth < 0)
				currentHealth = 0;

			UpdateHealthBar (currentHealth / maxHealth);
		}

		if (currentHealth <= 0 && !isDead) {
			Death ();
		}
	}

	void Death(){

		isDead = true;
		CancelInvoke ();
		Instantiate (deathExplosion, transform.position, transform.rotation);
		this.gameObject.SetActive (false);

        if(lives <= 0)
        {
            Invoke("GameOver", 2f);
        }
        else
        {
            Invoke("Respawn", 5f);
        }
        
		

		//Debug.Log("WE ARE AT 0 HEALTH");

	}

	void DeactivateShield()
	{
		shield.SetActive(false);
	}

	public float getShield(){
		return currentShield;
	}

    public float getHealth()
    {
        return currentHealth;
    }

	void GameOver (){
		gameOverScreen.SetActive (true);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void UpdateHealthBar (float percent){
		healthBar.sizeDelta = new Vector2 (maxWidthHealth * percent, 10f);
	}

	public void UpdateShieldBar(float percent){
		shieldBar.sizeDelta = new Vector2 (maxWidthShield * percent, 10f);
	}

    public void Repair()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(1);
        lives++;
        brojZivota.text = "X " + lives;
    }

    public void RecoverShield()
    {
        currentShield = maxShield;

        UpdateShieldBar(currentShield / maxShield);
    }

    public void Respawn()
    {
        lives--;
        brojZivota.text = "X " + lives;
        transform.position = respawnPosition;
        transform.rotation = respawnRotation;
        currentHealth = maxHealth;
        UpdateHealthBar(1);
        currentShield = maxShield;
        UpdateShieldBar(1);
        InvokeRepeating("RegenerateShield", shieldRegenTime, shieldRegenTime);
        this.gameObject.SetActive(true);
        GetComponent<Shooting>().laserLevel = 1;
        GameObject.Find("ScoreSistem").GetComponent<ScoreSistem>().UpdateScore(-100);
        isDead = false;
    }
}
