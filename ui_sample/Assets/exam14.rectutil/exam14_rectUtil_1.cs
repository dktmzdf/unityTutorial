﻿using UnityEngine;
using System.Collections;

using UnityEngine.UI;

using UniRx;
using UniRx.Triggers;

public class exam14_rectUtil_1 : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GameObject target_area = GameObject.Find ("target_area");
		GameObject test_4 = GameObject.Find ("test4");

		GameObject test_2 = GameObject.Find ("test2");
		//GameObject Canvas_1 = GameObject.Find ("Canvas");

		//mouse point -> transform.position
		//사각형 영역에다가 대고 마우스 우측버튼을 누루면 해당위치로 이동한다.
		this.UpdateAsObservable ()
			.Select (_ => Input.mousePosition)
			.Where(_=>Input.GetMouseButtonDown(1))
			.Subscribe((x)=> {

//사각형영에 충돌검사 
				if( RectTransformUtility.RectangleContainsScreenPoint(
					target_area.GetComponent<RectTransform>(),
					x) == true) {
					Vector3 world_pos;
					//사각형 영역내에서 마우스 포인터를 충돌좌표로 변환 
					RectTransformUtility.ScreenPointToWorldPointInRectangle(
						gameObject.GetComponent<RectTransform>(), //대상이 되는 사각형 좌표 
						//null,
						x,
						Camera.current,
						out world_pos //사각형 영역좌표
					);
					test_4.transform.position = world_pos;
				}
				
			});


		this.UpdateAsObservable ()
			.Select (_ => Input.mousePosition)
			.Where(_=>Input.GetMouseButtonDown(0))
			.Subscribe ((cur_mpos) => {

				Vector2 tempPt;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(
					test_2.GetComponent<RectTransform>(),
					cur_mpos,
					Camera.current,
					out tempPt
				);

				Debug.Log(tempPt);

				bool bTemp = RectTransformUtility.RectangleContainsScreenPoint(
					test_2.GetComponent<RectTransform>(),
					cur_mpos
				);
				Debug.Log(bTemp);

				if(bTemp) {
					test_2.transform.FindChild("Panel").GetComponent<Image>().color = Color.cyan;
				}
				else {
					test_2.transform.FindChild("Panel").GetComponent<Image>().color = Color.white;
				}


				/*
				Rect tempRt = RectTransformUtility.PixelAdjustRect(test_2.GetComponent<RectTransform>(),
					Canvas_1.GetComponent<Canvas>());

				Debug.Log(tempRt);
			


				Vector3 temp3dPt;
				RectTransformUtility.ScreenPointToWorldPointInRectangle(
					test_2.GetComponent<RectTransform>(),
					tempPt,
					Camera.current,
					out temp3dPt
				);

				Debug.Log(temp3dPt);
				*/


			});

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
