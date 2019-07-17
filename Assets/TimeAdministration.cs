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
    private int minutes;
    [SerializeField, Range(0, 59), HeaderAttribute("秒")]
    private int seconds;
    [SerializeField, HeaderAttribute("初期値をリアルタイムにする")]
    private bool useRealTime;
    [SerializeField, HeaderAttribute("再生速度。0:静止、1:リアルタイム 2～:x倍速 -:逆再生")]
    private float simulateSpeed;
    private int flame = 0;
    private readonly int fps = 50;

    //デバッグ用
    private float real;
    private float wasReal;
    //デバッグ用終了

    private readonly int[] month30 = new int[5] { 2, 4, 6, 9, 11 };

    void Start() {
        //デバッグ用
        real = 0;
        wasReal = 0;
        //デバッグ用終了
        if (useRealTime) {
            DateTime now = DateTime.Now;
            year = now.Year;
            month = now.Month;
            day = now.Day;
            hour = now.Hour;
            minutes = now.Minute;
            seconds = now.Second;
        }
    }

    //0.02秒ごとに呼ばれる
    private void FixedUpdate() {
        if (simulateSpeed != 0)
            flame++;
        else {
            flame = 0;
            //デバッグ用
            real = Time.realtimeSinceStartup;
            wasReal = Time.realtimeSinceStartup;
            //デバッグ用終了
        }

        if (Mathf.Abs(flame * simulateSpeed) >= fps) {
            seconds += (int)( ( flame * simulateSpeed ) / (float) fps );
            //デバッグ用
            real = Time.realtimeSinceStartup;
            Debug.Log((int)( ( flame * simulateSpeed ) / (float)fps ) + "　経過時間　" + ( real - wasReal ));
            wasReal = real;
            //デバッグ用終了
            bool flg;
            //ループチェック実行　ついでにループが終わるまで実行
            do {
                flg = loopCheck();
            } while (flg);
            flame = 0;
        }
    }

    void dayCheck() {
        for (int i = 0; i < month30.Length; i++) {
            if (month == month30[i]) {
                if (month == 2) {
                    if (day > 28)
                        day = 28;
                } else {
                    month = 30;
                }
            }
        }
    }

    bool loopCheck() {
        //実行チェック
        bool wasLoop = false;

        //以下時間が増えた場合
        if (seconds >= 60) {
            seconds -= 60;
            minutes++;
            wasLoop = true;
        }

        if (minutes >= 60) {
            minutes -= 60;
            hour++;
            wasLoop = true;
        }

        if (hour >= 24) {
            hour -= 24;
            day++;
            wasLoop = true;
        }

        for (int i = 0; i < month30.Length; i++) {
            //30日の月である
            if (month == month30[i]) {
                if (month == 2 && day > 28) {
                    //2月かつ29日以上
                    day -= 28;
                    month++;
                    wasLoop = true;
                } else if (/* ( year % 4 == 0 && month == 2 && day > 29 )*/false) {
                    //閏年の2月30日以上
                    day -= 29;
                    month++;
                    wasLoop = true;
                } else if (day > 30) {
                    //30の月かつ31日以上
                    day -= 30;
                    month++;
                    wasLoop = true;
                }
            } else if (i >= month30.Length - 1) {
                //最後の一周まで処理されない
                if (day > 31) {
                    day -= 31;
                    month++;
                    wasLoop = true;
                }
            }
        }

        if (month > 12) {
            month -= 12;
            year++;
            wasLoop = true;
        }

        //以下時間が減った場合
        if (seconds < 0) {
            seconds += 60;
            minutes--;
            wasLoop = true;
        }

        if (minutes < 0) {
            minutes += 60;
            hour--;
            wasLoop = true;
        }

        if (hour < 0) {
            hour += 24;
            day--;
            wasLoop = true;
        }

        for (int i = 0; i < month30.Length; i++) {
            //30日の月の次月である
            if (month == month30[i] + 1) {
                if (month == 2 + 1 && day < 1) {
                    //2月
                    day += 28;
                    month--;
                    wasLoop = true;
                } else if (/* ( year % 4 == 0 && month == 2 + 1 && day < 1 )*/false) {
                    //閏年の2月
                    day += 29;
                    month--;
                    wasLoop = true;
                } else if (day < 1) {
                    //30の月次月
                    day += 30;
                    month--;
                    wasLoop = true;
                }
            } else if (i >= month30.Length - 1) {
                //最後の一周まで処理されない
                if (day < 1) {
                    day += 31;
                    month--;
                    wasLoop = true;
                }
            }
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
        set { this.minutes = value; }
        get { return this.minutes; }
    }

    public int Seconds {
        set { this.seconds = value; }
        get { return this.seconds; }
    }

    public float SimulateSpeed {
        set { this.simulateSpeed = value; }
        get { return this.simulateSpeed; }
    }
}
