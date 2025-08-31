#### `Bounds.Intersects(...)`
- Là hàm có sẵn trong Unity, dùng để kiểm tra xem hai vùng không gian (`Bounds`) có giao nhau hay không.
#### `Render Queue ảnh hưởng đến thứ tự render toàn cục hơn Order in Layer`
- Render Queue khác nhau → Render Queue quyết định
- Render Queue bằng nhau → Order in Layer quyết định
#### `ADDRESSABLES `
- Load bằng đường dẫn project thật: Addressables.LoadAssetAsync<GameObject>("Assets/Path/To/Asset.prefab")
- Addressable Groups chỉ để đóng gói quản lý load/unload theo nhóm
- Tối ưu memory:
    - Load theo Group → chỉ nạp vùng nhớ cần thiết
    - Unload Group khi không dùng → giải phóng bộ nhớ
    - Chia nhỏ Group theo khu vực/chức năng → tiết kiệm RAM