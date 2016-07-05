$(document).ready(function(e)
{
    //server = "http://localhost:5219";
    server = "http://stas.intouchclub.ru/RestWeb";
    ClubId = 102;
    socialServer = "http://stas.intouchclub.ru/SocRest/signalr";
    session = "745AB2DC-8240-4749-BECD-0074348E8255";
    session2 = "92EC0731-8831-41ED-9C21-3C73D3FCB0BF";
    session3 = "D54126ED-E8BB-411B-89E4-5842DCD31E97";
    

    $.connection.hub.url = socialServer;
    var social = $.connection.restHub;
    $.connection.hub.start(function () {
        //social.server.connect(session);
        //social.server.usersInChat(session, ClubId);
        //social.server.sendMessage("message0123", 1, session, ClubId);
        social.server.getMessages(session, ClubId);
    });

    //social.client.onMessage = function (string) {
    //    console.log(string);
    //}

    //social.client.sendMessages = function (string) {
    //    console.log(string);
    //}

    //social.client.onConnected = function (string) {
    //    console.log(string);
    //}

    //1 зарегистрировать юзера и получить код
    //TestRegistration("044", "name", "pass");
    //2 проверка кода и получить айди сессии
    //CheckCodeAndAddToSession(2350, 4);
    //3 проверка меню
    //cat(1);
    //4 проверка продуктов
    //prod(1);
    //5 инфа об одном продуктов
    //oneProd(1);
    //6 добавление рейтинга и комментирия
    //setRating(1, 1, 2, "second")
    //oneProd(1);
    //7 открытие заказа
    //openOrder(session);
    //8 добавление в заказ
    //addToOrder(1);
    //9 каскадное удаление в меню
    //catDel(26);
    //TestPayment();
    //инфа о заказе
    //OrderInfo(1)
    //OrderSum(3);
    //история заказов по сессии
    //GetOrderHistory("aea2ddfa-a44e-45e9-b1ee-09d04edf0d66");
    //инфа о клубе
    //point(ClubId)
    //юзер инфо
    //userInfo("953ECE66-70C8-4772-93CB-F796EB76A94A");

    //addSms("123", "953ECE66-70C8-4772-93CB-F796EB76A94A")
    //changePhone(8134, "953ECE66-70C8-4772-93CB-F796EB76A94A", "123")

    //ConnectToConversation(session3, 102)

    //payment();

    //var day = 24;
    //var month = 12;
    //var year = new Date().getFullYear();
    //var hour = 15;
    //var minute = 30;

    //var time = day + "/" + month + "/" + year + " " + hour + ':' + minute + ":00 " + "GMT+0000";
    //console.log(time);
    //var comment = "хочу заказать столик епт";
    //var time = "15:30";
    //Restaurant/Web/Cabinet/ReservationTable?day=24&year=2016&month=11&time=15%3A30&phone=066&people=2&comment=%D1%85%D0%BE%D1%87%D1%83+%D0%B7%D0%B0%D0%BA%D0%B0%D0%B7%D0%B0%D1%82%D1%8C+%D1%81%D1%82%D0%BE%D0%BB%D0%B8%D0%BA+%D0%B5%D0%BF%D1%82&pointId=102
    //reservation(time, comment);
    //orderHistory(session);
    //review(ClubId);
    //getActivity(1, 1, 2016, 5, 4, 2016, ClubId);

    $("body").on("click", "#uploadFile", function () {
        //uploadFile();
        //addMenus(path, 103);
    });
    //var path = "C:/www/stas.intouchclub.ru/intouchsoft/Content/Restaurant/XLS/menu.xlsx";
    
    //addMenu(path, 103);

    //userList(102);

    //searchUser("077");

    function searchUser(searchPattern) {

        $.ajax({
            url: server + "/Staff/ClientsBySearch",
            async: false,
            data: { searchPattern: searchPattern },
            success: function (result) {
                console.log(result);
            }
        })
    }
    function userList(pointId) {
        $.ajax({
            url: server + "/Personal/GetUserList",
            async: false,
            data: {pointId: pointId},
            success: function (result) {
                console.log(result);
            }
        });
    }

    function addMenus(filePath, pointId) {
        $.ajax({
            url: server + "/Base/ParseFile",
            async: false,
            data: { pointId: pointId, filePath: filePath },
            success: function (result) {
                console.log(result);
            }
        });
    }
    
    function uploadFile() {
        var formData = new FormData();
        var file = document.getElementById("file").files[0];
        formData.append("file", file);
        formData.append("pointId", ClubId);
        $.ajax({
            url: server + "/Crud/AddExcelMenu",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function (result) {
                console.log(result);
            }
        });
    }

    function getActivity(dayStart, monthStart, yearStart, dayFinish, monthFinish, yearFinish, pointId) {
        $.ajax({
            url: server + "/Personal/ActivityByDate",
            async: false,
            data: { dayStart: dayStart, monthStart: monthStart, yearStart: yearStart, dayFinish: dayFinish, monthFinish: monthFinish, yearFinish: yearFinish, pointId: pointId },
            success: function (result) {
                console.log(result);
            }
        });
    }

    function review(pointId) {
        $.ajax({
            url: server + "/Cabinet/GetReviewList",
            async: false,
            data: { pointId: pointId },
            success: function (result) {
                console.log(result);
            }
        });
    }
    
    function orderHistory(session) {
        $.ajax({
            url: server + "/Cabinet/GetOrderHistory",
            async: false,
            data: { sessionId: session },
            success: function(result) {
                console.log(result);
            }
        });
    }

    function reservation(time, comment) {
        $.ajax({
            url: server + "/Cabinet/ReservationTable",
            async: false,
            data: { minute: "30", hour:"01", day: "24", year: "2016", month: "11", phone: "066", people: 2, comment: comment, pointId: ClubId },
            success: function (result) {
                console.log(result);
            }
        })
    }

    function payment() {

        $.ajax({
            url: server + "/Order/Payment",
            async: false,
            data: { OrderId: 18, SessionId: session, TypePayment: 1, orderSum: 1 },
            success: function (result) {
                console.log(result);
            }
        })
    }

    function ConnectToConversation(SessionId, PointId) {

        $.ajax({
            url: server + "/Chat/ConnectToConversation",
            async: false,
            data: { SessionId: SessionId, PointId: PointId },
            success: function (result) {
                console.log(result);
            }
        })
    }

    function changePhone(code, SessionId, Phone) {

        $.ajax({
            url: server + "/Cabinet/ChangePhone",
            async: false,
            data: { code: code, SessionId: SessionId, NewPhone: Phone },
            success: function (result) {
                console.log(result);
            }
        })
    }

    function addSms(Phone, SessionId) {

        $.ajax({
            url: server + "/Cabinet/SendSms",
            async: false,
            data: { Phone: Phone, SessionId: SessionId },
            success: function (result) {
                console.log(result);
            }
        })
    }

    function userInfo(userId) {

        $.ajax({
            url: server + "/Cabinet/GetUserInfo",
            async: false,
            data: { SessionId: userId },
            success: function (result) {
                console.log(result);
            }
        })
    }

    function point(pointId) {

        $.ajax({
            url: server + "/Club/RestNetworkInfo",
            async: false,
            data: { pointId: pointId },
            success: function (result) {
                console.log(result);
            }
        })
    }

    function OrderSum(orderId) {

        $.ajax({
            url: server + "/Order/GetOrderSum",
            async: false,
            data: { OrderId: orderId },
            success: function (result) {
                console.log(result);
            }
        })
    }

    function GetOrderHistory(userId) {
        $.ajax({
            url: server + "/Cabinet/GetOrderHistory",
            async: false,
            data: { SessionId: userId},
            success: function (result) {
                console.log(result);
            }
        })
    }

    function OrderInfo(orderId) {

        $.ajax({
            url: server + "/Order/OrderInfo",
            async: false,
            data: { OrderId: orderId },
            success: function (result) {
                console.log(result);
            }
        })
    }

    function TestPayment() {
        $.ajax({
            url: server + "/Order/Test",
            async: false,
            success: function (result) {
                console.log(result)
            }
        })
    }

    function TestRegistration(phone, name, password) {
        $.ajax({
            url: server + "/User/Registration",
            async: false,
            data: { phone: phone, name: name, password: password },
            success: function(result) {
                console.log(result)
            }
        });
    }

    function CheckCodeAndAddToSession(code, userId) {
        $.ajax({
            url: server + "/User/RegistrationCheckCode",
            async: false,
            data: { code: code, UserId: userId },
            success: function (result) {
                console.log(result)
            }
        })
    }

    function cat(category) {
        $.ajax({
            url: server + "/Menu/CatMenu",
            async: false,
            data: { RestId: ClubId, CatId: category },
            success: function (result) {
                console.log(result);
            }
        });
    }

    function prod(catId) {
        $.ajax({
            url: server + "/Menu/CatProd",
            async: false,
            data: { CategoryId: catId },
            success: function (result) {
                console.log(result);
            }
        })
    }

    function oneProd(prodId) {
        $.ajax({
            url: server + "/Menu/OneProd",
            async: false,
            data: { ProductId: prodId },
            success: function (result) {
                console.log(result);
            }
        })
    }

    function setRating(prodId, userId, rating, comment) {
        $.ajax({
            url: server + "/Menu/SetRating",
            async: false,
            data: { ProdId: prodId, Rating: rating, Comment: comment, UserId: userId },
            success: function (result) {
                //console.log(result);
            }
        })
    }

    function openOrder(userId) {
        var order = [];
        order.push({ 'ProductId': 1, 'Quantity': 2 });
        order.push({ 'ProductId': 3, 'Quantity': 3 });
        order.push({ 'ProductId': 5, 'Quantity': 1 });
        order.push({ 'ProductId': 11, 'Quantity': 1 });
        order.push({ 'ProductId': 12, 'Quantity': 1 });
        order.push({ 'ProductId': 23, 'Quantity': 5 });
        
        var orders = JSON.stringify(order);
        console.log(orders);

        $.ajax({
            url: server + "/Order/OpenOrder",
            async: false,
            data: { SessionId: userId, Orders: orders, TypeOrder: 1, PointId: ClubId, CookTime: "3:20" },
            success: function(result) {
                console.log(result);
            }
        });
    }

    function addToOrder(orderId) {
        var order = [];
        order.push({ 'ProductId': 1, 'Quantity': 2 });
        order.push({ 'ProductId': 3, 'Quantity': 3 });
        order.push({ 'ProductId': 5, 'Quantity': 1 });
        order.push({ 'ProductId': 11, 'Quantity': 1 });
        order.push({ 'ProductId': 12, 'Quantity': 1 });
        order.push({ 'ProductId': 23, 'Quantity': 5 });

        var orders = JSON.stringify(order);
        //console.log(orders);

        $.ajax({
            url: server + "/Order/AddToOrder",
            async: false,
            data: { OrderId: orderId, Orders: orders, TypeOrder: 2 },
            success: function (result) {
                console.log(result);
            }
        })
    }

    function catDel(catId) {
        $.ajax({
            url: server + "/AdminMenu/DeleteCategory",
            async: false,
            data: { catId: catId},
            success: function (result) {
                console.log(result);
            }
        })
    }
});
