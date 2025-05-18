using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SnappingScrollRect : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    [Tooltip("Component ScrollRect mà script này điều khiển.")]
    public ScrollRect scrollRect;

    [Tooltip("RectTransform của panel Content bên trong ScrollRect.")]
    public RectTransform contentPanel;

    [Tooltip("Tốc độ mà content \"snap\" (bắt dính) vào vị trí mục tiêu sau khi kết thúc kéo.")]
    public float snapSpeed = 10f;

    [Tooltip("Xác định các giá trị anchoredPosition.x mục tiêu cho Content panel cho mỗi zone. \n" +
             "Ví dụ: Nếu Zone_1 ở vị trí local x=350 bên trong Content, và bạn muốn đưa Zone_1 ra đầu viewport, giá trị này nên là -350.")]
    public float[] snapTargetContentXPositions;

    [Tooltip("Chỉ số của zone sẽ được snap tới ban đầu khi Start.")]
    public int startingZoneIndex = 0;

    private bool isDragging = false;
    private int currentSnappedZoneIndex = 0;
    private Vector2 targetContentAnchoredPosition;
    private bool hasInitialized = false;

    void Start()
    {
        // Đảm bảo startingZoneIndex hợp lệ
        startingZoneIndex = Mathf.Clamp(startingZoneIndex, 0, snapTargetContentXPositions.Length - 1);

        // Snap ngay lập tức đến zone bắt đầu mà không cần Lerp
        SnapToZone(startingZoneIndex, true);
        hasInitialized = true;
    }

    void Update()
    {
        if (!hasInitialized || isDragging || contentPanel == null || snapTargetContentXPositions == null || snapTargetContentXPositions.Length == 0)
        {
            return;
        }

        // Nếu không kéo và chưa ở vị trí mục tiêu, Lerp về phía đó.
        // Sử dụng một khoảng cách nhỏ để tránh Lerp vô hạn do sai số float.
        if (Vector2.Distance(contentPanel.anchoredPosition, targetContentAnchoredPosition) > 0.01f)
        {
            // Sử dụng Time.unscaledDeltaTime để việc Lerp không bị ảnh hưởng bởi Time.timeScale (hữu ích cho UI)
            Vector2 newPosition = Vector2.Lerp(contentPanel.anchoredPosition, targetContentAnchoredPosition, Time.unscaledDeltaTime * snapSpeed);
            contentPanel.anchoredPosition = newPosition;

            // Nếu rất gần, snap vào vị trí chính xác và dừng chuyển động của ScrollRect.
            if (Vector2.Distance(contentPanel.anchoredPosition, targetContentAnchoredPosition) < 0.01f)
            {
                contentPanel.anchoredPosition = targetContentAnchoredPosition;
                if (scrollRect != null) scrollRect.velocity = Vector2.zero;
            }
        }
        else
        {
            // Đảm bảo nó ở chính xác vị trí mục tiêu nếu vòng lặp trên không làm được
            if (contentPanel.anchoredPosition != targetContentAnchoredPosition)
            {
                contentPanel.anchoredPosition = targetContentAnchoredPosition;
                if (scrollRect != null) scrollRect.velocity = Vector2.zero;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!hasInitialized) return;
        isDragging = true;
        if (scrollRect != null) scrollRect.inertia = true; // Cho phép inertia bình thường khi đang kéo
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!hasInitialized) return;
        isDragging = false;
        if (scrollRect != null) scrollRect.inertia = false; // Tắt inertia của ScrollRect vì chúng ta sẽ điều khiển
        SnapToClosest();
    }

    private void SnapToClosest()
    {
        if (contentPanel == null || snapTargetContentXPositions == null || snapTargetContentXPositions.Length == 0) return;

        float currentX = contentPanel.anchoredPosition.x;
        float closestTargetX = snapTargetContentXPositions[0];
        int closestIndex = 0;
        float minDistance = Mathf.Abs(currentX - closestTargetX);

        for (int i = 1; i < snapTargetContentXPositions.Length; i++)
        {
            float distance = Mathf.Abs(currentX - snapTargetContentXPositions[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestTargetX = snapTargetContentXPositions[i];
                closestIndex = i;
            }
        }

        currentSnappedZoneIndex = closestIndex;
        targetContentAnchoredPosition = new Vector2(closestTargetX, contentPanel.anchoredPosition.y);

        // Dừng mọi vận tốc hiện có ngay lập tức để bắt đầu snap mượt mà
        if (scrollRect != null) scrollRect.velocity = Vector2.zero;
        // Việc Lerp trong Update sẽ xử lý chuyển động.
    }

    // Phương thức public để cho phép snap đến một zone cụ thể thông qua code (ví dụ: nút bấm)
    public void SnapToZone(int zoneIndex, bool immediate = false)
    {
        if (contentPanel == null || snapTargetContentXPositions == null || zoneIndex < 0 || zoneIndex >= snapTargetContentXPositions.Length)
        {
            if (hasInitialized) Debug.LogWarning("SnapToZone: Chỉ số zone không hợp lệ hoặc có vấn đề cài đặt: " + zoneIndex);
            return;
        }

        isDragging = false; // Đảm bảo snap xảy ra
        if (scrollRect != null) scrollRect.inertia = false;

        currentSnappedZoneIndex = zoneIndex;
        targetContentAnchoredPosition = new Vector2(snapTargetContentXPositions[zoneIndex], contentPanel.anchoredPosition.y);
        if (scrollRect != null) scrollRect.velocity = Vector2.zero;

        if (immediate)
        {
            contentPanel.anchoredPosition = targetContentAnchoredPosition;
        }

    }
    public void SnapToNextZone()
    {
        if (!hasInitialized) return;
        int nextZoneIndex = currentSnappedZoneIndex + 1;
        if (nextZoneIndex >= snapTargetContentXPositions.Length)
        {
            return; 
        }
        SnapToZone(nextZoneIndex);
    }

    public void SnapToPreviousZone()
    {
        if (!hasInitialized) return;
        int prevZoneIndex = currentSnappedZoneIndex - 1;
        if (prevZoneIndex < 0)
        {
            return; 
        }
        SnapToZone(prevZoneIndex);
    }
}