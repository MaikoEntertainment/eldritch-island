using TMPro;
using UnityEngine;

public class UITextLanguage : MonoBehaviour
{
    public TextLanguageOwn text;

    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = text.GetText();
    }
}
