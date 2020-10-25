using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Utils
{
    public static T DeepClone<T>(T obj)
    {
        using (var stream = new System.IO.MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }
    }

    static readonly string[] suffixes = { "", "k", "M", "B", "T" };
    public static string ToFormat(double cash, string prefix = "")
    {
        int k;
        if (cash == 0)
            k = 0;    // log10 of 0 is not valid
        else
            k = (int)(Math.Log10(cash) / 3); // get number of digits and divide by 3
        var dividor = Math.Pow(10, k * 3);  // actual number we print
        string format = (k > 0) ? "F1" : "F0";
        var text = prefix + (cash / dividor).ToString(format) + suffixes[k];
        return text;
    }

    public static Color GetSuccessColor()
    {
        return new Color(.24f,.78f,.27f);
    }

    public static Color GetWrongColor()
    {
        return new Color(1f, 0, .37f);
    }

    public static Color GetToolColor()
    {
        return new Color(0.8f, 0.53f, 0.17f);
    }

    public static Color GetClothesColor()
    {
        return GetSuccessColor();
    }
}
