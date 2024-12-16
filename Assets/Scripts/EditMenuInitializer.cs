using System.Globalization;
using Assets.src;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class EditMenuInitializer : MonoBehaviour
    {
        [SerializeField] private TMP_InputField InputFieldPrefab; // Префаб TMP_InputField
        [SerializeField] private RectTransform CanvasRect; // Ссылка на Canvas

        [SerializeField] private float FieldSpacing = 10f; // Зазор между полями
        [SerializeField] private float FieldScale = 1.5f; // Масштаб ширины инпут-филдов
        [SerializeField] private float EdgePadding = 20f;
        
        private GameManager _gameManager => GameManager.Instance;
        private Labyrinth _labyrinth => _gameManager.Labyrinth;

        private GameObject _editMenuGo = null;

        public void GenerateEditMenu()
        {
            if (_editMenuGo != null) Destroy(_editMenuGo);
            _editMenuGo = new GameObject("EditMenu");
            _editMenuGo.transform.SetParent(CanvasRect);
            _editMenuGo.transform.localPosition = Vector3.zero;

            float screenWidth = CanvasRect.rect.width - 2 * EdgePadding;
            float screenHeight = CanvasRect.rect.height - 2 * EdgePadding;

            float inputFieldWidth = (screenWidth - (_labyrinth.Width - 1) * FieldSpacing) / _labyrinth.Width * FieldScale;
            float inputFieldHeight = (screenHeight - (_labyrinth.Height - 1) * FieldSpacing) / _labyrinth.Height;

            float fieldSize = Mathf.Min(inputFieldWidth, inputFieldHeight);

            float fieldWidth = fieldSize * FieldScale;
            float fieldHeight = fieldSize;

            float totalMatrixWidth = _labyrinth.Width * fieldWidth + (_labyrinth.Width - 1) * FieldSpacing;
            float totalMatrixHeight = _labyrinth.Height * fieldHeight + (_labyrinth.Height - 1) * FieldSpacing;

            float startX = -totalMatrixWidth / 2 + fieldWidth / 2;
            float startY = totalMatrixHeight / 2 - fieldHeight / 2;

            for (int i = 0; i < _labyrinth.Height; i++)
            {
                for (int j = 0; j < _labyrinth.Width; j++)
                {
                    TMP_InputField newInputField = Instantiate(InputFieldPrefab, _editMenuGo.transform);

                    RectTransform rectTransform = newInputField.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(fieldWidth, fieldHeight);

                    float posX = startX + j * (fieldWidth + FieldSpacing);
                    float posY = startY - i * (fieldHeight + FieldSpacing);
                    rectTransform.anchoredPosition = new Vector2(posX, posY);

                    newInputField.text = _labyrinth[i, j].Weight.ToString(CultureInfo.CurrentCulture);
                }
            }
        }

        public void DestroyEditMenu()
        {
            if (_editMenuGo != null) Destroy(_editMenuGo);
            _editMenuGo = null;
        }
    }
}