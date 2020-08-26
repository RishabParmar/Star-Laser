using System.Collections;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    float moveSpeed = 15f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float padding = 0.5f;    
    [SerializeField] GameObject laserPrefab;
    [SerializeField] AudioClip destructionSound;
    TextMeshProUGUI healthText;
    Coroutine firing;
    int health = 300;

    // Start is called before the first frame update
    void Start()
    {
        // Just see the video on ViewportToWorldPoint
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        healthText = GameObject.Find("Health Info").GetComponent<TextMeshProUGUI>();
        healthText.text = health.ToString();
    }

    private void ShipMovement()
    {
        // GetAxis deals with the x and y axis (Horizontal and Vertical respectively) with some pre configured key combinations
        // such as left arrow and a key moves the player along the x and y axis. The factor being added here is being multiplied 
        // with Time.deltaTime as it creates the exact same effect on a slow or a fast pc independent of the frame rate. Movespeed is 
        // just a variable that makes the ship go faster or slower which depends on player tuning. You can also control the speed of 
        // the ship by not multiplying the Time.deltaTime and moveSpeed and engaging with the sensitivity option of project input 
        // settings but the different frame rate support offered by deltaTime won't be added to the project here.
        var deltaX = transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = transform.position.y + Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        // Adding some padding so that the ship stays in the game and not go partially of the screen as the camera gives the limit
        // due to the pivot points of the ship
        deltaX = Mathf.Clamp(deltaX, xMin + padding, xMax - padding);
        deltaY = Mathf.Clamp(deltaY, yMin + padding, yMax - padding);
        transform.position = new Vector2(deltaX, deltaY);
    }

    // Update is called once per frame
    void Update()
    {
        ShipMovement();
        if(Input.GetButtonDown("Fire1"))
        {
            // We have created a coroutine as we want the bullets to be fired continuously if we kept the space bar pressed
            firing = StartCoroutine(FireContinuously());            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firing);
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            ShootLaser();
            // yield return will return the control after firing a single bullet in the while loop again since we are dealing
            // with a IENumberator I assume. So in simpler terms, fire a bullet, return the control to the start of the infinite
            // while loop since true and then 
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ShootLaser()
    {
        // Here, we assign the output of Instantiate() to GameObject laser because Instantiate() provides a clone of the originial
        // laserPrefab for us to work on. Hence, to perform any operations, we work with the laser handle
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 20f);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.returnDamage();        
        healthText.text = health.ToString();
        damageDealer.Hit();
        if (health <= 0)
        {
            healthText.text = "0";
            GameOver();            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {      
        if (collision.gameObject.tag == "Enemy")
        {
            healthText.text = "0";
            GameOver();            
        }
    }

    public void GameOver()
    {
        AudioSource.PlayClipAtPoint(destructionSound, Camera.main.transform.position, 0.5f);
        Destroy(gameObject);
        FindObjectOfType<SceneLoader>().LoadNextSceneWithDelay();
    }
}
