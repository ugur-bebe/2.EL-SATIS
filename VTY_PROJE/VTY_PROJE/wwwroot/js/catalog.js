function isNumeric(value) {
    return /^-?\d+$/.test(value);
}

$("#btn-filter").click(function () {
    var minPrice = $("#min-price").val();
    var maxPrice = $("#max-price").val();
    var search_txt = $("#search_txt").val();

    if (!isNumeric(minPrice)) {
        alert("Min Fiyat Sayı Olmalı");
        retun;
    }

    if (!isNumeric(maxPrice)) {
        alert("Max Fiyat Sayı Olmalı");
        retun;
    }


    var category = $('#categoryOptions option').filter(function () {
        return this.value == $('#catagoryOptionsList').val();;
    }).data('val');

    var p_type;
    if (new String(document.getElementById("p_type_ProductTypeList").value) != "")
        p_type = $('#p_type_Options option').filter(function () {
            return this.value == $('#p_type_ProductTypeList').val();;
        }).data('val');
    else p_type = 0;


    category = (category === undefined) ? "" : "&category=" + category;
    p_type = (p_type == undefined) ? "" : "&type=" + p_type;
    search_txt = (search_txt == undefined) ? "" : "&search_txt=" + search_txt;

    //alert("search?minPrice=" + minPrice + "&maxPrice=" + maxPrice + "&category=" + category + "&p_type=" + p_type);

    window.location.href = "search?minPrice=" + minPrice + "&maxPrice=" + maxPrice + category + p_type + search_txt;
});

$(".tr-product").click(function () {
    window.location.href = "search/detail/" + $(this).attr("id");
});

$("#catagoryOptionsList").on('change', function () {
    var category = $('#categoryOptions option').filter(function () {
        return this.value == $('#catagoryOptionsList').val();;
    }).data('val');

    var p_type_category_id = $('#p_type_Options option').filter(function () {
        return this.value == $('#p_type_ProductTypeList').val();
    }).data('data-catagoryId');


    $('#p_type_ProductTypeList').val("");
    $('#p_type_Options').val("");

    var x = document.getElementById("p_type_Options");

    for (i = 0; i < x.options.length; i++) {
        var val = x.options[i].getAttribute("data-catagoryid");
        var val2 = x.options[i].getAttribute("data-catagor_val");

        if (parseInt(val) != category) {
            $(x.options[i]).val("");
        }
        else {
            $(x.options[i]).val(val2);
        }
    }
});

$(window).on("load", function () {
    var category = $('#categoryOptions option').filter(function () {
        return this.value == $('#catagoryOptionsList').val();;
    }).data('val');

    var x = document.getElementById("p_type_Options");

    for (i = 0; i < x.options.length; i++) {
        var val = x.options[i].getAttribute("data-catagoryid");
        var val2 = x.options[i].getAttribute("data-catagor_val");

        if (parseInt(val) != category) {
            $(x.options[i]).val("");
        }
        else {
            $(x.options[i]).val(val2);
        }
    }
});

$("#chooseCategory").on('change', function () {
    var id = this.value;

    $("#chooseType option").each(function () {
        if (this.id != id) {
            $(this).css("display", "none");
        }
        else {
            $(this).css("display", "block");
        }
    });
});



/*
$("#---").click(function () {

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

});
*/
