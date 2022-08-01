using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Mathf;
//Zeller's congruence
//ツェラーの公式
//https://ja.wikipedia.org/wiki/%E3%83%84%E3%82%A7%E3%83%A9%E3%83%BC%E3%81%AE%E5%85%AC%E5%BC%8F#:~:text=%E3%83%84%E3%82%A7%E3%83%A9%E3%83%BC%E3%81%AE%E5%85%AC%E5%BC%8F%EF%BC%88%E3%83%84%E3%82%A7%E3%83%A9%E3%83%BC%E3%81%AE,Christian%20Zeller)%20%E3%81%8C%E8%80%83%E6%A1%88%E3%81%97%E3%81%9F%E3%80%82

//曜日を計算する
public class ZellerCongruence : MonoBehaviour {
	public enum week{
		saturday,
		sunday,
		monday,
		tuesday,
		wednesday,
		thursday,
		friday,
	}

	public string zellerCongruence(int y, int m, int d){
		if(y <= 4 && (m < 3)){
			Debug.LogWarning("西暦4年3月1日以前は閏年の計算が違うため、曜日に誤りが出ます。");
		}
		int C = Gaus(y / 100f);
		int Y = y % 100;
		int ganma = 
			1582 <= y ?
			-2 * C + Gaus(C / 4) :
			-1 * C + 5 ;
		int h = (d + Gaus((26 * (m + 1)) / 10) + Y + Gaus(Y / 4) + ganma) % 7;

		return ((week)h).ToString();
	}

	public int Gaus(float g){
		return (int)(Mathf.Floor(g));
	}
}
