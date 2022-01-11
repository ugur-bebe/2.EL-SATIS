$("#btn_profile_edit").click(function () {
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

    if (!phone_number) {
        alert("Lütfen Telefon Giriniz"); return;
    }
    if (!cityOptions) {
        alert("Lütfen Şehir Şeçiniz"); return;
    }
    if (!districtOptions) {
        alert("Lütfen İlçe Şeçiniz"); return;
    }

    var a = $("#name").val();
    $.ajax({
        url: "profile/edit",
        data: JSON.stringify
            ({
                name: $("#name").val(),
                surname: $("#surname").val(),
                user_name: $("#user_name").val(),
                email: $("#email").val(),
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
            else {
                alert("Başarısız");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            console.log(ajaxOptions);
            console.log(thrownError);
        }
    });

});


$("#btn-update-pw").click(function () {

    if (!$("#new-psw").val() || !$("#new-psw-repeat").val() || !$("#old-psw").val()) {
        alert("Lütfen Şifre Giriniz");
    }
    else {

        var val = new String($("#new-psw").val()).valueOf();
        var val2 = new String($("#new-psw-repeat").val()).valueOf();

        if (val != val2) {
            $("#_alert").css("display", "block");
            return;
        }

        $.ajax({
            url: "edit-password",
            headers: {
                oldPassword: $("#old-psw").val(),
                newPassword: $("#new-psw").val()
            },
            contentType: "application/json; charset=utf-8",
            type: "post",
            cache: false,
            success: function (data, textStatus, xhr) {
                statusCode = xhr.status;


                if (statusCode == 204) {
                    alert("Bir hata oluştur!");
                }
                else if (statusCode == 200) {
                    $("#_alert").css("display", "block");
                    $("#_alert").text("Şifre Başaraıyla Değiştirlidi!");
                    $("#_alert").css("color", "green");

                    $("#_alert_old_psw").css("display", "none");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                statusCode = xhr.status;
                if (statusCode == 404) {
                    $("#_alert_old_psw").css("display", "block");
                    $("#_alert_old_psw").text("Hatalı Eski Şifre!");
                }

                console.log(xhr);
                console.log(ajaxOptions);
                console.log(thrownError);
            }
        });
    }

});

$("#cityOptionsList").on('change', function () {
    var city = $('#cityOptions option').filter(function () {
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

$(window).on("load", function () {
    var city = $('#cityOptions option').filter(function () {
        return this.value == $('#cityOptionsList').val();;
    }).data('val');

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
