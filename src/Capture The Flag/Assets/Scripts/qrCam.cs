using System;

using UnityEngine;
using UnityEngine.UI;

using com.google.zxing.qrcode;
using System.Collections;
using System.Linq;
using com.google.zxing.common;

public class qrCam : MonoBehaviour
{
    public delegate void QRScanHandler(string text);
    public event QRScanHandler OnQRScan;

    public string text;
    [SerializeField]
    private Text _debugText;

    [SerializeField]
    private Texture2D _texture2D;
    public Sprite sprite;
    private Renderer _planeRenderer;

    private WebCamTexture camTexture;

    private Color32[] c;
    private sbyte[] d;
    private int W, H, WxH;
    private int x, y, z;

    private readonly QRCodeReader qrReader = new QRCodeReader();

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        if (this.camTexture == null) return;
        this.camTexture.Play();
        this.W = this.camTexture.width;
        this.H = this.camTexture.height;
        this.WxH = this.W * this.H;
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        if (this.camTexture != null)
        {
            this.camTexture.Pause();
        }
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
        this.camTexture.Stop();
    }
    private void Awake()
    {
        this._planeRenderer = this.GetComponent<Renderer>();
    }
    private void Start()
    {
        this._texture2D = new Texture2D(250, 250);
        this.camTexture = new WebCamTexture();
        this._planeRenderer.material.mainTexture = this.camTexture;
        this.OnEnable();
    }

    private void Update()
    {
        this.c = this.camTexture.GetPixels32();
        this.DecodeQR();
    }

    private void DecodeQR()
    {
        try
        {
            this.d = new sbyte[this.WxH];
            this.z = 0;
            for (this.y = this.H - 1; this.y >= 0; this.y--)
            {
                for (this.x = 0; this.x < this.W; this.x++)
                {
                    this.d[this.z++] = (sbyte)(((int)this.c[this.y * this.W + this.x].r) << 16 | ((int)this.c[this.y * this.W + this.x].g) << 8 | ((int)this.c[this.y * this.W + this.x].b));
                }
            }
            // debug testing
            string decoded = String.Empty;
            //decoded = "1:Player:1:1:1";
            if (String.IsNullOrEmpty(decoded))
                decoded = this.qrReader.decode(this.d, this.W, this.H).Text;
            if (this._debugText != null)
                this._debugText.text = decoded;
            if (OnQRScan != null)
                OnQRScan.Invoke(decoded);
            this.text = decoded;
        }
        catch
        { // ignore -> Alot of exceptions are thrown.
        }
    }

    public void WriteQR()
    {
        QRCodeWriter writer = new QRCodeWriter();
        Hashtable hints = new Hashtable();

        hints.Add(com.google.zxing.EncodeHintType.ERROR_CORRECTION, com.google.zxing.qrcode.decoder.ErrorCorrectionLevel.M);
        hints.Add("Version", "7");
        ByteMatrix byteIMGNew = writer.encode("Hello", com.google.zxing.BarcodeFormat.QR_CODE, 350, 350, hints);
        sbyte[][] imgNew = byteIMGNew.Array;
        int count = imgNew.Length;
        int count2 = imgNew[0].Length;
        Debug.Log(count + "  " + count2);

        Texture2D tex = new Texture2D(350, 350);
        Color[] colors = new Color[tex.width * tex.height];
        for (int i = 0; i < imgNew.Length; i++)

            for (int j = 0; j < imgNew[i].Length; j++)
            {
                if (imgNew[i][j] == 0)
                    colors[i + j] = imgNew[i][j] == 0 ? Color.black : Color.white;
            }
        tex.SetPixels(colors);
        tex.Apply();
        Debug.Log(colors[0]);
        int countWHite = colors.Count(cx => cx == Color.white);
        int countBlack = colors.Count(cx => cx == Color.black);
        Debug.Log("white: " + countWHite + "  black: " + countBlack);
        sprite = Sprite.Create(tex, new Rect(0, 0, 100, 100), Vector2.zero);
        transform.GetComponent<Image>().sprite = sprite;
        //Bitmap bmp1 = new Bitmap(byteIMGNew.Width, byteIMGNew.Height);
        //Graphics g1 = Graphics.FromImage(bmp1);
        //g1.Clear(Color.White);
        //for (int i = 0; i <= imgNew.Length - 1; i++)
        //{
        //    for (int j = 0; j <= imgNew[i].Length - 1; j++)
        //    {
        //        if (imgNew[j][i] == 0)
        //        {
        //            g1.FillRectangle(Brushes.Black, i, j, 1, 1);
        //        }
        //        else
        //        {
        //            g1.FillRectangle(Brushes.White, i, j, 1, 1);
        //        }
        //    }
        //}
        //bmp1.Save("D:\\QREncode.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
    }


    private void UpdateTexture()
    {
        //var writer = new BarcodeWriter
        //{
        //    Format = BarcodeFormat.QR_CODE,
        //    Options = new QrCodeEncodingOptions
        //    {
        //        Height = height,
        //        Width = width
        //    }
        //};
        //Color32 color = writer.Write(textForEncoding);
        QRCodeWriter writer2 = new QRCodeWriter();
        com.google.zxing.common.ByteMatrix matrix = writer2.encode("hey", com.google.zxing.BarcodeFormat.QR_CODE, this._texture2D.width, this._texture2D.height);

    }
}
