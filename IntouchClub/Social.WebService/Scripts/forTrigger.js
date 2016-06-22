$(document).ready(function (e) {



    $(window).bind("ic_newMessage", function (e, data) {
        $("#allConversations").text("Всего диалогов: " + data.conversations);
        //console.log(dialogs[0].Name);
        //console.log(data);
        if ($("#dialogs>#forDialogs>div[id=" + data.dialogs[0].Name + "]").length == 0) {
            $("#forDialogs").append($("<div>", { id: data.dialogs[0].DATA[0].ConversationId, class: "dialog" }).append($("<a>", { html: data.dialogs[0].DATA[0].OponentName + " (" + data.dialogs[0].Count + ")", href: "chatroom.html?Conversation=" + data.dialogs[0].DATA[0].ConversationId })));
        } else {
            $("div[id=" + data.dialogs[0].Name + "]>a").text(data.dialogs[0].DATA[0].OponentName + " (" + data.dialogs[0].Count + ")");
        }
    });


});