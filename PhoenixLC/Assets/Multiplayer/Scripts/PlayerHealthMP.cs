using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthMP : NetworkBehaviour {

    public const float maxHealth = 100f;
    public const float maxShield = 100f;
    public float shieldRegenTime = 2f;
    public float shieldRegenAmount = 10f;
    public GameObject deathExplosion;
    public RectTransform healthBar;
    public RectTransform shieldBar;

    [SerializeField]
    GameObject shield;
    [SerializeField]
    GameObject gameOverScreen;

    [SyncVar(hook = "OnChangeHealth")]
    float currentHealth = maxHealth;
    [SyncVar(hook = "OnChangeShield")]
    float currentShield = maxShield;
    bool isDead = false;

    float maxWidthHealth;
    float maxWidthShield;

    float shieldBeforeHit;

    [SyncVar(hook = "OnChangeLives")]
    int lives = 1;
    Vector3 respawnPosition;
    Quaternion respawnRotation;
    public TextMeshProUGUI brojZivota;

    void Start()
    {
        maxWidthHealth = healthBar.rect.width;
        maxWidthShield = shieldBar.rect.width;
        lives = 1;
        if (!isServer)
        {
            respawnPosition = transform.position;
            respawnRotation = transform.rotation;
            return;
        }

        RpcFlipShield();
        InvokeRepeating("RegenerateShield", shieldRegenTime, shieldRegenTime);
    }

    void RegenerateShield()
    {

        if (currentShield < maxShield)
        {
            currentShield += shieldRegenAmount;
        }
        if (currentShield > maxShield)
        {
            currentShield = maxShield;
        }

    }

    public void TakeDamage(float dmg)
    {
        if (!isServer)
            return;

        CancelInvoke("RegenerateShield");
        InvokeRepeating("RegenerateShield", shieldRegenTime, shieldRegenTime);

        shieldBeforeHit = currentShield;

        if (currentShield > 0)
        {
            currentShield -= dmg;

            RpcFlipShield();


            if (currentShield < 0)
            {
                currentShield = 0;
                dmg -= shieldBeforeHit;
                currentHealth -= dmg;
            }

        }
        else
        {
            currentHealth -= dmg;
            if (currentHealth < 0)
                currentHealth = 0;
        }

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {

        isDead = true;
        CancelInvoke();
        NetworkServer.Spawn(Instantiate(deathExplosion, transform.position, transform.rotation));
        RpcHideOnDeath();

        if (lives <= 0)
        {
            //needs rework
            Invoke("RpcGameOver", 2f);
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

    public float getShield()
    {
        return currentShield;
    }

    public float getHealth()
    {
        return currentHealth;
    }

    [ClientRpc]
    void RpcGameOver()
    {
        Instantiate(gameOverScreen, null);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void UpdateHealthBar(float percent)
    {
        healthBar.sizeDelta = new Vector2(maxWidthHealth * percent, 10f);
    }

    public void UpdateShieldBar(float percent)
    {
        shieldBar.sizeDelta = new Vector2(maxWidthShield * percent, 10f);
    }

    public void Repair()
    {
        currentHealth = maxHealth;
        lives++;
    }

    public void RecoverShield()
    {
        currentShield = maxShield;
    }

    public void Respawn()
    {
        lives--;

        currentHealth = maxHealth;
        currentShield = maxShield;
        InvokeRepeating("RegenerateShield", shieldRegenTime, shieldRegenTime);
        RpcResetPosition();
        RpcShowOnRespawn();
        GetComponent<ShootingMP>().LaserLevelReset();
        isDead = false;
    }

    [ClientRpc]
    void RpcFlipShield()
    {
        shield.SetActive(true);
        Invoke("DeactivateShield", 1f);
    }

    void OnChangeHealth(float health)
    {
        UpdateHealthBar(health / maxHealth);
    }

    void OnChangeShield(float shield)
    {
        UpdateShieldBar(shield / maxShield);
    }

    [ClientRpc]
    void RpcHideOnDeath()
    {
        this.gameObject.SetActive(false);
    }

    void OnChangeLives(int amount)
    {
        brojZivota.text = "X " + amount;
    }

    [ClientRpc]
    void RpcResetPosition()
    {
        if (isLocalPlayer)
        {
            transform.position = respawnPosition;
            transform.rotation = respawnRotation;
        }
    }

    [ClientRpc]
    void RpcShowOnRespawn()
    {
        this.gameObject.SetActive(true);
    }
}
