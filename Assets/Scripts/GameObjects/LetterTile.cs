using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LetterTile : MonoBehaviour
{
    [NonSerialized] public bool isMoving = false;

    private Vector2 _newPosition;

    public void Init(Vector2 position, Vector2 size, string letter)
    {
        transform.localPosition = position;
        GetComponent<RectTransform>().sizeDelta = size;
        GetComponent<TMP_Text>().text = letter;
    }

    public void MoveTo(Vector2 position)
    {
        _newPosition = position;
        isMoving = true;
        StartCoroutine(MovingLerp());
    }

    IEnumerator MovingLerp()
    {
        float timeElapsed = 0;
        AnimationCurve curve = AnimationCurve.EaseInOut(timeElapsed, 0, GameConfig.GetMixingLerpTime(), 1);

        while (timeElapsed < GameConfig.GetMixingLerpTime())
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, _newPosition, curve.Evaluate(timeElapsed / GameConfig.GetMixingLerpTime()));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = _newPosition;
        isMoving = false;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
