using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_130Ctrl : BaseDragController<L130_MixerTap>
{
    public L130_Boy boy;
    public L130_ShowerHead showerHead;

    // Biến trạng thái hiện tại
    private bool isCurrentHot = false;
    private bool isCurrentCold = false;

    protected override void OnDragEnded()
    {
        if (isCurrentCold)
        {
            boy.PlayHotAnimation();
            boy.PlayColdAnimation();
            
        }
        else if (isCurrentHot)
        {
            boy.PlayHotAnimation();
        }
        else
        {
            boy.ChangeSpriteDefault();
            StartCoroutine(HandleWinCondition());
        }
    }

    float rotationAmount;
    float currentZ;
    float clampedZ;
    float newZ;
    bool inColdZone;
    bool inHotZone;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        rotationAmount = deltaMousePosition.x * 25f;

        currentZ = draggableComponent.transform.eulerAngles.z;
        clampedZ = currentZ > 180 ? currentZ - 360 : currentZ;
        newZ = Mathf.Clamp(clampedZ + rotationAmount, -60f, 60f);
        draggableComponent.transform.rotation = Quaternion.Euler(0, 0, newZ);

         inColdZone = (newZ >= 5f && newZ <= 60f); 
         inHotZone = (newZ <= -5f && newZ >= -60f);   
                                                         

        // Đảm bảo chỉ gọi hàm khi trạng thái thực sự thay đổi
        if (inColdZone) // Nếu đang ở vùng lạnh
        {
            if (!isCurrentCold) // Và trước đó không phải là lạnh
            {
                showerHead.ActiveColdEffect();
                boy.ChangeSpriteCold(); // Gọi sprite lạnh
                isCurrentHot = false;
                isCurrentCold = true;
                Debug.Log("Entered Cold Zone: Boy is now Cold.");
            }
        }
        else if (inHotZone) // Nếu đang ở vùng nóng
        {
            if (!isCurrentHot) // Và trước đó không phải là nóng
            {
                showerHead.ActiveHotEffect();
                boy.ChangeSpriteHot(); // Gọi sprite nóng
                isCurrentCold = false;
                isCurrentHot = true;
                Debug.Log("Entered Hot Zone: Boy is now Hot.");
            }
        }
        else 
        {
            if (isCurrentHot || isCurrentCold) // Chỉ thay đổi nếu trước đó đang ở trạng thái nóng hoặc lạnh
            {
                showerHead.DeactiveAllEffects();
                boy.ChangeSpriteDefault(); // Gọi sprite mặc định
                isCurrentHot = false;
                isCurrentCold = false;
                Debug.Log("Entered Neutral Zone: Boy is now Default.");
            }
        }
    }

    protected override void OnDragStarted()
    {
        boy.StopCurrentAnimation();
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

}