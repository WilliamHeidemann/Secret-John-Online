using System.Linq;
using _0_MainMenu;
using UnityEngine;
using ZXing;
using ZXing.Common;

namespace _1_Lobby
{
    public class QRGenerator : MonoBehaviour
    {
        [SerializeField] private Material material;
        [SerializeField] private Color32 backgroundColor;
        private readonly Color32 beige = new(246, 226, 194, 255);

        private void Start()
        {
            GenerateQrCode($"https://game-dev-blog-xi.vercel.app/QR-game?pin={GameConnector.JoinCode}", 256, 256);
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
            qrCode = qrCode.Select(color => color.r == 0 ? beige : backgroundColor).ToArray();
            var texture = new Texture2D(256, 256);
            texture.SetPixels32(qrCode);
            texture.Apply();
            material.mainTexture = texture;
        }
    }
}