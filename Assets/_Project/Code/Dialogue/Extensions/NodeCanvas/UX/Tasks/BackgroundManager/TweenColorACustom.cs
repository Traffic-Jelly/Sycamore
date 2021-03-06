﻿using UnityEngine;
using Sycamore.FX.UX;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using DG.Tweening;

namespace Sycamore.Dialogue.Extensions
{
	[Category ("Sycamore/FX/UX/Background")]
	public class TweenColorACustom : ActionTask
	{
		public BBParameter<Color> to = new BBParameter<Color> (Color.white);
		public BBParameter<float> duration = new BBParameter<float> (2f);
		public BBParameter<AnimationCurve> curve = new BBParameter<AnimationCurve> (AnimationCurve.Linear (0f, 0f, 1f, 1f));

		protected override void OnExecute ()
		{
			BackgroundManager.Instance.TweenColorA (to.value, duration.value, curve.value);
			EndAction ();
		}
	}
}