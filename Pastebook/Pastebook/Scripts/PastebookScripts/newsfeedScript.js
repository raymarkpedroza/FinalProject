$(document).ready(function () {
    $('#friend-list').hide();

    $(document).delegate("#viewFriendsLink", "click", function () {
        $('#writePost').hide();
        $('#newsfeedPost').hide();
        $('#friend-list').show();
    });

    $(document).delegate("#backToTimeline", "click", function () {
        $('#writePost').show();
        $('#newsfeedPost').show();
        $('#friend-list').hide();
    });

    $(document).delegate("#btnPost", "click", function () {
        var data = {
            content: $('#textAreaPost').val(),
            profileOwner: this.value
        }

        $.ajax({
            url: createPostURL,
            data: data,
            type: 'POST',
            success: function (data) {
                $.ajax({
                    url: getNewsfeedPostsURL,
                    dataType: "html",
                    success: function (result) {
                        $("#newsfeedPost").html(result);
                        $('#textAreaPost').val('')
                    }
                });
            },

            error: function () {
                window.location.href = errorURL;
            }
        })

    });

    function RefreshNewsfeed() {
        setTimeout(function () {
            loadNewsfeed();
            RefreshNewsfeed();
        }, 30000);
    }
    RefreshNewsfeed();

    function loadNewsfeed() {
        $.ajax({
            url: getNewsfeedPostsURL,
            dataType: "html",
            success: function (result) {
                $("#newsfeedPost").html(result);
            }
        })
    }

    $(document).delegate(".post-reaction-like", "click", function () {
        var data = {
            postId: this.value,
        }
        $.ajax({
            url: addLikeURL,
            data: data,
            type: 'POST',
            success: function (data) {
                $.ajax({
                    url: getNewsfeedPostsURL,
                    dataType: "html",
                    success: function (result) {
                        $("#newsfeedPost").html(result);
                    }
                });
            },

            error: function () {
                window.location.href = errorURL;
            }
        })

    });

    $(document).delegate(".post-reaction-unlike", "click", function () {
        var data = {
            likeId: this.value,
        }
        $.ajax({
            url: unlikeURL,
            data: data,
            type: 'POST',
            success: function (data) {
                $.ajax({
                    url: getNewsfeedPostsURL,
                    dataType: "html",
                    success: function (result) {
                        $("#newsfeedPost").html(result);
                    }
                });
            },

            error: function () {
                window.location.href = errorURL;
            }
        })

    });

    $(document).delegate("#btnComment", "click", function () {
        var data = {
            postId: this.value,
            content: $(".textArea-comment[value=" + this.value + "]").val()
        }
        $.ajax({
            url: addCommentURL,
            data: data,
            type: 'POST',
            success: function (data) {

                $.ajax({
                    url: getNewsfeedPostsURL,
                    dataType: "html",
                    success: function (result) {
                        $("#newsfeedPost").html(result);
                    }
                });

                $(".textArea-comment[value=" + this.value + "]").val('')
                $('.post-comments[value=' + data.postId + ']').show();
            },

            error: function () {
                
            }
        })

    });
});