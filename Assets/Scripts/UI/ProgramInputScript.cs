using UnityEngine;
using TMPro;

public class ProgramInputScript : MonoBehaviour {

    public string GetImputString() {
        return GetComponent<TMP_InputField>().text;
    }

    public string[] GetInputArrayStr() {
        return GetComponent<TMP_InputField>().text.Replace(" ", string.Empty).Split(',');
    }    
}
