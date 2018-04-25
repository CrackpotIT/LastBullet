using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (SpriteRenderer))]
public class CartController : AbstractEnemySpawn {

    public float offSetY = 0;
    public float damping = 0.5f;

    public Sprite[] cartSprites;
    public GameObject[] itemList;

    

    private Vector3 velocity = Vector3.zero;
    private bool moveBack = false;

    public override void Initialize(Vector2 newTargetPosition, float directionX) {
        base.Initialize(newTargetPosition, directionX);

        // manipulate position
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y + offSetY);
        transform.position = newPos;
        // manipulate targetPosition
        Vector2 newTargetPos = new Vector2(targetPosition.x + (directionX * 3), targetPosition.y + offSetY);
        targetPosition = newTargetPos;

        // Random sprite design
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int randomIndex = Random.Range(0, cartSprites.Length);
        spriteRenderer.sprite = cartSprites[randomIndex];

        // update Order in Layer
        int maxSortingOrder = GetMaxSortingOrderFromSiblings();
        spriteRenderer.sortingOrder = maxSortingOrder + 2;
        
        // set random items on cart
        InitializeItems(maxSortingOrder + 3);
        
    }

    private int GetMaxSortingOrderFromSiblings() {
        int maxSortingOrder = 0;
        for (int i = 0; i < transform.parent.childCount; i++) {
            SpriteRenderer srChild = transform.parent.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
            if (srChild) {
                if (maxSortingOrder < srChild.sortingOrder) {
                    maxSortingOrder = srChild.sortingOrder;
                }
            }
        }
        return maxSortingOrder;
    }


    private void UpdateSortingOrderChildren(int sortingOrderOffset) {
        for (int i=0; i < transform.childCount; i++) {
            SpriteRenderer srChild = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
            if (srChild) {
                // Child hat SpriteRenderer
                srChild.sortingOrder = sortingOrderOffset;
            }
        }
    }

    private void InitializeItems(int sortingOrder) {
        Debug.Log("InitializeItems");
        int randomAmount = Random.Range(1, transform.childCount+1);
        Debug.Log("RandomAmount: " + randomAmount);
        int itemsPlaced = 0;
        int availablePositions = transform.childCount;
        Debug.Log("AvailablePos:" + availablePositions);
        for (int i = 0; i < availablePositions; i++) {
            if (randomAmount > itemsPlaced) {
                // there are items left
                int placeIt = Random.Range(0, 2);
                bool positionMustBePlaced = ((availablePositions - (i + 1)) <= (randomAmount - itemsPlaced));
                if (placeIt == 1 || positionMustBePlaced) {
                    itemsPlaced++;
                    PlaceItem(i, sortingOrder);
                }
            }             
        }
    }

    private void PlaceItem(int position, int sortingOrder) {
        Debug.Log("PlazeItem");
        GameObject randomItem = itemList[Random.Range(0, itemList.Length)];
        Debug.Log("Item:" + randomItem.name); 
        Transform positionTransform = transform.GetChild(position);
        GameObject item = Instantiate(randomItem, positionTransform);
        SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = sortingOrder;
    }

    void Update() {
        if (moveBack) {
            Vector2 t = new Vector2(transform.position.x + directionX * moveSpeed, transform.position.y);
            transform.position = Vector3.SmoothDamp(transform.position, t, ref velocity, damping, moveSpeed);
        } else {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping, moveSpeed);
            if (Mathf.Abs(velocity.x) < 0.01f) {
                moveBack = true;
            }
        }
    }

}
