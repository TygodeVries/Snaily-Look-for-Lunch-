using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Trackable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Vector3 start_position;
    Quaternion start_rotation;
    MyInputManager manager;
    float round_start_time;
    struct state
    {
        public float time;
        public Vector3 position;
        public Quaternion rotation;
    };

    List<state> recorded_states;

    void Start()
    {
        manager = FindFirstObjectByType<MyInputManager>();
        start_position = transform.position;
        start_rotation = transform.rotation;
        recorded_states = new List<state>();
        manager.NewRoundEvent.AddListener(this.NewRound);
        manager.RegisterTrackable(this);
    }
    public void NewRound(float start_time, int player_count)
    { // To make sure it's absolutely at the right place
        transform.position = start_position;
        transform.rotation = start_rotation;
        round_start_time = start_time;
        recorded_states.Clear();
        rewinding = false;
    }
    void SetState(state s)
    {
        transform.rotation = s.rotation;
        transform.position = s.position;
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        if (body != null)
        {
            body.angularVelocity = 0;
            body.linearVelocity = new Vector2(0, 0);
        }
    }
    void SaveState()
    {
        state s;
        s.time = Time.time - round_start_time;
        s.position = transform.position;
        s.rotation = transform.rotation;
        recorded_states.Add(s);
    }
    bool rewinding = false;
    public void RewindTo(float time)
    {
        rewinding = true;
        while (recorded_states.Count>0 && recorded_states.Last().time > time)
        {
			SetState(recorded_states.Last());
			recorded_states.RemoveAt(recorded_states.Count - 1);

		}
	}
    // Update is called once per frame
    void Update()
    {
        if (!rewinding)
            SaveState();
    }
}
