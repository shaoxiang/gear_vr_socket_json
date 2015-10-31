using UnityEngine;
using UnityEngine.EventSystems;

//by Ralph Barbagallo
//www.flarb.com
//www.ralphbarbagallo.com
//@flarb

public class VRInputModule : BaseInputModule {
	
	public static GameObject targetObject;
	
	static VRInputModule _singleton;
	
	void Awake() {
		_singleton = this;
	}
	
	public override void Process()
	{
		if (targetObject == null)
			return;
		/*
		if (InputManager.press) { //I poll my own inputmanager class to see if the select button has been pressed
			
			//We tapped the select button so SUBMIT (not select).
			BaseEventData data = GetBaseEventData();
			data.selectedObject = targetObject;
			ExecuteEvents.Execute(targetObject, data, ExecuteEvents.submitHandler);
		}*/
	}
	
	public bool IsPointerOverEventSystemObject (int pointerId)
	{
		if (targetObject != null)
			return true;
		
		return false;
	}
	
	public static void SetTargetObject(GameObject obj) {
		
		//we're hovering over a new object, so unhover the current one
		if ((targetObject != null) && (targetObject != obj)) {
			PointerEventData pEvent = new PointerEventData(_singleton.eventSystem);
			//pEvent.worldPosition = FlarbCrosshair3D.GetRaycastStart();
			ExecuteEvents.Execute(targetObject, pEvent, ExecuteEvents.pointerExitHandler);
		}
		
		if (obj != null) {
			
			//this is the same object that was hovered last time, so bail
			if (obj == targetObject)
				return;
			
			//we've entered a new GUI object, so excute that event to highlight it
			PointerEventData pEvent = new PointerEventData(_singleton.eventSystem);
			pEvent.pointerEnter = obj;
			//pEvent.worldPosition = FlarbCrosshair3D.GetRaycastStart();
			
			ExecuteEvents.Execute(obj, pEvent, ExecuteEvents.pointerEnterHandler);
		}
		
		targetObject = obj;
	}
}