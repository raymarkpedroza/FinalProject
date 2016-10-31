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

                                    $("#successModal-header").text("Success")
                                    $("#successModal-body").text("Posted")
                                    $("#successModal").modal('show')
                                },

                                error: function () {
                                    $("#errorModal-header").text("Error")
                                    $("#errorModal-body").text("S2omething went wrong when processing your request")
                                    $("#errorModal").modal('show')
                                }
                            });
                        },

                        error: function () {
                            $("#errorModal-header").text("Error")
                            $("#errorModal-body").text("Something went wrong when processing your request1")
                            $("#errorModal").modal('show')
                        }
                    })
                }

                else {
                    $("#errorModal-header").text("Error")
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

                            $("#successModal-header").text("Success")
                            $("#successModal-body").text("Comment Added!")
                            $("#successModal").modal('show')
                        },

                        error: function () {
                            $("#errorModal-header").text("Error")
                            $("#errorModal-body").text("Error commenting")
                            $("#errorModal").modal('show')
                        }
                    })
                }

                else {
                    $("#errorModal-header").text("Error")

                    $("#errorModal-body").text(data.result)
                    $("#errorModal").modal('show')
                }
            }
        });
    });
});