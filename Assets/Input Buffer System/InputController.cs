using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour {

	public InputBuffer inputBuffer;
	public Text CurrentInput, NextInput;
	public int MaxBuffer = 2;
	
	// Update is called once per frame
	void Update () {

	//if there's no queue yet, but we're going to play the timer based on our input to wait for a new queue
	if(inputBuffer._MoveQueue.Count<=0){
		if(Input.GetButtonDown("Attack1")){
			Attack1();

			Move move = new Move(Move.MoveTypes.Attack1);
			inputBuffer.tmpTimer = move.tmpTimer;
		}
		if(Input.GetButtonDown("Attack2")){
			Attack2();

			Move move = new Move(Move.MoveTypes.Attack2);
			inputBuffer.tmpTimer = move.tmpTimer;
		}
		if(Input.GetButtonDown("Jump")){
			Jump();
		
			Move move = new Move(Move.MoveTypes.Jump);
			inputBuffer.tmpTimer = move.tmpTimer;
		}
		if(Input.GetButtonDown("Dash")){
			Dash();
		
			Move move = new Move(Move.MoveTypes.Dash);
			inputBuffer.tmpTimer = move.tmpTimer;
		}
	}else{
	//there's still a queue so take a peek at queue to check if which input it is. then play the queue
		Move current = inputBuffer._MoveQueue.Peek();
		if(inputBuffer.tmpTimer == current.tmpTimer)
		switch(current.moveType){
			case Move.MoveTypes.Attack1:
			Attack1();
			break;
			case Move.MoveTypes.Attack2:
			Attack2();
			break;
			case Move.MoveTypes.Jump:
			Jump();
			break;
			case Move.MoveTypes.Dash:
			Dash();
			break;
		}
	}

	//finally, check if the timer is already playing. so we can add a new queue.
	//we need to check if the current queue is less than the maximum buffer allowed.
	if(inputBuffer.tmpTimer>0){
		if(Input.GetButtonDown("Attack1")){
			if(inputBuffer._MoveQueue.Count<MaxBuffer){
				Move move = new Move(Move.MoveTypes.Attack1);
				inputBuffer._MoveQueue.Enqueue(move);
			}
		}
		if(Input.GetButtonDown("Attack2")){
			if(inputBuffer._MoveQueue.Count<MaxBuffer){
				Move move = new Move(Move.MoveTypes.Attack2);
				inputBuffer._MoveQueue.Enqueue(move);
			}
		}
		if(Input.GetButtonDown("Jump")){
			if(inputBuffer._MoveQueue.Count<MaxBuffer){
				Move move = new Move(Move.MoveTypes.Jump);
				inputBuffer._MoveQueue.Enqueue(move);
			}
		}
		if(Input.GetButtonDown("Dash")){
			if(inputBuffer._MoveQueue.Count<MaxBuffer){
				Move move = new Move(Move.MoveTypes.Dash);
				inputBuffer._MoveQueue.Enqueue(move);
			}
		}
	}

		UI();
	}

	//edit the script to add functions.
	void Attack1(){
			CurrentInput.transform.localScale=Vector3.one * 1.2f;
	}
	
	void Attack2(){
			CurrentInput.transform.localScale=Vector3.one * 1.2f;
	}

	void Jump(){
			CurrentInput.transform.localScale=Vector3.one * 1.2f;
	}

	void Dash(){
			CurrentInput.transform.localScale=Vector3.one * 1.2f;
	}


	//for debug.
	public Slider slider;
	void UI(){
		CurrentInput.transform.localScale = Vector3.Lerp(CurrentInput.transform.localScale, Vector3.one, 0.1f);
		if(inputBuffer._MoveQueue.Count>0){
			slider.maxValue = inputBuffer._MoveQueue.Peek().tmpTimer;
			slider.value = inputBuffer.tmpTimer;

			if(inputBuffer._moveArray.Length>1){
				NextInput.text = "Next Buffered Input: "+ inputBuffer._moveArray[1].moveType.ToString();
			}else{
				NextInput.text = "Next Buffered Input: None";
			}

			CurrentInput.text = inputBuffer._MoveQueue.Peek().moveType.ToString();
		}else{
			NextInput.text = "Next Buffered Input: None";
			CurrentInput.text = "--";
		}
	}

}
