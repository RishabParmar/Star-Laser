using UnityEngine;

public class Player : MonoBehaviour
{
    float moveSpeed = 15f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float padding = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        // Just see the video on ViewportToWorldPoint
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    // Update is called once per frame
    void Update()
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
}
