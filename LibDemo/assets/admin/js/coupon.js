function DelData(id) {
    $('#hdID_Del').val(id);
    $('#mdDel').modal("show");
}

function Delete() {
    var id = $('#hdID_Del').val();
    $.ajax({
        url: '/Admin/Coupon/Delete',
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

function Search() {
    var search = $('#txtSearch').val();
    if (search.trim() != '') {
        location.href = '/Admin/Coupon/Index?search=' + search;
    }
    else {
        location.href = '/Admin/Coupon/Index';
    }
}

function AddDate(id) {
    $.ajax({
        url: '/Admin/Coupon/UpdateDate',
        data: { id: id },
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                toastr.success('Gia hạn thêm 5 ngày thành công', '', { timeOut: 1000 });
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