using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    WaveConfiguration waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 1;    
    // Start is called before the first frame update
    void Start()
    {                
        waypoints = waveConfig.getWayPoints();
        transform.position = waypoints[0].position;        
    }

    public void SetWaveConfig(WaveConfiguration rWaveConfig)
    {
        waveConfig = rWaveConfig;
    }

    // Update is called once per frame
    void Update()
    {
        if(waypointIndex <= waypoints.Count - 1)
        {            
            transform.position =  Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, waveConfig.getMoveSpeed() * Time.deltaTime);
            if (transform.position == waypoints[waypointIndex].position)
            {                
                waypointIndex++;
            }
        } else
        {
            Destroy(gameObject);
        }
    }
}
