// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
// using UnityEditor.UI;
using UnityEngine.UI;

public class ApplyTime : MonoBehaviour {

	public Text textObj;
	public TimeAdministration ta;

	void Start() {

	}

	private void LateUpdate() {
		textObj.text = ta.Year + "/" + ta.Month + "/" + ta.Day + "\n"
					+ ta.Hour + ":" + ta.Minute + ":" + ta.Second + "." + ta.MilliSecond;
	}
}
