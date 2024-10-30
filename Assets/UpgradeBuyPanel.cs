using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeBuyPanel : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void OnEnable()
    {
        text.text = "Are you sure you want to upgrade " + FormatNameString(MenuController.menuControllerInst.OBJECTTOUPDATE.name) + " for 30 coins";
    }
    private string FormatNameString(string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        System.Text.StringBuilder formattedString = new System.Text.StringBuilder();

        for (int i = 0; i < str.Length; i++)
        {
            // Add space before uppercase letters (except for the first letter and consecutive uppercase letters)
            if (i > 0 && char.IsUpper(str[i]) && !char.IsUpper(str[i - 1]))
            {
                formattedString.Append(' ');
            }

            // Append the character, converting it to lowercase
            formattedString.Append(char.ToLower(str[i]));
        }

        return formattedString.ToString();
    }


}
