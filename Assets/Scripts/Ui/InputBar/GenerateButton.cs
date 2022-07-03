using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenerateButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField _widthInputField;
    [SerializeField] private TMP_InputField _heightInputField;

    public void OnClick()
    {
        Grid fieldSize = new(
            ValidateInput(_widthInputField),
            ValidateInput(_heightInputField)
        );

        EventManager.SendGenerateField(fieldSize);
    }

    private int ValidateInput(TMP_InputField input)
    {
        int.TryParse(input.text, out int output);

        if (output > GameConfig.GetMaxFieldSize())
        {
            output = GameConfig.GetMaxFieldSize();
            input.text = $"{output}";
        }

        return output;
    }
}
