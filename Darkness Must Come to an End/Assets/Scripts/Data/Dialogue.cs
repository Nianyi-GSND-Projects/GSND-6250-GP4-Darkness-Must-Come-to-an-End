using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Dialogue")]
	public class Dialogue : ScriptableObject
	{
		[System.Serializable]
		public class Line
		{
			public enum Speaker { Dad, Son, Delay }
			public Speaker speaker;
			[Min(0)] public float time;
			public string content;
		}

		public UnityEvent onFinished;
		public Line[] lines;
	}
}
