using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public delegate void BaseEventDelegate(Transform trans, BaseEventData eventData);

public class ClickListener : MonoBehaviour, IPointerClickHandler
{
    public BaseEventDelegate onClick;
    public bool CanPassClickEvent { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        Click(eventData, CanPassClickEvent);
    }

    private void Click(PointerEventData eventData, bool passEvent)
    {
        if(onClick != null)
        {
            onClick(transform, eventData);
        }

        if(CanPassClickEvent)
        {
            PassEvent(eventData, ExecuteEvents.pointerClickHandler);
        }
    }

    //把事件透下去
    public void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function) where T : IEventSystemHandler
    {
        List<RaycastResult> rEesults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, rEesults);
        bool findCurrent = false;

        for(int i = 0; i < rEesults.Count; i++)
        {
            if(!findCurrent && gameObject == rEesults[i].gameObject)
            {
                findCurrent = true;
                continue;
            }

            if(findCurrent)
            {
                ExecuteEvents.Execute(rEesults[i].gameObject, data, function);
                break;
                //RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
            }
        }

        if(!findCurrent && rEesults.Count > 0)
        {
            ExecuteEvents.Execute(rEesults[0].gameObject, data, function);
        }
    }

    public static T Get<T>(Component cmpt) where T : MonoBehaviour
    {
        GameObject go = cmpt.gameObject;
        T listener = go.GetComponent<T>();
        if(listener == null) 
            listener = go.AddComponent<T>();

        return listener;
    }

    public static void AddClick(Component component, BaseEventDelegate onClick, bool passClickEvent = false, bool posForce = false)
    {
        ClickListener rClickListener = Get<ClickListener>(component);
        rClickListener.onClick += onClick;
        rClickListener.CanPassClickEvent = passClickEvent;
    }

    public static void ClearListener(Component component)
    {
        ClickListener cl = Get<ClickListener>(component);
        cl.onClick = null;
    }
}