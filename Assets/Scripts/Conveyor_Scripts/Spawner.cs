using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject item;
    public Transform spawnPoint;
    public bool spawningItem = true;
    public float spawnTime;
    public int poolSize = 100;

    private List<GameObject> itemPool;
    private int currentIndex = 0;

    private void Awake()
    {
        CreateItemPool();
    }
    private void Start()
    {
        StartCoroutine(spawning());
    }

    IEnumerator spawning()
    {
        while (spawningItem == true)
        {
            yield return new WaitForSeconds(spawnTime);

            SpawnFromPool();
        }
    }

    private void CreateItemPool()
    {
        itemPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObject = Instantiate(item, spawnPoint.position, spawnPoint.rotation);
            newObject.transform.SetParent(transform);
            newObject.SetActive(false);

            itemPool.Add( newObject );
        }
    }

    private void SpawnFromPool()
    {
        GameObject itemToSpawn = itemPool[currentIndex];

        itemToSpawn.transform.position = spawnPoint.position;
        itemToSpawn.transform.rotation = spawnPoint.rotation;
        itemToSpawn.SetActive(true);

        Rigidbody rb = itemToSpawn.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(spawnPoint.forward * 5f, ForceMode.Impulse);
        }

        currentIndex++;

        if (currentIndex >= poolSize)
        {
            currentIndex = 0;
        }
    }

}
