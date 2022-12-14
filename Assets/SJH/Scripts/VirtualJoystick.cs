using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10, 150)]
    private float leverRange;
    private Vector2 inputDirection;
    private Vector2 inputRotation;
    private bool isInput;

    [SerializeField]
    private LHY_PlayerMove controller;
    public GameObject player;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        isInput = true;
        print("Beging");
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);

        print("Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        print(inputDirection);
        controller.MoveUpdate(Vector2.zero);
        
    }

    
    private void ControlJoystickLever(PointerEventData eventData)
    {
        Vector2 inputPos = eventData.position - rectTransform.anchoredPosition;
        Vector2 inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        //inputDirection = inputVector   / leverRange;
        inputDirection = inputVector / leverRange;
        
        //inputRotation.y += Mathf.Abs(inputDirection.y);
        
      
        
        
        
    }

    public void InputControlVector()
    {
        // 캐릭터에게 입력벡터를 전달
        controller.MoveUpdate(inputDirection);
        print(inputDirection.x + "/" + inputDirection.y);
        //player.transform.forward = -controller.MoveUpdate(inputDirection);



    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInput)
        {
            InputControlVector();
        }

        if(GameObject.Find("Player(Clone)").GetComponent<LHY_PlayerMove>())
        {
            player = GameObject.Find("Player(Clone)");
            controller = GameObject.Find("Player(Clone)").GetComponent<LHY_PlayerMove>();
        //player.transform.rotation = Quaternion.Euler(player.transform.rotation.x, inputRotation.y , player.transform.rotation.z);
        }
    }
}
