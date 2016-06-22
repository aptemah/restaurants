$(document).ready(function (e) {

    if (location.pathname == "/Conversation.html") {
        CountMsg(localStorage.UserID);
    }

    function CountMsg(UserId) {
        $.ajax({
            url: "/Home/CountForMessage",
            async: false,
            data: {UserId: UserId},
            success: function (result) {
                console.log(result);
            }
        });
    }
});