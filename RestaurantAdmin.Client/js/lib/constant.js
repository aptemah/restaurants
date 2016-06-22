ClubId = 101;
No = 0;
Count = 40;
//server = "http://localhost:5219";
server = "http://stas.intouchclub.ru/RestWeb";
//homepage = "http://localhost:34296/html/adminPage.html";
//homepage = "http://stas.intouchclub.ru/WIWAdmin/Client/html/adminPage.html";

    function CheckAdmin(sessionId) {
        var answer;
        $.ajax({
            url: server + "/Personal/CheckAdminBySessionId",
            async: false,
            data: { sessionId: sessionId },
            success: function (result) {
                answer = result;
            }
        });
        return answer;
    }

    function CheckId(sessionId) {

        if ((sessionId == null) || !(CheckAdmin(sessionId))) {
            localStorage.removeItem("SessionID");
            location.href = "../index.html";
        }
    }

    function getParam(paramName) {
        var match = location.href.match(new RegExp(".?" + paramName + "=([^&]*)"));
        return match ? match[1] : null;
    }
    
    function PopUp(selector) {
        $(selector).click(function (e) {
            $(".wrap *").hide();
            $(".grey-bg").show();
            $(".grey-bg *").show();
            addClassesToElements();
            e.preventDefault();
        });
    }

    function StaticPopUp() {
        $(".wrap *").hide();
        $(".grey-bg").show();
        $(".grey-bg *").show();
        addClassesToElements();
    }

    function StaticPopUpErrorPhone() {
        $(".wrap *").hide();
        $(".error-phone").show();
        $(".error-phone *").show();
        addClassesToElements();
    }


    function sendMail(to, toName, subject, msg, selector) {
        $.ajax({
            url: server + "/Email/Send",
            async: false,
            data: {
                From: EmailFrom,
                FromPassword: EmailFromPassword,
                FromName: EmailFromName,
                To: to,
                ToName: toName,
                Subject: subject,
                Content: msg
            },
            success: function (r) {
                if (r == "ok") {
                    StaticPopUp();
                } else {
                    alert("Не вышло!");
                }   
            }
        });
    }

    function userPhone(mac) {
        var res;
        $.ajax({
            url: server + "/User/Phone",
            async: false,
            data: {
                mac: mac
            },
            success: function (result) {
                res = result;
            }
        });
        return res;
    }

    function readCookie(name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    function ImageInMenuForLogo(image) {
        var names = image.split(".");
        var activName = names[0] + "_a";
        var newImage = activName + "." + names[1];
        return newImage;
    }
    
    

