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
		if (simulateSpeed != 0)
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
			bool flg;
			//ループチェック実行　ついでにループが終わるまで実行
			do {
				flg = loopCheck();
			} while (flg);
			flame = 0;
		}
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

	bool loopCheck() {
		//実行チェック
		bool wasLoop = false;

		//以下時間が増えた場合
		while (millisecond >= 1000) {
			millisecond -= 1000;
			second++;
			wasLoop = true;
		}

		while (second >= 60) {
			second -= 60;
			minute++;
			wasLoop = true;
		}

		while (minute >= 60) {
			minute -= 60;
			hour++;
			wasLoop = true;
		}

		while (hour >= 24) {
			hour -= 24;
			day++;
			wasLoop = true;
		}

		while (day >= getMonthend()) {
			day -= getMonthend();
			month++;
			wasLoop = true;
		}

		while (month > 12) {
			month -= 12;
			year++;
			wasLoop = true;
		}

		//以下時間が減った場合
		if (second < 0) {
			second += 60;
			minute--;
			wasLoop = true;
		}

		if (minute < 0) {
			minute += 60;
			hour--;
			wasLoop = true;
		}

		if (hour < 0) {
			hour += 24;
			day--;
			wasLoop = true;
		}

		if (month < 1) {
			month += 12;
			year--;
			wasLoop = true;
		}

		return wasLoop;
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

	public int Minutes {
		set { this.minute = value; }
		get { return this.minute; }
	}

	public int Seconds {
		set { this.second = value; }
		get { return this.second; }
	}

	public int MiliSeconds {
		set { this.millisecond = value; }
		get { return this.millisecond; }
	}

	public float SimulateSpeed {
		set { this.simulateSpeed = value; }
		get { return this.simulateSpeed; }
	}
}
