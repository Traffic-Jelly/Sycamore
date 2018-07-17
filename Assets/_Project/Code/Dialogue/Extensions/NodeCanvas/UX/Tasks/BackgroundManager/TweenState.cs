using Sycamore.FX.UX;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using DG.Tweening;

namespace Sycamore.Dialogue.Extensions
{
	[Category ("Sycamore/FX/UX")]
	public class TweenState : ActionTask
	{
		[RequiredField]
		public BBParameter<BackgroundState> to;
		public BBParameter<float> duration = new BBParameter<float> (2f);
		public BBParameter<Ease> ease = new BBParameter<Ease> (Ease.InOutSine);

		protected override string info
		{
			get { return "Tween Background State to " + ((to.value == null) ? "[Missing]" : to.value.name); }
		}

		protected override void OnExecute ()
		{
			BackgroundManager.Instance.TweenState (to.value, duration.value, ease.value);
			EndAction ();
		}
	}
}