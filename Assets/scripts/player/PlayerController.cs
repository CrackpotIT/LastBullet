using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float gunPosTopX;
    public float gunPosTopY;

    public float gunPosBottomX;
    public float gunPosBottomY;


    public float moveSpeed;
    public Gun gun;
    public Coordinates coordinates;

    [HideInInspector]
    public Inventory inventory = new Inventory();
    [HideInInspector]
    public int currentDirectionX = 1;  // 1=right, -1=left
    [HideInInspector]
    public int currentDirectionY = 1;  // 1=up, 0=moving, -1=down
    [HideInInspector]
    public bool isMoving = false;

    private Animator anim;

    private AbstractState currentState;        

    void Start() {
        SoundManager.SetGlobalVolume(.5f);
        anim = GetComponent<Animator>();

        currentState = new IdleTopState(AbstractState.ACTION.NA);
    }

    private void Update() {
        // handle StateMachine
        AbstractState newState = currentState.UpdateState(this, anim);
        if (newState != null) {
            currentState.OnExit(this, anim);
            currentState = newState;
            currentState.OnEnter(this, anim);
        }
    }


    public void SetDirectionX(int x) {
        currentDirectionX = x;
        transform.localScale = new Vector2(currentDirectionX, transform.localScale.y);
    }
 

    public void EventTopLeft() {
        gun.transform.localPosition = new Vector2(gunPosTopX, gunPosTopY);
        currentState.HandleEvent(AbstractState.ACTION.TOP_LEFT);
    }

    public void EventTopRight() {
        gun.transform.localPosition = new Vector2(gunPosTopX, gunPosTopY);
        currentState.HandleEvent(AbstractState.ACTION.TOP_RIGHT);
    }
    public void EventBottomLeft() {
        gun.transform.localPosition = new Vector2(gunPosBottomX, gunPosBottomY);
        currentState.HandleEvent(AbstractState.ACTION.BOTTOM_LEFT);
    }
    public void EventBottomRight() {
        gun.transform.localPosition = new Vector2(gunPosBottomX, gunPosBottomY);
        currentState.HandleEvent(AbstractState.ACTION.BOTTOM_RIGHT);
    }
    public void EventReload() {
        gun.Reload();
    }
    public void EventAnimation(string param) {
        currentState.HandleAnimEvent(param, this, anim);
    }


    public void MovePlayer(Vector3 endPosition) {
        StartCoroutine(MoveOverSpeed(gameObject, endPosition, moveSpeed));
    }
    

    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed) {
        // speed should be 1 unit per second
        isMoving = true;
        while (objectToMove.transform.position != end) {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        isMoving = false;
    }


}
