using Engine.AxGridUnityTools.Base;
using SmartFormat;
using UnityEngine;

namespace Engine.AxGridUnityTools.Binders
{
	public class UIPositionDataBind : MonoBehaviourExtBind
	{
		public Vector3 enablePosition;
		public Vector3 disablePosition;
		
		public bool defaultValue = true;
		public bool invert = false;
		public string field = "Visible";
		
		public bool readEnablePositionFromObject = false;
        
		[OnStart(RunLevel.High)]
		public void Bind()
		{
			Settings.Model.EventManager.AddAction(Smart.Format("On{0}Changed", field), OnFieldChange);
			if (readEnablePositionFromObject)
				enablePosition = GetComponent<RectTransform>().localPosition;
		}

		[OnStart(RunLevel.Low)]
		public void OnFieldChange()
		{
			var val = Settings.Model.GetBool(field, defaultValue);
			SetPosition(invert ? !val : val);
			
		}

		private void SetPosition(bool value)
		{
			this.GetComponent<RectTransform>().localPosition = value ? enablePosition : disablePosition;
		} 
		
		[OnDestroy]
		public void UnBind()
		{
			Settings.Model.EventManager.RemoveAction(Smart.Format("On{0}Changed", field), OnFieldChange);
		}

		
	}
}