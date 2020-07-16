using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseFractal : MonoBehaviour {

	public string sceneName;
	public Button button;
	// Use this for initialization
	void Start () {
		button.onClick.AddListener(RunScene);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// OnMouseDown is called when the user has pressed the mouse button while
	/// over the GUIElement or Collider.
	/// </summary>
	void RunScene()
	{
		SceneManager.LoadScene(sceneName);
	}
}
