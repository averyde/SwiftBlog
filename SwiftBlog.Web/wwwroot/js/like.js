async function addLikeToBlog() {
    console.log('here');

    fetch('/api/BlogPostLike/Add', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': '*/*',
        },
        body: JSON.stringify({
            blogPostId: '@Model.Id',
            userId: '@userManager.GetUserId(User)'
        })
    }).then(console.log("Request finished"));
}

$(document).ready(() => {
    $("#like-button").click(addLikeToBlog);
});