using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class ItemController : MonoBehaviour
{
    public GameController.ItemType itemType;
    public string itemName;
    public GameObject newItemPrefab;
    public GameController.goodEvil side;
    public int sidePoints;
    public TextMeshProUGUI text;

    public bool rotating;
    public float rotatingSpeed =20;

    public AudioClip getItemSound;

    private void OnTriggerEnter(Collider other) {
        if (other.tag=="Player"){
            GameController.GetItem(this);
            Destroy(this.gameObject);   
        }
    }

    private void Update() {
        if (rotating){
            transform.Rotate(rotatingSpeed * Time.deltaTime,0, 0);
        }
        if (text){
            text.text = sidePoints.ToString();
            GameController.instance.TransformToCamera(text.canvas.transform);
        }
    }
}
