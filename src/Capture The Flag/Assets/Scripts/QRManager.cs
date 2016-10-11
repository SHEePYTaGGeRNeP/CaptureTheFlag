using UnityEngine;
using System.Collections;
using com.google.zxing;
using com.google.zxing.qrcode;
using ZXing;

public class QRManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    Texture2D GenerateQRCode(string text)
    {
        
        QRCodeWriter qr = new QRCodeWriter();
        //qr.encode("hoi", BarcodeFormat.QR_CODE, 128, 128)
        //Texture2D.LoadImage(;
        return null;
    }

   
}

