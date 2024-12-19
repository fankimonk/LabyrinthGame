using System.Globalization;
using Assets.src;
using TMPro;
using UnityEngine;

public class LabyrinthBoundsTextInitializer : MonoBehaviour
{
    [SerializeField] private TMP_Text MinValueTMPText;
    [SerializeField] private TMP_Text MaxValueTMPText;
    
    private GameManager _gameManager => GameManager.Instance;
    private Labyrinth _labyrinth => _gameManager.Labyrinth;

    public void InitializeText()
    {
        MinValueTMPText.text = $"Min value: {_labyrinth.MinValue.ToString(CultureInfo.CurrentCulture)}";
        MaxValueTMPText.text = $"Max value: {_labyrinth.MaxValue.ToString(CultureInfo.CurrentCulture)}";
    }
}
