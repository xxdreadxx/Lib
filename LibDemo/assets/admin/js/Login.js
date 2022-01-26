function DangNhap() {
    var username = $('#txtUsername').val();
    var pass = $('#txtPassword').val();
    $.ajax({
        url: '/Login/Login',
        data: {
            username: username,
            pass: pass,
        },
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                toastr.success(response.message, '', { timeOut: 2000 });
                setTimeout(function () {
                    window.location = '/Admin/Home';
                }, 1000);
            }
            else {
                toastr.error(response.message, '', { timeOut: 2000 });
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}
function LogOut() {
    $.ajax({
        url: '/Login/LogOut',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                window.location = '/Admin/Login/Index';
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function updatePass() {
    var kt = true;
    var IDNV = $('#hdIDNV').val();
    var PassOld = $('#txtOldPass').val();
    var NewPass = $('#txtNewPass').val();
    var PassConfirm = $('#txtPassConfirm').val();
    if (PassOld.trim() == '') {
        toastr.warning('Chưa nhập mật khẩu!', '', { timeOut: 1000 });
        $('#txtOldPass').focus();
        kt = false;
    }
    else {
        if (NewPass.trim() == '') {
            toastr.warning('Chưa nhập mật khẩu mới!', '', { timeOut: 1000 });
            $('#txtOldPass').focus();
            kt = false;
        }
        else if (NewPass.trim().length < 6 || NewPass.trim().length > 20) {
            toastr.warning('Mật khẩu phải từ 6 đến 20 kí tự!', '', { timeOut: 1000 });
            $('#txtNewPass').focus();
            kt = false;
        }
        else if (PassConfirm.trim() == '') {
            toastr.warning('Chưa nhập lại mật khẩu mới!', '', { timeOut: 1000 });
            $('#txtPassConfirm').focus();
            kt = false;
        }
        else if (NewPass != PassConfirm) {
            toastr.warning('Mật khẩu nhập lại không khớp!', '', { timeOut: 1000 });
            kt = false;
        }
    }
    if (kt == true) {
        var formData = new FormData();
        formData.append("ID", IDNV);
        formData.append("NewPass", NewPass);
        $.ajax({
            url: '/Admin/User/UpdatePass',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    toastr.success('Cập nhật thành công', '', { timeOut: 1000 });
                    setTimeout(function () {
                        location.reload();
                    }, 1000);
                }
                else {
                    if (response.type == 2) {
                        toastr.error('Có lỗi xảy ra, cập nhật không thành công', '', { timeOut: 2000 });
                    }
                    else {
                        toastr.error('Mật khẩu mới trùng với mật khẩu cũ', '', { timeOut: 2000 });
                    }
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
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

function UpdateInfo() {
    var kt= true;
    var IDNV = $('#hdIDNV').val();
    var HoTen = $('#txtHoTen').val();
    var NgaySinh = $('#txtNgaySinh').val();
    var CMT = $('#txtCMT').val();
    var DiaChi = $('#txtDiaChi').val();
    var SDT = $('#txtSDT').val();
    var Email = $('#txtEmail').val();
    var Image = document.getElementById("fImage").files[0];
    if (HoTen.trim() == '') {
        toastr.warning('Chưa nhập Họ tên!', '', { timeOut: 1000 });
        $('#txtHoTen').focus();
        kt = false;
    }
    else {
        if (NgaySinh.trim() == '') {
            toastr.warning('Chưa nhập Họ tên!', '', { timeOut: 1000 });
            $('#txtNgaySinh').focus();
            kt = false;
        }
    }
    if (kt == true) {
        var formData = new FormData();
        formData.append("ID", IDNV);
        formData.append("HoTen", HoTen);
        formData.append("NgaySinh", NgaySinh);
        formData.append("CMT", CMT);
        formData.append("DiaChi", DiaChi);
        formData.append("SDT", SDT);
        formData.append("Email", Email);
        formData.append("Image", Image);
        $.ajax({
            url: '/Admin/User/UpdateInfo',
            data: formData,
            cache: false,
            contentType: false,
            processData:false,
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    toastr.success('Cập nhật thành công', '', { timeOut: 1000 });
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