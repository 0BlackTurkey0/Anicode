using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private InstructionType type;
    private Text typeString;
    private RectTransform rect;
    private ushort originPosition;
    private ushort originLevel;

    private Game game;

    void Start() {
        game = GameObject.Find("GameHandler").GetComponent<Game>();
        typeString = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        rect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        switch (typeString.text) {
            case "Move":
                type = InstructionType.Move;
                break;
            case "Loop":
                type = InstructionType.Loop;
                break;
            case "If":
                type = InstructionType.If;
                break;
            case "Assign":
                type = InstructionType.Assign;
                break;
            case "Attack":
                type = InstructionType.Attack;
                break;
        }
        rect.sizeDelta = new Vector2(200, rect.sizeDelta.y);
        originPosition = (ushort)((rect.position.y - 1042.5) / -75);
        originLevel = (ushort)((rect.position.x - 105) / 60);
    }

    public void OnDrag(PointerEventData eventData) {
        Vector3 newPosition = new Vector3(eventData.position.x, eventData.position.y, transform.position.z);
        rect.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        ushort Position = (rect.position.y - 1080) / -75 < 0 ? (ushort)0 : (ushort)((rect.position.y - 1080) / -75);
        ushort Level = (rect.position.x - 105) / 120 < 0 ? (ushort)0 : (ushort)((rect.position.x - 105) / 120);
        if (Position > originPosition + 1 || Position < originPosition || Level != originLevel) {
            if (Position > originPosition) Position--;
            game.Players[0].code.Delete(originPosition);
            game.Players[0].code.Insert(type, Position, Level);
        }
        game.UpdateCode();
        Destroy(gameObject);
    }
}
