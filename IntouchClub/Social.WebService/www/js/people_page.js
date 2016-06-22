$(document).ready(function (e) {

    var social = $.connection.mainHub;

    function getParam(paramName) {
        var match = location.href.match(new RegExp(".?" + paramName + "=([^&]*)"));
        return match ? match[1] : null;
    }



    var oponentid = getParam("userId");
    console.log(oponentid);
    var mac = localStorage.mac;

    function people(oponentid, mac) {
        $.ajax({
            url: "http://localhost:1700/Home/People",
            async: false,
            data: { oponentId: oponentid, mac: mac },
            success: function (result) {
                console.log(result);
                $("#people-zoom").append($("<h1>", { html: result.Name }))
                    .append($("<div>", { class: "people-zoom" }).append($("<img>", { src: "http://lorempixel.com/300/300/people" })));
                //.append($("<button>", { type: "button", html: "Начать общение" }).append($("<img>", { src: "http://lorempixel.com/20/20/abstract" })));
                if (result.status == "user") {
                    $("#people-zoom").append($("<button>", { type: "button", html: "Добавить в друзья", class: "addFriend" }));
                } else if (result.status == "request") {
                    $("#people-zoom").append($("<button>", { type: "button", html: "Подтвердить дружбу", class: "confirmFriend" }));
                    $("#people-zoom").append($("<button>", { type: "button", html: "Отказать в общении", class: "discardFriend" }));
                } else if (result.status == "friend") {
                    $("#people-zoom").append($("<button>", { type: "button", html: "Начать общение", class: "startChat" }));
                    $("#people-zoom").append($("<button>", { type: "button", html: "Удалить из друзей", class: "deleteFriend" }));
                } else if (result.status == "confirm") {
                    $("#people-zoom").append($("<button>", { type: "button", html: "Ждем подтверждения", class: "timeout", disabled: "disabled" }));     
                }
            }
        });
    }

    //Функции выполняемые со стороны пользователя
    social.client.onFriendRequest = function () {
        $("#people-zoom>.addFriend").html("Ждем подтверждения");
        $("#people-zoom>.addFriend").attr({ class: "timeout", disabled: "disabled" });   
    }

    social.client.onFriendDelete = function () {
        $(".startChat").remove();
        $(".deleteFriend").html("Добавить в друзья");
        $(".deleteFriend").attr({ class: "addFriend" });      
    }

    social.client.onFriendConfirm = function () {
        $(".confirmFriend").html("Начать общение");
        $(".confirmFriend").attr({ class: "startChat" });
        $(".discardFriend").html("Удалить из друзей");
        $(".discardFriend").attr({ class: "deleteFriend" });
    }

    social.client.onFriendDiscard = function () {
        $(".confirmFriend").remove();
        $(".discardFriend").html("Добавить в друзья");
        $(".discardFriend").attr({ class: "addFriend" });
    }

    $(document).on("click", ".startChat", function() {
        $.ajax({
            url: "http://localhost:1700/Home/ConversationSearch",
            async: false,
            data: { toUserId: oponentid, mac: mac },
            success: function (ConversationId) {
                location.href = "/chatroom.html?Conversation=" + ConversationId;
            }
        });
    });



    // Функции выполняемые со стороны опонента

    social.client.onClientFriendRequest = function() {
        $(".addFriend").remove();
        $("#people-zoom").append($("<button>", { type: "button", html: "Подтвердить дружбу", class: "confirmFriend" }));
        $("#people-zoom").append($("<button>", { type: "button", html: "Отказать в общении", class: "discardFriend" }));
    }

    social.client.onClientFriendConfirm = function () {
        $(".timeout").remove();
        $("#people-zoom").append($("<button>", { type: "button", html: "Начать общение", class: "startChat" }));
        $("#people-zoom").append($("<button>", { type: "button", html: "Удалить из друзей", class: "deleteFriend" }));
    }

    social.client.onClientFriendDelete = function () {
        $(".startChat").remove();
        $(".deleteFriend").remove();
        $("#people-zoom").append($("<button>", { type: "button", html: "Добавить в друзья", class: "addFriend" }));
    }

    social.client.onClientFriendDiscard = function () {
        $(".timeout").remove();
        $("#people-zoom").append($("<button>", { type: "button", html: "Добавить в друзья", class: "addFriend" }));
    }

    $.connection.hub.start().done(function () {
        social.server.connect(mac);

        $(document).on("click", ".addFriend", function () {
            social.server.friendRequest(mac, oponentid);
        });

        $(document).on("click", ".confirmFriend", function () {
            social.server.friendConfirm(mac, oponentid);
        });

        $(document).on("click", ".deleteFriend", function () {
            social.server.friendDelete(mac, oponentid);
        });

        //$(document).on("click", ".startChat", function () {

        //});

        $(document).on("click", ".discardFriend", function () {
            social.server.friendDiscard(mac, oponentid);
        });
    });


    people(oponentid, mac);
});