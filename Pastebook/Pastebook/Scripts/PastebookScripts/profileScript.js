$(document).ready(function () {
    $('.editmode').hide();
    $('.post-comments').hide();
    $('#friend-list').hide();
    $('#saveButton').hide();
    $('#cancelEditButton').hide();
    $('#camera-icon').hide();  

    $(document).delegate("#saveButton", "click", function () {
        var data = {
            gender:  $('#selectGender').val(),
            country: $('#selectCountry').val(),
            birthday: $('#dtpBirthday').val(),
            mobilenumber: $('#txtMobileNumber').val(),
            email: $('#txtEmailAddress').val(),
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

                $('#saveButton').hide();
                $('#cancelEditButton').hide();
                $('#camera-icon').hide();

                $.ajax({
                    url: getProfileDetails,
                    dataType: "html",
                    success: function (result) {
                        $("#profile-user-details").html(result);
                        $('.viewmode').show();
                        $('.editmode').hide();
                    }
                })
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
        $("#profile-prof-pic").css({ "border": "2px solid #428bca" });

        $('#saveButton').show();
        $('#cancelEditButton').show();
        $('#camera-icon').show();
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
        $("#profile-prof-pic").css({ "border": "none" });

        $('#saveButton').hide();
        $('#cancelEditButton').hide();
        $('#camera-icon').hide();
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

    $(function () {
        $("#dtpBirthday").datepicker({ dateFormat: 'mm/dd/yy', minDate: new Date('01/01/1600'), maxDate: '0' });
        //$("#dtpBirthday").datepicker().datepicker("setDate", new Date());
        $("#dtpBirthday").keyup(function () {
            if ($(this).val().length == 2) {
                $(this).val($(this).val() + "/");
            } else if ($(this).val().length == 5) {
                $(this).val($(this).val() + "/");
            }
        });
    });

});