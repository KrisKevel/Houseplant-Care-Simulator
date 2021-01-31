using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMeasurer : MonoBehaviour
{
    public float UpdateTime = 10f;
    private float _timeBeforeUpdate;
    private RenderTexture _targetTexture;
    private double _luminosity;
    
    void Start()
    {
        _targetTexture = gameObject.GetComponent<Camera>().targetTexture;
        _timeBeforeUpdate = UpdateTime;
    }

    void Update()
    {
        _timeBeforeUpdate -= Time.deltaTime;
        if (_timeBeforeUpdate < 0)
        {
            _timeBeforeUpdate = UpdateTime;
            Color colorValue = _targetTexture.toTexture2D().GetPixel(16,16);
            _luminosity = 0.2126 * colorValue.r + 0.7152 * colorValue.g + 0.0722 * colorValue.b;
            print("Luminocity " + _luminosity);
        }
    }
}

public static class ExtensionMethod
{
    public static Texture2D toTexture2D(this RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(32, 32, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
