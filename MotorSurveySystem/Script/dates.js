var $j = jQuery.noConflict();

// Function to initialize all required features (Datepickers and number formatting)
function initializePageFeatures() {
    let currentYear = new Date().getFullYear();
    let minYear = currentYear;
    let maxYear = currentYear + 50;

    $j("#txtClmIntmDt,#txtClmRegDt").datepicker({
        dateFormat: 'dd-mm-yy',
        changeMonth: true,
        changeYear: true,
        yearRange: minYear + ':' + maxYear,
        maxDate: 0
    });

    $j("#txtPolFmDt").datepicker({
        dateFormat: 'dd-mm-yy',
        changeMonth: true,
        changeYear: true,
        yearRange: minYear + ':' + maxYear,
        minDate: 0
    });

    $j("#txtClmLossDt").datepicker({
        dateFormat: 'dd-mm-yy',
        changeMonth: true,
        changeYear: true,
        yearRange: 'c-100:c+10',
        maxDate: 0
    });

    // Reapply number formatting to existing fields
    var a = $j(".numberText");
    $j.each(a, function () {
        var parts = $j(this).val().toString().split(".");
        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        $j(this).val(parts.join("."));
    });

    // Attach keyup event for dynamic number formatting
    $j(".numberText").on("keyup", function () {
        var sanitizedValue = $j(this).val().replace(/[^0-9.]/g, "");
        var parts = sanitizedValue.split(".");

        if (parts[0].length > 7) {
            parts[0] = parts[0].slice(0, 7);
        }

        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");

        if (parts[1] && parts[1].length > 2) {
            parts[1] = parts[1].slice(0, 2);
        }

        $j(this).val(parts.join("."));
    });
}

$j(document).ready(function () {
    initializePageFeatures();
});

Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
    initializePageFeatures();
});


////var $j = jQuery.noConflict();

////$j(document).ready(function () {
////    let currentYear = new Date().getFullYear();
////    let minYear = currentYear;
////    let maxYear = currentYear + 50;

    
////    $j("#txtClmIntmDt,#txtClmRegDt").datepicker({
////        dateFormat: 'dd-mm-yy',
////        changeMonth: true,
////        changeYear: true,
////        yearRange: minYear + ':' + maxYear,
////        maxDate: 0
////    });

////    $j("#txtPolFmDt").datepicker({
////        dateFormat: 'dd-mm-yy',
////        changeMonth: true,
////        changeYear: true,
////        yearRange: minYear + ':' + maxYear,
////        minDate: 0
////    });

////    // Loss Date Datepicker
////    $j("#txtClmLossDt").datepicker({
////        dateFormat: 'dd-mm-yy',
////        changeMonth: true,
////        changeYear: true,
////        yearRange: 'c-100:c+10',
////        maxDate: 0
////    });
////});



//////$(function () {
//////    var a = $(".numberText");
//////    $.each(a, function () {
//////        var parts = $(this).val().toString().split(".");
//////        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
//////        $(this).val(parts.join("."));
//////    });
//////});
//////$(function () {
//////    $(".numberText").on("keyup", function () {
//////        var sanitizedValue = $(this).val().replace(/[^0-9.]/g, "");
//////        var parts = sanitizedValue.split(".");
//////        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
//////        if (parts[1] && parts[1].length > 2) {
//////            parts[1] = parts[1].slice(0, 2);
//////        }
//////        $(this).val(parts.join("."));
//////    });
//////});

////$(function () {
////    var a = $(".numberText");
////    $.each(a, function () {
////        var parts = $(this).val().toString().split(".");
////        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
////        $(this).val(parts.join("."));
////    });
////});

////$(function () {
////    $(".numberText").on("keyup", function () {
////        var sanitizedValue = $(this).val().replace(/[^0-9.]/g, "");
////        var parts = sanitizedValue.split(".");

////        // Restrict to 7 digits before the decimal
////        if (parts[0].length > 7) {
////            parts[0] = parts[0].slice(0, 7);
////        }

////        // Format with commas for the integer part
////        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");

////        // Restrict to 2 digits after the decimal
////        if (parts[1] && parts[1].length > 2) {
////            parts[1] = parts[1].slice(0, 2);
////        }

////        $(this).val(parts.join("."));
////    });
////});