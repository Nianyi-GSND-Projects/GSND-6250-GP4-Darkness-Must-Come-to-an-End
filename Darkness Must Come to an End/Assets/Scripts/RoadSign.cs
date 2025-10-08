using UnityEngine;
using TMPro;

namespace Game
{
	public class RoadSign : MonoBehaviour
	{
		[SerializeField] TMP_Text text;
		[System.NonSerialized] public int number;

		protected void Start()
		{
			GameManager.Instance.RegisterRoadSign(this);
			GameManager.Instance.onRoadSignAssigned += UpdateNumber;
		}

		void UpdateNumber()
		{
			text.text = $"{number} Pleasant St";
		}
	}
}
