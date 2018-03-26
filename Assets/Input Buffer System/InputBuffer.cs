using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Move {
	//edit the script to add more enum for movement types
	public enum MoveTypes {Attack1, Attack2, Jump, Dash}
	public MoveTypes moveType;
	public float tmpTimer;

	//we can also adjust the timer for each move.
	public Move(MoveTypes _Move){
		this.moveType = _Move;
		switch(_Move){
			case MoveTypes.Attack1:
			tmpTimer = .2f;
			break;
			case MoveTypes.Attack2:
			tmpTimer = .3f;
			break;
			case MoveTypes.Jump:
			tmpTimer = .4f;
			break;
			case MoveTypes.Dash:
			tmpTimer = .6f;
			break;
		}
	}

}

public class InputBuffer : MonoBehaviour {
	
	public Queue<Move> _MoveQueue = new Queue<Move>();
	public Move[] _moveArray;
	public float tmpTimer;

	// Update is called once per frame
	void Update () {
		_moveArray=_MoveQueue.ToArray();
		if(_MoveQueue.Count>0){
			Move current = _MoveQueue.Peek();
			if(tmpTimer<=0){
				_MoveQueue.Dequeue();
				if(_MoveQueue.Count>0){
					tmpTimer = _MoveQueue.Peek().tmpTimer;
				}
			}else{
				tmpTimer-=Time.deltaTime;
			}
		}
	}
	
}
