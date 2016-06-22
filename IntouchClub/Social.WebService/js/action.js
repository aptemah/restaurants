$(document).ready(function () {


    $(window).bind("social_onNewUserConnected", function (e, data) {
        data.text;
    });

    $(window).bind("social_onOldUserConnected", function (e, data) {
        data.text;
    });

    $(window).bind("social_onUserDisconnected", function (e, data) {
        data.text;
    });

});
