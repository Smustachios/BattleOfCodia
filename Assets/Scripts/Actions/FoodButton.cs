using UnityEngine;

public class FoodButton : MonoBehaviour, IAction
{
	private bool _itemChosen = true;
	private Camera _camera;

	// Start item choosing action
	public void InvokeAction()
	{
		GetItem();
	}

	private void GetItem()
	{
		_itemChosen = false;
	}

	private void Update()
	{
		// Look for clicks on items
		if (Input.GetMouseButtonDown(0) && !_itemChosen)
		{
			RaycastHit2D rayHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));

			if (rayHit.collider != null)
			{
				// If clicked on consumable item use it and destroy it
				if (rayHit.collider.gameObject.CompareTag("Consumable"))
				{
					IItem item = rayHit.collider.gameObject.GetComponent<IItem>();
					item.UseItem();
					item.DestroyItem();
					_itemChosen = true;
				}
			}
		}
	}

	private void Awake()
	{
		_camera = Camera.main;
	}
}
