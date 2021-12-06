using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Instruction_Block : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private InstructionType type;
    private RectTransform rect;
    private ushort originPosition;
    private ushort originLevel;

    private Game game;

    void Start() {
        game = GameObject.Find("GameHandler").GetComponent<Game>();
        rect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        originPosition = (ushort)Mathf.Round((rect.localPosition.y + 35) / -75);
        originLevel = (ushort)Mathf.Round((rect.localPosition.x - 110) / 60);
        type = game.Players[0].code[originPosition].Type;
        rect.sizeDelta = new Vector2(400, rect.sizeDelta.y);
    }

    public void OnDrag(PointerEventData eventData) {
        Vector3 newPosition = new Vector3(eventData.position.x, eventData.position.y, transform.position.z);
        rect.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        ushort Position = Mathf.Round(rect.localPosition.y / -75) < 0 ? (ushort)0 : (ushort)Mathf.Round(rect.localPosition.y / -75);
        ushort Level = Mathf.Round((rect.localPosition.x - 210) / 240) < 0 ? (ushort)0 : (ushort)Mathf.Round((rect.localPosition.x - 210) / 240);
        if (rect.position.x > Screen.width / 2)
            game.Players[0].code.Delete(originPosition);
        else
            game.Players[0].code.Change(originPosition, Position, Level);
        game.UpdateCode();
    }
}
