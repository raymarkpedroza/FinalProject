$(document).ready(function () {
    $('.post-comments').hide();
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

    $(document).delegate(".btnAcceptRequest", "click", function () {
        alert(this.value)
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
                    url: getNewsfeedPostsURL,
                    dataType: "html",
                    success: function (result) {
                        $("#newsfeedPost").html(result);
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

    //function RefreshNewsfeed() {
    //    //this will wait 3 seconds and then fire the load partial function
    //    setTimeout(function () {
    //        loadNewsfeed();
    //        //recall this function so that it will continue to loop
    //        RefreshNewsfeed();
    //    }, 3000);
    //}
    ////initialize the loop
    //RefreshNewsfeed();

    //function loadNewsfeed() {
    //    $.ajax({
    //        url: getNewsfeedPostsURL,
    //        dataType: "html",
    //        success: function (result) {
    //            $("#newsfeedPost").html(result);
    //        }
    //    })
    //}

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
                    url: getNewsfeedPostsURL,
                    dataType: "html",
                    success: function (result) {
                        $("#newsfeedPost").html(result);
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