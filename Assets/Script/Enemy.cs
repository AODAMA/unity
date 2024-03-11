using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Rigidbody2D rb;

    private ContactPoint2D[] listContacts = new ContactPoint2D[1];

    public int enemyHealth = 3;
    public int currentEnemyHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentEnemyHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")) {
            other.GetContacts(listContacts);
            if(listContacts[0].normal.y < -0.5f) {
                currentEnemyHealth = currentEnemyHealth - 1;
                if (currentEnemyHealth == 0) {
                    Destroy(gameObject);
                }
            } else {
                PlayerHealth PlayerHealth = other.gameObject.GetComponent<PlayerHealth>();
                PlayerHealth.Hurt(1);
            }

        }
    }
}
