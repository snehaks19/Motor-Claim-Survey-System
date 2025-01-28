
//$(function () {
//    var a = $(".numberText");
//    $.each(a, function () {
//        var parts = $(this).val().toString().split(".");
//        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
//        $(this).val(parts.join("."));
//    });
//});
//$(function () {
//    $(".numberText").on("keyup", function () {
//        var sanitizedValue = $(this).val().replace(/[^0-9.]/g, "");
//        var parts = sanitizedValue.split(".");
//        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
//        if (parts[1] && parts[1].length > 2) {
//            parts[1] = parts[1].slice(0, 2);
//        }
//        $(this).val(parts.join("."));
//    });
//});