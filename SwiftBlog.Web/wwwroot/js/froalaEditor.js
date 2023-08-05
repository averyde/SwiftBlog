$(document).ready(() => {
    var editor = new FroalaEditor('#content', {
        imageUploadURL: '/api/images'
    });
})