using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float moveSpeed;
    public Coordinates coordinates;
    
    [HideInInspector]
    public int currentDirectionX = 1;  // 1=right, -1=left
    [HideInInspector]
    public bool isMoving = false;

    public AbstractState currentState;
    [HideInInspector]
    public GunModel gunModel;
    [HideInInspector]
    public PlayerModel playerModel;



    void Start() {
        GameObject instance = Instantiate(Resources.Load("player/weapons/GunModel_SuckSour", typeof(GameObject))) as GameObject;
        instance.transform.position = transform.position;
        instance.transform.parent = transform;

        gunModel = instance.GetComponent<GunModel>();

        SoundManager.SetGlobalVolume(.5f);

        currentState = new IdleTopState(AbstractState.ACTION.NA, this);

        // search model for player and gun
        //gunModel = transform.GetComponentInChildren<GunModel>();
        playerModel = transform.GetComponentInChildren<PlayerModel>();
    }

    private void Update() {
        // handle StateMachine
        AbstractState newState = currentState.UpdateState();
        if (newState != null) {
            currentState.OnExit();
            currentState = newState;
            currentState.OnEnter();
        }
    }


    public void SetDirectionX(int x) {
        currentDirectionX = x;
        transform.localScale = new Vector2(currentDirectionX, transform.localScale.y);
    }
 

    public void EventTopLeft() {
        currentState.HandleEvent(AbstractState.ACTION.TOP_LEFT);
    }

    public void EventTopRight() {
        currentState.HandleEvent(AbstractState.ACTION.TOP_RIGHT);
    }
    public void EventBottomLeft() {
        currentState.HandleEvent(AbstractState.ACTION.BOTTOM_LEFT);
    }
    public void EventBottomRight() {
        currentState.HandleEvent(AbstractState.ACTION.BOTTOM_RIGHT);
    }
    public void EventReload() {
        currentState.HandleEvent(AbstractState.ACTION.RELOAD);
    }
    public void EventLoot() {
        Debug.Log("EVENT LOOT");
        GameController.GetInstance().GetLoot();
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
