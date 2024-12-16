using Assets.src;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Character : MonoBehaviour
{
    public Vector2Int PositionInLabyrinth { get; set; }

    public UnityEvent OnMoved = new UnityEvent();
    
    protected bool _isMoving = false;
    protected bool _canMove = true;

    private GameManager _gameManager => GameManager.Instance;

    protected Labyrinth _labyrinth => _gameManager.Labyrinth;

    protected void MoveUp() => StartCoroutine(Move(new Vector2Int(PositionInLabyrinth.x, PositionInLabyrinth.y - 1)));
    protected void MoveDown() => StartCoroutine(Move(new Vector2Int(PositionInLabyrinth.x, PositionInLabyrinth.y + 1)));
    protected void MoveRight() => StartCoroutine(Move(new Vector2Int(PositionInLabyrinth.x + 1, PositionInLabyrinth.y)));
    protected void MoveLeft() => StartCoroutine(Move(new Vector2Int(PositionInLabyrinth.x - 1, PositionInLabyrinth.y)));

    protected IEnumerator Move(Vector2Int newPositionInLabyrinth)
    {
        if (_isMoving || !_canMove) yield break;
        if (!_labyrinth.IsInBounds(newPositionInLabyrinth)) yield break;

        _isMoving = true;

        var cellToMoveTo = _labyrinth.GetCell(newPositionInLabyrinth);
        var time = cellToMoveTo.Weight;
        var cellPos = cellToMoveTo.Position;
        var moveTo = new Vector3(cellPos.x, transform.position.y, cellPos.y);

        var startPosition = transform.position;
        float timePassed = 0f;

        while (timePassed < time)
        {
            timePassed += Time.deltaTime;
            float t = Mathf.Clamp01(timePassed / time);
            
            transform.position = Vector3.Lerp(startPosition, moveTo, t);

            yield return null;
        }

        transform.position = moveTo;
        PositionInLabyrinth = newPositionInLabyrinth;
        _isMoving = false;
        
        OnMoved.Invoke();
    }
}
