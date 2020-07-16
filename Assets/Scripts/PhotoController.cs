using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoController : MonoBehaviour
{

    public Button button;
    // Use this for initialization
    void Start()
    {
        button.onClick.AddListener(
			delegate 
			{
				StartCoroutine(TakeScreenshotAndSave());
				});
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D((int)(Screen.width * 0.8f) , Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect((int)(Screen.width * 0.1f), 0, (int)(Screen.width * 0.9f), Screen.height), 0, 0);
        ss.Apply();

        // Save the screenshot to Gallery/Photos
        Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, "Gallery Test", "My img {0}.png"));

        // To avoid memory leaks
        Destroy(ss);
    }

}
