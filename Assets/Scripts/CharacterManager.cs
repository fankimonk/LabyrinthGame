using Assets.src;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;
    private float _playerScaleX => PlayerPrefab.transform.localScale.x;
    private float _playerScaleY => PlayerPrefab.transform.localScale.y;
    private float _playerScaleZ => PlayerPrefab.transform.localScale.z;

    [SerializeField] private GameObject EnemyPrefab;
    private float _enemyScaleX => EnemyPrefab.transform.localScale.x;
    private float _enemyScaleY => EnemyPrefab.transform.localScale.y;
    private float _enemyScaleZ => EnemyPrefab.transform.localScale.z;
    
    [SerializeField] private GameObject _labyrinthCellPrefab;
    private float _cellScaleY => PlayerPrefab.transform.localScale.y;
    
    [SerializeField] private LabyrinthBuilder _labyrinthBuilder;
    private Labyrinth _labyrinth => _labyrinthBuilder.Labyrinth;
    private float _scaleCoeffX => _labyrinthBuilder.ScaleCoeffX;
    private float _scaleCoeffZ => _labyrinthBuilder.ScaleCoeffZ;

    private GameObject _player = null;
    private GameObject _enemy = null;
    
    public void SpawnPlayer()
    {
        var posInLabyrinth = new Vector2Int(0, _labyrinth.Height - 1);
        var cellPos = _labyrinth.GetCell(posInLabyrinth).Position;
        var spawnPos = new Vector3(cellPos.x, _cellScaleY / 2 + _playerScaleY, cellPos.y);
        
        _player = Instantiate(PlayerPrefab, spawnPos, Quaternion.identity);
        _player.transform.localScale = new Vector3(_playerScaleX * _scaleCoeffX, _playerScaleY, _playerScaleZ * _scaleCoeffZ);
        _player.GetComponent<Renderer>().material.color = Color.black;
        _player.GetComponent<Player>().PositionInLabyrinth = posInLabyrinth;
    }

    public void DespawnPlayer()
    {
        if (_player != null) Destroy(_player);
        _player = null;
    }
    
    public void SpawnEnemy()
    {
        var posInLabyrinth = new Vector2Int(_labyrinth.Height - 1, 0);
        var cellPos = _labyrinth.GetCell(posInLabyrinth).Position;
        var spawnPos = new Vector3(cellPos.x, _cellScaleY / 2 + _enemyScaleY, cellPos.y);
        
        _enemy = Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
        _enemy.transform.localScale = new Vector3(_enemyScaleX * _scaleCoeffX, _enemyScaleY, _enemyScaleZ * _scaleCoeffZ);
        _enemy.GetComponent<Renderer>().material.color = Color.black;
        _enemy.GetComponent<Enemy>().PositionInLabyrinth = posInLabyrinth;
        _enemy.GetComponent<Enemy>().Player = _player.GetComponent<Player>();
    }

    public void DespawnEnemy()
    {
        if (_enemy != null) Destroy(_enemy);
        _enemy = null;
    }
    
}
