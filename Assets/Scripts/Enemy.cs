using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{    
    [SerializeField] AudioClip destructionSound;
    [SerializeField] GameObject enemyLaser;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] int health = 500;

    private void Start()
    {
        StartCoroutine(ShootLaser());            
    }

    private IEnumerator ShootLaser()
    {
        while(true)
        {
            GameObject spawnedEnemyLaser = Instantiate(enemyLaser, transform.position, Quaternion.identity);
            spawnedEnemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(-12f, -15f));           
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.returnDamage();
        damageDealer.Hit();       
        if(health <=0)
        {
            AudioSource.PlayClipAtPoint(destructionSound, Camera.main.transform.position, 0.2f);
            Destroy(Instantiate(explosionVFX, transform.position, Quaternion.identity), 1f);
            Destroy(gameObject);                      
        }
    }
}
