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
                window.location = '/Login/Index';
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function updatePass() {

}

function updateInfo() {

}