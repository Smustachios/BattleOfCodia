// Every action in the game has this type

public interface IAction
{
	// Every action has its own way of invoking its logic
	public void InvokeAction(Character character, Controller controller);
}
