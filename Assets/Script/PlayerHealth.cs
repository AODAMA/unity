using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    //public int maxLifePoints;

    //public int currentLifePoints;

    public bool isInvulnerable = false;

    public SpriteRenderer sr;

    public float invulnerableDuration = 3;

    public float invulnerableDelta = 0.15f;

    //public HealthBar healthBar;

    public PlayerData playerData;

    public VoidEventChannel OnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        playerData.currentLifePoints = playerData.maxLifePoints;

        //currentLifePoints = maxLifePoints;

        //float amount = ((float)currentLifePoints / (float)maxLifePoints);
        //healthBar.Fill(amount);
    }

    public void Hurt(int damage) {
        if(isInvulnerable) {
            return;
        }

        playerData.currentLifePoints = playerData.currentLifePoints - damage;

        if(playerData.currentLifePoints <= 0) {
            Die();
        } else {
            StartCoroutine(Invulnerable());
        }
        //float amount = ((float)playerData.currentLifePoints / (float)playerData.maxLifePoints);
        //healthBar.Fill(amount);
    }

    private void Die()
    {
        OnPlayerDeath?.Raise();
        sr.color = Color.red;
        foreach (Transform child in transform)
        {
            SpriteRenderer childSr = child.GetComponent<SpriteRenderer>();
            if (childSr != null) {
                childSr.color = Color.red;
            }
        }
        Time.timeScale = 0;
    }

    public IEnumerator Invulnerable() { //IEnumerator permet de mettre des pauses dans le code (yield) "StartCoroutine"
        isInvulnerable = true;

        WaitForSeconds invulnerableDeltaWait = new WaitForSeconds(invulnerableDelta);

        for (float i = 0; i <= invulnerableDuration; i += invulnerableDelta) {
            if(sr.color.a == 1) {
                sr.color = new Color(1f, 1f, 1f, 0f);
            } else {
                sr.color = Color.white;
            }

            yield return invulnerableDeltaWait; //le yield permet au code d'attendre
        }

        sr.color = Color.white;
        isInvulnerable =  false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
