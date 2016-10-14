using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text text;
	string qrString = "";
	bool background;

	void Start(){
		//AndroidNFCReader.enableBackgroundScan ();
		//AndroidNFCReader.ScanNFC (gameObject.name, "OnFinishScan");
	}

	void OnGUI ()
	{
		if (!background) {
			// Scan NFC button
			if (GUI.Button (new Rect (0, Screen.height - 50, Screen.width, 50), "Scan NFC")) {
				AndroidNFCReader.ScanNFC (gameObject.name, "OnFinishScan");
			}
			if (GUI.Button (new Rect (0, Screen.height - 100, Screen.width, 50), "Enable Backgraound Mode")) {
				AndroidNFCReader.enableBackgroundScan ();
				background = true;
			}
		}else{
			if (GUI.Button (new Rect (0, Screen.height - 50, Screen.width, 50), "Disable Backgraound Mode")) {
				AndroidNFCReader.disableBackgroundScan ();
				background = false;
			}
		}
		GUI.Label (new Rect (0, 0, Screen.width, 50), "Result: " + qrString);
	}

	// NFC callback
	void OnFinishScan (string result)
	{
	    try
	    {
            switch (result)
            {
                case AndroidNFCReader.CANCELLED:
                case AndroidNFCReader.ERROR:
                case AndroidNFCReader.NO_HARDWARE:
                case AndroidNFCReader.NO_ALLOWED_OS:
                    text.text = result;
                    break;
            }

	        qrString = getToyxFromUrl(result);
	        text.text = qrString;
	    }
        catch (System.Exception ex)
        {
            text.text = "ex : " + ex.Message;
        }
	}

	// Extract toyxId from url
	string getToyxFromUrl (string url)
	{		
		int index = url.LastIndexOf ('/') + 1;
		
		if (url.Length > index) {
			return url.Substring (index);		
		} 
		
		return url;
	}

}
