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

        var content = $('#textAreaPost').val();
        var owner = this.value;

        $.ajax({
            url: checkPostValid,
            data: data,
            type: 'POST',
            success: function (data) {
                if (data.result.length == 0) {
                    var data = {
                        content: content,
                        profileOwner: owner
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

                                    $("#successModal-header").text("Success posting")
                                    $("#successModal-body").text("You've updated your status")
                                    $("#successModal").modal('show')
                                },

                                error: function () {
                                    $("#errorModal-header").text("Error posting")
                                    $("#errorModal-body").text("Error in posting something. Please try again later")
                                    $("#errorModal").modal('show')
                                }
                            });
                        },

                        error: function () {
                            $("#errorModal-header").text("Error posting")
                            $("#errorModal-body").text("Something went wrong when posting something. Please try again later")
                            $("#errorModal").modal('show')
                        }
                    })
                }

                else {
                    $("#errorModal-header").text("Error posting")
                    $("#errorModal-body").text(data.result)
                    $("#errorModal").modal('show')
                }

            }

        });
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
                $("#errorModal-header").text("Like error")
                $("#errorModal-body").text("Error in liking a post. Please try again later")
                $("#errorModal").modal('show')
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
                $("#errorModal-header").text("Unlike error")
                $("#errorModal-body").text("Error in unliking a post. Please try again later")
                $("#errorModal").modal('show')
            }
        })

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

                            $.ajax({
                                url: getNewsfeedPostsURL,
                                dataType: "html",
                                success: function (result) {
                                    $("#newsfeedPost").html(result);
                                }
                            });

                            $(".textArea-comment[value=" + this.value + "]").val('')
                            $('.post-comments[value=' + data.postId + ']').show();

                            $("#successModal-header").text("Comment success")
                            $("#successModal-body").text("Your comment is added")
                            $("#successModal").modal('show')
                        },

                        error: function () {
                            $("#errorModal-header").text("Comment error")
                            $("#errorModal-body").text("Error in commenting on a post. Please try again later")
                            $("#errorModal").modal('show')
                        }
                    })
                }

                else {
                    $("#errorModal-header").text("Comment Error")
                    $("#errorModal-body").text(data.result)
                    $("#errorModal").modal('show')
                }
            }
        });
    });
});