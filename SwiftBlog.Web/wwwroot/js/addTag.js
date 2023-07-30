const colorSelect = document.getElementById('color-select');

function updateBackgroundColor() {
    colorSelect.style.backgroundColor = colorSelect.value;
}

colorSelect.addEventListener('change', updateBackgroundColor);

updateBackgroundColor();