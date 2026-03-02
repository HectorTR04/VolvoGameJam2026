using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MachineBase
{
    [SerializeField] 
    public GameObject[] item;
    public Transform spawnPoint;
    public bool spawningItem = true;

    // Maybe for energy stuff
    // public bool isOn  = true;
    // public float currentEnergy;
    // public float maxEnergy;

    public float spawnTime = 1f;
    public int poolSize = 100;

    private List<GameObject> itemPool;
    private int currentIndex = 0;
        private Coroutine spawnRoutine;

    protected  override void Awake()
    {
        base.Awake();
            CreateItemPool();
    }
    private void Start()
    {
            // StartCoroutine(SpawningLoop());
            TurnOn();
    }

    private void CreateItemPool()
    {
        itemPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject itemToInstantiate = item[Random.Range(0, item.Length)];

            GameObject newObject = Instantiate(itemToInstantiate, spawnPoint.position, spawnPoint.rotation);
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
    protected override void OnTurnedOn()
    {
        if (spawnRoutine == null)
        {
            spawnRoutine = StartCoroutine(SpawningLoop());
        }
    }
    protected override void OnTurnedOff()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }

    }
     private IEnumerator SpawningLoop()
    {
        // This loop runs while the machine is on
        while (isOn)
        {
            yield return new WaitForSeconds(spawnTime);

            // In case it was turned off during the wait
            if (!isOn) yield break;

            SpawnFromPool();
        }

        spawnRoutine = null;
    }
}
