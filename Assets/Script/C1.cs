using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class C1 : MonoBehaviour
{
	public Vector2 number;
	public List<C1> Connctedonboard = new List<C1>();
	public List<C1> Connectedbywire = new List<C1>();
	internal float Voltage = -1;
	internal float Current = -1;
	internal int tool;

	public void Addconnection(C1 connectedNode)
	{
		
		if (GameManager.instance.state == 3)
		{
			Connectedbywire.Add((connectedNode));
			if (GameManager.instance.selectedtool == 1)
			{
				this.Current = (this.Voltage - connectedNode.Voltage) / 10;
				connectedNode.Current = -(this.Current);
			}
			Connectedbywire.Add((connectedNode));
			if (GameManager.instance.selectedtool == 2)
			{
				this.Current = (this.Voltage - connectedNode.Voltage) / 100;
				connectedNode.Current = -(this.Current);
			}
			Connectedbywire.Add((connectedNode));
			if (GameManager.instance.selectedtool == 3)
			{
				this.Current = (this.Voltage - connectedNode.Voltage) / 1000;
				connectedNode.Current = -(this.Current);
			}
			GameManager.instance.state = 0;
			Debug.Log(number + "Connection by wire to" + Connectedbywire[Connectedbywire.Count-1].number );
		}
		else
		{
			Connctedonboard.Add(connectedNode);
		}
	}

	public void SetVoltage(float v)
	{
		for (int i = 0; i < Connctedonboard.Count; i++)
		{
			Connctedonboard[i].Voltage = v;
		}
	}
	/*public void SetCurrent(float c)
	{
		for (int i = 0; i < Connctedonboard.Count; i++)
		{
			Connctedonboard[i].Current = c;
		}
	}*/
	
	// Use this for initialization
	private void OnMouseDown()
	{
		if (GameManager.instance.state == 0)
		{
			Debug.Log(" tool not selected");
		}
		else if (GameManager.instance.state == 1)
		{
			GameManager.instance.pins[0] = number;
			GameManager.instance.state = 2;
			Debug.Log("first pin choosed" + number);
		}
			
		else if (GameManager.instance.state == 2)
		{
			GameManager.instance.pins[1] = number;
			Debug.Log("second pin choosed" + number);
			GameManager.instance.state = 3;
			GameManager.instance.connect2pins();
		}
			
	}
}
