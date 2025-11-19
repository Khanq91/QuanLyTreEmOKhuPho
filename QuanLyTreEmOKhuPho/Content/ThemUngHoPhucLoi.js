try {
    const rawData = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(ViewBag.DsUngHo));

    console.log("=== DEBUG KHỞI TẠO ===");
    console.log("Raw data type:", typeof rawData);
    console.log("Raw data:", rawData);

    // Xử lý nhiều trường hợp cấu trúc dữ liệu
    if (rawData && typeof rawData === 'object') {
        if (Array.isArray(rawData)) {
            // Trường hợp 1: rawData đã là array
            dsUngHo = rawData;
        } else if (Array.isArray(rawData.value)) {
            // Trường hợp 2: rawData.value là array
            dsUngHo = rawData.value;
        } else if (Array.isArray(rawData.data)) {
            // Trường hợp 3: rawData.data là array
            dsUngHo = rawData.data;
        } else if (rawData.$values && Array.isArray(rawData.$values)) {
            // Trường hợp 4: JSON.NET reference handling
            dsUngHo = rawData.$values;
        } else {
            console.warn("⚠️ Cấu trúc dữ liệu không mong đợi:", rawData);
            dsUngHo = [];
        }
    } else if (rawData === null || rawData === undefined) {
        console.warn("⚠️ Không có dữ liệu");
        dsUngHo = [];
    } else {
        dsUngHo = [];
    }

    console.log("Số lượng ủng hộ:", dsUngHo.length);
    if (dsUngHo.length > 0) {
        console.log("Dữ liệu mẫu:", dsUngHo[0]);
        console.log("Các property:", Object.keys(dsUngHo[0]));
    }
    console.log("=====================");

} catch (error) {
    console.error("❌ Lỗi parse dữ liệu:", error);
    dsUngHo = [];
}

// ============================================
// HÀM HỖ TRỢ LẤY PROPERTY (hỗ trợ cả camelCase và PascalCase)
// ============================================
function getProp(obj, propName) {
    if (!obj) return null;

    // Thử camelCase trước
    if (obj[propName] !== undefined) return obj[propName];

    // Thử PascalCase (viết hoa chữ cái đầu)
    const pascalCase = propName.charAt(0).toUpperCase() + propName.slice(1);
    if (obj[pascalCase] !== undefined) return obj[pascalCase];

    return null;
}

// ============================================
// KHỞI TẠO KHI TRANG LOAD XONG
// ============================================
document.addEventListener("DOMContentLoaded", function () {
    console.log("✅ DOM đã load xong");

    const searchInput = document.getElementById("searchUngHo");
    const searchResults = document.getElementById("searchResults");

    if (!searchInput || !searchResults) {
        console.error("❌ Không tìm thấy element searchUngHo hoặc searchResults");
        return;
    }

    // ===== SỰ KIỆN INPUT =====
    searchInput.addEventListener("input", function () {
        const keyword = this.value.trim().toLowerCase();

        console.log("🔍 Nhập từ khóa:", keyword);

        if (keyword === "") {
            searchResults.innerHTML = "";
            searchResults.classList.remove("show");
            return;
        }

        // ===== KIỂM TRA DỮ LIỆU =====
        if (!dsUngHo || dsUngHo.length === 0) {
            console.warn("⚠️ Không có dữ liệu để tìm kiếm");
            searchResults.innerHTML = `
                    <div class="no-results">
                        <i class="fa-solid fa-exclamation-circle"></i>
                        <span>Không có dữ liệu ủng hộ</span>
                    </div>
                `;
            searchResults.classList.add("show");
            return;
        }

        // ===== TÌM KIẾM ĐA TRƯỜNG (hỗ trợ cả camelCase và PascalCase) =====
        const filtered = dsUngHo.filter(x => {
            if (!x) return false;

            const searchFields = [
                getProp(x, 'ghiChu'),
                getProp(x, 'tenManhThuongQuan'),
                getProp(x, 'tenVatPham'),
                getProp(x, 'doiTuong'),
                getProp(x, 'loaiUngHo'),
                getProp(x, 'tenKhuPho')
            ];

            const matched = searchFields.some(field =>
                field && String(field).toLowerCase().includes(keyword)
            );

            if (matched) {
                console.log("✓ Tìm thấy:", getProp(x, 'ghiChu'));
            }

            return matched;
        });

        console.log(`📊 Kết quả: ${filtered.length}/${dsUngHo.length} items`);
        renderSearchResults(filtered);
    });

    // ===== ĐÓNG DROPDOWN KHI CLICK BÊN NGOÀI =====
    document.addEventListener("click", function (e) {
        if (!e.target.closest(".search-ungho-wrapper")) {
            searchResults.classList.remove("show");
        }
    });

    console.log("✅ Khởi tạo hoàn tất");
});

// ============================================
// RENDER KẾT QUẢ TÌM KIẾM
// ============================================
function renderSearchResults(results) {
    const searchResults = document.getElementById("searchResults");
    searchResults.innerHTML = "";

    if (!results || results.length === 0) {
        searchResults.innerHTML = `
                <div class="no-results">
                    <i class="fa-solid fa-exclamation-circle"></i>
                    <span>Không tìm thấy kết quả nào</span>
                </div>
            `;
        searchResults.classList.add("show");
        return;
    }

    results.forEach(uh => {
        const item = document.createElement("div");
        item.className = "search-result-item";
        item.onclick = () => selectUngHo(uh);

        const ghiChu = getProp(uh, 'ghiChu') || 'Không có ghi chú';
        const tenMTQ = getProp(uh, 'tenManhThuongQuan') || 'N/A';
        // ✅ Ưu tiên dùng ngày đã format từ API
        const ngayDisplay = getProp(uh, 'ngayUngHoDisplay') || formatDate(getProp(uh, 'ngayUngHo'));
        const soLuongConLai = getProp(uh, 'soLuongConLai') || 0;

        item.innerHTML = `
                <div class="result-title">${ghiChu}</div>
                <div class="result-meta">
                    <span><i class="fa-solid fa-user"></i> ${tenMTQ}</span>
                    <span><i class="fa-solid fa-calendar"></i> ${ngayDisplay}</span>
                    <span><i class="fa-solid fa-box"></i> Còn: ${soLuongConLai}</span>
                </div>
            `;

        searchResults.appendChild(item);
    });

    searchResults.classList.add("show");
}

// ============================================
// FORMAT NGÀY THÁNG
// ============================================
function formatDate(dateStr) {
    if (!dateStr) return "N/A";

    try {
        // Xử lý nhiều format từ C# DateTime
        let d;

        // Nếu là string ISO hoặc DateTime từ C#
        if (typeof dateStr === 'string') {
            // Loại bỏ timezone nếu có và parse
            const cleanDate = dateStr.replace('T', ' ').split('.')[0];
            d = new Date(cleanDate);
        } else if (dateStr instanceof Date) {
            d = dateStr;
        } else {
            // Thử parse trực tiếp
            d = new Date(dateStr);
        }

        // Kiểm tra valid date
        if (isNaN(d.getTime())) {
            console.warn("Invalid date:", dateStr);
            return "N/A";
        }

        const day = String(d.getDate()).padStart(2, '0');
        const month = String(d.getMonth() + 1).padStart(2, '0');
        const year = d.getFullYear();

        return `${day}/${month}/${year}`;
    } catch (e) {
        console.error("Lỗi format date:", dateStr, e);
        return "N/A";
    }
}

// ============================================
// CHỌN ỦNG HỘ TỪ DROPDOWN
// ============================================
function selectUngHo(item) {
    selectedUngHo = item;

    // Điền vào ô input
    document.getElementById("searchUngHo").value = getProp(item, 'ghiChu') || '';

    // Đóng dropdown
    document.getElementById("searchResults").innerHTML = "";
    document.getElementById("searchResults").classList.remove("show");

    // Hiển thị thông tin chi tiết
    renderUngHoInfo(item);

    // Enable form
    enableForm();

    // Hiển thị alert
    const alertEl = document.getElementById("alertConLai");
    const soLuongConLai = getProp(item, 'soLuongConLai') || 0;
    const tenVatPham = getProp(item, 'tenVatPham') || '';

    alertEl.style.display = "flex";
    alertEl.innerHTML = `
            <i class="fa-solid fa-exclamation-triangle"></i>
            <span>Còn lại <strong id="conLaiAlert">${soLuongConLai} ${tenVatPham}</strong> có thể phân phát</span>
        `;

    console.log("✅ Đã chọn ủng hộ:", item);
}

// ============================================
// HIỂN THỊ THÔNG TIN ỦNG HỘ ĐÃ CHỌN
// ============================================
function renderUngHoInfo(uh) {
    const container = document.getElementById("ungHoInfo");

    const tenMTQ = getProp(uh, 'tenManhThuongQuan') || 'N/A';
    const loaiUngHo = getProp(uh, 'loaiUngHo') || 'N/A';
    const tenVatPham = getProp(uh, 'tenVatPham') || 'N/A';
    const soLuongVatPham = getProp(uh, 'soLuongVatPham') || 0;
    const soLuongConLai = getProp(uh, 'soLuongConLai') || 0;
    const doiTuong = getProp(uh, 'doiTuong') || 'N/A';
    const tenKhuPho = getProp(uh, 'tenKhuPho') || 'N/A';
    // ✅ Ưu tiên dùng ngày đã format từ API
    const ngayDisplay = getProp(uh, 'ngayUngHoDisplay') || formatDate(getProp(uh, 'ngayUngHo'));
    const daPhat = soLuongVatPham - soLuongConLai;

    container.innerHTML = `
            <div class="info-row">
                <span class="info-label">Mạnh thường quân</span>
                <span class="info-value">${tenMTQ}</span>
            </div>
            <div class="info-row">
                <span class="info-label">Loại ủng hộ</span>
                <span class="info-value">
                    <span class="badge badge-${loaiUngHo === 'Tiền mặt' ? 'success' : 'warning'}">
                        ${loaiUngHo}
                    </span>
                </span>
            </div>
            <div class="info-row">
                <span class="info-label">Vật phẩm</span>
                <span class="info-value">${tenVatPham}</span>
            </div>
            <div class="info-row">
                <span class="info-label">Số lượng tổng</span>
                <span class="info-value">${soLuongVatPham}</span>
            </div>
            <div class="info-row">
                <span class="info-label">Đã phát</span>
                <span class="info-value" style="color: var(--success);">
                    ${daPhat}
                </span>
            </div>
            <div class="info-row">
                <span class="info-label">Còn lại</span>
                <span class="info-value" id="soLuongConLaiDisplay" style="color: var(--warning);">
                    ${soLuongConLai}
                </span>
            </div>
            <div class="info-row">
                <span class="info-label">Đối tượng</span>
                <span class="info-value">${doiTuong}</span>
            </div>
            <div class="info-row">
                <span class="info-label">Khu vực</span>
                <span class="info-value">${tenKhuPho}</span>
            </div>
            <div class="info-row">
                <span class="info-label">Ngày ủng hộ</span>
                <span class="info-value">${ngayDisplay}</span>
            </div>
            <div class="change-ungho-btn" onclick="resetSearch()">
                <i class="fa-solid fa-exchange-alt"></i> Đổi đợt ủng hộ khác
            </div>
        `;
}

// ============================================
// RESET TÌM KIẾM
// ============================================
function resetSearch() {
    document.getElementById("searchUngHo").value = "";
    document.getElementById("searchUngHo").focus();
    selectedUngHo = null;

    document.getElementById("ungHoInfo").innerHTML = `
            <div class="empty-state">
                <i class="fa-solid fa-hand-holding-heart"></i>
                <p>Chưa chọn đợt ủng hộ</p>
                <p style="font-size: 12px; margin-top: 8px;">Vui lòng tìm kiếm và chọn đợt ủng hộ</p>
            </div>
        `;

    document.getElementById("alertConLai").style.display = "none";

    disableForm();
}

// ============================================
// ENABLE/DISABLE FORM
// ============================================
function enableForm() {
    document.getElementById("loaiHoTro").disabled = false;
    document.getElementById("ngayCap").disabled = false;
    document.getElementById("nguoiChiuTrachNhiem").disabled = false;
    document.getElementById("trangThaiPhat").disabled = false;
    document.getElementById("moTa").disabled = false;
    document.getElementById("ghiChuTNV").disabled = false;
    document.getElementById("btnChonTreEm").disabled = false;
    document.getElementById("btnSubmit").disabled = false;
}

function disableForm() {
    document.getElementById("loaiHoTro").disabled = true;
    document.getElementById("ngayCap").disabled = true;
    document.getElementById("nguoiChiuTrachNhiem").disabled = true;
    document.getElementById("trangThaiPhat").disabled = true;
    document.getElementById("moTa").disabled = true;
    document.getElementById("ghiChuTNV").disabled = true;
    document.getElementById("btnChonTreEm").disabled = true;
    document.getElementById("btnSubmit").disabled = true;
}

// ============================================
// MODAL FUNCTIONS (placeholders - bạn cần implement)
// ============================================
function openChildModal() {
    console.log("Open child modal");
    // TODO: Implement modal logic
}

function closeChildModal() {
    console.log("Close child modal");
    // TODO: Implement modal logic
}

function confirmSelection() {
    console.log("Confirm selection");
    // TODO: Implement confirmation logic
}
// ============================================
// THÔNG BÁO (NẾU CHƯA CÓ)
// ============================================
function showNotification(message, type = 'success') {
    const notification = document.getElementById('notification');
    const text = document.getElementById('notificationText');

    if (!notification || !text) {
        console.warn("⚠️ Không tìm thấy notification element");
        console.log(message);
        return;
    }

    notification.className = `notification ${type} show`;
    text.textContent = message;

    // Auto hide sau 3s
    setTimeout(() => {
        notification.classList.remove('show');
    }, 3000);
}

// ============================================
// HIỂN THỊ THÔNG BÁO LỖI CHI TIẾT (MODAL CUSTOM)
// ============================================
function showDetailedError(htmlContent) {
    // Tạo overlay
    const overlay = document.createElement('div');
    overlay.id = 'errorOverlay';
    overlay.style.cssText = `
        position: fixed;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background: rgba(0, 0, 0, 0.6);
        z-index: 10000;
        display: flex;
        align-items: center;
        justify-content: center;
        animation: fadeIn 0.2s ease;
    `;

    // Tạo error box
    const errorBox = document.createElement('div');
    errorBox.style.cssText = `
        background: white;
        border-radius: 12px;
        padding: 24px;
        max-width: 500px;
        width: 90%;
        box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
        animation: slideDown 0.3s ease;
        max-height: 80vh;
        overflow-y: auto;
    `;

    errorBox.innerHTML = `
        ${htmlContent}
        <div style="margin-top: 20px; text-align: right;">
            <button
                onclick="document.getElementById('errorOverlay').remove()"
                style="
                    background: var(--primary);
                    color: white;
                    border: none;
                    padding: 10px 24px;
                    border-radius: 8px;
                    font-weight: 600;
                    cursor: pointer;
                    font-size: 14px;
                "
            >
                <i class="fa-solid fa-check"></i> Đã hiểu
            </button>
        </div>
    `;

    overlay.appendChild(errorBox);
    document.body.appendChild(overlay);

    // Đóng khi click overlay
    overlay.addEventListener('click', function (e) {
        if (e.target === overlay) {
            overlay.remove();
        }
    });

    // Thêm CSS animation nếu chưa có
    if (!document.getElementById('errorAnimationStyle')) {
        const style = document.createElement('style');
        style.id = 'errorAnimationStyle';
        style.textContent = `
            @@keyframes fadeIn {
                from { opacity: 0; }
                to { opacity: 1; }
            }
            @@keyframes slideDown {
                from {
                    transform: translateY(-50px);
                    opacity: 0;
                }
                to {
                    transform: translateY(0);
                    opacity: 1;
                }
            }
        `;
        document.head.appendChild(style);
    }
}// ============================================
// KHAI BÁO BIẾN CHO MODULE TRẺ EM
// ============================================
let danhSachTreEm = [];
let selectedChildren = [];
let currentFilteredChildren = [];

// ============================================
// KHỞI TẠO DANH SÁCH TRẺ EM
// ============================================
function initDanhSachTreEm() {
    try {
        const rawData = @Html.Raw(Json.Encode(ViewBag.Lst_TreEm));

        console.log("=== DEBUG DANH SÁCH TRẺ EM ===");
        console.log("Raw data type:", typeof rawData);
        console.log("Raw data:", rawData);

        // Xử lý nhiều trường hợp cấu trúc dữ liệu
        if (rawData && typeof rawData === 'object') {
            if (Array.isArray(rawData)) {
                danhSachTreEm = rawData;
            } else if (Array.isArray(rawData.value)) {
                danhSachTreEm = rawData.value;
            } else if (Array.isArray(rawData.data)) {
                danhSachTreEm = rawData.data;
            } else if (rawData.$values && Array.isArray(rawData.$values)) {
                danhSachTreEm = rawData.$values;
            } else {
                console.warn("⚠️ Cấu trúc dữ liệu trẻ em không mong đợi:", rawData);
                danhSachTreEm = [];
            }
        } else {
            danhSachTreEm = [];
        }

        console.log("Số lượng trẻ em:", danhSachTreEm.length);
        if (danhSachTreEm.length > 0) {
            console.log("Dữ liệu trẻ mẫu:", danhSachTreEm[0]);
            console.log("Các property:", Object.keys(danhSachTreEm[0]));
        }
        console.log("=============================");

    } catch (error) {
        console.error("❌ Lỗi parse dữ liệu trẻ em:", error);
        danhSachTreEm = [];
    }
}

// ============================================
// MỞ MODAL CHỌN TRẺ EM
// ============================================
function openChildModal() {
    if (!selectedUngHo) {
        showNotification('Vui lòng chọn đợt ủng hộ trước!', 'error');
        return;
    }

    console.log("🔓 Mở modal chọn trẻ em");
    console.log("Đợt ủng hộ:", selectedUngHo);
    console.log("Số lượng còn lại:", selectedUngHo.soLuongConLai);

    // Render danh sách trẻ
    renderChildrenList();

    // Hiển thị modal
    const modal = document.getElementById("childModal");
    modal.classList.add("active");
    modal.style.display = "flex";

    // Focus vào ô tìm kiếm
    setTimeout(() => {
        document.getElementById("searchChild").focus();
    }, 100);
}

// ============================================
// ĐÓNG MODAL
// ============================================
function closeChildModal() {
    console.log("🔒 Đóng modal");
    const modal = document.getElementById("childModal");
    modal.classList.remove("active");
    modal.style.display = "none";

    // Reset search
    document.getElementById("searchChild").value = "";
}

// ============================================
// RENDER DANH SÁCH TRẺ EM TRONG MODAL
// ============================================
function renderChildrenList() {
    const tbody = document.getElementById("childrenList");

    if (!tbody) {
        console.error("❌ Không tìm thấy element childrenList");
        return;
    }

    tbody.innerHTML = "";

    if (!danhSachTreEm || danhSachTreEm.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="6" style="text-align: center; padding: 40px; color: var(--gray-600);">
                    <i class="fa-solid fa-inbox" style="font-size: 48px; margin-bottom: 12px; display: block;"></i>
                    Không có dữ liệu trẻ em
                </td>
            </tr>
        `;
        return;
    }

    console.log(`📋 Render ${danhSachTreEm.length} trẻ em`);
    currentFilteredChildren = [...danhSachTreEm];

    // Lấy số lượng còn lại để set max
    const soLuongConLai = getProp(selectedUngHo, 'soLuongConLai') || 0;

    danhSachTreEm.forEach(child => {
        const treEmID = getProp(child, 'treEmId') || getProp(child, 'treEmID');
        const hoTen = getProp(child, 'tenTreEm') || getProp(child, 'hoTen') || 'N/A';
        const ngaySinhDisplay = getProp(child, 'ngaySinhDisplay') || formatDate(getProp(child, 'ngaySinh'));
        const khuPho = getProp(child, 'khuPho') || getProp(child, 'tenKhuPho') || 'N/A';
        const tinhTrang = getProp(child, 'tinhTrang') || 'N/A';

        // Kiểm tra xem trẻ đã được chọn chưa
        const isSelected = selectedChildren.find(c => {
            const selectedID = getProp(c, 'treEmId') || getProp(c, 'treEmID');
            return selectedID === treEmID;
        });

        const row = document.createElement("tr");
        row.setAttribute("data-id", treEmID);
        row.innerHTML = `
            <td>
                <input
                    type="checkbox"
                    class="checkbox child-checkbox"
                    ${isSelected ? 'checked' : ''}
                    data-id="${treEmID}"
                    onchange="updateSelectedCount()"
                >
            </td>
            <td>${hoTen}</td>
            <td>${ngaySinhDisplay}</td>
            <td>${khuPho}</td>
            <td>${tinhTrang}</td>
            <td>
                <input
                    type="number"
                    class="form-control quantity-input"
                    value="${isSelected ? isSelected.soLuong : 1}"
                    min="1"
                    max="${soLuongConLai}"
                    data-id="${treEmID}"
                    onchange="validateQuantity(this)"
                    oninput="validateQuantity(this)"
                >
            </td>
        `;

        tbody.appendChild(row);
    });

    // Cập nhật số lượng đã chọn
    updateSelectedCount();

    console.log("✅ Đã render xong danh sách trẻ");
}

// ============================================
// TÌM KIẾM TRẺ EM TRONG MODAL
// ============================================
document.addEventListener('DOMContentLoaded', function () {
    // Khởi tạo danh sách trẻ em
    initDanhSachTreEm();

    const searchChildInput = document.getElementById("searchChild");
    if (searchChildInput) {
        searchChildInput.addEventListener("input", function (e) {
            const keyword = e.target.value.trim().toLowerCase();
            const rows = document.querySelectorAll("#childrenList tr");

            console.log("🔍 Tìm kiếm trẻ:", keyword);

            if (keyword === "") {
                rows.forEach(row => row.style.display = "");
                return;
            }

            let visibleCount = 0;
            rows.forEach(row => {
                const text = row.textContent.toLowerCase();
                const isVisible = text.includes(keyword);
                row.style.display = isVisible ? "" : "none";
                if (isVisible) visibleCount++;
            });

            console.log(`📊 Hiển thị ${visibleCount}/${rows.length} trẻ`);
        });
    }

    // Select All checkbox
    const selectAllCheckbox = document.getElementById("selectAll");
    if (selectAllCheckbox) {
        selectAllCheckbox.addEventListener("change", function () {
            const checkboxes = document.querySelectorAll(".child-checkbox");
            checkboxes.forEach(cb => {
                // Chỉ check những row đang hiển thị
                const row = cb.closest('tr');
                if (row && row.style.display !== 'none') {
                    cb.checked = this.checked;
                }
            });
            updateSelectedCount();
        });
    }
});

// ============================================
// CẬP NHẬT SỐ LƯỢNG ĐÃ CHỌN
// ============================================
function updateSelectedCount() {
    const checkboxes = document.querySelectorAll(".child-checkbox:checked");
    const count = checkboxes.length;

    const countElement = document.getElementById("selectedCount");
    if (countElement) {
        countElement.textContent = count;
    }

    console.log(`✓ Đã chọn ${count} trẻ`);
}

// ============================================
// VALIDATE SỐ LƯỢNG INPUT
// ============================================
function validateQuantity(input) {
    const value = parseInt(input.value) || 1;

    // Validate min
    if (value < 1) {
        input.value = 1;
        return;
    }

    // Lấy số lượng còn lại
    const soLuongConLai = getProp(selectedUngHo, 'soLuongConLai') || 0;

    // Tính tổng số lượng đã chọn (không bao gồm input hiện tại)
    const checkboxes = document.querySelectorAll(".child-checkbox:checked");
    let tongDaChon = 0;

    checkboxes.forEach(cb => {
        const inputOther = document.querySelector(`.quantity-input[data-id="${cb.dataset.id}"]`);
        if (inputOther && inputOther !== input) {
            tongDaChon += parseInt(inputOther.value) || 1;
        }
    });

    // Số lượng tối đa cho input này
    const maxAllowed = soLuongConLai - tongDaChon;

    console.log(`📊 Validate: Còn lại ${soLuongConLai}, Đã chọn ${tongDaChon}, Max cho input này: ${maxAllowed}`);

    if (maxAllowed <= 0) {
        input.value = 1;
        showNotification('Không đủ số lượng! Vui lòng bỏ chọn bớt trẻ khác', 'error');
        return;
    }

    if (value > maxAllowed) {
        input.value = maxAllowed;
        showNotification(`Chỉ còn ${maxAllowed} phần! Đã tự động điều chỉnh`, 'warning');
    }
}

// ============================================
// XÁC NHẬN CHỌN TRẺ
// ============================================
function confirmSelection() {
    const checkboxes = document.querySelectorAll(".child-checkbox:checked");

    if (checkboxes.length === 0) {
        showNotification('Vui lòng chọn ít nhất 1 trẻ em!', 'error');
        return;
    }

    selectedChildren = [];
    let tongSoLuong = 0;

    checkboxes.forEach(cb => {
        const treEmID = parseInt(cb.dataset.id);

        // Tìm trẻ trong danh sách
        const child = danhSachTreEm.find(c => {
            const childID = getProp(c, 'treEmId') || getProp(c, 'treEmID');
            return childID === treEmID;
        });

        if (!child) {
            console.warn("⚠️ Không tìm thấy trẻ với ID:", treEmID);
            return;
        }

        // Lấy số lượng
        const quantityInput = document.querySelector(`.quantity-input[data-id="${treEmID}"]`);
        const soLuong = parseInt(quantityInput.value) || 1;

        tongSoLuong += soLuong;

        // Thêm vào danh sách đã chọn
        selectedChildren.push({
            treEmID: treEmID,
            hoTen: getProp(child, 'tenTreEm') || getProp(child, 'hoTen'),
            ngaySinh: getProp(child, 'ngaySinh'),
            ngaySinhDisplay: getProp(child, 'ngaySinhDisplay') || formatDate(getProp(child, 'ngaySinh')),
            khuPho: getProp(child, 'khuPho') || getProp(child, 'tenKhuPho'),
            tinhTrang: getProp(child, 'tinhTrang'),
            soLuong: soLuong
        });
    });

    console.log("=== XÁC NHẬN CHỌN TRẺ ===");
    console.log("Số lượng trẻ:", selectedChildren.length);
    console.log("Tổng số lượng phát:", tongSoLuong);
    console.log("Số lượng còn lại:", selectedUngHo.soLuongConLai);

    // Kiểm tra số lượng
    if (tongSoLuong > selectedUngHo.soLuongConLai) {
        showNotification(
            `Không đủ số lượng! Chỉ còn ${selectedUngHo.soLuongConLai} ${selectedUngHo.tenVatPham}`,
            'error'
        );
        return;
    }

    // Render danh sách trẻ đã chọn
    renderSelectedChildren();

    // Cập nhật số lượng còn lại
    updateRemainingQuantity(tongSoLuong);

    // Đóng modal
    closeChildModal();

    showNotification(`Đã chọn ${selectedChildren.length} trẻ em (${tongSoLuong} phần)`, 'success');
    console.log("========================");
}

// ============================================
// RENDER DANH SÁCH TRẺ ĐÃ CHỌN
// ============================================
function renderSelectedChildren() {
    const container = document.getElementById("selectedChildren");

    if (!container) {
        console.error("❌ Không tìm thấy element selectedChildren");
        return;
    }

    if (selectedChildren.length === 0) {
        container.innerHTML = `
            <div class="empty-state">
                <i class="fa-solid fa-users"></i>
                <p>Chưa có trẻ em nào được chọn</p>
            </div>
        `;
        return;
    }

    container.innerHTML = "";

    selectedChildren.forEach(child => {
        // Lấy ID đúng (có thể là treEmID hoặc treEmId)
        const childID = child.treEmID || child.treEmId;

        const item = document.createElement("div");
        item.className = "child-item";
        item.innerHTML = `
            <div class="child-info">
                <div class="child-name">${child.hoTen}</div>
                <div class="child-meta">
                    <span><i class="fa-solid fa-location-dot"></i> ${child.khuPho}</span>
                    <span><i class="fa-solid fa-info-circle"></i> ${child.tinhTrang}</span>
                    <span><i class="fa-solid fa-box"></i> Số lượng: <strong>${child.soLuong}</strong></span>
                </div>
            </div>
            <button type="button" class="remove-btn" data-child-id="${childID}">
                <i class="fa-solid fa-times"></i>
            </button>
        `;

        // Thêm event listener cho nút xóa (an toàn hơn onclick)
        const removeBtn = item.querySelector('.remove-btn');
        removeBtn.addEventListener('click', function () {
            xoaTreEmDaChon(childID);
        });

        container.appendChild(item);
    });

    console.log(`✅ Đã render ${selectedChildren.length} trẻ đã chọn`);
}

// ============================================
// XÓA TRẺ KHỎI DANH SÁCH ĐÃ CHỌN
// ============================================
function xoaTreEmDaChon(treEmID) {
    console.log("🗑️ Đang xóa trẻ ID:", treEmID);
    console.log("Danh sách hiện tại:", selectedChildren);

    // Tìm trẻ với cả treEmID và treEmId
    const child = selectedChildren.find(c => {
        const childID = c.treEmID || c.treEmId;
        return childID === treEmID;
    });

    if (!child) {
        console.warn("⚠️ Không tìm thấy trẻ để xóa:", treEmID);
        console.log("Các ID trong danh sách:", selectedChildren.map(c => c.treEmID || c.treEmId));
        return;
    }

    console.log("✓ Tìm thấy trẻ:", child.hoTen, "- Số lượng:", child.soLuong);

    // Xóa khỏi danh sách (xử lý cả 2 trường hợp property name)
    selectedChildren = selectedChildren.filter(c => {
        const childID = c.treEmID || c.treEmId;
        return childID !== treEmID;
    });

    console.log("Còn lại:", selectedChildren.length, "trẻ");

    // Re-render
    renderSelectedChildren();

    // Tính lại tổng số lượng đang chọn
    const tongSoLuongDangChon = selectedChildren.reduce((sum, c) => sum + (parseInt(c.soLuong) || 0), 0);

    console.log(`📊 Sau khi xóa: Tổng đang chọn = ${tongSoLuongDangChon}`);

    // Cập nhật số lượng còn lại
    updateRemainingQuantity(tongSoLuongDangChon);

    showNotification(`Đã xóa ${child.hoTen}`, 'success');
}

// ============================================
// CẬP NHẬT SỐ LƯỢNG CÒN LẠI
// ============================================
function updateRemainingQuantity(tongSoLuongDangChon) {
    if (!selectedUngHo) {
        console.warn("⚠️ Chưa chọn đợt ủng hộ");
        return;
    }

    // GIỮ NGUYÊN SỐ LƯỢNG CÒN LẠI BAN ĐẦU - KHÔNG TRỪ
    const soLuongConLaiBanDau = getProp(selectedUngHo, 'soLuongConLai') || 0;

    console.log(`📊 Giữ nguyên số còn lại: ${soLuongConLaiBanDau} (không thay đổi)`);

    // Cập nhật trong thẻ hiển thị - GIỮ NGUYÊN
    const displayElement = document.getElementById("soLuongConLaiDisplay");
    if (displayElement) {
        displayElement.textContent = soLuongConLaiBanDau;
        displayElement.style.color = soLuongConLaiBanDau > 0 ? "var(--warning)" : "var(--danger)";
    }

    // Cập nhật trong alert - GIỮ NGUYÊN
    const tenVatPham = getProp(selectedUngHo, 'tenVatPham') || '';
    const alertElement = document.getElementById("conLaiAlert");
    if (alertElement) {
        alertElement.textContent = `${soLuongConLaiBanDau} ${tenVatPham}`;
    }

    console.log(`✅ Số còn lại không đổi: ${soLuongConLaiBanDau}`);
}

// ============================================
// ĐÓNG MODAL KHI CLICK BÊN NGOÀI
// ============================================
document.addEventListener('DOMContentLoaded', function () {
    const modal = document.getElementById("childModal");
    if (modal) {
        modal.addEventListener("click", function (e) {
            if (e.target === this) {
                closeChildModal();
            }
        });
    }
});

// ============================================
// HIỂN THỊ THÔNG BÁO LỖI CHI TIẾT (MODAL CUSTOM)
// ============================================
function showDetailedError(htmlContent) {
    // Tạo overlay
    const overlay = document.createElement('div');
    overlay.id = 'errorOverlay';
    overlay.style.cssText = `
        position: fixed;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background: rgba(0, 0, 0, 0.6);
        z-index: 10000;
        display: flex;
        align-items: center;
        justify-content: center;
        animation: fadeIn 0.2s ease;
    `;

    // Tạo error box
    const errorBox = document.createElement('div');
    errorBox.style.cssText = `
        background: white;
        border-radius: 12px;
        padding: 24px;
        max-width: 500px;
        width: 90%;
        box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
        animation: slideDown 0.3s ease;
        max-height: 80vh;
        overflow-y: auto;
    `;

    errorBox.innerHTML = `
        ${htmlContent}
        <div style="margin-top: 20px; text-align: right;">
            <button
                onclick="document.getElementById('errorOverlay').remove()"
                style="
                    background: var(--primary);
                    color: white;
                    border: none;
                    padding: 10px 24px;
                    border-radius: 8px;
                    font-weight: 600;
                    cursor: pointer;
                    font-size: 14px;
                "
            >
                <i class="fa-solid fa-check"></i> Đã hiểu
            </button>
        </div>
    `;

    overlay.appendChild(errorBox);
    document.body.appendChild(overlay);

    // Đóng khi click overlay
    overlay.addEventListener('click', function (e) {
        if (e.target === overlay) {
            overlay.remove();
        }
    });

    // Thêm CSS animation nếu chưa có
    if (!document.getElementById('errorAnimationStyle')) {
        const style = document.createElement('style');
        style.id = 'errorAnimationStyle';
        style.textContent = `
            @@keyframes fadeIn {
                from { opacity: 0; }
                to { opacity: 1; }
            }
            @@keyframes slideDown {
                from {
                    transform: translateY(-50px);
                    opacity: 0;
                }
                to {
                    transform: translateY(0);
                    opacity: 1;
                }
            }
        `;
        document.head.appendChild(style);
    }
}
// ============================================
// CẬP NHẬT HÀM ENABLE/DISABLE FORM
// ============================================
function enableForm() {
    // Thông tin hỗ trợ
    document.getElementById("loaiHoTro").disabled = false;
    document.getElementById("ngayCap").disabled = false;
    document.getElementById("nguoiChiuTrachNhiem").disabled = false;
    document.getElementById("trangThaiPhat").disabled = false;
    document.getElementById("moTa").disabled = false;
    document.getElementById("ghiChuTNV").disabled = false;

    // Thông tin quà tặng (optional)
    document.getElementById("tenQua").disabled = false;
    document.getElementById("donGia").disabled = false;
    document.getElementById("doiTuongNhan").disabled = false;
    document.getElementById("suKienId").disabled = false;
    document.getElementById("moTaQua").disabled = false;

    // Thông tin phân phát
    document.getElementById("ngayPhanPhat").disabled = false;
    document.getElementById("nguoiPhanPhat").disabled = false;
    document.getElementById("ghiChuPhanPhat").disabled = false;
    document.getElementById("ngayHenLai").disabled = false;

    // Buttons
    document.getElementById("btnChonTreEm").disabled = false;
    document.getElementById("btnSubmit").disabled = false;

    // Set ngày mặc định = hôm nay
    const today = new Date().toISOString().split('T')[0];
    if (!document.getElementById("ngayCap").value) {
        document.getElementById("ngayCap").value = today;
    }
    if (!document.getElementById("ngayPhanPhat").value) {
        document.getElementById("ngayPhanPhat").value = today;
    }
}

function disableForm() {
    // Thông tin hỗ trợ
    document.getElementById("loaiHoTro").disabled = true;
    document.getElementById("ngayCap").disabled = true;
    document.getElementById("nguoiChiuTrachNhiem").disabled = true;
    document.getElementById("trangThaiPhat").disabled = true;
    document.getElementById("moTa").disabled = true;
    document.getElementById("ghiChuTNV").disabled = true;

    // Thông tin quà tặng
    document.getElementById("tenQua").disabled = true;
    document.getElementById("donGia").disabled = true;
    document.getElementById("doiTuongNhan").disabled = true;
    document.getElementById("suKienId").disabled = true;
    document.getElementById("moTaQua").disabled = true;

    // Thông tin phân phát
    document.getElementById("ngayPhanPhat").disabled = true;
    document.getElementById("nguoiPhanPhat").disabled = true;
    document.getElementById("ghiChuPhanPat").disabled = true;
    document.getElementById("ngayHenLai").disabled = true;

    // Buttons
    document.getElementById("btnChonTreEm").disabled = true;
    document.getElementById("btnSubmit").disabled = true;
}
