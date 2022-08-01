using UnityEngine;
using UnityEngine.UI;

public class ApplyTime : MonoBehaviour {

	public Text textObj;
	public TimeAdministration ta;
	public ZellerCongruence zellerCongruence;

	private void LateUpdate() {
		if(zellerCongruence == null){
			textObj.text = ta.Year + "/" + ta.Month + "/" + ta.Day + "\n"
						+ ta.Hour + ":" + ta.Minute + ":" + ta.Second + "." + ta.MilliSecond;
		}else{
			textObj.text = ta.Year + "/" + ta.Month + "/" + ta.Day + "(" + zellerCongruence.zellerCongruence(ta.Year, ta.Month, ta.Day) + ")" + "\n"
						+ ta.Hour + ":" + ta.Minute + ":" + ta.Second + "." + ta.MilliSecond;
		}
	}
}
