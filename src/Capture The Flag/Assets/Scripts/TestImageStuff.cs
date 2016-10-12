using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TestImageStuff : MonoBehaviour {

	[SerializeField]
	private RawImage _rawImage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Test1()
	{
		if (this._rawImage.transform.eulerAngles.y < 0)
			this._rawImage.transform.eulerAngles = new Vector3 (0, 0, 0);
		else
			this._rawImage.transform.eulerAngles = new Vector3 (0, -180, 0);
	}

	public void TestUV()
	{
		
	}
}
