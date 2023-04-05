using System.Collections.Generic;

public interface IStatsRenderer
{
	public Dictionary<string, int> Stats { get; }
	public StatsRenderer StatsInfoRenderer { get; }

	private void UpdateStatsInfoText() { }
}
