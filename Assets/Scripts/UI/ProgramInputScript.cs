using UnityEngine;
using TMPro;

public class ProgramInputScript : MonoBehaviour {

    /// <summary>
    /// Get current value of imput field as string
    /// </summary>
    /// <returns> Imput value as string </returns>
    public string GetImputString() {
        return GetComponent<TMP_InputField>().text;
    }

    /// <summary>
    /// Get current value of imput field as string[].
    /// Values split by commas (E.g.: 1,2,3,4).
    /// </summary>
    /// <returns> String list of imput values</returns>
    public string[] GetInputArrayStr() {
        return GetComponent<TMP_InputField>().text.Replace(" ", string.Empty).Split(',');
    }    
}
