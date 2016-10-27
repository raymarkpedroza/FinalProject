$(document).ready(function () {
    function RefreshNavBar() {
        setTimeout(function () {
            CountNotif();
            RefreshNavBar();
        }, 3000);
    }
    RefreshNavBar();

    $(".textArea-comment").focus(function (e) {
        RefreshNavBar();
    });

    function CountNotif() {

        $.ajax({
            url: countNotifUrl,
            type: 'GET',
            dataType: 'JSON',
            success: function (result) {
                $('#other-notification').text(result.result);
            }
        })
    }

    $("#notifications").click(function () {
        $.ajax({
            url: getNotificationItemUrl,
            dataType: 'html',
            success: function (result) {
                $('#notificationContainer').load(result);
            }
        })

        $.ajax({
            url: sawNotificationURL,
            type: 'POST',
            success: function (data) {
            },

            error: function () {
                window.location.href = errorURL;
            }
        })
    });

    $('#logo-icon').hover(function () {
        $(this).attr('src', blackLogo);
    });

    $('#logo-icon').mouseout(function () {
        $(this).attr('src', grayLogo);
    });

    $('#likeModal').on('show.bs.modal', function () {
        $(this).find('.modal-content').css({
            width: 'auto', 
            height: 'auto',
            'max-height': '100%'
        });
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
                window.location.href = errorURL;

            }
        })
    });

    $(document).delegate(".btnIgnoreRequest", "click", function () {
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

    $("#btnCancel").click(function (event) {
        event.preventDefault();
        var url = cancelURL;
        window.location.href = url;
    });

});
