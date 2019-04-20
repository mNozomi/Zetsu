using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Utility {
	// 左スティックの傾きを返す、極端に小さいのは無視
	public static float StickLXSlople() {
		float slope = Mathf.Abs( Input.GetAxisRaw( "StickL X" ) );
		return slope >= 0.2 ? slope : 0.0f;
	}

	public static float StickLYSlople() {
		float slope = Mathf.Abs( Input.GetAxisRaw( "StickL Y" ) );
		return slope >= 0.2 ? slope : 0.0f;
	}

}
