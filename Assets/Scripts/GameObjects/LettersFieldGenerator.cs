using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LettersFieldGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _letterTilePrefab;

    private char[] _lettersArray;
    private float _fieldWidth;
    private float _fieldHeight;
    private bool _isLettersMixing;
    private readonly List<LetterTile> _letterTilesList = new();

    private void OnEnable()
    {
        EventManager.OnGenerateField.AddListener(GenerateField);
        EventManager.OnMixLetters.AddListener(MixLetters);
    }

    private void Start()
    {
        _lettersArray = Enumerable.Range('A', 'Z' - 'A' + 1).Select(c => (char)c).ToArray();
        _fieldWidth = GetComponent<RectTransform>().rect.width;
        _fieldHeight = GetComponent<RectTransform>().rect.height;
    }

    private void Update()
    {
        CheckLettersMixing();
    }

    private void CheckLettersMixing()
    {
        _isLettersMixing = _letterTilesList.Any(tile => tile.isMoving);
    }

    private void GenerateField(Grid fieldSize)
    {
        if (_isLettersMixing) return;
        ClearField();

        (int cols, int rows) = fieldSize;
        float tileWidth = _fieldWidth / cols;
        float tileHeight = Mathf.Min(_fieldHeight / rows, tileWidth);
        Vector2 tileSize = new(tileWidth, tileHeight);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                CreateLettersTile(tileSize, new Grid(j, i));
            }
        }
    }

    private void CreateLettersTile(Vector2 tileSize, Grid grid)
    {
        Vector2 position = new(tileSize.x / 2 + tileSize.x * grid.col, tileSize.y / 2 + tileSize.y * grid.row);
        string letter = _lettersArray[Random.Range(0, _lettersArray.Length - 1)].ToString();
        LetterTile letterTile = Instantiate(_letterTilePrefab, transform).GetComponent<LetterTile>();
        letterTile.Init(position, tileSize, letter);
        _letterTilesList.Add(letterTile);
    }

    private void ClearField()
    {
        if (_letterTilesList.Count > 0)
        {
            _letterTilesList.ForEach(el => el.Destroy());
            _letterTilesList.Clear();
        }
    }

    private void MixLetters()
    {
        if (_isLettersMixing || _letterTilesList.Count == 0) return;

        LetterTile nextTileToMove = _letterTilesList[0];
        Vector2 firstTilePosition = _letterTilesList[0].transform.localPosition;

        for (int i = 0; i < _letterTilesList.Count; i++)
        {
            List<LetterTile> notMovingTiles = _letterTilesList.FindAll(tile => !tile.isMoving && !tile.Equals(nextTileToMove));
            if (notMovingTiles.Count == 0)
            {
                nextTileToMove.MoveTo(firstTilePosition);
                break;
            }

            LetterTile letterTileTarget = notMovingTiles[Random.Range(0, notMovingTiles.Count - 1)];
            nextTileToMove.MoveTo(letterTileTarget.transform.localPosition);
            nextTileToMove = letterTileTarget;
        }
    }
}
