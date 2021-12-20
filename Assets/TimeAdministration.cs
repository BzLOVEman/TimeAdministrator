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

	void Update() {
		MilliSecond += (int)( Time.deltaTime * 1000 * simulateSpeed );
		timeCarryUpDown();

	}

	//月末の日付を返す
	int getMonthend() {
		for (int i = 0; i < monthEnd.Length; i++) {
			if (month == i + 1) {
				//閏年対応
				if (i + 1 == 2 && year % 4 == 0) {
					return monthEnd[i] + 1;
				} else {
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
		if (simulateSpeed > 0) {
			for (int i = millisecond / 1000; i > 0; i--) {
				millisecond -= 1000;
				second++;
			}

			for (int i = second / 60; i > 0; i--) {
				second -= 60;
				minute++;
			}

			for (int i = minute / 60; i > 0; i--) {
				minute -= 60;
				hour++;
			}

			for (int i = hour / 24; i > 0; i--) {
				hour -= 24;
				day++;
			}

			for (int i = ( day - 1 ) / getMonthend(); i > 0; i--) {
				day -= getMonthend();
				month++;
			}

			for (int i = ( month - 1 ) / 12; i > 0; i--) {
				month -= 12;
				year++;
			}
		}

		//以下時間が減った場合
		if (simulateSpeed < 0) {
			for (int i = millisecond / 1000; i < 0; i++) {
				millisecond += 1000;
				second--;
			}

			for (int i = second / 60; i < 0; i++) {
				second += 60;
				minute--;
			}

			for (int i = minute / 60; i < 0; i++) {
				minute += 60;
				hour--;
			}

			for (int i = hour / 24; i < 0; i++) {
				hour += 24;
				day--;
			}

			for (int i = day / getMonthend(); i <= 0; i++) {
				month--;
				day += getMonthend();
			}

			for (int i = month / 12; i <= 0; i++) {
				month += 12;
				year--;
			}
		}
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
