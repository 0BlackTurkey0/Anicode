using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Commodity_Block : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [SerializeField] private GameObject Code_Area;

    private InstructionType type;
    private RectTransform rect;
    private Vector2 position;

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
        if (rect.position.x < Screen.width / 2 && game.IncPurchaseCount() != -1) {
            float width = (rect.localPosition.x - Code_Area.GetComponent<RectTransform>().localPosition.x + 1218);
            ushort position = (Mathf.Round(rect.localPosition.y / -75) < 0) ? (ushort)0 : (ushort)Mathf.Round(rect.localPosition.y / -75);
            ushort level = (Mathf.Round((width - 210) / 240) < 0) ? (ushort)0 : (ushort)Mathf.Round((width - 210) / 240);
            int[] arg;
            switch (type) {
                case InstructionType.Move:
                    arg = new int[1];
                    arg[0] = transform.GetChild(0).GetChild(2).GetComponent<Dropdown>().value;
                    break;
                case InstructionType.Attack:
                    arg = null;
                    break;
                case InstructionType.Assign:
                    arg = new int[5];
                    for (int i = 0; i < 3; i++)
                        arg[i] = transform.GetChild(0).GetChild(i + 2).GetComponent<Dropdown>().value;
                    arg[3] = int.Parse(transform.GetChild(0).GetChild(5).GetComponent<InputField>().text);
                    arg[4] = transform.GetChild(0).GetChild(6).GetComponent<Dropdown>().value;
                    break;
                case InstructionType.If:
                    arg = new int[4];
                    for (int i = 0; i < 3; i++)
                        arg[i] = transform.GetChild(0).GetChild(i + 2).GetComponent<Dropdown>().value;
                    arg[3] = int.Parse(transform.GetChild(0).GetChild(5).GetComponent<InputField>().text);
                    break;
                case InstructionType.Loop:
                    arg = new int[4];
                    for (int i = 0; i < 3; i++)
                        arg[i] = transform.GetChild(0).GetChild(i + 2).GetComponent<Dropdown>().value;
                    arg[3] = int.Parse(transform.GetChild(0).GetChild(5).GetComponent<InputField>().text);
                    break;
                case InstructionType.Swap:
                    arg = new int[4];
                    arg[0] = transform.GetChild(0).GetChild(2).GetComponent<Dropdown>().value;
                    arg[1] = int.Parse(transform.GetChild(0).GetChild(3).GetComponent<InputField>().text);
                    arg[2] = transform.GetChild(0).GetChild(4).GetComponent<Dropdown>().value;
                    arg[3] = int.Parse(transform.GetChild(0).GetChild(5).GetComponent<InputField>().text);
                    break;
                default:
                    arg = null;
                    break;
            }
            game.Players[0].Code.Insert(type, position, level, arg);
            game.UpdateCode(0);
        }
        rect.position = position;
    }
}
