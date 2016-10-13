using UnityEngine;

public class VersionText : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    this.GetComponent<UnityEngine.UI.Text>().text = "Version: " + Application.version;
	}
}
