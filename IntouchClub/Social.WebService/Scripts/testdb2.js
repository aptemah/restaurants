$(document).ready(function(e)
{
    var mac = "40:16:7E:52:29:C8";

    var chatHub = $.connection.mainHub;

    chatHub.client.onConnected = function (id, userName, allUsers, messages) {

    }

    $.connection.hub.start().done(function () {
        chatHub.server.connect(mac);
    });

});