using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static UnityEvent<Grid> OnGenerateField = new();
    public static UnityEvent OnMixLetters = new();

    public static void SendGenerateField(Grid fieldSize) => OnGenerateField.Invoke(fieldSize);
    public static void SendMixLetters() => OnMixLetters.Invoke();
}
