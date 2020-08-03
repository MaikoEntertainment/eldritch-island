using System;

[Serializable]
public class TextLanguageOwn
{
    public string english;

    public string GetText()
    {
        // Make it return the text in the language the user chose by calling a sigleton
        return english;
    }
}
