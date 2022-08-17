using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomPrefabs : MonoBehaviour
{
    int m_CurrentSpawnCount = 0, SpawnCount = 5;
    public GameObject Enemy;
    void Start()
    {
        StartCoroutine(delayCall());
    }

    // Update is called once per frame
    IEnumerator delayCall()
    {
        /*if(m_CurrentSpawnCount <  SpawnCount)*/
        {
            for (int i = 0; i < 10; i++) // try 50 times. Brute force approach, randomly try to spawn and make sure its in the Spawnable zone. 
            {
                Vector3 position = transform.position + Random.insideUnitSphere * 100;
                position.y = this.transform.position.y;
                RaycastHit hitInfo;
                if (Physics.Raycast(position + new Vector3(0, 1, 0), Vector3.down, out hitInfo, 10) && hitInfo.collider.tag == "Spawnable")
                {
                    Instantiate(Enemy, position, Quaternion.identity);
                    yield return new WaitForSeconds(1);
                    StartCoroutine("delayCall");
                    break;
                }
            }
        }
    }
}
