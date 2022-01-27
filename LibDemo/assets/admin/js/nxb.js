function AddData() {
    $('#h5Name').text('Thêm mới nhà xuất bản');
    $('#mdAdd').modal("show");
    $('#hdID').val(0);
    $('#txtNXB').val('');
    $("input[name=r2][value=1]").prop("checked", true);
}

function Edit(id) {
    $('#h5Name').text('Chỉnh sửa thông tin nhà xuất bản');
    $.ajax({
        url: '/Admin/PublishingCompany/GetData',
        data: { id: id },
        cache: false,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                $('#hdID').val(response.data.ID);
                $('#txtNXB').val(response.data.TenNXB);
                $("input[name=r2]").prop("checked", false);
                $("input[name=r2][value='" + response.data.TrangThai + "']").prop("checked", true);
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
    var NXB = $('#txtNXB').val();
    var TrangThai = $('input[name=r2]:checked').val();
    if (NXB.trim() == '') {
        toastr.warning('Chưa nhập tên nhà xuất bản!', '', { timeOut: 1000 });
        $('#txtNXB').focus();
        kt = false;
    }
    if (kt == true) {
        var formData = new FormData();
        formData.append("ID", ID);
        formData.append("NXB", NXB);
        formData.append("TrangThai", TrangThai);
        $.ajax({
            url: '/Admin/PublishingCompany/SaveData',
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
                    toastr.error('Có lỗi xảy ra, thao tác không thành công', '', { timeOut: 2000 });
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}

function DelData(id) {
    $('#hdID_Del').val(id);
    $('#mdDel').modal("show");
}

function Delete() {
    var id = $('#hdID_Del').val();
    $.ajax({
        url: '/Admin/PublishingCompany/Delete',
        data: { id: id },
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
        url: '/Admin/PublishingCompany/Change',
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

function Search() {
    var search = $('#txtSearch').val();
    if (search.trim() != '') {
        location.href = '/Admin/PublishingCompany/Index?search=' + search;
    }
    else {
        location.href = '/Admin/PublishingCompany/Index';
    }
}