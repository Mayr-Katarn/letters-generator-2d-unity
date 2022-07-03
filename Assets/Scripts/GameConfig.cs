using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    [SerializeField] private int _maxFieldSize = 16;
    [SerializeField] private float _mixingLerpTime = 2f;

    public static GameConfig instance;

    private void Start()
    {
        instance = this;
    }

    public static int GetMaxFieldSize() => instance._maxFieldSize;
    public static float GetMixingLerpTime() => instance._mixingLerpTime;
}
