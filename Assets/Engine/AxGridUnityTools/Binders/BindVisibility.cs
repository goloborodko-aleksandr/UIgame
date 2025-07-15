using Engine.AxGridUnityTools.Base;
using UnityEngine;

namespace Engine.AxGridUnityTools.Binders
{
	/// <inheritdoc />
	/// <summary>
	/// Класс привязывается на событие модели, изменения поля
	/// </summary>
	public class BindVisibility : MonoBehaviourExtBind
	{ 
		public string fieldName = "";
		public bool def;

		[OnStart]
		public void Bind() {
			Settings.Model.EventManager.AddAction($"On{fieldName}Changed", Changed);
		}

		[OnDestroy]
		public void UnBind() {
			Settings.Model.EventManager.RemoveAction($"On{fieldName}Changed", Changed);
		}
		
		/// <summary>
		/// Модель изменилась
		/// </summary>
		/// <param name="fieldName">Поле, которое поменялось</param>
		/// <param name="newValue">Новое значение</param>
		[OnStart(RunLevel.Low)]
		public void Changed()
		{
			transform.localScale = Settings.Model.GetBool(fieldName, def) ? Vector3.one : Vector3.zero;
		}
	}
}