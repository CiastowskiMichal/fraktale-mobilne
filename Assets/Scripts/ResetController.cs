using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetController : MonoBehaviour {
	public Material material;
	public float ColorR, ColorG, ColorB, Iterations, PosX, PosY, Zoom;
	public Button button;
	public GameObject fractal;
	public 

	// Use this for initialization
	void Start () {
		button.onClick.AddListener(delegate { 
			ResetFractal(fractal, ColorR, ColorG, ColorB, Iterations, PosX, PosY, Zoom);
			});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void ResetFractal(GameObject _fractal, float _colorR, float _colorG, float _colorB, float _Iterations, float _PosX, float _PosY, float _Zoom)
	{
		Scene scene = SceneManager.GetActiveScene();
		string name = scene.name;
		//Debug.Log(name);
		switch (name)
		{
			case "MandelbrotScene":
			_fractal.GetComponent<MandelbrotController>().iterationSlider.value = _Iterations;
			_fractal.GetComponent<MandelbrotController>().ColorRSlider.value = _colorR;
			_fractal.GetComponent<MandelbrotController>().ColorGSlider.value = _colorG;
			_fractal.GetComponent<MandelbrotController>().ColorBSlider.value = _colorB;
			break;

			case "DragonScene":
			_fractal.GetComponent<DragonController>().iterationSlider.value = _Iterations;
			_fractal.GetComponent<DragonController>().ColorRSlider.value = _colorR;
			_fractal.GetComponent<DragonController>().ColorGSlider.value = _colorG;
			_fractal.GetComponent<DragonController>().ColorBSlider.value = _colorB;
			break;

			case "SierpinskiCarpetScene":
			_fractal.GetComponent<CustomSierpinskiController>().iterationSlider.value = _Iterations;
			_fractal.GetComponent<CustomSierpinskiController>().ColorRSlider.value = _colorR;
			_fractal.GetComponent<CustomSierpinskiController>().ColorGSlider.value = _colorG;
			_fractal.GetComponent<CustomSierpinskiController>().ColorBSlider.value = _colorB;
			break;

			case "SierpinskiTriangleScene":
			_fractal.GetComponent<SierpinskiTriangleController>().iterationSlider.value = _Iterations;
			_fractal.GetComponent<SierpinskiTriangleController>().ColorRSlider.value = _colorR;
			_fractal.GetComponent<SierpinskiTriangleController>().ColorGSlider.value = _colorG;
			_fractal.GetComponent<SierpinskiTriangleController>().ColorBSlider.value = _colorB;
			break;

			case "JuliaScene":
			_fractal.GetComponent<SierpinskiTriangleController>().iterationSlider.value = _Iterations;
			_fractal.GetComponent<SierpinskiTriangleController>().ColorRSlider.value = _colorR;
			_fractal.GetComponent<SierpinskiTriangleController>().ColorGSlider.value = _colorG;
			_fractal.GetComponent<SierpinskiTriangleController>().ColorBSlider.value = _colorB;
			material.SetFloat("_SeedX", 0);
        	material.SetFloat("_SeedY", 0);
			break;

			default:
			break;
		}
		//_fractal.
		//material.SetVector("_Hue", new Vector3(_colorR,_colorG,_colorB));
		material.SetFloat("_Zoom",_Zoom);
		//material.SetFloat("_Iterations",_Iterations);
		material.SetVector("_Pan", new Vector3(_PosX,_PosY,0));
	}
}
