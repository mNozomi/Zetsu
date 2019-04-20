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
		if( Utility.StickLXSlople() < 0.9 || 1.0f < Utility.StickLXSlople() ) return false;
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
	[SerializeField] private float mSlowWalkLowerLim;
	[SerializeField] private float mSlowWalkUpperLim;
	[SerializeField] private float mWalkLowerLim;
	[SerializeField] private float mWalkUpperLim;
	[SerializeField] private float mRunLowerLim;
	[SerializeField] private float mRunUpperLim;
	public float MoveSpeed { get; set; }



}
