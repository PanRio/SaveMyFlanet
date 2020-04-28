using UnityEngine;
using System.Collections;

public class NoiseDemo : MonoBehaviour
{
    // Width and height of the texture in pixels.
    public int pixWidth = 256;
    public int pixHeight = 256;

    // The number of cycles of the basic noise pattern that are repeated
    // over the width and height of the texture.
    public float scale = 16f;
    public float swirliness = 0.5f;


    private Texture2D noiseTex;
    private Color[] pix;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Set up the texture and a Color array to hold pixels during processing.
        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
        rend.material.mainTexture = noiseTex;
    }

    void CalcNoise()
    {
        var noise = new SwirlNoise(swirliness, seed: 0);
        for (var y = 0; y < noiseTex.height; y++)
        {
            for (var x = 0;  x < noiseTex.width; x++)
            {
                float xCoord = (float)x / noiseTex.width * scale;
                float yCoord = (float)y / noiseTex.height * scale;
                float sample = noise.Sample(new Vector3(xCoord, yCoord));
                pix[y * noiseTex.width + x] = new Color(sample, sample, sample);
            }
        }

        // Copy the pixel data to the texture and load it into the GPU.
        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }

    void Update()
    {
        CalcNoise();
    }
}