$(document).ready(function (e) {

    //var ConversationId = location.search.substr(14);

    function getParam(paramName) {
        var match = location.href.match(new RegExp(".?" + paramName + "=([^&]*)"));
        return match ? match[1] : null;
    }

    var ConversationId = getParam("Conversation");
    var mac = localStorage.mac;
    console.log(ConversationId);

    $.ajax({
        url: "/Home/Readed",
        async: false,
        data: { mac: mac, ConversationId: ConversationId },
        success: function (result) {
            console.log("readed");
        }
    });

    // var chatHub = $.connection.chatHub;
    var social = $.connection.mainHub;
    
    /*
    chatHub.client.onConnected = function (id, userName, allUsers, messages) {

        
        // Add All Users
        for (i = 0; i < allUsers.length; i++) {
            if (allUsers[i].Id == localStorage.UserID) {
                $("#divusers").append($("<div>", { class: "user", userId: allUsers[i].Id, table: allUsers[i].TableNo, ConnectionId: allUsers[i].ConnectionId }).append($("<span>", { html: allUsers[i].Name, href: "Personal.html?User=" + allUsers[i].Id })));
            } else {
                $("#divusers").append($("<div>", { class: "user", userId: allUsers[i].Id, table: allUsers[i].TableNo, ConnectionId: allUsers[i].ConnectionId }).append($("<a>", { html: allUsers[i].Name, href: "Personal.html?User=" + allUsers[i].Id })));
            }
            
        }

        // Add Existing Messages
        for (i = 0; i < messages.length; i++) {
            $("#divChatWindow").append($("<p>", { html: "<strong>" + messages[i].SenderName + "</strong>: " + messages[i].Message }))
        }


    }*/

    social.client.onConnectedToConversation = function (allUsers, messages, Oponent) {
        //console.log(allUsers);
        //console.log(messages);
        $("#divChat").attr("conversation", ConversationId);

        $("#divusers").append($("<div>", { class: "user", userId: Oponent.Id }).append($("<a>", { html: Oponent.Name, href: "Personal.html?User=" + Oponent.Id })));
        //for (i = 0; i < allUsers.length; i++) {
        //    if (allUsers[i].Id == localStorage.UserID) {
        //        $("#divusers").append($("<div>", { class: "user", userId: allUsers[i].Id, table: allUsers[i].TableNo, ConnectionId: allUsers[i].ConnectionId }).append($("<span>", { html: allUsers[i].Name, href: "Personal.html?User=" + allUsers[i].Id })));
        //    } else {
        //        $("#divusers").append($("<div>", { class: "user", userId: allUsers[i].Id, table: allUsers[i].TableNo, ConnectionId: allUsers[i].ConnectionId }).append($("<a>", { html: allUsers[i].Name, href: "Personal.html?User=" + allUsers[i].Id })));
        //    }

        //}


        // Add Existing Messages
        for (i = 0; i < messages.length; i++) {
            $("#divChatWindow").append($("<p>", { html: "<strong>" + messages[i].SenderName + "</strong>: " + messages[i].Message }))
        }
    }

    social.client.sendPrivateMessage = function (ConvId, Name, msg, msgId) {
        if ($("#divChat[conversation=" + ConvId + "]").length) {
            console.log(msgId);
            isRead(localStorage.mac, msgId);
            $("#divChat[conversation=" + ConvId + "] .content #divChatWindow").append($("<p>", { html: "<strong>" + Name + "</strong>: " + msg }))
        }
    }

    

    $.connection.hub.start().done(function () {
        social.server.connect(mac);
        social.server.connectToPersonalConversation(ConversationId, mac);

        $("#btnSendMsg").click(function () {
            social.server.sendMsgToPrivateConversation(ConversationId, $("#txtMessage").val(), mac);
            $("#txtMessage").val('');
            social.server.countForMessage(mac, ConversationId);
        });
    });




    function isRead(mac, MessageID) {
        $.ajax({
            url: "http://localhost:1700/Home/isRead",
            async: false,
            data: { mac: mac, MessageId: MessageID },
            success: function (result) {
                console.log(result);
            }
        });
    }

});