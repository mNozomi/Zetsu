using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//--------------------------------------------------------------------------
// Neutral
//--------------------------------------------------------------------------
public class Neutral : IState<Player> {
	public override void Enter( Player player ) {
		Debug.Log( "ENter" );
		player.Animator_.SetBool( "PlayNeutral", true );
	}


	public override void TransitionCheck( Player player ){
		if( player.CanRun() ) return;
	}


	public override void Execute( Player player ){
		Debug.Log( "Exe" );
	}


	public override void Exit( Player player ){
		player.Animator_.SetBool( "PlayNeutral", false );
	}

}


//--------------------------------------------------------------------------
// Run
//--------------------------------------------------------------------------
public class Run : IState<Player> {
	public override void Enter( Player player ) {
		player.Animator_.SetBool( "PlayRun", true );
	}


	public override void TransitionCheck( Player player ){
		if( player.CanNeutral() ) return;
	}


	public override void Execute( Player player ){}


	public override void Exit( Player player ){
		player.Animator_.SetBool( "PlayRun", false );
	}

}