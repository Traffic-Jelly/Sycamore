using System.Collections;
using UnityEngine;
using NodeCanvas.DialogueTrees;

namespace Sycamore.Dialogue.UI
{
	public class DialogueUI : MonoBehaviour
	{
		[Header ("References (Optional)")]
		[SerializeField] private GameObject waitForInputIndicator;
		[Header ("References (Required)")]
		[SerializeField] private TypingDelays typingDelay;
		[SerializeField] private DialogueTreeController dialogueTreeController;
		[SerializeField] private TextWriter dialogueWriter;
		[SerializeField] private TextWriter nameWriter;
		[SerializeField] private OptionPicker optionPicker;

		private Coroutine subtitleRequestRoutine;

		private void Awake ()
		{
			ShowInputIndicator (false);
		}
		private void OnEnable ()
		{
			DialogueTree.OnSubtitlesRequest += OnSubtitlesRequest;
			DialogueTree.OnMultipleChoiceRequest += OnMultipleChoiceRequest;
		}
		private void OnDisable ()
		{
			DialogueTree.OnSubtitlesRequest -= OnSubtitlesRequest;
			DialogueTree.OnMultipleChoiceRequest -= OnMultipleChoiceRequest;
		}

		private void OnSubtitlesRequest (SubtitlesRequestInfo info)
		{
			if (subtitleRequestRoutine != null)
				StopCoroutine (subtitleRequestRoutine);
			subtitleRequestRoutine = StartCoroutine (SubtitlesRequestRoutine (info));
		}

		private void OnMultipleChoiceRequest (MultipleChoiceRequestInfo info)
		{
			if (info.showLastStatement)
				dialogueWriter.WriteInstant (dialogueWriter.LastString);

			optionPicker.CreateOptions (info.options, (optionIndex) => 
			{
				info.SelectOption (optionIndex);
				optionPicker.HideOptions ();
			});
		}

		private IEnumerator SubtitlesRequestRoutine (SubtitlesRequestInfo info)
		{
			var actor = info.actor;
			string text = info.statement.text;
			var audio = info.statement.audio;
			var endDelay = info.statement.endDelay;
			bool additive = info.statement.meta.Contains ("additive");//info.statement.additive;
			bool waitForInput = !info.statement.meta.Contains ("nowait");//info.statement.waitForInput;

			Debug.Log ("End Delay:" + endDelay);
			Debug.Log ("Additive:" + additive);
			Debug.Log ("Wait For Input:" + waitForInput);

			// Change name
			if (nameWriter != null)
				nameWriter.Write (actor.name, typingDelay, actor.dialogueColor);

			// Type dialogue
			var isTyping = true;

			dialogueWriter.Write (text, typingDelay, actor.dialogueColor, additive, () => isTyping = false);

			while (isTyping)
				yield return null;

			yield return null;

			// Wait for input
			ShowInputIndicator (true);

			if (waitForInput)
				while (!Input.anyKeyDown && !Input.GetMouseButtonDown (0))
					yield return null;

			yield return endDelay;

			ShowInputIndicator (false);

			info.Continue ();
		}

		private void ShowInputIndicator (bool show)
		{
			if (waitForInputIndicator != null)
				waitForInputIndicator.SetActive (show);
		}
	}
}