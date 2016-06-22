$(document).ready(function () {
    
    var social = $.connection.mainHub;

    // Выполняется при подключении совсем нового пользователя. Которого небыло раньше в списке

    social.client.onNewUserConnected = function () {
        $(window).trigger("social_onNewUserConnected", { });
    }

    // Выполняется при подключении пользщователя который уже был в списке

    social.client.onOldUserConnected = function () {
        $(window).trigger("social_onOldUserConnected", { });
    }

    // Выполняется при отключении от приложения или сайта

    social.client.onUserDisconnected = function () {
        $(window).trigger("social_onUserDisconnected", { });
    }

    //Выполняется при подключении пользователя к личному чату с кем-то!

    social.client.onConnectedToConversation = function () {
        $(window).trigger("social_onConnectedToConversation", { });
    }

    social.client.sendPrivateMessage = function () {
        $(window).trigger("social_sendPrivateMessage", {});
    }



});