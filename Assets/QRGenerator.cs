using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.Linq;
using UnityEngine.UI;
using ZXing;
using ZXing.Common;

public class QRGenerator : MonoBehaviour
{
    [SerializeField] private Material material;
    private void Start()
    {
        GenerateQrCode("https://docs.nvidia.com/cuda/cuda-c-programming-guide/index.html#memory-fence-functions", 256, 256);
    }

    private void GenerateQrCode(string data, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new EncodingOptions
            {
                Width = width,
                Height = height
            }
        };
    
        var qrCode = writer.Write(data);
        var texture = new Texture2D(256, 256);
        texture.SetPixels32(qrCode);
        texture.Apply();
        material.mainTexture = texture;
    }
}