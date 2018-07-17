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
			var text = info.statement.text;
			var audio = info.statement.audio;
			var endDelay = info.statement.endDelay;
			var additive = info.statement.additive;
			var inputWaitMode = info.statement.inputWaitMode;

			if (inputWaitMode == InputWaitMode.Beginning)
				yield return WaitForInput ();

			yield return null;

			// Change name
			if (nameWriter != null)
				nameWriter.Write (actor.name, typingDelay, actor.dialogueColor);

			// Type dialogue
			var isTyping = true;

			dialogueWriter.Write (text, typingDelay, actor.dialogueColor, additive, () => isTyping = false);

			while (isTyping)
				yield return null;

			if (inputWaitMode == InputWaitMode.End)
				yield return WaitForInput ();

			yield return endDelay;

			info.Continue ();
		}

		private void ShowInputIndicator (bool show)
		{
			if (waitForInputIndicator != null)
				waitForInputIndicator.SetActive (show);
		}

		private IEnumerator WaitForInput ()
		{
			ShowInputIndicator (true);

			while (!Input.anyKeyDown && !Input.GetMouseButtonDown (0))
				yield return null;

			ShowInputIndicator (false);
		}
	}
}