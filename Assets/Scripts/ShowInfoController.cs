using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfoController : MonoBehaviour {

	public Button button;
	bool isShown;
	public RectTransform panel;
	public int showPositionY, hidePositionY;
	Vector3 showPosition, hidePosition, currentPosition;
	Vector3 firstPos;
	// Use this for initialization
	void Start () {
		firstPos = panel.transform.position - new Vector3(7.5f,0,0);
		showPosition = new Vector3(firstPos.x,firstPos.y - showPositionY,firstPos.z);
		hidePosition = new Vector3(firstPos.x,firstPos.y - hidePositionY,firstPos.z);
		currentPosition = new Vector3(firstPos.x,firstPos.y + showPositionY,firstPos.z);
		panel.transform.position = currentPosition;
		isShown = false;
		button.onClick.AddListener(
			delegate 
			{ 
				showInfo(ref isShown);
				});
	}
	
	// Update is called once per frame
	void Update () {
		if (isShown==true)
		{
			currentPosition = Vector3.Lerp(currentPosition,hidePosition,0.3f);
			panel.transform.position = currentPosition;
			// currentPanel = Vector3.Lerp(currentPanel,showPanel,0.05f);
			// arrowCurrent = Vector3.Lerp(arrowCurrent,arrowShow,0.05f);
			// panel.transform.position = new Vector3(currentPanel.x,0,0);
			// blurMaterial.SetFloat("_Radius",currentPanel.y);
			// transparentMaterial.SetFloat("_Transparency",currentPanel.z);
			// arrow.transform.rotation = Quaternion.Slerp(arrow.transform.rotation, 
			// 	Quaternion.Euler(arrowHide),0.1f);
			// btn.GetComponentInChildren<Text>().text = "UKRYJ PANEL";
		}
		else if (isShown==false)
		{
			currentPosition = Vector3.Lerp(currentPosition,showPosition,0.3f);
			panel.transform.position = currentPosition;
			// currentPanel = Vector3.Lerp(currentPanel,hidePanel,0.05f);
			// arrowCurrent = Vector3.Lerp(arrowCurrent,arrowHide,0.05f);
			// panel.transform.position = new Vector3(currentPanel.x,0,0);
			// blurMaterial.SetFloat("_Radius",currentPanel.y);
			// transparentMaterial.SetFloat("_Transparency",currentPanel.z);
			// arrow.transform.rotation = Quaternion.Slerp(arrow.transform.rotation, Quaternion.Euler(arrowShow),0.1f);
			// btn.GetComponentInChildren<Text>().text = "POKAŻ PANEL";
		}
	}
	void showInfo(ref bool PanelVisible)
	{
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
