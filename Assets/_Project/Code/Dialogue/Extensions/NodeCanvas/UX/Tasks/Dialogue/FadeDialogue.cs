using Sycamore.Dialogue.UI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Sycamore.Dialogue.Extensions
{
	[Category ("Sycamore/FX/UX/Dialogue")]
	public class FadeDialogue : ActionTask
	{
		public BBParameter<float> target = new BBParameter<float> (0f);
		public BBParameter<float> duration = new BBParameter<float> (1f);
		public BBParameter<bool> waitForComplete = new BBParameter<bool> (true);

		protected override void OnExecute ()
		{
			var tween = DialogueUI.Instance.Fade (target.value, duration.value);
			if (waitForComplete.value)
				tween.onComplete += EndAction;
			else
				EndAction ();
		}
	}
}