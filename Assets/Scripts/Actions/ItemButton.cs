using UnityEngine;

public class ItemButton : MonoBehaviour, IAction
{
    private bool _itemSelected = false;
    private Camera _camera;

    public void InvokeAction()
    {
        GetItem();
    }

    private void GetItem()
    {
        _itemSelected = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_itemSelected)
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));

            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.CompareTag("Item"))
                {
                    rayHit.collider.GetComponent<IItem>().UseItem();
                    _itemSelected = true;
                }
            }
        }
    }

    private void Awake()
    {
        _camera = Camera.main;
    }
}
