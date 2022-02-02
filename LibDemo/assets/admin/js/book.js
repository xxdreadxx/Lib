function AddData() {
    $('#h5Name').text('Thêm mới ấn phẩm');
    $('#mdAdd').modal("show");
    $('#hdID').val(0);
    $('#txtNhanDe').val('');
    $('#txtMaAP').val('');
    $('#txtGioiThieu').val('');
    $('#txtDongTG').val('');
    $('#ddlKieuAP').val(1);
    $('#txtSo').val('');
    $('#txtNgayXB').val('');
    $('#ddlTG').val(0);
    $('#ddlNXB').val(0);
    $('#ddlPLAP').val(0);
    document.getElementById("imgAvatar").src = "/assets/admin/images/no-image.jpg";
    $("input[name=r2]").prop("checked", false);
    $("input[name=r2][value=1]").prop("checked", true);
}

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

function Edit(id) {
    $('#h5Name').text('Chỉnh sửa thông tin ấn phẩm');
    $.ajax({
        url: '/Admin/Book/GetData',
        data: { id: id },
        cache: false,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                $('#hdID').val(response.data.ID);
                $('#txtNhanDe').val(response.data.NhanDe);
                $('#txtMaAP').val(response.data.MaAnPham);
                $('#txtGioiThieu').val(response.data.GioIThieu);
                $('#txtDongTG').val(response.data.DongTacGia);
                $('#ddlKieuAP').val(response.data.LKieuAP);
                $('#txtSo').val(response.data.So);
                $('#txtNgayXuatBan').val(response.data.NgayXuatBan);
                $('#ddlTG').val(response.data.IDTacGia);
                $('#ddlNXB').val(response.data.IDNXB);
                $('#ddlPLAP').val(response.data.IDPLAP);
                document.getElementById("imgAvatar").src = "/assets/admin/images/no-image.jpg";
                if (response.data.HinhAnh != null && response.data.HinhAnh != '') {
                    document.getElementById("imgAvatar").src = response.data.HinhAnh;
                }
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
    var NhanDe = $('#txtNhanDe').val();
    var MaAP = $('#txtMaAP').val();
    var GioiThieu = $('#txtGioiThieu').val();
    var DongTG = $('#txtDongTG').val();
    var KieuAP = $('#ddlKieuAP').val();
    var So = $('#txtSo').val();
    var NgayXB = $('#txtNgayXuatBan').val();
    var TG = $('#ddlTG').val();
    var NXB = $('#ddlNXB').val();
    var PLAP = $('#ddlPLAP').val();
    var TrangThai = $('input[name=r2]:checked').val();
    var Image = document.getElementById("fImage").files[0];
    if (NhanDe.trim() == '') {
        toastr.warning('Chưa nhập nhan đề!', '', { timeOut: 1000 });
        $('#txtNhanDe').focus();
        kt = false;
    }
    else if (MaAP.trim() == '') {
        toastr.warning('Chưa nhập mã ấn phẩm!', '', { timeOut: 1000 });
        $('#txtMaAP').focus();
        kt = false;
    }
    if (kt == true) {
        var formData = new FormData();
        formData.append("ID", ID);
        formData.append("NhanDe", NhanDe);
        formData.append("MaAP", MaAP);
        formData.append("GioiThieu", GioiThieu);
        formData.append("DongTG", DongTG);
        formData.append("KieuAP", KieuAP);
        formData.append("So", So);
        formData.append("NgayXB", NgayXB);
        formData.append("TG", TG);
        formData.append("NXB", NXB);
        formData.append("PLAP", PLAP);
        formData.append("TrangThai", TrangThai);
        formData.append("Image", Image);
        $.ajax({
            url: '/Admin/Book/SaveData',
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
        url: '/Admin/Book/GetCount',
        data: { id: id },
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                if (response.data > 0) {
                    toastr.warning('Có mã cá biệt, không thể xóa!', '', { timeOut: 1000 });
                }
                else {
                    $.ajax({
                        url: '/Admin/Book/Delete',
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
        url: '/Admin/Book/Change',
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
        location.href = '/Admin/Book/Index?search=' + search;
    }
    else {
        location.href = '/Admin/Book/Index';
    }
}

function MCB(id) {
    $('#hdIDAP').val(id);
    $.ajax({
        type: 'GET',
        url: "/Admin/Book/GetMCB",
        data: { id: id },
        cache: false,
        dataType:'json',
        success: function (response) {
            if (response.status == true) {
                $('#mdData').modal("show");
                var html = '';
                $.each(response.data, function (i, item) {
                    html += "<tr id='tr_" + item.ID + "'><td>" + item.MCB + "</td><td>" + item.DonViHienTai + "</td><td><a onclick=\"DelMCB(" + item.ID + ")\" style=\"padding-right:5px\" title=\"Xóa\"><i class=\"fa fa-recycle\"></i></a></td></tr>";
                });
                $('#btlData').html(html);
            }
            else {
                return false
            }
        },
        error: function (err) {
            console.log(err)
        }
    })
}

function AddMCB() {
    $('#mdAddMCB').modal("show");
}

function SaveDataSLMCB() {
    var idAP = $('#hdIDAP').val();
    var sl = $('#txtSLMCB').val();
    $.ajax({
        url: '/Admin/Book/AddMCB',
        data: {
            id: idAP,
            sl: sl
        },
        cache: false,
        type: 'POST',
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                $('#mdAddMCB').modal("hide");
                toastr.success('Thêm mới thành công', '', { timeOut: 1000 });
                setTimeout(function () {
                    MCB(idAP);
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