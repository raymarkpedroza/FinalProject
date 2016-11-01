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
                if (data.result)
                {

                }
            },

            error: function () {
                $("#errorModal-header").text("Like error")
                $("#errorModal-body").text("Error in liking a post. Please try again later")
                $("#errorModal").modal('show')
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
                $("#errorModal-header").text("Unlike Error")
                $("#errorModal-body").text("Error in unliking a post. Please try again later")
                $("#errorModal").modal('show')

            }
        })
        window.location.reload();


    });


    $(document).delegate("#btnComment", "click", function () {
        var data = {
            postId: this.value,
            content: $(".textArea-comment[value=" + this.value + "]").val()
        }

        var postId = this.value
        var content = $(".textArea-comment[value=" + this.value + "]").val()

        $.ajax({
            url: checkCommentValid,
            data: data,
            type: 'POST',
            success: function (data) {
                if (data.result == 0) {
                    var data = {
                        postId: postId,
                        content: content
                    }
                    $.ajax({
                        url: addCommentURL,
                        data: data,
                        type: 'POST',
                        success: function (data) {
                            $(".textArea-comment[value=" + this.value + "]").val('')
                            $('.post-comments[value=' + data.postId + ']').show();
                            $("#successModal-header").text("Comment success")
                            $("#successModal-body").text("Your comment is added")
                            $("#successModal").modal('show')
                            setTimeout(
                              function () {
                                  window.location.reload();
                              }, 3000);

                        },

                        error: function () {
                            $("#errorModal-header").text("Comment error")
                            $("#errorModal-body").text("Error commenting on a post. Please try again later")
                            $("#errorModal").modal('show')
                        }
                    })
                }

                else {
                    $("#errorModal-header").text("Comment error")
                    $("#errorModal-body").text(data.result)
                    $("#errorModal").modal('show')
                }
            }
        });
    });
});