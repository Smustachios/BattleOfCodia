using UnityEngine;

public class ItemButton : MonoBehaviour, IAction
{
    private bool _itemSelected = false;
    private Camera _camera;
	public SpriteRenderer Frame;

	public void InvokeAction(Character character)
    {
        GetItem();
		Frame.color = Color.green;
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
                    //rayHit.collider.GetComponent<IItem>().UseItem();
					Frame.color = Color.white;
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
