using UnityEngine;
using UnityEngine.UI;
using System.Threading;

using com.google.zxing.qrcode;

public class qrCam : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    [SerializeField]
    private RawImage _rawImage;

    [SerializeField]
    private Slider _slider;

    private WebCamTexture camTexture;
    private Thread qrThread;

    private Color32[] c;
    private sbyte[] d;
    private int W, H, WxH;
    private int x, y, z;

    private QRCodeReader qrReader = new QRCodeReader();

    void OnEnable()
    {
        Debug.Log("OnEnable");
        if (camTexture != null)
        {
            camTexture.Play();
            W = camTexture.width;
            H = camTexture.height;
            WxH = W * H;
        }
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        if (camTexture != null)
        {
            camTexture.Pause();
        }
    }


    void OnDestroy()
    {
        Debug.Log("OnDestroy");
        qrThread.Abort();
        camTexture.Stop();
    }

    void Start()
    {
        camTexture = new WebCamTexture();
        this._rawImage.texture = camTexture;
        OnEnable();
        qrThread = new Thread(DecodeQR);
        //qrThread.Start();
    }

    void Update()
    {
        c = camTexture.GetPixels32();
        DecodeQR();
    }

    public void UpdateSlider()
    {
        this._rawImage.transform.rotation = Quaternion.AngleAxis(camTexture.videoRotationAngle * this._slider.value, Vector3.right);
    }

    void DecodeQR()
    {
        try
        {
            d = new sbyte[WxH];
            z = 0;
            for (y = H - 1; y >= 0; y--)
            {
                for (x = 0; x < W; x++)
                {
                    d[z++] = (sbyte)(((int)c[y * W + x].r) << 16 | ((int)c[y * W + x].g) << 8 | ((int)c[y * W + x].b));
                }
            }
            string decoded = qrReader.decode(d, W, H).Text;
            print(decoded);
            _text.text = decoded;
        }
        catch
        {
        }
    }
}
