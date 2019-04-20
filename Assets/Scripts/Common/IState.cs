using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class IState<Type> {

	// State開始時に一度だけコールされる
	public abstract void Enter( Type owner );

	// Executeの前にステートの遷移チェックを行うために
	public abstract void TransitionCheck( Type owner );

	// ステート更新処理
	public abstract void Execute( Type owner );

	// State終了時に一度だけコールされる
	public abstract void Exit( Type ownerr );

}