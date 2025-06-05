using UnityEngine;

public abstract class BaseDragController<T> : MonoBehaviour where T : Component
{
    public bool isWin = false;
    protected T draggableComponent;     // Component đang được kéo
    protected Vector3 mouseWorldPos;    // Vị trí chuột trong không gian thế giới
    protected Vector3 prevMouseWorldPos; // Vị trí chuột ở frame trước đó (dùng để tính delta)
    protected Vector3 mouseDelta;
    protected bool isDragging;          // Cờ xác định có đang kéo hay không

    protected virtual void Update()
    {
        if (isWin) return;

        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; 

        HandleDragInput();
    }

    /// <summary>
    /// Xử lý các sự kiện đầu vào từ chuột (nhấn, kéo, thả).
    /// </summary>
    protected virtual void HandleDragInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Chỉ thử bắt đầu kéo mới nếu chưa kéo đối tượng nào
            if (!isDragging)
            {
                TryStartDrag(mouseWorldPos);
            }
        }

        if (isDragging && draggableComponent != null)
        {
            mouseDelta = mouseWorldPos - prevMouseWorldPos;
            OnDragLogic(mouseWorldPos, mouseDelta); // Thực hiện logic kéo thả
            prevMouseWorldPos = mouseWorldPos;      // Cập nhật vị trí chuột trước đó
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Chỉ thử kết thúc kéo nếu đang thực sự kéo một đối tượng
            if (isDragging && draggableComponent != null)
            {
                TryEndDrag();
            }
        }
    }

    /// <summary>
    /// Cố gắng bắt đầu quá trình kéo dựa trên vị trí chuột.
    /// </summary>
    protected virtual void TryStartDrag(Vector3 position)
    {
        // Raycast để tìm collider tại vị trí chuột
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);

        if (hit.collider != null)
        {
            T component = hit.collider.GetComponent<T>();
            if (component != null && CanStartDragCondition(component))
            {
                draggableComponent = component;
                isDragging = true;
                prevMouseWorldPos = mouseWorldPos; // Khởi tạo cho việc tính delta ở frame đầu tiên kéo
                OnDragStarted(); // Gọi hook khi bắt đầu kéo
            }
        }
    }

    /// <summary>
    /// Cố gắng kết thúc quá trình kéo.
    /// </summary>
    protected virtual void TryEndDrag()
    {
        OnDragEnded(); // Gọi hook khi kết thúc kéo

        isDragging = false;
        draggableComponent = null; // Giải phóng tham chiếu đến đối tượng vừa kéo
    }

    /// <summary>
    /// Dừng việc kéo một cách có chủ đích (ví dụ: do một điều kiện trong game).
    /// </summary>
    protected void StopDragging()
    {
        if (isDragging && draggableComponent != null)
        {
            OnDragEnded(); // Cho phép lớp con dọn dẹp
        }
        isDragging = false;
        draggableComponent = null;
    }

    protected virtual bool CanStartDragCondition(T component)
    {
        return true; // Mặc định: nếu tìm thấy component và đúng kiểu T, có thể kéo
    }

    protected abstract void OnDragStarted();
    protected abstract void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition);
    protected abstract void OnDragEnded();
}