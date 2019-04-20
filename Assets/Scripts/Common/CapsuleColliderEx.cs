using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleColliderEx : MonoBehaviour {

	void Awake() {

		Collider = gameObject.AddComponent<CapsuleCollider>();
		Collider.center = mCenter;
		Collider.radius = mRadius;
		Collider.height = mHeight;
		Collider.direction = mDirection;
		Collider.isTrigger = true;


	}


	void LateUpdate () {
		SeparationCollision();
	}


	// コライダがめり込んでいる場合、分離できる最短距離で分離する
	void SeparationCollision() {
		Vector3 prevPos = Collider.transform.position;

		// カプセルに当たるコライダーを全て取得

		Vector3 capCenterPos = Collider.transform.position + Collider.center;

		// ( 0, 1, 2 ) => ( X, Y, Z )
		Vector3 capDir = mDirection == 0 ? new Vector3( 1.0f, 0.0f, 0.0f ) : mDirection == 1 ?
			new Vector3( 0.0f, 1.0f, 0.0f ) : new Vector3( 0.0f, 0.0f, 1.0f );

		Vector3 capTopPos = capCenterPos + ( capDir * Collider.height * 0.5f );

		Vector3 capBottomPos = capCenterPos - ( capDir * Collider.height * 0.5f );

		Collider[] colliderList = Physics.OverlapCapsule( capTopPos, capBottomPos, Collider.radius );

		Vector3 allPushBackVector = Vector3.zero;
		if( colliderList.Length > 0 ) {
			for( int i = 0; i < colliderList.Length; ++i ) {
				Collider targetCollider = colliderList[ i ];
				if( targetCollider == Collider  ) continue;

				// めり込み調整の為のパラメータを取得
				Vector3 pushBackVector;
                float pushBackDistance;
                bool isCollided = Physics.ComputePenetration ( Collider, Collider.transform.position,
																Collider.transform.rotation, targetCollider,
																targetCollider.transform.position,
																targetCollider.transform.rotation,
																out pushBackVector,
																out pushBackDistance
																);

				// めり込んでいた場合はめり込まないように位置を調整
				if( isCollided ) {
					Collider.transform.position += pushBackVector * pushBackDistance;
					allPushBackVector += pushBackVector;
				}
			}

			// めり込み解消があった場合、完全に分離するのではなく若干めり込ませておくようにする
			// これは物体に触れているときにTriggerStayが反応し続けるようにするため
			if( allPushBackVector != Vector3.zero ) {
				Collider.transform.position += -allPushBackVector.normalized * 0.001f;
			}

		}
	}

	public CapsuleCollider Collider { get; private set; }


	[SerializeField] private Vector3 mCenter;
	public Vector3 Center {
		get { return mCenter; }
		set { Collider.center = value; }
	}

	[SerializeField] private float mRadius;
	public float Radius {
		get { return mRadius; }
	}


	[SerializeField] private float mHeight;
	public float Height {
		get { return mHeight; }
	}

	// ( 0, 1, 2 ) => ( X, Y, Z )
	[SerializeField] private int mDirection;


}
