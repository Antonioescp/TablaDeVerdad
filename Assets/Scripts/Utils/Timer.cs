using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	
	#region Fields
	
	// time tracking support
	float elapsedTime = 0f;
	float targetTime = 0f;
	
	// timer state support
	bool started = false;
	bool running = false;
	
	// Finish event support
	UnityEvent finishedEvent;
	
	#endregion
	
	#region Properties
	
	/// <summary>
	/// Wether the timer has finished or not
	/// </summary>
	/// <value>True if finished, false otherwise</value>
	public bool Finished
	{
		get => started && !running;
	}
	
	/// <summary>
	/// Timer duration in seconds
	/// </summary>
	public float Duration
	{
		set => targetTime = value > 0 ? value : 0;
	}

	public bool Running => running;
	public bool Started => started;
	
	#endregion

	#region Methods
	
	/// <summary>
	/// Instantiate timer finish event
	/// </summary>
	private void Awake()
	{
		finishedEvent = new UnityEvent();
	}
	
	/// <summary>
	/// Updates time elapsed and timer state
	/// </summary>
    void Update()
    {
	    if(running)
	    {
	    	elapsedTime += Time.deltaTime;
	    	if(elapsedTime >= targetTime)
	    	{
	    		running = false;
	    		finishedEvent.Invoke();
	    	}
	    }
    }
    
    
	/// <summary>
	/// Adds a listener to timer finished event
	/// </summary>
	/// <param name="listener">Event handler</param>
	public void AddListener(UnityAction listener)
	{
		finishedEvent.AddListener(listener);
	}
	
	/// <summary>
	/// Starts the timer
	/// </summary>
	public void Run()
	{
		if(targetTime > 0)
		{
			elapsedTime = 0;
			running = true;
			started = true;
		}
	}
	
	
	/// <summary>
	/// Resets timer finished state
	/// </summary>
	public void Stop()
	{
		running = false;
		started = false;
	}
    
    #endregion
}
