using Assets.src;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    private GameManager _gameManager => GameManager.Instance;

    private Labyrinth _labyrinth => _gameManager.Labyrinth;

    private bool _isMoving = false;

    protected Vector2Int _positionInLabyrinth;

    protected void MoveUp() => StartCoroutine(Move(new Vector2Int(_positionInLabyrinth.x, _positionInLabyrinth.y - 1)));
    protected void MoveDown() => StartCoroutine(Move(new Vector2Int(_positionInLabyrinth.x, _positionInLabyrinth.y + 1)));
    protected void MoveRight() => StartCoroutine(Move(new Vector2Int(_positionInLabyrinth.x + 1, _positionInLabyrinth.y)));
    protected void MoveLeft() => StartCoroutine(Move(new Vector2Int(_positionInLabyrinth.x - 1, _positionInLabyrinth.y)));

    private IEnumerator Move(Vector2Int newPositionInLabyrinth)
    {
        if (_isMoving) yield break;
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
        _positionInLabyrinth = newPositionInLabyrinth;
        _isMoving = false;
    }
}
