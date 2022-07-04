using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixButton : MonoBehaviour
{
    public void OnClick()
    {
        EventManager.SendMixLetters();
    }
}
