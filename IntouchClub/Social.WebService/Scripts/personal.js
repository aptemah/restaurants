$(document).ready(function (e) {

    function getParam(paramName) {
        var match = location.href.match(new RegExp(".?" + paramName + "=([^&]*)"));
        return match ? match[1] : null;
    }

    var userId = getParam("suser");
    var mac = localStorage.mac;
    console.log(userId);

    $("#startChat").click(function () {

        $.ajax({ 
            url: "/Home/ConversationSearch",
            async: false,
            data: { toUserId: userId, mac: mac},
            success: function (ConversationId) {
                location.href = "/chatroom.html?Conversation=" + ConversationId;
            }
        });
    });
});