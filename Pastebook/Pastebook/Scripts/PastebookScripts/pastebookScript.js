$(document).ready(function () {
    //function RefreshNavBar() {
    //    //this will wait 3 seconds and then fire the load partial function
    //    setTimeout(function () {
    //        loadNavBar();
    //        //recall this function so that it will continue to loop
    //        RefreshNavBar();
    //    }, 3000);
    //}
    ////initialize the loop
    //RefreshNavBar();

    //function loadNavBar() {
    //    $.ajax({
    //        url: getNavBarUrl,
    //        dataType: "html",
    //        success: function (result) {
    //            $("#pastebook-nav-bar").html(result);
    //        }
    //    })
    //}

    $('#logo-icon').hover(function () {
        $(this).attr('src', '../Content/Images/logo-gray.png');
    });

    $('#logo-icon').mouseout(function () {
        $(this).attr('src', '../Content/Images/logo-black.png');
    });

});
