using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LettersFieldGenerator : MonoBehaviour
{
    [SerializeField] GameObject _letterTilePrefab;

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

        //Debug.Log($"w: {_fieldWidth} | h:{_fieldHeight}");
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
        //Debug.Log("GEN");
        ClearField();

        (int cols, int rows) = fieldSize;
        Vector2 tileSize = new(_fieldWidth / cols, _fieldHeight / rows);

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
        string letter = _lettersArray[UnityEngine.Random.Range(0, _lettersArray.Length - 1)].ToString();
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
        List<LetterTile> list = _letterTilesList;

        //_letterTilesList[0].MoveTo(_letterTilesList[1].transform.localPosition);
        //_letterTilesList[1].MoveTo(_letterTilesList[0].transform.localPosition);
    }


    public void PositionSwap(LetterTile firstTile, LetterTile secondTile)
    {
        Vector2 firstPosition = firstTile.transform.localPosition;
        Vector2 secondPosition = secondTile.transform.localPosition;

        firstTile.transform.localPosition = Vector2.Lerp(firstPosition, secondPosition, 2f);
        secondTile.transform.localPosition = Vector2.Lerp(secondPosition, firstPosition, 2f);
    }
}
