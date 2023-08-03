function updateButton() {
    if ($('#name').val() === "" || $('#name').val().includes(" ") || $('#display-name').val() === "")
        $('.btn').prop('disabled', true);
    else
        $('.btn').prop('disabled', false);
}

function updateBackgroundColor() {
    var selectedColor = $('#color-select').val();
    $('#color-select').css('background-color', selectedColor);
}

updateButton();
updateBackgroundColor();

$(document).ready(() => {
    $('#name').change(updateButton);
    $('#name').keypress((event) => {
        var key = event.keyCode;
        if (key === 32) {
            event.preventDefault();
        }
    });

    $('#display-name').change(updateButton);

    $('#color-select').change(updateBackgroundColor);
})
