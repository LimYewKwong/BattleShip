using System;
using System.Collections.Generic;

/// <summary>
/// AIHardPlayer is a type of player. This AI will know directions of ships
/// when it has found 2 ship tiles and will try to destroy that ship. If that ship
/// is not destroyed it will shoot the other way. Ship still not destroyed, then
/// the AI knows it has hit multiple ships. Then will try to destoy all around tiles
/// that have been hit.
/// </summary>
public class AIEasyPlayer : AIPlayer
{

	/// <summary>
	/// Target allows the AI to know more things, for example the source of a
	/// shot target
	/// </summary>
	protected class Target
	{
		private readonly Location _ShotAt;

		private readonly Location _Source;
		/// <summary>
		/// The target shot at
		/// </summary>
		/// <value>The target shot at</value>
		/// <returns>The target shot at</returns>
		public Location ShotAt {
			get { return _ShotAt; }
		}

		/// <summary>
		/// The source that added this location as a target.
		/// </summary>
		/// <value>The source that added this location as a target.</value>
		/// <returns>The source that added this location as a target.</returns>
		public Location Source {
			get { return _Source; }
		}

		internal Target (Location shootat, Location source)
		{
			_ShotAt = shootat;
			_Source = source;
		}

		/// <summary>
		/// If source shot and shootat shot are on the same row then
		/// give a boolean true
		/// </summary>
		public bool SameRow {
			get { return _ShotAt.Row == _Source.Row; }
		}

		/// <summary>
		/// If source shot and shootat shot are on the same column then
		/// give a boolean true
		/// </summary>
		public bool SameColumn {
			get { return _ShotAt.Column == _Source.Column; }
		}
	}

	/// <summary>
	/// Private enumarator for AI states. currently there are two states,
	/// the AI can be searching for a ship, or if it has found a ship it will
	/// target the same ship
	/// </summary>
	private enum AIStates
	{
		/// <summary>
		/// The AI is searching for its next target
		/// </summary>
		Searching,
	}

	private AIStates _CurrentState = AIStates.Searching;
	private Stack<Target> _Targets = new Stack<Target> ();
	private List<Target> _LastHit = new List<Target> ();

	private Target _CurrentTarget;
	public AIEasyPlayer (BattleShipsGame game) : base (game)
	{
	}

	/// <summary>
	/// GenerateCoords will call upon the right methods to generate the appropriate shooting
	/// coordinates
	/// </summary>
	/// <param name="row">the row that will be shot at</param>
	/// <param name="column">the column that will be shot at</param>
	protected override void GenerateCoords (ref int row, ref int column)
	{
		do {
			//check which state the AI is in and uppon that choose which coordinate generation
			//method will be used.
			switch (_CurrentState) {
			case AIStates.Searching:
				SearchCoords (ref row, ref column);
				break;
			default:
				throw new ApplicationException ("AI has gone in an invalid state");
			}

		} while ((row < 0 || column < 0 || row >= EnemyGrid.Height || column >= EnemyGrid.Width || EnemyGrid [row, column] != TileView.Sea));
		//while inside the grid and not a sea tile do the search
	}

	/// <summary>
	/// SearchCoords will randomly generate shots within the grid as long as its not hit that tile already
	/// </summary>
	/// <param name="row">the generated row</param>
	/// <param name="column">the generated column</param>
	private void SearchCoords (ref int row, ref int column)
	{
		row = _Random.Next (0, EnemyGrid.Height);
		column = _Random.Next (0, EnemyGrid.Width);
	}

	/// <summary>
	/// ProcessShot is able to process each shot that is made and call the right methods belonging
	/// to that shot. For example, if its a miss = do nothing, if it's a hit = process that hit location
	/// </summary>
	/// <param name="row">the row that was shot at</param>
	/// <param name="col">the column that was shot at</param>
	/// <param name="result">the result from that hit</param>
	protected override void ProcessShot (int row, int col, AttackResult result)
	{
		switch (result.Value) {
		case ResultOfAttack.Miss:
			_CurrentTarget = null;
			break;
		case ResultOfAttack.ShotAlready:
			throw new ApplicationException ("Error in AI");
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================