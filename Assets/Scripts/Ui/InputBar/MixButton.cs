using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixButton : MonoBehaviour
{
    public InputBarButtonType buttonType;

    public void OnClick()
    {
        EventManager.SendMixLetters();
    }
}
