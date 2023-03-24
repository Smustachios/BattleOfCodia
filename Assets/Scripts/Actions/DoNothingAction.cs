using UnityEngine;

public class DoNothingAction : MonoBehaviour, IAction
{
	public void InvokeAction(params Character[] characters)
	{
		Debug.Log($"{characters[0].name} did nothing..");
	}
}
