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

	// スティックが傾いてるかどうかを返す
	public static bool IsStickLXSlope() {
		return StickLXSlople() > 0.0f;
	}

	public static bool IsStickLYSlope() {
		return StickLYSlople() > 0.0f;
	}

	// 2つのベクトルのなす角を度数法(Dgree)で返す( 0 ~ 180 )
	// 引数に零ベクトルが含まれる場合は0度を返す
	public static float VectorAngle( Vector3 vec1, Vector3 vec2 ) {
        if( vec1 == Vector3.zero || vec2 == Vector3.zero ) {
            return 0.0f;
        }
        return Mathf.Acos( Mathf.Clamp( Vector3.Dot( vec1.normalized, vec2.normalized ), -1.0f, 1.0f ) ) * Mathf.Rad2Deg;
    }

}
