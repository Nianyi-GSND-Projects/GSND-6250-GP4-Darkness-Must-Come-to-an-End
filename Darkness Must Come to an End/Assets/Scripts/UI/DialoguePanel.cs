using UnityEngine;
using TMPro;

namespace Game
{
	public class DialoguePanel : MonoBehaviour
	{
		[SerializeField] TMP_Text speakerName, content;

		public string SpeakerName
		{
			get => speakerName.text;
			set => speakerName.text = value;
		}

		public string Content
		{
			get => content.text;
			set => content.text = value;
		}
	}
}
