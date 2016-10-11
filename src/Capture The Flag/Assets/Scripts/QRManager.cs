using UnityEngine;
using System.Collections;
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
        BarcodeWriter barcodeWriter = new BarcodeWriter();

        barcodeWriter.Write("hoi");
        return null;
    }

   
}

