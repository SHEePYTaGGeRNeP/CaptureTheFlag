using UnityEngine;
using System.Collections;
using com.google.zxing;
using com.google.zxing.qrcode;
using ZXing;
using System.Collections.Generic;

public class QRManager : MonoBehaviour {
    public List<Sprite> QRCodes;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public Sprite GenerateQRCode(string text)
    {
        string trimmedText = text.Trim(new System.Char[] { ':'});
        foreach(Sprite code in QRCodes)
        {
            if (code.name == trimmedText)
                return code;
        }
        return null;
    }

   
}

