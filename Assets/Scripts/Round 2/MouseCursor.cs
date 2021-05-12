using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
	public PaddleController paddle;
	public RectTransform rectTransform;


	void Start()
	{
		Cursor.visible = false;
	}

	void Update()
	{
		if (Cursor.visible) Cursor.visible = false;
		rectTransform.position = paddle.mouseWorldPos;
	}
}
