using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class MyInputManager : MonoBehaviour
{
    [SerializeField]
    public GameObject playerObject;
    public double time_left = 10;
    public double round_time = 10;
    public UnityEvent NewRoundEvent;
    class MyInputs
    {
        public enum Actions
        {
            JUMP,
            START_RIGHT,
            STOP_RIGHT,
            START_LEFT,
            STOP_LEFT
        }
        public MyInputs(double t, Actions i)
        {
            time = t;
            input = i;
        }
        public double time;
        public Actions input;
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<MyInputUser> players;
    private List<List<MyInputs>> inputs;
    void Start()
    {
        players = new List<MyInputUser>();
        inputs = new List<List<MyInputs>>();
        StartNewRound();
    }
    double round_start = 0;

    public void StartNewRound()
    {
        round_start = Time.time;
        time_left = round_time;
        foreach (MyInputUser p in players)
            GameObject.Destroy(p.gameObject);
        players.Clear();
        inputs.Insert(0, new List<MyInputs>());
        for (int cx = 0; cx < inputs.Count; cx++)
        {
            MyInputUser player = GameObject.Instantiate(playerObject).GetComponent<MyInputUser>();

            player.player_id = cx;
			players.Add(player);
        }
        NewRoundEvent.Invoke();
    }
    KeyCode[] left = { KeyCode.A, KeyCode.LeftArrow };
    KeyCode[] right = { KeyCode.D, KeyCode.E, KeyCode.RightArrow };
    KeyCode[] jump = { KeyCode.Space, KeyCode.W, KeyCode.Comma, KeyCode.UpArrow };
    // Update is called once per frame
    void Update()
    {
		double t0 = Time.time - round_start;
		foreach (KeyCode l in jump)
			if (Input.GetKeyDown(l))
        {
            inputs[0].Add(new MyInputs(t0, MyInputs.Actions.JUMP));
        }
        foreach (KeyCode l in left)
        {
            if (Input.GetKeyDown(l))
                inputs[0].Add(new MyInputs(t0, MyInputs.Actions.START_LEFT));
            if (Input.GetKeyUp(l))
                inputs[0].Add(new MyInputs(t0, MyInputs.Actions.STOP_LEFT));
        }
		foreach (KeyCode l in right)
		{
			if (Input.GetKeyDown(l))
				inputs[0].Add(new MyInputs(t0, MyInputs.Actions.START_RIGHT));
			if (Input.GetKeyUp(l))
				inputs[0].Add(new MyInputs(t0, MyInputs.Actions.STOP_RIGHT));
		}

        time_left = round_time - t0;
		if (t0 > round_time)
            StartNewRound();
        for (int cx = 0; cx < players.Count; cx++)
        {
            for (int cy = 0; cy < inputs[cx].Count; cy++)
            {
                if (inputs[cx][cy].time > t0 - Time.deltaTime && inputs[cx][cy].time <= t0)
                {
                    if (inputs[cx][cy].input == MyInputs.Actions.JUMP)
                        players[cx].Jump();
                    if (inputs[cx][cy].input == MyInputs.Actions.START_RIGHT)
                        players[cx].StartRight();
                    if (inputs[cx][cy].input == MyInputs.Actions.STOP_RIGHT)
                        players[cx].StopRight();
                    if (inputs[cx][cy].input == MyInputs.Actions.START_LEFT)
                        players[cx].StartLeft();
                    if (inputs[cx][cy].input == MyInputs.Actions.STOP_LEFT)
                        players[cx].StopLeft();
                }
            }
        }
    }
}

