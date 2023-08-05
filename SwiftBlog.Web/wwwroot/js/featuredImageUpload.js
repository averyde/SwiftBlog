async function uploadFeaturedImage(e) {

    let data = new FormData();
    data.append('file', e.target.files[0]);

    await fetch('/api/images', {
        method: 'POST',
        headers: {
            'Accept': '*/*',
        },
        body: data
    }).then(response => response.json())
        .then(result => {
            $('#featured-image-url').val(result.link);
            $('#featured-image-display').attr('src', result.link);
            $('#featured-image-display').css('display', 'block');
        });
}

$(document).ready(() => {
    $('#featured-image-upload').change(uploadFeaturedImage);
});