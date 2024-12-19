using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Assets.src;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class LabyrinthEditor : MonoBehaviour
    {
        [SerializeField] private TMP_InputField InputFieldPrefab; // Префаб TMP_InputField
        [SerializeField] private RectTransform CanvasRect; // Ссылка на Canvas

        [SerializeField] private float FieldSpacing = 10f; // Зазор между полями
        [SerializeField] private float FieldScale = 1.5f; // Масштаб ширины инпут-филдов
        [SerializeField] private float EdgePadding = 20f;

        [SerializeField] private LabyrinthBuilder LabyrinthBuilder;
        private Labyrinth _labyrinth => LabyrinthBuilder.Labyrinth;

        private GameObject _editMenuGo = null;

        private TMP_InputField[,] _inputFields;

        public void GenerateEditMenu()
        {
            if (_editMenuGo != null) Destroy(_editMenuGo);
            _editMenuGo = new GameObject("EditMenu");
            _editMenuGo.transform.SetParent(CanvasRect);
            _editMenuGo.transform.localPosition = Vector3.zero;

            _inputFields = new TMP_InputField[_labyrinth.Height, _labyrinth.Width];
            
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
                    _inputFields[i, j] = newInputField;
                }
            }
        }

        public void EditLabyrinth()
        {
            var weights = new float[_inputFields.GetLength(0), _inputFields.GetLength(1)];
            for (int i = 0; i < weights.GetLength(0); i++)
                for (int j = 0; j < weights.GetLength(1); j++)
                    weights[i, j] = float.Parse(_inputFields[i, j].text);
            LabyrinthBuilder.Rebuild(weights);
        }

        public void DestroyEditMenu()
        {
            if (_editMenuGo != null) Destroy(_editMenuGo);
            _editMenuGo = null;
        }
    }
}