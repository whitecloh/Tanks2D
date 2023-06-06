using System.Collections;
using Tanks;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] _enemySpawnPoints;
    [SerializeField]
    private Transform _playerSpawnPoint;
    [SerializeField , Range(1,6)]
    private int _enemyCount;
    [SerializeField]
    private ShootComponent _playerPrefab;
    [SerializeField]
    private ShootComponent _enemyPrefab;
    [SerializeField]
    private float _spawnDelay=2f;
    [SerializeField]
    private int _maxEnemies=10;


    private void Start()
    {
        ShuffleList();
        //for (int i = 0; i < _enemyCount; i++)
        //{
        for (int i = 0; i <= Random.Range(1,6); i++)
        {
            Spawn(_enemyPrefab);
        }
        Spawn(_playerPrefab);
    }
    private void Spawn(ShootComponent prefab) // спаун ботов
    {

        if(prefab.GetSide!=SideType.Enemy)
        {
            Instantiate(prefab, _playerSpawnPoint);
            prefab.transform.position = Vector2.zero;
        }
        else
        {
            for(int i = 0;i<_enemySpawnPoints.Length; i++)
            {
                if (_enemySpawnPoints[i].childCount==0)
                {
                    Instantiate(prefab, _enemySpawnPoints[i]);
                    prefab.transform.position = Vector2.zero;
                    _maxEnemies--;
                    break;
                }
            }
        }
    }

    public IEnumerator SpawnAfterDead() // спаун бота после смерти
    {

        yield return new WaitForSeconds(_spawnDelay);
        if(_maxEnemies>0) Spawn(_enemyPrefab);
    }
    private void ShuffleList()
    {
        for (int i = _enemySpawnPoints.Length - 1; i >= 1; i--)
        {
            int j = Random.Range(0,i);
            var temp = _enemySpawnPoints[j];
            _enemySpawnPoints[j] = _enemySpawnPoints[i];
            _enemySpawnPoints[i] = temp;
        }
    } // шафл массива точек респавна ботов
}
