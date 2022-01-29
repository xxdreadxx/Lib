////function AddData() {
////    $('#h5Name').text('Thêm mới tác giả');
////    $('#mdAdd').modal("show");
////    $('#hdID').val(0);
////    $('#txtHoTen').val('');
////    $('#txtMaTG').val('');
////    $('#txtDiaChi').val('');
////    $('#txtGioiThieu').val('');
////    $('#txtNgaySinh').val('');
////    $('#txtNgayMat').val('');
////    $("input[name=r2][value=1]").prop("checked", true);
////    document.getElementById("imgAvatar").src = "/assets/admin/images/no-image.jpg";
////}

function changeIMG() {
    var f = document.getElementById("fImage").files;
    if (f.length > 0) {
        var fileToLoad = f[0];
        var fileReader = new FileReader();
        fileReader.onload = function (fileLoadedEvent) {
            var srcData = fileLoadedEvent.target.result; // <--- data: base64
            document.getElementById("imgAvatar").src = srcData;
        }
        fileReader.readAsDataURL(fileToLoad);
    }
};

//function Edit(id) {
//    $('#h5Name').text('Chỉnh sửa thông tin tác giả');
//    $.ajax({
//        url: '/Admin/Reader/GetData',
//        data: { id: id },
//        cache: false,
//        type: 'GET',
//        dataType: 'json',
//        success: function (response) {
//            if (response.status == true) {
//                $('#hdID').val(response.data.ID);
//                $('#txtHoTen').val(response.data.HoTen);
//                $('#txtMaTG').val(response.data.CMTND);
//                $('#txtGioiThieu').val(response.data.SDT);
//                $('#txtDiaChi').val(response.data.DiaChi);
//                $('#txtNgayMat').val(response.data.Email);
//                $('#txtNgaySinh').val(response.data.NgaySinh);
//                document.getElementById("imgAvatar").src = "/assets/admin/images/no-image.jpg";
//                if (response.data.AnhDaiDien != null && response.data.AnhDaiDien != '') {
//                    document.getElementById("imgAvatar").src = response.data.AnhDaiDien;
//                }
//                $("input[name=r2]").prop("checked", false);
//                $("input[name=r2][value='" + response.data.TrangThai + "']").prop("checked", true);
//            }
//            else {
//                toastr.error('Có lỗi xảy ra', '', { timeOut: 2000 });
//            }
//        },
//        error: function (err) {
//            console.log(err);
//        }
//    });
//    $('#mdAdd').modal("show");
//}

//function SaveData() {
//    var kt = true;
//    var ID = $('#hdID').val();
//    var HoTen = $('#txtHoTen').val();
//    var MaTG = $('#txtMaTG').val();
//    var GioiThieu = $('#txtGioiThieu').val();
//    var DiaChi = $('#txtDiaChi').val();
//    var NgayMat = $('#txtNgayMat').val();
//    var NgaySinh = $('#txtNgaySinh').val();
//    var TrangThai = $('input[name=r2]:checked').val();
//    var Image = document.getElementById("fImage").files[0];
//    if (HoTen.trim() == '') {
//        toastr.warning('Chưa nhập tên tác giả!', '', { timeOut: 1000 });
//        $('#txtHoTen').focus();
//        kt = false;
//    }
//    else if (SDT.trim() == '') {
//        toastr.warning('Chưa nhập mã tác giả!', '', { timeOut: 1000 });
//        $('#txtSDT').focus();
//        kt = false;
//    }
//    else if (CMTND.trim() == '') {
//        toastr.warning('Chưa nhập cổng!', '', { timeOut: 1000 });
//        $('#txtMaTG').focus();
//        kt = false;
//    }
//    if (kt == true) {
//        var formData = new FormData();
//        formData.append("ID", ID);
//        formData.append("HoTen", HoTen);
//        formData.append("MaTG", MaTG);
//        formData.append("DiaChi", DiaChi);
//        formData.append("NgayMat", NgayMat);
//        formData.append("NgaySinh", NgaySinh);
//        formData.append("TrangThai", TrangThai);
//        formData.append("GioiThieu", GioiThieu);
//        formData.append("Image", Image);
//        $.ajax({
//            url: '/Admin/Author/SaveData',
//            data: formData,
//            cache: false,
//            contentType: false,
//            processData: false,
//            type: 'POST',
//            dataType: 'json',
//            success: function (response) {
//                if (response.status == true) {
//                    if (ID == 0) {
//                        toastr.success('Thêm mới thành công', '', { timeOut: 1000 });
//                    }
//                    else {
//                        toastr.success('Cập nhật thành công', '', { timeOut: 1000 });
//                    }
//                    setTimeout(function () {
//                        location.reload();
//                    }, 1000);
//                }
//                else {
//                    toastr.error('Có lỗi xảy ra, thao tác không thành công', '', { timeOut: 2000 });
//                }
//            },
//            error: function (err) {
//                console.log(err);
//            }
//        });
//    }
//}

function DelData(id) {
    $('#hdID_Del').val(id);
    $('#mdDel').modal("show");
}

function Delete() {
    var id = $('#hdID_Del').val();
    $.ajax({
        url: '/Admin/Reader/Delete',
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
        url: '/Admin/Reader/Change',
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
        location.href = '/Admin/Reader/Index?search=' + search;
    }
    else {
        location.href = '/Admin/Reader/Index';
    }
}

function AddDate(id) {
    $.ajax({
        url: '/Admin/Reader/UpdateDate',
        data: { id: id },
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                toastr.success('Gia hạn thành công', '', { timeOut: 1000 });
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