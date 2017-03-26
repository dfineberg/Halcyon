using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourScheme : MonoBehaviour
{
    [System.Serializable]
    public struct Colours
    {
        public Color Background;
        public Color Foreground;
    }

    public int ColourSchemeNo;
    public bool RandomiseOnStart;
    public Colours[] ColourSchemes;

    [Space] public Image Gradient;
    public Text TitleText;
    public Material[] ForegroundMaterials;

    private void Start()
    {
        if (RandomiseOnStart)
            ColourSchemeNo = Random.Range(0, ColourSchemes.Length);

        SetColourScheme(ColourSchemes[ColourSchemeNo]);
    }

    private void SetColourScheme(Colours colourScheme)
    {
        Camera.main.backgroundColor = colourScheme.Background;
        Gradient.color = colourScheme.Background;
        TitleText.color = colourScheme.Foreground;

        foreach (var mat in ForegroundMaterials)
            mat.color = colourScheme.Foreground;


    }
}
