using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Game State")]
	public class GameState : ScriptableObject
	{
		public bool checkedNote = false;
		public bool confirmedDirection = false;
		public bool sawStore1 = false;
		public bool sawStore2 = false;
		public bool store2Closed = false;
		public bool sawJennys = false;
	}
}