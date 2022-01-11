function uploadImage() {
    const preview = document.getElementById('img_product');
    const file = document.querySelector('input[type=file]').files[0];
    const reader = new FileReader();

    reader.addEventListener("load", function () {
        // convert image file to base64 string
        preview.src = reader.result;
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}

$("#form-submit-new-product").click(function () {
    var val = $('#datalistOptions option').filter(function () {
        return this.value == $('#datalistOptionsProductType').val();;
    }).data('val');

    var _base64 = document.getElementById('img_product').src;

    $.ajax({
        url: "new-product",
        data: JSON.stringify
            ({
                id: 0,
                product_type_id: val,
                title: $("#title").val(),
                explanation: $("#explanation").val(),
                price: $("#price").val(),
                base64: _base64
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


$(".delete-icon").click(function () {

    var val = $(this).attr("_val");
    if (confirm('Ürünü silmek istediğinizden emin misiniz?')) {

        $.ajax({
            url: "delete-product",
            headers: {
                "id": "" + val
            },
            contentType: "application/json; charset=utf-8",
            type: "post",
            cache: false,
            success: function (data, textStatus, xhr) {
                statusCode = xhr.status;

                if (statusCode == 201) {
                    document.getElementById("tr_" + val).remove();
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
    }
});

$(".edit-icon").click(function () {
    window.location.href = "edit-product/" + $(this).attr("_val");
});


$("#form-submit-edit-product").click(function () {
    var val = $('#datalistOptions option').filter(function () {
        return this.value == $('#datalistOptionsProductType').val();;
    }).data('val');

    $.ajax({
        url: "../update-product",
        data: JSON.stringify
            ({
                id: $("#parameter").val(),
                product_type_id: val,
                title: $("#title").val(),
                explanation: $("#explanation").val(),
                price: $("#price").val()
            }),
        contentType: "application/json; charset=utf-8",
        type: "post",
        cache: false,
        success: function (data, textStatus, xhr) {
            statusCode = xhr.status;

            if (statusCode == 201) {
                alert("Başaralı");
                window.location.reload();
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