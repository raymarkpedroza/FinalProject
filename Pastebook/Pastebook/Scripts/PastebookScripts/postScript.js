$(document).ready(function () {
    $(document).delegate(".post-reaction-like", "click", function () {
        var data = {
            postId: this.value,
        }
        $.ajax({
            url: addLikeURL,
            data: data,
            type: 'POST',
            success: function (data) {
            },

            error: function () {
                window.location.href = errorURL;
            }
        })
        window.location.reload();

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
            },

            error: function () {
                window.location.href = errorURL;

            }
        })
        window.location.reload();


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
            },

            error: function () {
                window.location.href = errorURL;

            }
        })
        window.location.reload();

    });

    $(document).delegate(".post-reaction-comment", "click", function () {
        if ($('.post-comments[value=' + this.value + ']').is(':visible') == false) {
            $('.post-comments[value=' + this.value + ']').show();
        }

        else {
            $('.post-comments[value=' + this.value + ']').hide();
        }

    });

    $(document).delegate(".comment-count", "click", function () {
        if ($('.post-comments[value=' + $(this).data("value") + ']').is(':visible') == false) {
            $('.post-comments[value=' + $(this).data("value") + ']').show();
        }

        else {
            $('.post-comments[value=' + $(this).data("value") + ']').hide();
        }

    });
});