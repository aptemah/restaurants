restaurantApp.value('sessionData', {
  "userId" : null,
  "sessionId" : localStorage.sessionId,
  "server" : "http://anton.intouchclub.ru/RestWeb/",
  "clubId" : localStorage.point,
  "imagePath" : "http://anton.intouchclub.ru/Content/Restaurant/",
  "order": [],
  "bonuses" : 0,
  "SocialServer": "http://anton.intouchclub.ru/SocRest/signalr",
  "HubName": "restHub",
  "bonusCardUrl": "http://artem.intouchclub.ru/BonusCard"
});

restaurantApp.service("userIdService", ['$rootScope', 'sessionData', function ($rootScope, sessionData) {
  return {
    "setUserId": function(id){
      document.cookie = "userId=" + id + "; expires=Tue, 19 Jan 2038 03:14:07 GMT";
    },
    "checkUserId": function(){

    },
    "readUserId": function(){
      sessionData.userId = getCookie("userId")
    },
    "deleteUserId": function(){
      document.cookie = "userId=; expires=";
    }
  };
}]);

restaurantApp.service('validationService', function(){

  var parentObj = {
    validateFormsForced : function(){
      $("input-validate").each(function(){
        $(this).append($("<div>", {"class" : "validate-sign", html: "Ошибка"}));
        parentObj.validateFunction($(this).find("input")[0]);
      });
    },
    validateFunction : function(elem){
      var x = $(elem).val();
      var inputReg = $(elem).closest("input-validate");
      var validatePattern = /./;
      if (elem.getAttribute("type") == "text")   {validatePattern = /./};
      if (elem.getAttribute("type") == "password")   {validatePattern = /./};
      if (elem.getAttribute("type") == "email")  {validatePattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,6})+$/};
      if (elem.getAttribute("type") == "number") {validatePattern = /^\d+$/};
      //if (elem.getAttribute("type") == "tel") {validatePattern = /^(\+7|7|8|\+8)(9)[0-9]{9}$/};
      if (elem.getAttribute("type") == "tel") {validatePattern = /./};
      if (validatePattern.test(x) == false) {
        inputReg.removeClass("validate-ok");
        inputReg.addClass("validate-error");
      } else {
        inputReg.addClass("validate-ok");
        inputReg.removeClass("validate-error");
      }
      var previous = elem.getAttribute("data-confirmID");
      if (previous != undefined) {//если проверяем совпадение паролей
        var previousInput = $("#" + previous)[0];
        if (previousInput.getAttribute("type") == "password")   {
            var previousVal = $(previousInput).val();
          if (x !== previousVal) {
            inputReg.removeClass("validate-ok");
            inputReg.addClass("validate-error");
            inputReg.find(".validate-sign").html("Не совпадает");
              return false;
          } else {
            inputReg.addClass("validate-ok");
            inputReg.removeClass("validate-error");
          }
        };
      };
    }
  };
  return parentObj;
});

restaurantApp.service('popup', [function(){

  var parentObj = {
    popupChangeShow : function(type){
      $("form").on("click", function(e){
        e.stopPropagation();
      });
      $(".popup-change").fadeIn();
      if (type == "name") {$(".change-name").show();};
      if (type == "email") {$(".change-email").show();};
      if (type == "password") {$(".change-password").show();};
      if (type == "phone") {$(".change-phone").show();};
    },

    popupChangeHide : function(){
      $(".popup-change").fadeOut(function(){
        $(".popup-change > form").hide();
      });

    },
    "popupShow": function(){
      $("form").on("click", function(e){
        e.stopPropagation();
      });
    },
    "checkValid": function(type) {
      if ($(".validate-error")[0] == undefined) {
        return true;
      };
    }
  };
  return parentObj;
}]);
restaurantApp.factory('httpPostFactory', ['$http', 'sessionData', function ($http, sessionData) {
    return function (file, data, callback) {
        $http({
            url: sessionData.server + file,
            method: "POST",
            data: data,
            headers: {'Content-Type': undefined}
        }).success(function (response) {
            callback(response);
        });
    };
}]);

restaurantApp.service("signalRHubProxy", ['$rootScope', 'sessionData', function ($rootScope, sessionData) {
    function signalRHubProxyService() {
        var connection = $.hubConnection(sessionData.SocialServer);
        var proxy = connection.createHubProxy(sessionData.HubName);
        proxy.on("ontest", function () {
            console.log("нет сессии!");
        });
        connection.start().done(function(){

        });

        return {
            social: proxy,
            connection: connection,

            startResponse: function (eventName, callback) {
                proxy.on(eventName, function (result) {
                    $rootScope.$apply(function () {
                        if (callback) {
                            callback(result);
                        }
                    });
                });
            },
            endResponse: function (eventName) {
                proxy.off(eventName);
            },
            response: function(eventName, callback) {
                proxy.off(eventName);
                proxy.on(eventName, function (result) {
                    $rootScope.$apply(function () {
                        if (callback) {
                            callback(result);
                        }
                    });
                });
            }
        };
    };
    return signalRHubProxyService;
}]);

restaurantApp.service('customFunctions', function(){

  var parentObj = {
    "scrollToBottom" : function(){
      $('.page div').animate({ scrollTop: $('.page div')[0].scrollHeight }, 1000);
    },
    "scrollToTop" : function(){
        $('.page div').animate({ scrollTop: 0}, 1000);
    }
  };
  return parentObj;
});
