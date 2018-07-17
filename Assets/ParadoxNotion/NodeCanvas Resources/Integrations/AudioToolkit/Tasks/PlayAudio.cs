using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.AudioToolkit
{
	[Category ("AudioToolkit")]
	[Icon ("AudioToolkit")]
	public class PlayAudio : ActionTask<Transform>
	{
		public enum SoundType
		{
			SFX = 0,
			Music = 1,
			AmbienceSound = 2,
		}

		[RequiredField]
		public BBParameter<string> audioID;
		public SoundType soundType;
		public BBParameter<float> volume = 1f;
		public BBParameter<float> delay = 0f;
		public BBParameter<float> startTime = 0f;
		public bool useAgentPosition = false;

		protected override string info { get { return string.Format ("Play {0} {1}", soundType, audioID); } }

		protected override void OnExecute ()
		{
			var target = useAgentPosition ? agent.transform : null;

			switch (soundType)
			{
				case SoundType.SFX:
					if (target != null)
						AudioController.Play (audioID.value, target.position, null, volume.value, delay.value, startTime.value);
					else
						AudioController.Play (audioID.value, volume.value, delay.value, startTime.value);
					break;

				case SoundType.Music:
					if (target != null)
						AudioController.PlayMusic (audioID.value, target.position, null, volume.value, delay.value, startTime.value);
					else
						AudioController.PlayMusic (audioID.value, volume.value, delay.value, startTime.value);
					break;

				case SoundType.AmbienceSound:
					if (target != null)
						AudioController.PlayAmbienceSound (audioID.value, target.position, null, volume.value, delay.value, startTime.value);
					else
						AudioController.PlayAmbienceSound (audioID.value, volume.value, delay.value, startTime.value);
					break;
			}

			EndAction ();
		}
	}
}