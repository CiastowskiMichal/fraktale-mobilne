using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMenuController : MonoBehaviour {
	public Button button;
	// Use this for initialization
	void Start () {
		button.onClick.AddListener(
			delegate {
				BackToMenu("MainMenu");
				});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void BackToMenu(string name)
	{
		SceneManager.LoadScene(name);
	}
}
