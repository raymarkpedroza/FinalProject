$(document).ready(function () {
    $('.editmode').hide();
    $('#friend-list').hide();
    $('#saveButton').hide();
    $('#cancelEditButton').hide();
    
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
                                $("#successModal-header").text("Success")
                                $("#successModal-body").text("About me updated")
                                $("#successModal").modal('show')
                            }
                    });
                }

                else {
                    $("#errorModal-header").text("Error")
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

                    else
                    {
                        $("#errorModal-header").text("Error")
                        $("#errorModal-body").text(data.result)
                        $("#errorModal").modal('show')
                    }

                }

            });
            
          

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
                    window.location.href = errorURL;

                }
            })
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
                    window.location.href = errorURL;

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
                        }
                    });
                },

                error: function () {
                    window.location.href = errorURL;

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
                        url: getTimelinePostUrl,
                        dataType: "html",
                        success: function (result) {
                            $("#timelinePost").html(result);
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
                        url: getTimelinePostUrl,
                        dataType: "html",
                        success: function (result) {
                            $("#timelinePost").html(result);
                        }
                    });

                    $(".textArea-comment[value=" + this.value + "]").val('')
                    $('.post-comments[value=' + data.postId + ']').show();
                },

                error: function () {
                    window.location.href = errorURL;

                }
            })

        });
});