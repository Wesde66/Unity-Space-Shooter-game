using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _Enemy;
    [SerializeField] float _spawnTime = 2;
    [SerializeField] GameObject _EnemyContainer;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            float RandomX = Random.Range(-8f, 8f);
            Vector3 spawnPoint = new Vector3(RandomX, 11.5f, 0);
            GameObject newEnemy = Instantiate(_Enemy, spawnPoint, Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;

            yield return new WaitForSeconds(_spawnTime);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
