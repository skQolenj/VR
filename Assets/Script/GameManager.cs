using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	public static GameManager instance;
	public int state, selectedtool;
	internal GameObject[,] board;
	public GameObject cube;
	public GameObject rescube;
	public GameObject res2cube;
	public GameObject res3cube;
	public Vector2[] pins;
	
	void Start ()
	{
		instance = this;
		board = new GameObject[30,7];
		pins = new Vector2[2];
		state = 0;
		selectedtool = 0;
		
		for (int j = 0; j < 7; j++)
		{
			for (int i = 0; i < 30; i++)
			{
				if (j < 5)
				{
					GameObject g0 = Instantiate(cube, new Vector3(i * 1.1F - 15, 11.5F, j * 1.3F), Quaternion.identity);
					board[i, j] = g0;
					g0.GetComponent<C1>().number = new Vector2(i, j);
				}
				else
				{
					GameObject g0 = Instantiate(cube, new Vector3(i * 1.1F - 15, 11.5F, 1F + j * 1.3F), Quaternion.identity);
					board[i, j] = g0;
					g0.GetComponent<C1>().number = new Vector2(i, j);

				}
			}	
		}
		for (int j = 0; j < 5; j++)
			for (int i = 0; i < 30; i++)
				for (int k = 0; k < 5; k++)
					if (k != j)
						board[i,j].GetComponent<C1>().Addconnection(board[i,k].GetComponent<C1>());
		for (int j = 5; j < 7; j++)
			for (int i = 0; i < 30; i++)
				for (int k = 0; k < 30; k++)
					if (k != i)
						board[i,j].GetComponent<C1>().Addconnection(board[k,j].GetComponent<C1>());
		board[0,6].GetComponent<C1>().SetVoltage(10F);
	}

	private void OnMouseDown()
	{
		
	}

	internal void connect2pins()
	{
		float rotateAngle;
		Vector3 middlepos = new Vector3();
		Vector3 firstpin = board[(int) pins[0].x, (int) pins[0].y].transform.position;
		Vector3 secondpin = board[(int) pins[1].x, (int) pins[1].y].transform.position;
		rotateAngle = -(firstpin.z - secondpin.z) / (firstpin.x - secondpin.x);
		middlepos = (firstpin + secondpin) / 2;
		middlepos.y += 1;
		if (GameManager.instance.selectedtool == 1)
		{
			GameObject g0 = Instantiate(rescube, middlepos,Quaternion.identity);
			g0.transform.localScale = new Vector3(Mathf.Sqrt((firstpin.x - secondpin.x)*(firstpin.x - secondpin.x) + (firstpin.z - secondpin.z)*(firstpin.z - secondpin.z)),1,1);
			g0.transform.localRotation = Quaternion.Euler(0,Mathf.Rad2Deg*Mathf.Atan(rotateAngle),0);
			board[(int) pins[0].x, (int) pins[0].y].GetComponent<C1>().Addconnection(board[(int) pins[1].x, (int) pins[1].y].GetComponent<C1>());
		}
		else if (GameManager.instance.selectedtool == 2)
		{
			GameObject g0 = Instantiate(res2cube, middlepos,Quaternion.identity);
			g0.transform.localScale = new Vector3(Mathf.Sqrt((firstpin.x - secondpin.x)*(firstpin.x - secondpin.x) + (firstpin.z - secondpin.z)*(firstpin.z - secondpin.z)),1,1);
			g0.transform.localRotation = Quaternion.Euler(0,Mathf.Rad2Deg*Mathf.Atan(rotateAngle),0);
			board[(int) pins[0].x, (int) pins[0].y].GetComponent<C1>().Addconnection(board[(int) pins[1].x, (int) pins[1].y].GetComponent<C1>());
		}
		else if (GameManager.instance.selectedtool == 3)
		{
			GameObject g0 = Instantiate(res3cube, middlepos,Quaternion.identity);
			g0.transform.localScale = new Vector3(Mathf.Sqrt((firstpin.x - secondpin.x)*(firstpin.x - secondpin.x) + (firstpin.z - secondpin.z)*(firstpin.z - secondpin.z)),1,1);
			g0.transform.localRotation = Quaternion.Euler(0,Mathf.Rad2Deg*Mathf.Atan(rotateAngle),0);
			board[(int) pins[0].x, (int) pins[0].y].GetComponent<C1>().Addconnection(board[(int) pins[1].x, (int) pins[1].y].GetComponent<C1>());
		}
		
	}
	
	public void ItemClick(int item)
	{
		if (item == 0)
		{
			GameManager.instance.state = 1;
			GameManager.instance.selectedtool = 1;
			Debug.Log("clicked on resistance1");
		}		
		if (item == 1)
		{
			GameManager.instance.state = 1;
			GameManager.instance.selectedtool = 2;
			Debug.Log("clicked on resistance2");
		}		
		if (item == 2)
		{
			GameManager.instance.state = 1;
			GameManager.instance.selectedtool = 3;
			Debug.Log("clicked on resistance3");
		}		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
