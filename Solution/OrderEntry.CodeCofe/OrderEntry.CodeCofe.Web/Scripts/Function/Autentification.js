
$(document).ready(function () {
    $("#contRegister").css("display", "none")
});

function Login() {
    var email = $("#txtEmailL").val();
    var password = $("#txtPasswordL").val();

    $.ajax({
        type: 'GET',
        data: { pEmail: email, pPassword: password },
        url: '/Autentification/Login',
        success: function (result) {
            $("#dvMessage").html(result);

            if (result == "") {
                window.location.href = '/Home/Index/';
            }
        }
    });
}

function SaveUser() {
    var email = $("#txtEmail").val();
    var password = $("#txtPassword").val();

    $.ajax({
        type: 'GET',
        data: { pEmail: email, pPassword: password },
        url: '/Autentification/SaveUser',
        success: function (result) {
            alert(result);
            Cancelar();
        }
    });
}

function ForgotPassword() {
    var email = $("#txtEmailL").val();

    $.ajax({
        type: 'GET',
        data: { pEmail: email },
        url: '/Autentification/ForgotPassword',
        success: function (result) {
            alert(result);
            Cancelar();
        }
    });
}

function Register() {
    $("#contLogin").css("display", "none");
    $("#contRegister").css("display", "inline")
}

function Cancelar() {
    $("#contLogin").css("display", "inline");
    $("#contRegister").css("display", "none")
}