/// <summary>
/// Every action must be invokable.
/// Must carry attacker and controller with action flow
/// </summary>
public interface IAction
{
	public void InvokeAction(Character character, Controller controller);
}
