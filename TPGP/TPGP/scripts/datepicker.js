$(document).ready(function () {
    $('input[type=datetime]').datepicker({
        dateFormat: "yy/M/dd",
        changeMonth: true,
        changeYear: true,
        yearRange: "-60:+0"
    });
});