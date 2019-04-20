using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class StateMachine<Type>  {

	public StateMachine( Type owner ) {
		mOwner = owner;
		mCurrentState = null;
		mPrevState = null;
	}


    public void SetCurrentState( IState<Type> newState ) {
		newState.Enter( mOwner );
		mCurrentState = newState;
	}

    public void ChangeState( IState<Type> newState ) {
		mCurrentState.Exit( mOwner );
		mPrevState = mCurrentState;
		newState.Enter( mOwner );
		newState.Execute( mOwner );
		mCurrentState = newState;
    }

	public void RestorePrevState() {
		mCurrentState = mPrevState;
	}


	public void Update( Player player ) {
		Assert.IsNotNull( mCurrentState, "StateMachine > Update() : CurrentStaeがnullです" );
		mCurrentState.TransitionCheck( mOwner );
		mCurrentState.Execute( mOwner );

    }

	public System.Type GetCurrentStateType() {
		return mCurrentState.GetType();
	}

	public System.Type GetPrevStateType() {
		if( mPrevState == null ) {
			return null;
		}

		return mPrevState.GetType();
	}

	private Type mOwner;

	private IState<Type> mCurrentState;

	private IState<Type> mPrevState;

}
