using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AudioSystem;
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
            if (spawnPoint == null) Debug.LogError("Spawner: spawnPoint not assigned", this);
if (item == null || item.Length == 0) Debug.LogError("Spawner: item[] not assigned", this);
    }
    private void CreateItemPool()
    {
        itemPool = new List<GameObject>();

        for (int i = 0; i < item.Length; i++)
        {
            GameObject newObject = Instantiate(item[i], spawnPoint.position, spawnPoint.rotation);
            newObject.transform.SetParent(transform);
            newObject.SetActive(false);
            itemPool.Add(newObject);
        }

        for (int i = item.Length; i < poolSize; i++)
        {
            GameObject itemToInstantiate = item[Random.Range(0, item.Length)];
            GameObject newObject = Instantiate(itemToInstantiate, spawnPoint.position, spawnPoint.rotation);
            newObject.transform.SetParent(transform);
            newObject.SetActive(false);
            itemPool.Add(newObject);
        }

        Shuffle(itemPool);
    }

    private void Shuffle(List<GameObject> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            GameObject value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private void SpawnFromPool()
    {
        GameObject itemToSpawn = itemPool[currentIndex];

        itemToSpawn.transform.position = spawnPoint.position;
        itemToSpawn.transform.rotation = spawnPoint.rotation;
        itemToSpawn.SetActive(true);

        Debug.Log($"Spawned {itemToSpawn.name} at {spawnPoint.position}");
        SoundManager.PlayAt(SoundType.SFX_Bloop, transform.position);

        Rigidbody rb = itemToSpawn.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(spawnPoint.forward * 5f, ForceMode.Impulse);
        }

        currentIndex = (currentIndex + 1) % itemPool.Count;
    }
    protected override void OnTurnedOn()
    {
        if (spawnRoutine == null)
        {
            spawnRoutine = StartCoroutine(SpawningLoop());
            SoundManager.PlayAt(SoundType.SFX_SpawnerSound, transform.position);
        }
    }
    protected override void OnTurnedOff()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
            SoundManager.StopSfx(SoundType.SFX_SpawnerSound);
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
