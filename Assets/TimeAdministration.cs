using System;
using UnityEngine;
//時間を管理
public class TimeAdministration : MonoBehaviour {
	[SerializeField, HeaderAttribute("年")]
	private int year;
	[SerializeField, Range(1, 12), HeaderAttribute("月")]
	private int month;
	[SerializeField, Range(1, 31), HeaderAttribute("日")]
	private int day;
	[SerializeField, Range(0, 23), HeaderAttribute("時")]
	private int hour;
	[SerializeField, Range(0, 59), HeaderAttribute("分")]
	private int minute;
	[SerializeField, Range(0, 59), HeaderAttribute("秒")]
	private int second;
	[SerializeField, Range(0, 1000), HeaderAttribute("ミリ秒")]
	private int millisecond;
	[SerializeField, HeaderAttribute("初期値をリアルタイムにする")]
	private bool useRealTime;
	[SerializeField, HeaderAttribute("再生速度。0:静止、1:リアルタイム 2～:x倍速 -:逆再生")]
	private float simulateSpeed;
	private int flame = 0;
	private readonly int fps = 50;

	//デバッグ用
	// private float real;
	// private float wasReal;
	//デバッグ用終了

	private readonly int[] monthEnd = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

	void Start() {
		//デバッグ用
		// real = 0;
		// wasReal = 0;
		//デバッグ用終了
		if (useRealTime) {
			DateTime now = DateTime.Now;
			year = now.Year;
			month = now.Month;
			day = now.Day;
			hour = now.Hour;
			minute = now.Minute;
			second = now.Second;
			millisecond = now.Millisecond;
		} else {
			//何もしなくてもインスペクターから初期化されている
		}
	}

	//0.02秒ごとに呼ばれる
	private void FixedUpdate() {
		Debug.Log("run31a");

	}

	private void LateUpdate() {

		Debug.Log("run32a");
	}

	void Update() {
		/*		if (simulateSpeed != 0)
					flame++;
				else {
					flame = 0;
					//デバッグ用
					// real = Time.realtimeSinceStartup;
					// wasReal = Time.realtimeSinceStartup;
					//デバッグ用終了
				}

				if (Mathf.Abs(flame * simulateSpeed) >= fps) {
					second += (int)( ( flame * simulateSpeed ) / (float)fps );
					//デバッグ用
					// real = Time.realtimeSinceStartup;
					// Debug.Log((int)((flame * simulateSpeed) / (float)fps) + "　経過時間　" + (real - wasReal));
					// wasReal = real;
					//デバッグ用終了
					//時間の繰り上がり、繰り下がりの計算
					timeCarryUpDown();
					flame = 0;
				}
		*/
		Debug.Log("run25b");
		MilliSecond += (int)( Time.deltaTime * 1000 * simulateSpeed );
		Debug.Log("run25c");
		timeCarryUpDown();
		Debug.Log("run25a");

	}

	//月末の日付を返す
	int getMonthend() {
		for (int i = 0; i < monthEnd.Length; i++) {
			if (month == i + 1) {
				//閏年対応
				if (i + 1 == 2 && year % 4 == 0) {
					return monthEnd[i] + 1;
				} else {
					Debug.Log("run9b " + i);
					return monthEnd[i];
				}
			}
		}
		//エラー
		return -1;
	}

	//時間の繰り上がり、繰り下がりの計算を実施
	void timeCarryUpDown() {
		//以下時間が増えた場合
		Debug.Log("run16a");
		while (millisecond >= 1000) {
			Debug.Log("run16");
			millisecond -= 1000;
			second++;
		}

		while (second >= 60) {
			Debug.Log("run17");
			second -= 60;
			minute++;
		}

		while (minute >= 60) {
			Debug.Log("run18");
			minute -= 60;
			hour++;
		}

		while (hour >= 24) {
			Debug.Log("run19");
			hour -= 24;
			day++;
		}

		Debug.Log("run10a");
		while (day > getMonthend()) {
			Debug.Log("run10");
			day -= getMonthend();
			Debug.Log("run11");
			month++;
			Debug.Log("run12");
		}

		while (month > 12) {
			Debug.Log("run1");
			month -= 12;
			Debug.Log("run2");
			year++;
			Debug.Log("run3");
		}

		//以下時間が減った場合
		while (millisecond < 0) {
			Debug.Log("run20");
			millisecond += 1000;
			second--;
		}

		while (second < 0) {
			Debug.Log("run21");
			second += 60;
			minute--;
		}

		while (minute < 0) {
			Debug.Log("run22");
			minute += 60;
			hour--;
		}

		while (hour < 0) {
			Debug.Log("run23");
			hour += 24;
			day--;
		}

		Debug.Log("run13a");
		while (day <= 0) {
			Debug.Log("run13");
			month--;
			Debug.Log("run14");
			day += getMonthend();
			Debug.Log("run15");
		}

		Debug.Log("run4a");
		while (month <= 0) {
			Debug.Log("run4");
			month += 12;
			Debug.Log("run5");
			year--;
			Debug.Log("run6");
		}
		Debug.Log("run6a");
	}

	//以下ゲッターとセッター
	public int Year {
		set { this.year = value; }
		get { return this.year; }
	}

	public int Month {
		set { this.month = value; }
		get { return this.month; }
	}

	public int Day {
		set { this.day = value; }
		get { return this.day; }
	}

	public int Hour {
		set { this.hour = value; }
		get { return this.hour; }
	}

	public int Minute {
		set { this.minute = value; }
		get { return this.minute; }
	}

	public int Second {
		set { this.second = value; }
		get { return this.second; }
	}

	public int MilliSecond {
		set { this.millisecond = value; }
		get { return this.millisecond; }
	}

	public float SimulateSpeed {
		set { this.simulateSpeed = value; }
		get { return this.simulateSpeed; }
	}
}
