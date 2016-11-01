$(document).ready(function () {
    $('#other-notification').hide();

    function RefreshNavBar() {
        setTimeout(function () {
            CountNotif();
            RefreshNavBar();
        }, 3000);
    }
    RefreshNavBar();

    function CountNotif() {
        $.ajax({
            url: countNotifUrl,
            type: 'GET',
            dataType: 'JSON',
            success: function (result) {
                if (result.result > 0) {
                    $('#other-notification').show();
                    $('#other-notification').text(result.result);
                   
                }
            }
        })
    }

    $("#notifications").click(function () {
        $.ajax({
            url: sawNotificationURL,
            type: 'POST',
            success: function (data) {
                $.ajax({
                    url: getNotificationURL,
                    dataType: "html",
                    success: function (result) {
                        $("#pastebook-nav-bar-notification").html(result);
                    }
                });
            },
        })
        $('#other-notification').hide();

    });

    $('#likeModal').on('show.bs.modal', function () {
        $(this).find('.modal-content').css({
            width: 'auto', 
            height: 'auto',
            'max-height': '100%'
        });
    });

    $('#searchForm').submit(function (e) {
        if ($('#textboxSearch').val().length < 1) {
            e.preventDefault();
            $("#errorModal-header").text("Search error")
            $("#errorModal-body").text("Searching empty keyword is not allowed")
            $("#errorModal").modal('show')
        }
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
                            url: interactButtonsUrl,
                            dataType: "html",
                            success: function (result) {
                                $("#interactButtons").html(result);

                                $("#successModal-header").text("Accept friend request success")
                                $("#successModal-body").text("Friend request accepted")
                                $("#successModal").modal('show')
                            }
                        })
            },
            error: function () {
                $("#errorModal-header").text("Accept friend request error")
                $("#errorModal-body").text("Error in accepting friend request. Please try again later")
                $("#errorModal").modal('show')

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

    $("#btnCancel").click(function (event) {
        event.preventDefault();
        var url = cancelURL;
        window.location.href = url;
    });

});
