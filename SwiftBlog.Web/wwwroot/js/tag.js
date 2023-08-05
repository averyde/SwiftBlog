async function updateBackgroundColor() {
    var selectedColor = $('#color-select').val();
    $('#color-select').css('background-color', selectedColor);
}

await updateBackgroundColor();

$(document).ready(() => {
    $('#name').keypress((event) => {
        var key = event.keyCode;
        if (key === 32) {
            event.preventDefault();
        }
    });

    $('#color-select').change(updateBackgroundColor);
})
