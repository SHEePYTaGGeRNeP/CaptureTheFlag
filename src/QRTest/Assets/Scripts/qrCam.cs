using System;

using UnityEngine;
using UnityEngine.UI;
using System.Threading;

using com.google.zxing.qrcode;

public class qrCam : MonoBehaviour
{
	[SerializeField]
	private Text _text;
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
		this._planeRenderer = this.GetComponent<Renderer> ();
	}
    private void Start()
    {
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
            string decoded = this.qrReader.decode(this.d, this.W, this.H).Text;
            print(decoded);
			if (this._text != null)
				this._text.text = decoded;
        }
        catch
        { // ignore -> Alot of exceptions are thrown.
        }
    }
}
