using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Valve.VR;
public class DynamicDropDown : MonoBehaviour
{
	public Text TextBox;
	public SteamVR_Input_Sources handType;
	public SteamVR_Action_Boolean grabAction;

	public SteamVR_Action_Boolean TouchTrackPadActionBoolean;
	public SteamVR_Action_Vector2 TrackPadActionVector2;

	private float scroll_item = 0.0f;

	public Dropdown dropdown;
	public int listIndex;

	public bool padTouchState;
	public bool padTouchStatePrev;

	public int counter;
	public bool grabState;
	public bool grabStatePrev;
	// Use this for initialization
	void Start()
	{
		
		dropdown.options.Clear();
		List<string> items = new List<string>();
		items.Add("Item 1");
		items.Add("Item 2");
		items.Add("Item 3");

		// Fill dropdown with items
		foreach(var item in items)
        {
			dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }
		DropdownItemSelected(dropdown);
		dropdown.onValueChanged.AddListener(
			delegate {
				DropdownItemSelected(dropdown); 
			}
		);
	}
	public bool GetGrab() // 2
	{
		return grabAction.GetState(handType);
	}

	void DropdownItemSelected(Dropdown dropdown)
    {
			int index = dropdown.value;
			TextBox.text = dropdown.options[index].text;

    }
	void Update()
    {
		padTouchState = TouchTrackPadActionBoolean.GetState(handType);
		//if button pressed
		if (padTouchState != padTouchStatePrev && padTouchState == true)
		{
			// Take the values
			scroll_item = TrackPadActionVector2.GetAxis(handType).y *10;
			print(scroll_item);
			// Limite the speeds
			scroll_item = (-Mathf.Clamp(scroll_item, -1.5f, 1.5f));
			
			print(scroll_item);
			listIndex = listIndex + (int)scroll_item;
			if (listIndex >= dropdown.options.Count)
			{
				print("Reset counter");
				listIndex = 0;
			}
			if (listIndex < 0)
			{
				print("Reset counter");
				listIndex = dropdown.options.Count;
			}

			dropdown.value = listIndex;
			
		}
		padTouchStatePrev = padTouchState;
		//print(padTouchState);

		grabState = GetGrab();
		if (grabState != grabStatePrev && grabState == true)
		{
			/*
			counter = counter + (int)scroll_item;
            if (counter >= dropdown.options.Count)
            {
				print("Reset counter");
				counter = 0;
            }
			dropdown.value = counter;
			*/
			counter++;
			dropdown.options.Add(new Dropdown.OptionData() { text = "Choice "+ counter.ToString() });
		}
		grabStatePrev = grabState;

	}
}