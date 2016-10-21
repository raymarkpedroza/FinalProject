$(document).ready(function () {
    $('.post-comments').hide();

    $(document).delegate(".btnAcceptRequest", "click", function () {
        var data = {
            friendRequestId: this.value
        }

        $.ajax({
            url: acceptFriendURL,
            data: data,
            type: 'POST',
            success: function (data) {
                $.ajax({
                    url: getFriendRequestsURL,
                    dataType: "html",
                    success: function (result) {
                        $("#pastebook-nav-bar-friendRequests").html(result);
                        $.ajax({
                            url: interactButtonsUrl,
                            dataType: "html",
                            success: function (result) {
                                $("#interactButtons").html(result);
                            }
                        })
                    }
                })
            },
            error: function () {
                alert('Something went wrong.')
            }
        })
    });

        
    $(document).delegate("#btnAcceptFriend", "click", function () {
        var data = {
            friendRequestId: this.value
        }

        $.ajax({
            url: acceptFriendURL,
            data: data,
            type: 'POST',
            success: function (data) {
                $.ajax({
                    url: getFriendRequestsURL,
                    dataType: "html",
                    success: function (result) {
                        $("#pastebook-nav-bar-friendRequests").html(result);
                        $.ajax({
                            url: interactButtonsUrl,
                            dataType: "html",
                            success: function (result) {
                                $("#interactButtons").html(result);
                            }
                        })
                    }
                })
            },
            error: function () {
                alert('Something went wrong.')
            }
        })
    });

        $(document).delegate("#btnAddFriend", "click", function () {
            alert(this.value)

        var data = {
            friendId: this.value
        }

        $.ajax({
            url: addFriendURL,
            data: data,
            type: 'POST',
            success: function (data) {
                $.ajax({
                    url: interactButtonsUrl,
                    dataType: "html",
                    success: function (result) {
                        $("#interactButtons").html(result);
                    }
                });
            },

            error: function () {
                alert('Something went wrong.')
            }
        })
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
                    url: getTimelinePostUrl,
                    dataType: "html",
                    success: function (result) {
                        $("#timelinePost").html(result);
                        $('#textAreaPost').val('')
                        $('.post-comments').hide();
                    }
                });
            },

            error: function () {
                alert('Something went wrong.')
            }
        })

    });


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
                    url: getTimelinePostUrl,
                    dataType: "html",
                    success: function (result) {
                        $("#timelinePost").html(result);
                        $('.post-comments').hide();
                    }
                });
            },

            error: function () {
                alert('Something went wrong.')
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
                    url: getTimelinePostUrl,
                    dataType: "html",
                    success: function (result) {
                        $("#timelinePost").html(result);
                        $('.post-comments').hide();
                    }
                });

                $(".textArea-comment[value=" + this.value + "]").val('')
                $('.post-comments[value=' + data.postId + ']').show();
            },

            error: function () {
                alert('Something went wrong.')
            }
        })

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