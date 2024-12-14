using Assets.src;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;
    private float _playerScaleX => PlayerPrefab.transform.localScale.x;
    private float _playerScaleY => PlayerPrefab.transform.localScale.y;
    private float _playerScaleZ => PlayerPrefab.transform.localScale.z;

    [SerializeField] private GameObject EnemyPrefab;
    private float _enemyScaleY => PlayerPrefab.transform.localScale.y;
    
    [SerializeField] private GameObject _labyrinthCellPrefab;
    private float _cellScaleY => PlayerPrefab.transform.localScale.y;
    
    [SerializeField] private LabyrinthBuilder _labyrinthBuilder;
    private Labyrinth _labyrinth => _labyrinthBuilder.Labyrinth;
    private float _scaleCoeffX => _labyrinthBuilder.ScaleCoeffX;
    private float _scaleCoeffZ => _labyrinthBuilder.ScaleCoeffZ;

    private GameObject _player = null;
    
    public void SpawnPlayer()
    {
        var cellPos = _labyrinth[0, 0].Position;
        var spawnPos = new Vector3(cellPos.x, _cellScaleY / 2 + _playerScaleY, cellPos.y);
        
        _player = Instantiate(PlayerPrefab, spawnPos, Quaternion.identity);
        _player.transform.localScale = new Vector3(_playerScaleX * _scaleCoeffX, _playerScaleY, _playerScaleZ * _scaleCoeffZ);
        _player.GetComponent<Renderer>().material.color = Color.black;
    }

    public void DespawnPlayer()
    {
        if (_player != null) Destroy(_player);
        _player = null;
    }
    
    private void SpawnEnemy()
    {
        
    }
    
}
