using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonColliderChange : MonoBehaviour {

    [SerializeField, Tooltip("ColliderÇÃIsTriggerÇÃê›íË")]
    private bool isColliderTrigger;

    private Sprite _sprite;


    void Update() {
        if (this.GetComponent<SpriteRenderer>().sprite != _sprite) {
            _sprite = this.GetComponent<SpriteRenderer>().sprite;
            Destroy(this.GetComponent<PolygonCollider2D>());
            this.gameObject.AddComponent<PolygonCollider2D>();
        }//if
        if (!isColliderTrigger)
            return;
        if (!this.GetComponent<PolygonCollider2D>().isTrigger) {
            this.GetComponent<PolygonCollider2D>().isTrigger = true;
        }//if
    }//Update

}
