$(document).ready(function () {
    $('.editmode').hide();
    $('#friend-list').hide();
    $('#saveButton').hide();
    $('#cancelEditButton').hide();

    if (result)
    {
        $("#successModal-header").text("Profile update success")
        $("#successModal-body").text("Profile picture updated")
        $("#successModal").modal('show')
    }

    $('#camera-icon').click(function () {
        $('#pictureStatus').hide();
        $('#upload').hide();
        $('#file').val("");
    });
    
    $('#file').change(function () {
        $('#upload').hide();
        var filesize = $('#file')[0].files[0].size;
        var ext = $('#file').val().split('.').pop().toLowerCase();

        //http://stackoverflow.com/questions/651700/how-to-have-jquery-restrict-file-types-on-upload
        if ($.inArray(ext, ['png', 'jpg', 'jpeg']) == -1) {
            $('#pictureStatus').show();
            $('#pictureStatus').text("Not an image file or invalid file extension. Only .PNG , .JPG and .JPEG are allowed");
        }

        else
        {
            if (filesize > 2097152) {
                $('#pictureStatus').show();
                $('#pictureStatus').text("File size is too big. Maximum size is 2mb");
            }

            else {
                $('#pictureStatus').hide();
                $('#upload').show();
            }
        }

    });

    $(document).delegate("#saveButton", "click", function () {
        var data = {
            aboutme: $('#about-me').val()
        }

        $.ajax({
            url: checkAboutMeURL,
            data: data,
            type: 'POST',
            success: function (data) {
                if (data.result.length == 0) {
                    var data = {
                        aboutme: $('#about-me').val()
                    }

                    $.ajax({
                        url: updateProfileURL,
                        data: data,
                        type: 'POST',
                        success: function (data) {
                                $("#timelinePost *").prop("disabled", false);
                                $("#writePost *").prop("disabled", false);
                                $("#profile-options-friend *").prop("disabled", false);
                                $("#profile-options-post *").prop("disabled", false);

                                $("#timelinePost").css({ "-webkit-filter": "none", "-moz-filter": "none" });
                                $("#writePost").css({ "-webkit-filter": "none", "-moz-filter": "none" });
                                $("#friend-list").css({ "-webkit-filter": "none", "-moz-filter": "none" });
                                $("#profile-options-post").css({ "-webkit-filter": "none", "-moz-filter": "none" });
                                $("#profile-options-friend").css({ "-webkit-filter": "none", "-moz-filter": "none" });

                                $("#profile-user-details").css({ "border": "none" });
                                $("#profile-prof-pic").css({ "border": "none" });

                                $.ajax({
                                    url: getProfileDetails,
                                    dataType: "html",
                                    success: function (result) {
                                        $("#profile-user-details").html(result);
                                        $('.viewmode').show();
                                        $('.editmode').hide();

                                        $('#saveButton').hide();
                                        $('#cancelEditButton').hide();

                                    }
                                })
                                $("#successModal-header").text("Profile update success")
                                $("#successModal-body").text("About Me description updated")
                                $("#successModal").modal('show')
                            }
                    });
                }

                else {
                    $("#errorModal-header").text("Profile update error")
                    $("#errorModal-body").text(data.result)
                    $("#errorModal").modal('show')
                }
            }
        });
    });

        $(document).delegate("#edit-icon", "click", function () {
            $('.viewmode').hide();
            $('.editmode').show();

            $("#timelinePost *").attr("disabled", "disabled").off('click');
            $("#writePost *").attr("disabled", "disabled").off('click');
            $("#profile-options-friend *").attr("disabled", "disabled").off('click');
            $("#profile-options-post *").attr("disabled", "disabled").off('click');

        
            $("#timelinePost").css({ "-webkit-filter": "blur(2px)", "-moz-filter": "blur(2px)" });
            $("#writePost").css({ "-webkit-filter": "blur(2px)", "-moz-filter": "blur(2px)" });
            $("#friend-list").css({ "-webkit-filter": "blur(2px)", "-moz-filter": "blur(2px)" });
            $("#profile-options-post").css({ "-webkit-filter": "blur(2px)", "-moz-filter": "blur(2px)" });
            $("#profile-options-friend").css({ "-webkit-filter": "blur(2px)", "-moz-filter": "blur(2px)" });

            $("#profile-user-details").css({ "border": "2px solid #428bca" });

            $('#saveButton').show();
            $('#cancelEditButton').show();
        });

        $(document).delegate("#cancelEditButton", "click", function () {
            $('.viewmode').show();
            $('.editmode').hide();

            $("#timelinePost *").prop("disabled", false);
            $("#writePost *").prop("disabled", false);
            $("#profile-options-friend *").prop("disabled", false);
            $("#profile-options-post *").prop("disabled", false);

            $("#timelinePost").css({ "-webkit-filter": "none", "-moz-filter": "none" });
            $("#writePost").css({ "-webkit-filter": "none", "-moz-filter": "none" });
            $("#friend-list").css({ "-webkit-filter": "none", "-moz-filter": "none" });
            $("#profile-options-post").css({ "-webkit-filter": "none", "-moz-filter": "none" });
            $("#profile-options-friend").css({ "-webkit-filter": "none", "-moz-filter": "none" });

            $("#profile-user-details").css({ "border": "none" });
            $('#saveButton').hide();
            $('#cancelEditButton').hide();
        });

        $(document).delegate("#profile-options-friend", "click", function () {
            $('#writePost').hide();
            $('#timelinePost').hide();
            $('#friend-list').show();
        });

        $(document).delegate("#profile-options-post", "click", function () {
            $('#writePost').show();
            $('#timelinePost').show();
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
                                    url: getTimelinePostUrl,
                                    dataType: "html",
                                    success: function (result) {
                                        $("#timelinePost").html(result);
                                        $('#textAreaPost').val('')

                                        $("#successModal-header").text("Success posting")
                                        $("#successModal-body").text("Posted successfully")
                                        $("#successModal").modal('show')
                                    },

                                    error: function () {
                                        $("#errorModal-header").text("Error posting")
                                        $("#errorModal-body").text("Error in posting something")
                                        $("#errorModal").modal('show')
                                    }
                                });
                            },

                            error: function () {
                                $("#errorModal-header").text("Error posting")
                                $("#errorModal-body").text("Error in posting something. Please try again later")
                                $("#errorModal").modal('show')
                            }
                        })
                    }

                    else
                    {
                        $("#errorModal-header").text("Error posting")
                        $("#errorModal-body").text(data.result)
                        $("#errorModal").modal('show')
                    }

                }

            });
        });

        $(document).delegate("#btnRejectFriend", "click", function () {
            var data = {
                friendId: this.value
            }

            $.ajax({
                url: rejectFriendURL,
                data: data,
                type: 'POST',
                success: function (data) {
                            $.ajax({
                                url: interactButtonsUrl,
                                dataType: "html",
                                success: function (result) {
                                    $("#interactButtons").html(result);

                                },
                            });
                        },
                        error: function () {
                            $("#errorModal-header").text("Error rejecting friend request")
                            $("#errorModal-body").text("Error in rejecting friend request. Please try again later")
                            $("#errorModal").modal('show')
                        }
                    })
        });

        $(document).delegate("#btnAddFriend", "click", function () {
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
                            $("#successModal-header").text("Send friend request success")
                            $("#successModal-body").text("Friend request sent")
                            $("#successModal").modal('show')
                        }
                    });
                },

                error: function () {
                    $("#errorModal-header").text("Error sending friend request")
                    $("#errorModal-body").text("Error in sending friend request. Please try again later")
                    $("#errorModal").modal('show')

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
                        }
                    });
                },

                error: function () {
                    $("#errorModal-header").text("Like Error")
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
                        url: getTimelinePostUrl,
                        dataType: "html",
                        success: function (result) {
                            $("#timelinePost").html(result);
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
                                    url: getTimelinePostUrl,
                                    dataType: "html",
                                    success: function (result) {
                                        $("#timelinePost").html(result);
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