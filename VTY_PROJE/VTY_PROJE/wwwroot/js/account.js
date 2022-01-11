
$("#btn-sign-in").click(function () {
    var cityOptions = $('#cityOptions option').filter(function () {
        return this.value == $('#cityOptionsList').val();;
    }).data('val');


    var districtOptions;
    if (new String(document.getElementById("districtOptionsList").value) != "")
        districtOptions = $('#districtOptions option').filter(function () {
            return this.value == $('#districtOptionsList').val();;
        }).data('val');
    else districtOptions = 0;

    var name = $("#name").val();
    var surname = $("#surname").val();
    var user_name = $("#user_name").val();
    var email = $("#email").val();
    var password = $("#password").val();
    var phone_number = $("#phone_number").val();

    if (!name) {
        alert("Lütfen İsim Giriniz"); return;
    }
    if (!surname) {
        alert("Lütfen Soyad Giriniz"); return;
    }
    if (!user_name) {
        alert("Lütfen Kulalnıcı Adı Giriniz"); return;
    }
    if (!email) {
        alert("Lütfen email Giriniz"); return;
    }
    if (!password) {
        alert("Lütfen Şifre Giriniz"); return;
    }
    if (!phone_number) {
        alert("Lütfen Telefon Giriniz"); return;
    }
    if (!cityOptions) {
        alert("Lütfen Şehir Şeçiniz"); return;
    }
    if (!districtOptions) {
        alert("Lütfen İlçe Şeçiniz"); return;
    }


    $.ajax({
        url: "sign_in",
        data: JSON.stringify
            ({
                name: $("#name").val(),
                surname: $("#surname").val(),
                user_name: $("#user_name").val(),
                email: $("#email").val(),
                password: $("#password").val(),
                phone_number: $("#phone_number").val(),
                city_name: "" + cityOptions,
                district_name: "" + districtOptions
            }),
        contentType: "application/json; charset=utf-8",
        type: "post",
        cache: false,
        success: function (data, textStatus, xhr) {
            statusCode = xhr.status;

            if (statusCode == 201) {
                alert("Başaralı");
            }
            else if (statusCode == 204) {
                alert(user_name + " Kullıcı Adı Daha Önceden Alınmış!");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
});


$("#cityOptionsList").on('change', function () {
    var city = $('#CityOptions option').filter(function () {
        return this.value == $('#cityOptionsList').val();;
    }).data('val');


    $('#districtOptionsList').val("");

    var x = document.getElementById("districtOptions");

    for (i = 0; i < x.options.length; i++) {
        var val = x.options[i].getAttribute("data-city_id");
        var val2 = x.options[i].getAttribute("data-name");

        if (parseInt(val) != city) {
            $(x.options[i]).val("");
        }
        else {
            $(x.options[i]).val(val2);
        }
    }
});

/*
$("#sign_up").click(function () {

    $.ajax({
        url: "sign_in",
        data: JSON.stringify
            ({
                id: 0,
                user_name: $("#username").val(),
                name: $("#name").val(),
                surname: $("#surname").val(),
                password: $("#password").val(),
                phone_number: $("#p_no").val(),
                email: $("#email").val(),
                address: $("#address").val()
            }),
        contentType: "application/json; charset=utf-8",
        type: "post",
        cache: false,
        success: function (data, textStatus, xhr) {
            statusCode = xhr.status;

            if (statusCode == 201) {
                alert("Başaralı");
            }
            else {
                alert("Başarısız");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });

});*/



$("#login").click(function () {

    $.ajax({
        url: "login_check",
        data: JSON.stringify
            ({
                id: 0,
                user_name: $("#username").val(),
                password: $("#password").val()
            }),
        contentType: "application/json; charset=utf-8",
        type: "post",
        cache: false,
        success: function (data, textStatus, xhr) {
            statusCode = xhr.status;
            if (statusCode == 200) {
                window.location.href = "catalog";
            }
            else {
                $("#_alert").css("display", "block");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });

});