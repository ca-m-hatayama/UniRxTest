using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public class MockData
{
	public int rank = 0;
	public List<int> items = new List<int> ();
}

public class Main : MonoBehaviour
{
	public Button countButton;
	public Button mockDataButton;
	public Button playButton;
	public Button dieButton;

	private enum PlayerState
	{
		Init,
		Play,
		Dead
	}

	private ReactiveProperty<PlayerState> _state = new ReactiveProperty<PlayerState> (PlayerState.Init);
	private ReactiveProperty<MockData> _mockData = new ReactiveProperty<MockData> (new MockData ());
	private ReactiveProperty<int> _level = new ReactiveProperty<int> (0);

	void Start ()
	{
		mockDataButton.onClick.AddListener (
			() => {
				//mockData.Value.rank++; これだと変化した事にはならない
				_mockData.Value = new MockData ();
			});

		countButton.onClick.AddListener (
			() => {
				_level.Value++;
			});
		
		playButton.onClick.AddListener (
			() => {
				_state.Value = PlayerState.Play;
			});
		
		dieButton.onClick.AddListener (
			() => {
				_state.Value = PlayerState.Dead;
			});

		//		Observable.EveryUpdate ()
		//			.Where (_ => state.Value == PlayerState.Play)
		//			.Subscribe (_ => OnPlayUpdate ());

		//dataひも付
		_level.Subscribe ((int level) => {
			Debug.Log ("level changed. " + level);
		});

		_state.Subscribe ((PlayerState state) => {
			Debug.Log ("state changed. " + state);
		});

		_mockData.Subscribe ((MockData mockData) => {
			Debug.Log ("mockData changed. " + mockData);
		});
	}

	//state == Play 状態のみ走るUpdate処理
	void OnPlayUpdate ()
	{
		Debug.Log ("Update State.Play");
	}
}
