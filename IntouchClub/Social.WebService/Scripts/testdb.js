$(document).ready(function(e)
{
    //var chatHub = $.connection.chatHub;
    var chatHub = $.connection.chatHub;
    /*
    $.ajax({
        url: "/Home/UserList",
        async: false,
        success: function (result) {
            result.ForEach(function () {
                $("#wrapper").append($("<ul>").append($("<li>", {html: this.Name})));
            });
        }
    });*/
    //console.log(chatHub);

    chatHub.client.onUserReConnected = function (id, UserId) {
        if ($("div[userid=" + UserId + "]").length) {
            $("div[userid=" + UserId + "]").attr("ConnectionId", id);
        }
    }

    chatHub.client.onConnected = function (id, userName, allUsers, messages) {
        
        for (i = 0; i < allUsers.length; i++) {
            if (allUsers[i].Id == localStorage.UserID) {
                $("#divusers").append($("<div>", { class: "user", userId: allUsers[i].Id, table: allUsers[i].TableNo, ConnectionId: allUsers[i].ConnectionId }).append($("<span>", { html: allUsers[i].Name})));
            } else {
                $("#divusers").append($("<div>", { class: "user", userId: allUsers[i].Id, table: allUsers[i].TableNo, ConnectionId: allUsers[i].ConnectionId }).append($("<a>", { html: allUsers[i].Name, href: "Personal.html?User=" + allUsers[i].Id })));
            }
            
        }

        for (i = 0; i < messages.length; i++) {

            $("#divChatWindow").append($("<p>", { html: "[" + messages[i].Created + "]<strong>" + messages[i].SenderName + "</strong>: " + messages[i].Message }))
        }


    }

    var disconnectingUserThreads = {}; // set of userId: thread
    chatHub.client.onNewUserConnected = function (id, name, tableNo, userID) {
        /*console.log("newUser1");
        
        if (disconnectingUserThreads[userId]) {
            clearTimeout(disconnectingUserThreads[userId]);
            delete disconnectingUserThreads[userId];
        }
        */
        if (location.pathname == "/Conversation.html") {
            //console.log("newUser2");
            $("#divusers").append($("<div>", { class: "user", userId: userID, table: tableNo, connectionId: id }).append($("<a>", { html: name, href: "Personal.html?User=" + userID })));
        }
    }

    chatHub.client.onUserDisconnected = function (id, uid) {
        //console.log("disconnect=" + id);
        //console.log("div[userid=\"" + uid + "\"]");
        var userId = uid;

      //  disconnectingUserThreads[userId] = setTimeout(function () {
      //      delete disconnectingUserThreads[userId];
        $("div[userid=\"" + userId + "\"]").remove();
      //  }, 1000);
    }



    chatHub.client.countMessage = function (msgs, conversations, dialogs) {
        //console.log(msgs);
        //$("#AllUnreadedMsgs").text("Всего сообщений: " + msgs);
        console.log(dialogs);
        $(window).trigger("ic_newMessage", { msgs: msgs, conversations: conversations, dialogs: dialogs });
        /*
        $("#allConversations").text("Всего диалогов: " + conversations);
        //console.log(dialogs[0].Name);
        if ($("#dialogs>#forDialogs>div[id=" + dialogs[0].Name + "]").length == 0) {
            $("#forDialogs").append($("<div>", { id: dialogs[0].DATA[0].ConversationId, class: "dialog" }).append($("<a>", { html: dialogs[0].DATA[0].OponentName + " (" + dialogs[0].Count + ")", href: "chatroom.html?Conversation=" + dialogs[0].DATA[0].ConversationId })));
        } else {
            $("div[id=" + dialogs[0].Name + "]>a").text(dialogs[0].DATA[0].OponentName+" ("+dialogs[0].Count+")");
        }*/
        
    }
    //только для новых пользователей

    $.connection.hub.start().done(function () {
        chatHub.server.connect(localStorage.UserID, localStorage.ClubID);

        $("#forCheck").click(function () {
            chatHub.server.connect(localStorage.UserID, localStorage.ClubID);
        });

        if (location.pathname == "/index.html") {
            $("#wrapper").append($("<ul>", { hidden: true }));

            $.ajax({
                url: "/Home/UserList",
                async: false,
                success: function (result) {
                    result.ForEach(function () {
                        $("ul").append($("<li>", { html: this.Name }));
                    });
                }
            });

            
        } /*else if (location.pathname == "/chatroom.html") {
            chatHub.server.connect(localStorage.UserID, localStorage.ClubID);
        } else if (location.pathname == "/Conversation.html") {
            chatHub.server.connect(localStorage.UserID, localStorage.ClubID);
        }*/




        $('#btnSendMsg').click(function () {

            var msg = $("#txtMessage").val();
            if (msg.length > 0) {
                var userId = localStorage.UserID;
                var clubId = localStorage.ClubID;
                chatHub.server.sendMessageToAll(msg, userId, clubId);
                $("#txtMessage").val('');
            }
        });
    });

    if (location.pathname == "/chatroom.html") { // TODO: ScrollTop
        chatHub.client.messageReceived = function (userName, message, date, messageID) {
            AddMessage(userName, message, date);
        }
    }


    function AddMessage(userName, message, date) {
        //$('#divChatWindow').append('<div class="message"><span class="userName">' + userName + '</span>: ' + message + '</div>');
        $("#divChatWindow").append($("<p>", { html: "[" + date + "]<strong>" + userName + "</strong>: " + message }))
        var height = $('#divChatWindow')[0].scrollHeight;
        $('#divChatWindow').scrollTop(height);
    }

    function UnreadedMsgs(user) {
        $.ajax({
            url: "/Home/Dialogs",
            async: false,
            data: { UserId: user },
            success: function (result) {
                //console.log(result);
                result.ForEach(function () {
                    $("#forDialogs").append($("<div>", { class: "dialog", id: this.DATA[0].ConversationId }).append($("<a>", { html: this.DATA[0].OponentName + " (" + this.Count + ")", href: "chatroom.html?Conversation=" + this.DATA[0].ConversationId })))
                });
            }
        });
    }

    UnreadedMsgs(localStorage.UserID);

    function UniqueDialogs(user){
        $.ajax({
            url: "/Home/UniqueDialogCount",
            async: false,
            data: { UserId: user },
            success: function (result) {
                //console.log(result);
                if (result != null) {
                    $("#allConversations").text("Всего диалогов: " + result);
                }
                
            }
        });
    }

    UniqueDialogs(localStorage.UserID);

    

    
});