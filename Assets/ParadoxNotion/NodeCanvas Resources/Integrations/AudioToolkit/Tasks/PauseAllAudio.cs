using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.AudioToolkit{

	[Category("AudioToolkit")]
	public class PauseAllAudio : ActionTask{

	    public enum PauseType
	    {
	        All = 0,
	        Music = 1,
	        Ambience = 2,
	        Category = 3,
	    }

	    public PauseType pauseType;
	    public BBParameter<float> fadeOut = 0;
	    public BBParameter<string> categoryName;

	    protected override string info{
	    	get {return string.Format("Pause {0} {1}", pauseType, pauseType == PauseType.Category? categoryName.ToString() : "");}
	    }

		protected override void OnExecute(){

			switch (pauseType)
			{
				case PauseType.All:
				AudioController.PauseAll( fadeOut.value );
				break;

				case PauseType.Music:
				AudioController.PauseMusic( fadeOut.value );
				break;

				case PauseType.Ambience:
				AudioController.PauseAmbienceSound( fadeOut.value );
				break;

				case PauseType.Category:
				AudioController.PauseCategory( categoryName.value, fadeOut.value );
				break;
			}

			EndAction();
		}
	}
}