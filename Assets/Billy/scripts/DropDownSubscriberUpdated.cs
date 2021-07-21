using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Valve.VR;
using RosSharp.RosBridgeClient;

public class DropDownSubscriberUpdated : MonoBehaviour
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

	public GameObject RosItemListSubscriber;

	public List<string> previousItemList;
	// Use this for initialization
	void Start()
	{

		dropdown.options.Clear();
		string textArray = "Item1-Item2-Item3";
		string[] splitArray = textArray.Split(char.Parse("-"));
		List<string> items = new List<string>();

		for (int index = 0; index < splitArray.Length; index++)
		{
			items.Add(splitArray[index]);
		}
		previousItemList = items;
		// Fill dropdown with items
		/*
		foreach (var item in items)
		{
			dropdown.options.Add(new Dropdown.OptionData() { text = item });
		}
		*/
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

	bool CheckMatch(List<string> l1, List<string> l2)
	{
		if (l1.Count != l2.Count)
			return false;
		for (int i = 0; i < l1.Count; i++)
		{
			if (l1[i] != l2[i])
				return false;
		}
		return true;
	}

	void Update()
	{
		StringItemListSubscriber itemListSubscriber = RosItemListSubscriber.GetComponent<StringItemListSubscriber>();
		padTouchState = TouchTrackPadActionBoolean.GetState(handType);
		
		if(!CheckMatch(previousItemList, itemListSubscriber.items))
        {
			dropdown.options.Clear();
			foreach (var item in itemListSubscriber.items)
			{
				dropdown.options.Add(new Dropdown.OptionData() { text = item });
			}
		}
		
		//if button pressed
		if (padTouchState != padTouchStatePrev && padTouchState == true)
		{
			// Take the values
			scroll_item = TrackPadActionVector2.GetAxis(handType).y * 10;
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
				listIndex = dropdown.options.Count - 1;
			}

			dropdown.value = listIndex;

		}
		padTouchStatePrev = padTouchState;
		previousItemList = itemListSubscriber.items;
	}
}