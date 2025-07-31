using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MyInputManager : MonoBehaviour
{
    [SerializeField]
    public GameObject playerObject;
    public GameObject spawn_position;
    public float time_left = 10;
    public float round_time = 10;
    public UnityEvent<float> NewRoundEvent;
    class MyInputs
    {
        public enum Actions
        {
            JUMP,
            START_RIGHT,
            STOP_RIGHT,
            START_LEFT,
            STOP_LEFT,
            START_DANCE,
            STOP_DANCE
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
    private List<List<MyInputUser.state>> states;
    private List<List<MyInputs>> inputs;
    private List<Trackable> trackables = new List<Trackable>();
    void Start()
    {
    	players = new List<MyInputUser>();
    	states = new List<List<MyInputUser.state>>();
    	inputs = new List<List<MyInputs>>();
    	StartNewRound();
    }
    public void RegisterTrackable(Trackable t)
    {
        trackables.Add(t);
    }
    float round_start = 0;

    private System.Collections.IEnumerator reset_coroutine;
    public System.Collections.IEnumerator ResetPlayersCo()
    {
        while (players.Count > 0)
        {
            for (float scale = 1; scale > 0; scale -= 0.1f)
            {
                players[0].transform.localScale = new Vector3(scale, 1);
				yield return new WaitForSeconds(.01f);
			}
			GameObject.Destroy(players[0].gameObject);
			players.RemoveAt(0);
        }

        
        reset_coroutine = null;
        inputs.Clear();
        StartNewRound();
    }
    public System.Collections.IEnumerator ResetRoundCo()
    {
        float time = Time.time - round_start;
        while (time > 0)
        {
            time -= 0.1f;
            for (int cx = 0; cx < players.Count; cx++)
            {
                while (states[cx].Count > 0 && states[cx].Last().time > time)
                {
                    players[cx].SetState(states[cx].Last());
                    states[cx].RemoveAt(states[cx].Count - 1);
                }
            }
            foreach (Trackable t in trackables)
                t.RewindTo(time);
			yield return new WaitForSeconds(0.01f);
		}
        reset_coroutine = null;
        StartNewRound();
	}
	public void ResetPlayers()
	{
        reset_coroutine = ResetPlayersCo();
        StartCoroutine(reset_coroutine);
	}
    public void EndCurrentRound()
    {
        reset_coroutine = ResetRoundCo();
        StartCoroutine(reset_coroutine);
    }
	public void StartNewRound()
    {
        round_start = Time.time;
        time_left = round_time;
        foreach (MyInputUser p in players)
            GameObject.Destroy(p.gameObject);
        players.Clear();
        states.Clear();
        inputs.Insert(0, new List<MyInputs>());
        for (int cx = 0; cx < inputs.Count; cx++)
        {
            MyInputUser player = GameObject.Instantiate(playerObject).GetComponent<MyInputUser>();
            player.gameObject.transform.position = spawn_position.transform.position;

            player.player_id = cx;
			players.Add(player);
            states.Add(new List<MyInputUser.state>());
        }
        NewRoundEvent.Invoke(round_start);
    }
    KeyCode[] left = { KeyCode.A, KeyCode.LeftArrow };
    KeyCode[] right = { KeyCode.D, KeyCode.E, KeyCode.RightArrow };
    KeyCode[] jump = { KeyCode.Space, KeyCode.W, KeyCode.Comma, KeyCode.UpArrow };
    KeyCode[] dance = { KeyCode.S, KeyCode.O, KeyCode.DownArrow };
    // Update is called once per frame
    void Update()
    {
        if (reset_coroutine != null)
        {
            Debug.Log("Skip input");
			return;
        }

		float t0 = Time.time - round_start;
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
        foreach (KeyCode l in dance)
        {
			if (Input.GetKeyDown(l))
				inputs[0].Add(new MyInputs(t0, MyInputs.Actions.START_DANCE));
			if (Input.GetKeyUp(l))
				inputs[0].Add(new MyInputs(t0, MyInputs.Actions.STOP_DANCE));
		}
		time_left = round_time - t0;
		if (t0 > round_time)
            EndCurrentRound();
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
					if (inputs[cx][cy].input == MyInputs.Actions.START_DANCE)
						players[cx].StartDance();
					if (inputs[cx][cy].input == MyInputs.Actions.STOP_DANCE)
						players[cx].StopDance();
				}
			}
            states[cx].Add(players[cx].SaveCurrentState());
        }
    }
}

