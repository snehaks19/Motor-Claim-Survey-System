function validateNumericInput(event) {
    var charCode = event.which ? event.which : event.keyCode;

    // Allow numbers (0-9) and one decimal point
    if ((charCode < 48 || charCode > 57) && charCode !== 46) {
        event.preventDefault(); // Prevent any other character except numbers and dot
    }

    // Ensure only one decimal point can be entered
    if (charCode === 46 && event.target.value.includes('.')) {
        event.preventDefault(); // Prevent entering multiple decimal points
    }
}

function validateNumericOnly(event) {
    var charCode = event.which ? event.which : event.keyCode;
    if (charCode < 48 || charCode > 57) {
        event.preventDefault(); // Prevent any other character except numbers and dot
    }
}



function limitToTwoDecimalPlaces(input) {
    // Ensure that input has at most 2 decimal places
    const regex = /^\d+(\.\d{0,2})?$/;
    if (!regex.test(input.value)) {
        input.value = input.value.slice(0, -1);  // Remove the last character if it violates the condition
    }
}



//function limitToTwoDecimalPlaces(input) {
//    // Ensure that input has at most 2 decimal places
//    const regex = /^\d+(\.\d{0,2})?$/;
//    if (!regex.test(input.value)) {
//        input.value = input.value.slice(0, -1);  // Remove the last character if it violates the condition
//    }
//}


//function limitToTwoDecimalPlaces(input) {
//    // Ensure that input has at most 2 decimal places
//    const regex = /^\d+(\.\d{0,2})?$/;
//    if (!regex.test(input.value)) {
//        input.value = input.value.slice(0, -1);  // Remove the last character if it violates the condition
//    }
//}


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

//        // Restrict to 7 digits before the decimal
//        if (parts[0].length > 7) {
//            parts[0] = parts[0].slice(0, 7);
//        }

//        // Format with commas for the integer part
//        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");

//        // Restrict to 2 digits after the decimal
//        if (parts[1] && parts[1].length > 2) {
//            parts[1] = parts[1].slice(0, 2);
//        }

//        $(this).val(parts.join("."));
//    });
//});

$(document).ready(function () {
    bindNumberFormatting();
});

// Rebind the function after each postback in the UpdatePanel
Sys.Application.add_load(function () {
    bindNumberFormatting();
});

function bindNumberFormatting() {
    var a = $(".numberText");
    $.each(a, function () {
        var parts = $(this).val().toString().split(".");
        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        $(this).val(parts.join("."));
    });

    $(".numberText").on("keyup", function () {
        var sanitizedValue = $(this).val().replace(/[^0-9.]/g, "");
        var parts = sanitizedValue.split(".");

        // Restrict to 7 digits before the decimal
        if (parts[0].length > 7) {
            parts[0] = parts[0].slice(0, 7);
        }

        // Format with commas for the integer part
        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");

        // Restrict to 2 digits after the decimal
        if (parts[1] && parts[1].length > 2) {
            parts[1] = parts[1].slice(0, 2);
        }

        $(this).val(parts.join("."));
    });
}
