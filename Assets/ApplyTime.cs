using UnityEngine;
using UnityEngine.UI;

public class ApplyTime : MonoBehaviour {

	public Text textObj;
	public TimeAdministration ta;
	public ZellerCcongruence zellerCcongruence;

	private void LateUpdate() {
		if(zellerCcongruence == null){
			textObj.text = ta.Year + "/" + ta.Month + "/" + ta.Day + "\n"
						+ ta.Hour + ":" + ta.Minute + ":" + ta.Second + "." + ta.MilliSecond;
		}else{
			textObj.text = ta.Year + "/" + ta.Month + "/" + ta.Day + "(" + zellerCcongruence.zellerCcongruence(ta.Year, ta.Month, ta.Day) + ")" + "\n"
						+ ta.Hour + ":" + ta.Minute + ":" + ta.Second + "." + ta.MilliSecond;
		}
	}
}
