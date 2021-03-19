using System;
using UnityEngine;
using UnityEngine. EventSystems;
using UnityEngine. UI;

public class ButtonEx : Button/*,IBeginDragHandler,IDragHandler,IEndDragHandler*/
{
    public Action<Transform> onLeftClick { get; set; }
    public Action<Transform> onDoubleClick { get; set; }
    public Action<Transform> onMiddleClick { get; set; }
    public Action<Transform> onRightClick { get; set; }
    public Action<Transform> onEnter { get; set; }
    public Action<Transform> onExit { get; set; }
    public Action<Transform> onUp { get; set; }
    public Action<Transform> onDown { get; set; }
    public Action<Transform> onDeselect { get; set; }
    //public Action<Transform> onBeginDrag { get; set; }
    //public Action<Transform> onDrag { get; set; }
    //public Action<Transform> onEndDrag { get; set; }

    public override void OnPointerClick (PointerEventData eventData)
    {
        base. OnPointerClick(eventData);
        if (eventData. button == PointerEventData. InputButton. Left)
        {
            if (eventData. clickCount == 1)
            {
                onLeftClick?.Invoke(transform);
            }
            else if (eventData. clickCount == 2)
            {
                onDoubleClick?.Invoke(transform);
            }
        }
        else if (eventData. button == PointerEventData. InputButton. Middle)
        {
            onMiddleClick?.Invoke(transform);
        }
        else if (eventData. button == PointerEventData. InputButton. Right)
        {
            onRightClick?.Invoke(transform);
        }
    }

    public override void OnPointerEnter (PointerEventData eventData)
    {
        base. OnPointerEnter(eventData);
        onEnter?.Invoke(transform);
    }

    public override void OnPointerExit (PointerEventData eventData)
    {
        base. OnPointerExit(eventData);
        onExit?.Invoke(transform);
    }

    public override void OnPointerUp (PointerEventData eventData)
    {
        base. OnPointerUp(eventData);
        onUp?.Invoke(transform);
    }

    public override void OnPointerDown (PointerEventData eventData)
    {
        base. OnPointerDown(eventData);
        onDown?.Invoke(transform);
    }

    public override void OnDeselect (BaseEventData eventData)
    {
        base. OnDeselect(eventData);
        onDeselect?.Invoke(transform);
    }

    //public void OnBeginDrag (PointerEventData eventData)
    //{
    //    onBeginDrag?.Invoke(transform);
    //    //transform. position =eventData.position;
    //}

    //public void OnDrag ( PointerEventData eventData)
    //{
    //    onDrag?.Invoke(transform);
    //    //transform. position = eventData. position;
    //}

    //public void OnEndDrag (PointerEventData eventData)
    //{
    //    onEndDrag?.Invoke(transform);
    //    //transform. position = eventData. position;
    //}

    //public Action<GameObject> onLeftClick { get; set; }
    //public Action<GameObject> onDoubleClick { get; set; }
    //public Action<GameObject> onMiddleClick { get; set; }
    //public Action<GameObject> OnRightClick { get; set; }
    //public Action<GameObject> onEnter { get; set; }
    //public Action<GameObject> onExit { get; set; }
    //public Action<GameObject> onUp { get; set; }
    //public Action<GameObject> onDown { get; set; }
    //public Action<GameObject> onDeselect { get; set; }

    //public override void OnPointerClick(PointerEventData eventData) {
    //    base.OnPointerClick(eventData);
    //    if (eventData.button == PointerEventData.InputButton.Left) {
    //        if (eventData.clickCount == 1) {
    //            if (onLeftClick != null)onLeftClick(gameObject);
    //        } else if (eventData.clickCount == 2)
    //            if (onDoubleClick != null) onDoubleClick(gameObject);
    //    } else if (eventData.button == PointerEventData.InputButton.Middle) {
    //        if (onMiddleClick != null) onMiddleClick(gameObject);
    //    } else if (eventData.button == PointerEventData.InputButton.Right) {
    //        if (OnRightClick != null) OnRightClick(gameObject);
    //    }
    //}

    //public override void OnPointerEnter(PointerEventData eventData) {
    //    base.OnPointerEnter(eventData);
    //    if (onEnter != null) onEnter(gameObject);
    //}

    //public override void OnPointerExit(PointerEventData eventData) {
    //    base.OnPointerExit(eventData);
    //    if (onExit != null) onExit(gameObject);
    //}

    //public override void OnPointerUp(PointerEventData eventData) {
    //    base.OnPointerUp(eventData);
    //    if (onUp != null) onUp(gameObject);
    //}

    //public override void OnPointerDown(PointerEventData eventData) {
    //    base.OnPointerDown(eventData);
    //    if (onDown != null) onDown(gameObject);
    //}

    //public override void OnDeselect(BaseEventData eventData) {
    //    base.OnDeselect(eventData);
    //    if (onDeselect != null) onDeselect(gameObject);
    //}
}
