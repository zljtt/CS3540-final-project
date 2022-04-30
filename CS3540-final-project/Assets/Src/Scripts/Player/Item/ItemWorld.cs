using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represent the item in the world
public class ItemWorld : MonoBehaviour
{
    private float lowY;
    private bool isFalling;
    private float fallVelocity;


    private ItemStack item;

    public static GameObject InstantiateWorldItem(ItemStack item, Vector3 pos)
    {
        print("Spawning item");
        GameObject wi = Instantiate(Resources.Load("Prefabs/Items/ItemWorld"), pos, Quaternion.Euler(0, 0, 0)) as GameObject;
        wi.GetComponent<ItemWorld>().SetItemStack(item);
        wi.GetComponent<SpriteRenderer>().sprite = item.GetItem().GetSprite();
        wi.GetComponent<SpriteRenderer>().size = new Vector2(0.16f, 0.16f);
        return wi;
    }

    public void SetItemStack(ItemStack item)
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            transform.LookAt(player.transform);
        }
        if (!isFalling)
        {
            float offset = Mathf.PingPong(Time.time / 10, 0.2f);
            Vector3 newPos = transform.position;
            newPos.y = lowY + offset;
            transform.position = newPos;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.inventory.AddItem(item.GetItem(), item.GetAmount());
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
