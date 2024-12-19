using Assets.src;
using TMPro;
using UnityEngine;

public class LabyrinthBuilder : MonoBehaviour
{
    public Labyrinth Labyrinth { get; private set; } = null;
    
    private const int OptimalHeight = 15;
    private const int OptimalWidth = 15;

    public float ScaleCoeffX => (float)OptimalWidth / Width;
    public float ScaleCoeffZ => (float)OptimalHeight / Height;
    
    [SerializeField] private TMP_InputField WidthInput;
    [SerializeField] private TMP_InputField HeightInput;
    [SerializeField] private TMP_InputField MinValueInput;
    [SerializeField] private TMP_InputField MaxValueInput;

    [SerializeField] private GameObject CellPrefab;

    private int Width => int.Parse(WidthInput.text);
    private int Height => int.Parse(HeightInput.text);

    private float MinValue => float.Parse(MinValueInput.text);
    private float MaxValue => float.Parse(MaxValueInput.text);
    
    private float _prefabScaleY => CellPrefab.transform.localScale.y;
    
    private Gradient _gradient;

    private GameObject _labyrinthGo = null;
    
    private void Start()
    {
        InitializeGradient();
    }

    private void Update()
    {
        
    }

    public void Build()
    {
        Destroy();

        Labyrinth = new Labyrinth(Width, Height, MinValue, MaxValue, ScaleCoeffX, ScaleCoeffZ);

        GenerateGOs();
    }
    
    public void Rebuild(float[,] weights)
    {
        var newLabyrinth = new Labyrinth(weights);
        
        Destroy();
        
        Labyrinth = newLabyrinth;
        
        GenerateGOs();
    }

    private void GenerateGOs()
    {
        _labyrinthGo = new GameObject("Labyrinth");
        _labyrinthGo.transform.position = Vector3.zero;

        
        for (int i = 0; i < Labyrinth.Height; i++)
        {
            for (int j = 0; j < Labyrinth.Width; j++)
            {
                var currentCell = Labyrinth.Cells[i, j];
                var cellGo = Instantiate(CellPrefab, new Vector3(currentCell.Position.x, 0.0f, currentCell.Position.y), 
                    Quaternion.identity, _labyrinthGo.transform);
                
                cellGo.transform.localScale = new Vector3(ScaleCoeffX, _prefabScaleY, ScaleCoeffZ);
                cellGo.GetComponent<Renderer>().material.color = GetGradientColor(Labyrinth[i, j].Weight);
            }
        }
    }
    
    public void Destroy()
    {
        if (_labyrinthGo != null) Destroy(_labyrinthGo);
        _labyrinthGo = null;
        Labyrinth = null;
        Debug.Log("Labyrinth Destroyed");
    }

    private Color GetGradientColor(float weight)
    {
        float time = (weight - MinValue) / (MaxValue - MinValue);
        return _gradient.Evaluate(time);
    }
    
    private void InitializeGradient()
    {
        _gradient = new Gradient();
        var colors = new GradientColorKey[3];
        colors[0] = new GradientColorKey(Color.green, 0.0f);
        colors[1] = new GradientColorKey(Color.yellow, 0.5f);
        colors[2] = new GradientColorKey(Color.red, 1.0f);

        var alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphas[1] = new GradientAlphaKey(1.0f, 1.0f);
        
        _gradient.SetKeys(colors, alphas);
    }
}
