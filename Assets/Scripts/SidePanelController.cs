using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SidePanelController : MonoBehaviour {
	public GameObject panel;
	public Vector3 hidePanel, showPanel;
	Vector3 currentPanel;
	public Button btn;
	public bool isShown;
	public Material blurMaterial, transparentMaterial;
	public GameObject arrow;
	public Vector3 arrowHide, arrowShow;
	Vector3 arrowCurrent;
	
	// Use this for initialization
	void Start () {
		//hidePanel = new Vector3(15,0,0);
		//showPanel = new Vector3(9,15,0.3f);
		arrowCurrent = arrowHide;

		btn.onClick.AddListener(delegate {ChangePanel(ref isShown);});

		panel.transform.position = new Vector3(15.5f,0,0);
		//arrow.transform.rotation *= Quaternion.Euler(0,0,0);
		Debug.Log("Start");
		isShown = false;
		currentPanel = hidePanel;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isShown==true)
		{
			
			currentPanel = Vector3.Lerp(currentPanel,showPanel,0.05f);
			arrowCurrent = Vector3.Lerp(arrowCurrent,arrowShow,0.05f);
			panel.transform.position = new Vector3(currentPanel.x,0,0);
			blurMaterial.SetFloat("_Radius",currentPanel.y);
			transparentMaterial.SetFloat("_Transparency",currentPanel.z);
			arrow.transform.rotation = Quaternion.Slerp(arrow.transform.rotation, 
				Quaternion.Euler(arrowHide),0.1f);
			btn.GetComponentInChildren<Text>().text = "UKRYJ PANEL";
		}
		else if (isShown==false)
		{
			
			currentPanel = Vector3.Lerp(currentPanel,hidePanel,0.05f);
			arrowCurrent = Vector3.Lerp(arrowCurrent,arrowHide,0.05f);
			panel.transform.position = new Vector3(currentPanel.x,0,0);
			blurMaterial.SetFloat("_Radius",currentPanel.y);
			transparentMaterial.SetFloat("_Transparency",currentPanel.z);
			arrow.transform.rotation = Quaternion.Slerp(arrow.transform.rotation, Quaternion.Euler(arrowShow),0.1f);
			btn.GetComponentInChildren<Text>().text = "POKAŻ PANEL";
		}
	}
	 void ChangePanel(ref bool PanelVisible)
	{
		Debug.Log(PanelVisible);
		if (PanelVisible == true)
		{
			PanelVisible = false;
			return;
		}
		else if (PanelVisible == false)
		{
			PanelVisible = true;
			return;
		}	
	}
}
