using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represent the item in the world
public class ItemWorld : MonoBehaviour
{
    private float lowY;
    private bool isFalling;
    private float fallVelocity;


    private Item item;

    public static GameObject InstantiateWorldItem(Item item, Vector3 pos)
    {
        print("Spawning item");
        GameObject wi = Instantiate(Resources.Load("Prefabs/Items/ItemWorld"), pos, Quaternion.Euler(0, 0, 0)) as GameObject;
        wi.GetComponent<ItemWorld>().SetItem(item);
        wi.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
        return wi;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    // Start is called before the first frame update
    void Start()
    {
        isFalling = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        if (!isFalling)
        {
            float offset = Mathf.PingPong(Time.time / 10, 0.2f);
            Vector3 newPos = transform.position;
            newPos.y = lowY + offset;
            transform.position = newPos;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        print("Collided");
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInventory.inventory.AddItem(item, 1);
            Destroy(gameObject);
        }
        else if (isFalling && other.gameObject.isStatic)
        {
            isFalling = false;
            lowY = transform.position.y;
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }
}
