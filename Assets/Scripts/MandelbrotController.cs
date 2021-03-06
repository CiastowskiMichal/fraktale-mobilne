﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MandelbrotController : MonoBehaviour {

	Rect inputArea;
    public Text text1;
    public GameObject inputWidth;
    public Slider iterationSlider;
    public Text iterationLabel;
    public Material materialTarget;
    Touch touch1, touch2;
    public Slider ColorRSlider, ColorGSlider, ColorBSlider;
    public Text ColorRText,ColorGText, ColorBText;
	// Use this for initialization
	void Start () {
		inputArea = new Rect(0, 0, Screen.width * 0.9f, Screen.height);
        text1.text = inputArea.xMax + "  " + inputArea.yMax;

        Input.simulateMouseWithTouches = true;
        iterationSlider.value = 100;
        materialTarget.SetFloat("_Iterations", iterationSlider.value);
        iterationLabel.text = "Iteracje: " + iterationSlider.value;

        ColorRSlider.value = 0.0f;
        ColorGSlider.value = 0.6f;
        ColorBSlider.value = 1.0f;
        ColorRText.text = ColorRSlider.value.ToString("F1");
        ColorGText.text = ColorGSlider.value.ToString("F1");
        ColorBText.text = ColorBSlider.value.ToString("F1");
        materialTarget.SetVector("_Hue",new Vector3(ColorRSlider.value,ColorGSlider.value,ColorBSlider.value));
        materialTarget.SetFloat("_Zoom", 30000);
        materialTarget.SetVector("_Pan", new Vector3(0.5f,-0.3f,0));
	}
	
	// Update is called once per frame
	void Update () {
		/* Obsługa interfejsu uzytkownika */


        materialTarget.SetFloat("_Iterations", iterationSlider.value);
        iterationLabel.text = "Iteracje: " + iterationSlider.value;

        ColorRText.text = ColorRSlider.value.ToString("F1");
        ColorGText.text = ColorGSlider.value.ToString("F1");
        ColorBText.text = ColorBSlider.value.ToString("F1");
        materialTarget.SetVector("_Hue",new Vector3(ColorRSlider.value,ColorGSlider.value,ColorBSlider.value));

        /* Koniec obsługi interfejsu uzytkownika */


        /* Działanie obszaru ekranu dotykowego */

        text1.text = inputArea.xMax + "  " + inputArea.yMax;
        if (inputWidth.GetComponent<SidePanelController>().isShown == false)
        {
            inputArea = new Rect(0, 0, Screen.width * 0.9f, Screen.height);
            //text1.text = inputArea.xMax + "  "+inputArea.yMax;
        }
        else if (inputWidth.GetComponent<SidePanelController>().isShown == true)
        {
            inputArea = new Rect(0, 0, Screen.width * 0.5f, Screen.height);
            //text1.text = inputArea.xMax + "  "+inputArea.yMax;
        }

        /* Koniec obszaru ekranu dotykowego */

        /* Obsługa ekranu dotykowego */

        if (Input.touchCount > 0)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            if (inputArea.Contains(touchPos))
            {
                if (Input.touchCount == 1)
                {
                    Touch touchZero = Input.GetTouch(0);
                    if (touchZero.phase == TouchPhase.Moved)
                    {
                        Vector2 touchDelta = touchZero.deltaPosition;
                        Vector3 vect = materialTarget.GetVector("_Pan");
                        float zoomFloat = materialTarget.GetFloat("_Zoom");
                        Vector3 newVect = new Vector3(vect.x + touchDelta.x * 0.00000001f * zoomFloat, 
                        vect.y + touchDelta.y * 0.00000001f * zoomFloat, 
                        vect.z);
                        materialTarget.SetVector("_Pan", newVect);
                    }
                }
                if (Input.touchCount == 2)
                {
                    // Store both touches.
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    // Find the position in the previous frame of each touch.
                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    // Find the magnitude of the vector (the distance) between the touches in each frame.
                    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                    // Find the difference in the distances between each frame.
                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                    //text1.text = deltaMagnitudeDiff.ToString();
                    //Vector3 vect = materialTarget.GetVector("_Zoom");
                    //Vector3 newVect = new Vector3(vect.x + deltaMagnitudeDiff * 1f, vect.y + deltaMagnitudeDiff * 1f, vect.z);
                    //materialTarget.SetVector("_iResolution", newVect);
                    float zoomFloat = materialTarget.GetFloat("_Zoom");
					if(zoomFloat<=0)
					{
						if(deltaMagnitudeDiff<0)
						{
							deltaMagnitudeDiff=0;
						}
					}
                    float newZoomFloat = zoomFloat + deltaMagnitudeDiff*0.0003f*zoomFloat;
                        materialTarget.SetFloat("_Zoom",newZoomFloat);
                    // if(zoomFloat>30)
                    // {
                    //     float newZoomFloat = zoomFloat + deltaMagnitudeDiff*0.0001f*zoomFloat;
                    //     materialTarget.SetFloat("_Zoom",newZoomFloat);
                    // }
                    // else if(zoomFloat<=30 && zoomFloat>10)
                    // {
                    //     float newZoomFloat = zoomFloat + deltaMagnitudeDiff*0.000005f*zoomFloat;
                    //     materialTarget.SetFloat("_Zoom",newZoomFloat);
                    // }
					// else if(zoomFloat<=10)
                    // {
                    //     float newZoomFloat = zoomFloat + deltaMagnitudeDiff*0.000001f*zoomFloat;
                    //     materialTarget.SetFloat("_Zoom",newZoomFloat);
                    // }
                    
                    

                    //position = new Vector3(-pos.x, pos.y, 0.0f);

                    // Position the cube.
                    //transform.position = position;
                }

            }
        }

        /* Koniec obsługi ekranu dotykowego */
	}
}
