using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    public GameObject[] _PropPrefabs;
    private BoxCollider _area;
    public int _Count = 100;
    private List<GameObject> _props = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _area = GetComponent<BoxCollider>();

        for (int nIndex = 0; nIndex < _Count; nIndex++)
        {
            Spawn();
        }

        _area.enabled = false;
    }

    void Spawn()
    {
        int selection = Random.Range(0, _PropPrefabs.Length);

        GameObject selectedPrefab = _PropPrefabs[selection];

        Vector3 spawnPos = GetRandomPosition();

        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);

        _props.Add(instance);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = _area.size;

        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;
    }

    public void Reset()
    {
        for(int nIndex = 0; nIndex < _props.Count; nIndex++)
        {
            _props[nIndex].transform.position = GetRandomPosition();
            _props[nIndex].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

