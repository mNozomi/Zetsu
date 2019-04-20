using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent( typeof( CapsuleColliderEx ) ) ]

public class Player : MonoBehaviour {
	//--------------------------------------------------------------------
	// 初期化
	//--------------------------------------------------------------------
    private void Start() {
		Transform_ = GetComponent<Transform>();
		Animator_ = GetComponent<Animator>();
		BodyCollision = GetComponent<CapsuleColliderEx>();
		ActionStateMachine = new StateMachine<Player>( this );
		ActionStateMachine.SetCurrentState( new Neutral() );

    }


	//--------------------------------------------------------------------
	// 更新
	//--------------------------------------------------------------------
    private void Update() {
		Debug.Log( Input.GetAxisRaw( "StickL X" ) );
		ActionStateMachine.Update( this );
    }

	private void UpdateMoveSpeed() {
		float currentMaxMoveSpd = Utility.StickLXSlople() * mMaxMoveSpeed;
		// スティックが傾いているなら、加速か減速を行う
		if( Utility.IsStickLXSlope() ) {

			if( MoveSpeed <= mIniMoveSpeed ) {
				MoveSpeed = mIniMoveSpeed;
			}

			if( MoveSpeed > mMaxMoveSpeed ) {
				ToDeceleration();
				return;
			}


			// 現在出せる最高速度が今の速度より大きいなら、最高速度へ徐々に近づける
			if ( currentMaxMoveSpd >= MoveSpeed ) {
				ToAcceleration();
			}
			else {
				ToDeceleration();
			}

		}
		// スティックが傾いていないなら、徐々に0.0fに戻す
		else {
			if( MoveSpeed <= mIniMoveSpeed ) {
				MoveSpeed = 0.0f;
				return;
			}
			ToDeceleration();
		}
	}

	private void ToAcceleration() {
		MoveSpeed =  Mathf.Min( MoveSpeed + mMoveAcceleration * Time.deltaTime, mMaxMoveSpeed );
	}

	private void ToDeceleration() {
		MoveSpeed = Mathf.Max( mIniMoveSpeed, MoveSpeed - mMoveDeceleration * Time.deltaTime );
	}

	private void ResetMoveSpeed() {
		MoveSpeed = 0.0f;
	}

	//--------------------------------------------------------------------
	// 計算が必要なパラメータを返す
	//--------------------------------------------------------------------
	public Vector3 MoveDir() {
		return new Vector3( Input.GetAxisRaw( "StickL X" ), Input.GetAxisRaw( "StickL Y" ), 0.0f );
	}


	//--------------------------------------------------------------------
	// アクション遷移判定式
	//--------------------------------------------------------------------
	public bool CanNeutral() {
		if( Utility.StickLXSlople() > 0.0f ) return false;
		ActionStateMachine.ChangeState( new Neutral() );
		return true;
	}


	public bool CanRun() {
		if( Utility.StickLXSlople() < mRunLowerLim || mRunUpperLim < Utility.StickLXSlople() ) return false;
		ActionStateMachine.ChangeState( new Run() );
		return true;
	}



	//--------------------------------------------------------------------
	// メンバ変数
	//--------------------------------------------------------------------
	public Transform Transform_ { get; set; }
	public CapsuleColliderEx BodyCollision { get; set; }
	public RaycastHit GroundInfo { get; set; }
	public StateMachine<Player> ActionStateMachine { get; set; }
	public Animator Animator_ { get; set; }


	// 移動関連
	// 遷移のための閾値
	[SerializeField] private float mSlowWalkLowerLim;
	[SerializeField] private float mSlowWalkUpperLim;
	[SerializeField] private float mWalkLowerLim;
	[SerializeField] private float mWalkUpperLim;
	[SerializeField] private float mRunLowerLim;
	[SerializeField] private float mRunUpperLim;
	// 移動速度
	[SerializeField] private float mMaxMoveSpeed;
	[SerializeField] private float mIniMoveSpeed;
	[SerializeField] private float mMoveAcceleration;
	[SerializeField] private float mMoveDeceleration;
	public float MoveSpeed { get; set; }



}
