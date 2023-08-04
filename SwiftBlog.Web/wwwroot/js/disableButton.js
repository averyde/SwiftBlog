function checkForm() {
    var empty = false;
    $('form .required').each(function () {
        if ($(this).val() === "") {
            empty = true;
            return false;
        }
    })

    $('.btn').prop('disabled', empty);
}

checkForm();

$(document).ready(() => {
    $('form .required').on('input', checkForm);
});