using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.AudioToolkit{

	[Category("AudioToolkit")]
	public class UnpauseAllAudio : ActionTask{

	    public enum PauseType
	    {
	        All = 0,
	        Music = 1,
	        Ambience = 2,
	        Category = 3,
	    }

	    public PauseType unpauseType;
	    public BBParameter<float> fadeIn = 0;
	    public BBParameter<string> categoryName;

	    protected override string info{
	    	get {return string.Format("Unpause {0} {1}", unpauseType, unpauseType == PauseType.Category? categoryName.ToString() : "");}
	    }

		protected override void OnExecute(){

			switch (unpauseType)
			{
				case PauseType.All:
				AudioController.UnpauseAll( fadeIn.value );
				break;

				case PauseType.Music:
				AudioController.UnpauseMusic( fadeIn.value );
				break;

				case PauseType.Ambience:
				AudioController.UnpauseAmbienceSound( fadeIn.value );
				break;

				case PauseType.Category:
				AudioController.UnpauseCategory( categoryName.value, fadeIn.value );
				break;
			}

			EndAction();
		}
	}
}