function AddData() {
    $('#h5Name').text('Thêm mới đơn vị');
    $('#mdAdd').modal("show");
    $('#hdID').val(0);
    $('#txtMaDonVi').val('');
    $('#txtTenDonVi').val('');
    $('#txtUrl').val('');
}

function Edit(id) {
    $('#h5Name').text('Chỉnh sửa thông tin đơn vị');
    $.ajax({
        url: '/Admin/PortalLib/GetData',
        data: { id: id },
        cache: false,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                $('#hdID').val(id);
                $('#txtMaDonVi').val(response.data.MaDonVi);
                $('#txtTenDonVi').val(response.data.TenDonVi);
                $('#txtUrl').val(response.data.Url);
            }
            else {
                toastr.error('Có lỗi xảy ra', '', { timeOut: 2000 });
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
    $('#mdAdd').modal("show");
}

function SaveData() {
    var kt = true;
    var ID = $('#hdID').val();
    var MaDonVi = $('#txtMaDonVi').val();
    var TenDonVi = $('#txtTenDonVi').val();
    var Url = $('#txtUrl').val();
    if (MaDonVi.trim() == '') {
        toastr.warning('Chưa nhập mã đơn vị!', '', { timeOut: 1000 });
        $('#txtMaDonVi').focus();
        kt = false;
    }
    else if (TenDonVi.trim() == '') {
        toastr.warning('Chưa nhập tên đơn vị!', '', { timeOut: 1000 });
        $('#txtTenDonVi').focus();
        kt = false;
    }
    else if (Url.trim() == '') {
        toastr.warning('Chưa nhập cổng!', '', { timeOut: 1000 });
        $('#txtUrl').focus();
        kt = false;
    }
    if (kt == true) {
        var chk = $('input[name=r2]:checked').val();
        var formData = new FormData();
        formData.append("ID", ID);
        formData.append("MaDonVi", MaDonVi);
        formData.append("TenDonVi", TenDonVi);
        formData.append("Url", Url);
        formData.append("check", chk);
        $.ajax({
            url: '/Admin/PortalLib/SaveData',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    if (ID == 0) {
                        toastr.success('Thêm mới thành công', '', { timeOut: 1000 });
                    }
                    else {
                        toastr.success('Cập nhật thành công', '', { timeOut: 1000 });
                    }
                    setTimeout(function () {
                        location.reload();
                    }, 1000);
                }
                else {
                    toastr.error('Có lỗi xảy ra, cập nhật không thành công', '', { timeOut: 2000 });
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}

function DelData(id) {
    $('hdID_Del').val(id);
    $('#mdDel').modal("show");
}

function Delete() {
    var id = $('hdID_Del').val();
    $.ajax({
        url: '/Admin/PortalLib/DelData',
        data: { id: id },
        cache: false,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                toastr.success('Xóa thành công', '', { timeOut: 1000 });
                setTimeout(function () {
                    location.reload();
                }, 1000);
            }
            else {
                toastr.error('Có lỗi xảy ra', '', { timeOut: 2000 });
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function Change(id, status) {
    $.ajax({
        url: '/Admin/PortalLib/Change',
        data: {
            id: id,
            status: status
        },
        cache: false,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                toastr.success('Đổi trạng thái thành công', '', { timeOut: 1000 });
                setTimeout(function () {
                    location.reload();
                }, 1000);
            }
            else {
                toastr.error('Có lỗi xảy ra', '', { timeOut: 2000 });
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}