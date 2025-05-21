using UnityEngine;

/// <summary>
/// Lớp cơ sở trừu tượng để xử lý logic kéo thả đối tượng bằng chuột.
/// T là kiểu Component của đối tượng có thể kéo (ví dụ: Transform, Rigidbody2D, hoặc một script tùy chỉnh).
/// </summary>
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
        if (Camera.main == null)
        {
            Debug.LogError("Không tìm thấy Main Camera trong scene!");
            this.enabled = false; // Vô hiệu hóa script nếu không có camera chính
            return;
        }

        // Chuyển đổi vị trí chuột từ không gian màn hình sang không gian thế giới
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; // Mặc định cho 2D, bạn có thể tùy chỉnh nếu cần cho 3D

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

    // --- Các phương thức Hook để lớp con Override ---

    /// <summary>
    /// (Tùy chọn Override) Điều kiện tùy chỉnh bổ sung để kiểm tra xem có thể bắt đầu kéo component này không.
    /// Được gọi sau khi một component kiểu T được tìm thấy bởi raycast.
    /// </summary>
    protected virtual bool CanStartDragCondition(T component)
    {
        return true; // Mặc định: nếu tìm thấy component và đúng kiểu T, có thể kéo
    }

    /// <summary>
    /// (Tùy chọn Override) Được gọi một lần khi quá trình kéo bắt đầu thành công trên draggableComponent.
    /// Sử dụng để thiết lập ban đầu cho đối tượng kéo (ví dụ: thay đổi màu sắc, tắt vật lý tạm thời).
    /// </summary>
    protected virtual void OnDragStarted()
    {
        // Ví dụ: Debug.Log($"Bắt đầu kéo: {draggableComponent.gameObject.name}");
    }

    /// <summary>
    /// (Bắt buộc Override) Được gọi mỗi frame trong suốt quá trình kéo.
    /// Lớp con phải triển khai logic di chuyển thực tế của đối tượng tại đây.
    /// </summary>
    /// <param name="currentMousePosition">Vị trí chuột hiện tại trong không gian thế giới.</param>
    /// <param name="deltaMousePosition">Sự thay đổi vị trí chuột kể từ frame trước.</param>
    protected abstract void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition);

    /// <summary>
    /// (Tùy chọn Override) Được gọi một lần khi quá trình kéo kết thúc (ví dụ: khi nhả chuột).
    /// Sử dụng để dọn dẹp hoặc thực hiện các hành động cuối cùng trên draggableComponent (ví dụ: khôi phục vật lý, kích hoạt sự kiện game).
    /// </summary>
    protected virtual void OnDragEnded()
    {
        // Ví dụ: Debug.Log($"Kết thúc kéo: {draggableComponent.gameObject.name}");
    }
}