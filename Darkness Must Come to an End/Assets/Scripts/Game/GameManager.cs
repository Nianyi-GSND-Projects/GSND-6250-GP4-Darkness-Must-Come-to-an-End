using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Game
{
	public class GameManager : MonoBehaviour
	{
		#region Singleton
		static GameManager instance;
		public static GameManager Instance => instance;
		#endregion

		#region Reference
		[Header("Components")]
		[SerializeField] Player player;
		[SerializeField] Son son;
		[SerializeField] GameObject note;
		[SerializeField] Store2Controller store2;

		[Header("Dialogues")]
		[SerializeField] Dialogue afterCheckingNoteDialogue;
		[SerializeField] Dialogue confirmingDialogue;
		[SerializeField] Dialogue seeStore1Dialogue;
		[SerializeField] Dialogue seeStore2Dialogue;
		[SerializeField] Dialogue store2ClosedDialogue;
		[SerializeField] Dialogue seeJennysDialogue;
		#endregion

		#region Debug
		[Header("Debug")]
		[SerializeField] GameState debugGameState;
		[SerializeField] Transform spawnPoint;
		[SerializeField] float walkingSpeedBoost = 1f;
		[SerializeField] float dialogueSpeedBoost = 1f;
		#endregion

		#region Unity life cycle
		protected void Awake()
		{
			instance = this;

			if(spawnPoint)
				player.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
			if(!debugGameState)
				gameState = ScriptableObject.CreateInstance<GameState>();
			else
				gameState = Instantiate(debugGameState);

			worldDialoguePanel.GetComponent<WorldDialoguePanelPositioner>().speaker = son.transform;

			player.moveSpeed *= walkingSpeedBoost;
		}

		protected void OnDestroy()
		{
			instance = null;
		}
		#endregion

		#region Game state
		GameState gameState;

		void MarkCheckedNote()
		{
			gameState.checkedNote = true;
		}

		public void MarkLookedAtRoadSign()
		{
			if(gameState.confirmedDirection)
				return;
			if(gameState.checkedNote)
			{
				PlayDialogue(confirmingDialogue);
				gameState.confirmedDirection = true;
			}
		}

		public void SeeStore1()
		{
			if(gameState.sawStore1)
				return;

			gameState.sawStore1 = true;
			PlayDialogue(seeStore1Dialogue);
		}

		public void SeeStore2()
		{
			if(gameState.sawStore2 || gameState.store2Closed)
				return;

			gameState.sawStore2 = true;
			PlayDialogue(seeStore2Dialogue);
		}

		public void CloseStore2()
		{
			if(gameState.store2Closed)
				return;

			gameState.store2Closed = true;
			store2.Close();
			if(gameState.sawStore2)
				PlayDialogue(store2ClosedDialogue);
		}

		public void SeeJennys()
		{
			if(gameState.sawJennys)
				return;

			gameState.sawJennys = true;
			PlayDialogue(seeJennysDialogue);
		}

		public void ArriveAtJenny()
		{
			// TODO
			Debug.Log("Arrived at auntie Jenny's house.");
		}
		#endregion

		#region Control guidance
		[SerializeField] RectTransform controlGuidance;

		public void ShowControlGuidance(string content)
		{
			if(!string.IsNullOrEmpty(content))
				SetControlGuidance(content);
			controlGuidance.gameObject.SetActive(true);
		}

		public void HideControlGuidance()
		{
			controlGuidance.gameObject.SetActive(false);
		}

		void SetControlGuidance(string content)
		{
			controlGuidance.GetComponentInChildren<TMP_Text>(true).text = content;
		}

		public bool NoteOpen
		{
			get => note.activeSelf;
			set
			{
				if(value)
				{
					MarkCheckedNote();
					HideControlGuidance();
				}
				else
				{
					if(!gameState.checkedNote)
						PlayDialogue(afterCheckingNoteDialogue);
				}
				note.SetActive(value);
			}
		}
		#endregion

		#region Dialogue
		[SerializeField] DialoguePanel screenDialoguePanel, worldDialoguePanel;

		public void PlayDialogue(Dialogue dialogue)
		{
			StopCoroutine(nameof(PlayDialogueCoroutine));
			StartCoroutine(nameof(PlayDialogueCoroutine), dialogue);
		}

		IEnumerator PlayDialogueCoroutine(Dialogue dialogue)
		{
			if(!dialogue)
				yield break;
			foreach(var line in dialogue.lines)
			{
				if(line == null)
					continue;
				ShowDialogueLine(line);
				float time = CalculateDialogueLineTime(line);
				yield return new WaitForSeconds(time / dialogueSpeedBoost);
			}
			dialogue.onFinished?.Invoke();
			ActiveDialoguePanel = null;
		}

		void ShowDialogueLine(Dialogue.Line line)
		{
			ActiveDialoguePanel = line.speaker switch
			{
				Dialogue.Line.Speaker.Dad => screenDialoguePanel,
				Dialogue.Line.Speaker.Son => worldDialoguePanel,
				_ => null
			};
			if(ActiveDialoguePanel)
			{
				ActiveDialoguePanel.SpeakerName = line.speaker switch
				{
					Dialogue.Line.Speaker.Dad => "Dad",
					Dialogue.Line.Speaker.Son => "Son",
					_ => string.Empty,
				};
				ActiveDialoguePanel.Content = line.content;
			}
		}

		DialoguePanel ActiveDialoguePanel
		{
			get
			{
				if(screenDialoguePanel.isActiveAndEnabled)
					return screenDialoguePanel;
				if(worldDialoguePanel.isActiveAndEnabled)
					return worldDialoguePanel;
				return null;
			}
			set
			{
				if(ActiveDialoguePanel != value && ActiveDialoguePanel != null)
					ActiveDialoguePanel.gameObject.SetActive(false);
				if(value)
					value.gameObject.SetActive(true);
			}
		}

		float CalculateDialogueLineTime(Dialogue.Line line)
		{
			if(line.time > 0f)
				return line.time;
			return Mathf.Max(3f, line.content.Length * 0.05f);
		}
		#endregion
	}
}