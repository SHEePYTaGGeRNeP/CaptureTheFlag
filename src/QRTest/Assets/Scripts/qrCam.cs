using UnityEngine;
using UnityEngine.UI;
using System.Threading;

using com.google.zxing.qrcode;

public class qrCam : MonoBehaviour {

    [SerializeField]
	private Text _text;
    [SerializeField]
    private Image _image;

	private WebCamTexture camTexture;
	private Thread qrThread;

	private Color32[] c;
	private sbyte[] d;
	private int W, H, WxH;
	private int x, y, z;
	
	void OnEnable () {
		Debug.Log ("OnEnable");
		if(camTexture != null) {
			camTexture.Play();
			W = camTexture.width;
			H = camTexture.height;
			WxH = W * H;
		}
	}
	
	void OnDisable () {
		if(camTexture != null) {
			camTexture.Pause();
		}
	}
	
	void OnDestroy () {
		qrThread.Abort();
		camTexture.Stop();
	}
	
	void Start () {
		camTexture = new WebCamTexture();
		OnEnable();
		
		qrThread = new Thread(DecodeQR);
		qrThread.Start();
	}
	
	void Update () {
		c = camTexture.GetPixels32();
	}
	
	void DecodeQR () {
		while(true) {
			try {
				d = new sbyte[WxH];
				z = 0;
				for(y = H - 1; y >= 0; y--) {
					for(x = 0; x < W; x++) {
						d[z++] = (sbyte)(((int)c[y * W + x].r) << 16 | ((int)c[y * W + x].g) << 8 | ((int)c[y * W + x].b));
					}
				}

                print(new QRCodeReader().decode(d, W, H).Text);
				_text.text = new QRCodeReader().decode(d, W, H).Text;
			}
			catch {
				continue;
			}
		}
	}
}
