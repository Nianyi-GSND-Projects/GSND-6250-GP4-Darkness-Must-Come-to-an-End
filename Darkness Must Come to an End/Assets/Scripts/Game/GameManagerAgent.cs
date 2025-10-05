using UnityEngine;

namespace Game
{
	[CreateAssetMenu(menuName = "Game/Game Manager Agent")]
	public class GameManagerAgent : ScriptableObject
	{
		static GameManager Instance => GameManager.Instance;

		public static void MarkLookedAtRoadSign() => Instance.MarkLookedAtRoadSign();
		public static void SeeStore1() => Instance.SeeStore1();
		public static void SeeStore2() => Instance.SeeStore2();
		public static void CloseStore2() => Instance.CloseStore2();
		public static void SeeJennys() => Instance.SeeJennys();
		public static void ArriveAtJenny() => Instance.ArriveAtJenny();

		public static void ShowControlGuidance(string content) => Instance.ShowControlGuidance(content);
		public static void HideControlGuidance() => Instance.HideControlGuidance();

		public static void PlayDialogue(Dialogue dialogue) => Instance.PlayDialogue(dialogue);
	}
}