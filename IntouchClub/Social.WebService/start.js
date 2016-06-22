$(document).ready(function() {

    var social = $.connection.fitnessHub;
    var mac = localStorage.mac;


    social.client.onConnected = function(allUsers, friends, request) {
        console.log(allUsers);
        console.log(friends);
        console.log(request);
        for (i = 0; i < allUsers.length; i++) {
            if (allUsers[i].mac == mac) {
                //$("#users").append($("<a>", { html: allUsers[i].Name + " - " + allUsers[i].online, href: "/Personal.html?suser=" + allUsers[i].SocialUserId })).append($("<br>"));
            } else {
                $(".members")
                    .append($("<div>", { class: "member" })
                                .append($("<img>", { class: "photo", src: "http://lorempixel.com/100/100/people" }))
                                .append($("<a>", { href: "people_zoom.html?userId=" + allUsers[i].SocialUserId })
                                    .append($("<span>", { class: "name", html: allUsers[i].Name })))
                                .append($("<div>", {class: "add", html: "+"}))).append($("<br>"));
            }
            
        }

        for (i = 0; i < friends.length; i++) {
            $(".friends").append($("<div>", { class: "friend member" })
                                .append($("<img>", { class: "photo", src: "http://lorempixel.com/100/100/people" }))
                                .append($("<a>", { href: "people_zoom.html?userId=" + friends[i].SocialUserId })
                                    .append($("<span>", { class: "name", html: friends[i].Name })))
                                .append($("<div>", { class: "add", html: "+" }))).append($("<br>"));
        }

        for (i = 0; i < request.length; i++) {
            $(".friends-request")
                                .append($("<div>", { class: "request member" })
                                .append($("<img>", { class: "photo", src: "http://lorempixel.com/100/100/people" }))
                                .append($("<a>", { href: "people_zoom.html?userId=" + request[i].SocialUserId })
                                    .append($("<span>", { class: "name", html: request[i].Name })))
                                .append($("<div>", { class: "add", html: "+" }))).append($("<br>"));
        }
    }

    social.client.onOldUserConnected = function(userInfo) {
        console.log(userInfo);
    }

    social.client.onNewUserConnected = function (userInfo) {
        $("#users").append($("<a>", { html: userInfo.Name + " - " + userInfo.online, href: "localhost/" + userInfo.SocialUserId })).append($("<br>"));
    }

    social.client.countMessage = function (msgs, conversations, dialogs) {
        console.log(dialogs);
        //$(window).trigger("ic_newMessage", { msgs: msgs, conversations: conversations, dialogs: dialogs });
    }

    $.connection.hub.start().done(function() {
        social.server.connect("4c647280-4613-424d-99e6-87899f6e44f4");
    });


    //$.connection.hub.disconnected(function () {
    //    if ($.connection.hub.lastError)
    //    { alert("Disconnected. Reason: " + $.connection.hub.lastError.message); }
    //});
});