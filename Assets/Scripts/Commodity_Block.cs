using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Commodity_Block : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private InstructionType type;
    private RectTransform rect;
    private Vector2 position;

    [SerializeField] private GameObject Code_Area;
    private Game game;

    void Start() {
        game = GameObject.Find("GameHandler").GetComponent<Game>();
        rect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        position = rect.position;
        ushort index = (ushort)Mathf.Round((rect.localPosition.y + 35) / -75);
        type = (InstructionType)index;
    }

    public void OnDrag(PointerEventData eventData) {
        Vector3 newPosition = new Vector3(eventData.position.x, eventData.position.y, transform.position.z);
        rect.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (rect.position.x < Screen.width / 2) {
            float width = (rect.localPosition.x - Code_Area.GetComponent<RectTransform>().localPosition.x + 1218);
            ushort Position = Mathf.Round(rect.localPosition.y / -75) < 0 ? (ushort)0 : (ushort)Mathf.Round(rect.localPosition.y / -75);
            ushort Level = Mathf.Round((width - 110) / 120) < 0 ? (ushort)0 : (ushort)Mathf.Round((width - 110) / 120);
            game.Players[0].code.Insert(type, Position, Level);
            game.UpdateCode();
        }
        rect.position = position;
    }
}
