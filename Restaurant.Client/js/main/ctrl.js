restaurantApp.run(["$routeParams", "$rootScope", "$location", "$timeout", "$http", "sessionData", "userIdService", "signalRHubProxy",
  function ($routeParams, $rootScope, $location, $timeout, $http, sessionData, userIdService, signalR) {
  $rootScope.vmm = {};

  $rootScope.vmm.bonusCardUlr = sessionData.bonusCardUlr;

  userIdService.readUserId();
  if (localStorage.getItem("order") !== null) {
    sessionData.order = JSON.parse(localStorage.order);//заказ переводим из localStarage в value
  };
  if (localStorage.getItem("orderID") !== null) {
    sessionData.orderID = localStorage.orderID;//переводим ID заказа из localStarage в value
  };
  $rootScope.buttonHide = function() {
    if (localStorage.sessionId != "undefined" && localStorage.sessionId != undefined && !localStorage.getItem('agent')) {
      $rootScope.vmm.buttonHide = false;
    } else {
      $rootScope.vmm.buttonHide = true;
    };
  };
    //Синхронизация pointId и globalPoint с Local Storage
    $rootScope.vmm.globalPoint = localStorage.getItem("globalPoint");
    $rootScope.vmm.pointId = localStorage.getItem("point");


  $rootScope.vmm.userId = getCookie("userId");

    //Проверка зарегистрирован ли юзер
  $rootScope.$on( "$routeChangeSuccess", function() {



    if (localStorage.sessionId != "undefined" && localStorage.sessionId != undefined && !localStorage.getItem('agent')) {

      $rootScope.vmm.userIsNotRegistred = false;

        if (getCookie("userId") != undefined) {

        $rootScope.vmm.userId = getCookie("userId");

      }
    } else {

      if (getCookie("userId") != undefined) {

        $rootScope.vmm.userIsNotRegistred = false;

      } else {

        $location.path("/error");
        $rootScope.vmm.userIsNotRegistred = true;

      }
    };
  });

  if(!localStorage.getItem('point') && location.hash.search("/start") == -1) {//если в localStorage нет point, переадресовываем на страницу выбора клуба
    if(!localStorage.getItem('status'))//выполняется только если пользователь не администратор
      var result;
      var str = location.hash;
      var pagesWithNoPointAsk = //"проходимся" по страницам с которых не будет происходить переход на "выбор клуба"
      [
        "/admin_panel",
        "/admin_auth"
      ];
      for (var i = pagesWithNoPointAsk.length - 1; i >= 0; i--) {
        result = str.search(pagesWithNoPointAsk[i]);
        if (result !== -1) { break;} else {
          $timeout(function() {$location.path("/club_select");}, 3000);
        };
      };

  };
  $rootScope.buttonHide();
  $rootScope.$on( "$routeChangeSuccess", function() {
    $rootScope.buttonHide();
  });

  $rootScope.$on( "$routeChangeSuccess", function() {
    $http({
      method:'GET',
      url: sessionData.server + 'Club/OneClub',
      params: {
        "pointId": sessionData.clubId
      }
    }).success(function(result) {
      $rootScope.vmm.clubName = result.Rest.Name;
      $rootScope.vmm.clubNetwork = result.Rest.Network;
      $rootScope.vmm.clubAddress = result.Rest.Address;
      $rootScope.vmm.clubTelephone = result.Rest.Phone;
    });
  });
  //Получение количества бонусов
  $rootScope.$on( "$routeChangeSuccess", function() {
    $rootScope.vmm.bonusesShow = false;
    if (localStorage.sessionId != "undefined" && localStorage.sessionId != undefined && !localStorage.getItem('agent')) {
      $http({
        method:'GET',
        url: sessionData.server + 'User/GetUserBonus',
        params: {
          "sessionId": sessionData.sessionId
        }
      }).success(function(result) {
        if (result != false) {
          $rootScope.vmm.bonusesShow = true;
          $rootScope.vmm.bonuses = result;
        } else {
          $rootScope.vmm.bonusesShow = false;
        };
      }).error(function(){
        $rootScope.vmm.bonusesShow = false;
      });
    };
  });
  $rootScope.vmm.addZeroTODate = function(num) {
    num = num.toString();
    if (num.length < 2) {
        num = "0" + num;
    }
    return num;
  }
  $rootScope.vmm.bonusOverTenThousand = function() {
    if ($rootScope.vmm.bonuses > 1000) {
      var digitWithKCharacter = Math.floor($rootScope.vmm.bonuses / 1000) + "k";
      return digitWithKCharacter;
    } else {
      return $rootScope.vmm.bonuses;
    };
  }
  $rootScope.vmm.logout = function() {
    $http({
      method:"GET",
      url: sessionData.server + "User/CloseSession",
      params: {
        "sessionId" : localStorage.sessionId
      }
    }).success(function(result){
      delete localStorage.sessionId;
      delete sessionData.sessionId;
      $rootScope.vmm.buttonHide = true;
      $rootScope.vmm.userIsNotRegistred = true;
      delete localStorage.orderID;
      $location.path("/home");
    });
  }
  $rootScope.vmm.stopPropagation = function($event) {
    $event.stopPropagation();
  }
  $rootScope.go = function (path) {
    $location.path(path);
  };
  $rootScope.goForRegistred = function (path) {
    if ($rootScope.vmm.userIsNotRegistred != true) {
      $location.path(path);
    };
  };
}]);


restaurantApp.controller("chatCtrl", ["$scope", "$http", "sessionData", "$timeout", "signalRHubProxy",
  function ($scope, $http, sessionData, $timeout, signalRHubProxy) {
  var vm = this;

  vm.popupShow = true;
  if (localStorage.getItem("userName")){
    vm.popupShow = false;
  };
  vm.userChange = function() {
    if ($(".validate-error")[0] == undefined) {
      vm.errorMark = false;
      localStorage.userName = vm.userName;
      vm.popupShow = false;
    } else {
      vm.errorMark = true;
    };
  }

  vm.imagePath = sessionData.imagePath + "Photo/";
  vm.msgContent = "";
  vm.socialConnection = signalRHubProxy();

  vm.socialConnection.connection.start().done(function () {
      vm.socialConnection.social.invoke("Connect", sessionData.sessionId);
      vm.socialConnection.social.invoke("ConnectToConversation", sessionData.sessionId, sessionData.clubId);
      vm.socialConnection.social.invoke("GetMessages", sessionData.sessionId, sessionData.clubId);
      vm.socialConnection.social.invoke("UsersInChat", sessionData.sessionId, sessionData.clubId);
  });

  vm.socialConnection.social.on("onMessage", function (msg) {
      $timeout(function(){vm.scroll = true;},10)
      vm.msgs = msg;
  });
  vm.AddZeroTODate = function (num) {
      num = num.toString();
      if (num.length === 1) {
          num = "0" + num;
      }
      return num;
  };
    function htmlDecode(value) {
      return $('<div/>').html(value).text();
  }
  vm.sendMsg = function (string) {
      string = htmlDecode(string);
      $timeout(function(){vm.scroll = true;},10)
      if ($scope.vm.msgPhotoName) {
          vm.socialConnection.social.invoke("SendMessage", $scope.vm.msgPhotoName, 1, sessionData.sessionId, sessionData.clubId);
          $scope.vm.msgPhotoName = null;
      } else {
          vm.socialConnection.social.invoke("SendMessage", string, 0, sessionData.sessionId, sessionData.clubId);
          $scope.vm.msgContent = "" ;
      }
  }
  vm.socialConnection.startResponse("onConnected", function (con) {
  });
  vm.socialConnection.startResponse("sendMessages", function (msgs) {
      $timeout(function(){vm.scroll = true;},10)
      vm.msgs.push(msgs);
  });
  vm.socialConnection.startResponse("sendMessagesToCaller", function (cal) {
      vm.msgs.push(cal);
  });


}]);

restaurantApp.controller("clubSelectCtrl", ["$rootScope", "$scope", "$http", "$location", "$timeout", "sessionData", function($rootScope, $scope, $http, $location, $timeout, sessionData) {
  var vm = this;
  vm.clubClick = function(point, globalPoint, nameOfClub) {
    localStorage.setItem("point", point);
    sessionData.clubId = point;
    localStorage.setItem("pointName", nameOfClub);

    localStorage.setItem("globalPoint", globalPoint);
    $rootScope.vmm.globalPoint = globalPoint;

    window.history.back();
  };
  if (navigator.geolocation) {

    navigator.geolocation.getCurrentPosition(function(success) {
      $scope.$apply(function(){
        vm.position = success;
        $http({
          method :'GET',
          url : sessionData.server + 'Club/AllClubs',
          params : {
            "latitude" :success.coords.latitude,
            "longitude":success.coords.longitude
          }}).success(function(result) {
          vm.clubs = result.Rest;
          vm.showDistanse = true;
            ymaps.ready(init);
            function init() {
                myMap = new ymaps.Map("map", {
                    center: [success.coords.latitude, success.coords.longitude],
                    zoom: 10
                });
                var myPositionPlacemark = new ymaps.Placemark([success.coords.latitude, success.coords.longitude], {
                    hintContent: "Мое местоположение"
                }, {
                    iconImageSize: [30, 42],
                    iconImageOffset: [-3, -42]
                });
                myMap.geoObjects.add(myPositionPlacemark);
                $.each(result.Rest, function() {
                    var that = this;
                    var myPlacemark = new ymaps.Placemark([this.latitude, this.longitude], {
                        hintContent: String(this.Name),
                    }, {
                        iconLayout: 'default#image',
                        iconImageHref: 'img/balloon.png',
                        iconImageSize: [30, 42],
                        iconImageOffset: [-3, -42]
                    });
                    myMap.geoObjects.add(myPlacemark);
                });
              }
          });
      });
    }, function(error){
      vm.clubsWithOutNavigator();
    }, { frequency:5000, maximumAge: 0, timeout: 100, enableHighAccuracy:true }
    );
  } else {
    vm.clubsWithOutNavigator();
  };
  vm.clubsWithOutNavigator = function() {
    $http({
      method:'GET',
      url: sessionData.server + 'Club/AllClubs',
      params: {
      "sessionId" : sessionData.sessionId,
      "latitude" : null,
      "longitude" : null
      }
    }).success(function(result) {
      vm.clubs = result.Rest;
        function init() {
          myMap = new ymaps.Map("map", {
              center: [55.76, 37.64],
              zoom: 10
          });
          var myPositionPlacemark = new ymaps.Placemark([55.76, 37.64], {
              hintContent: "Мое местоположение"
          }, {
              iconImageSize: [30, 42],
              iconImageOffset: [-3, -42]
          });
          myMap.geoObjects.add(myPositionPlacemark);
          $.each(vm.clubs, function() {
              var that = this;
              var myPlacemark = new ymaps.Placemark([this.latitude, this.longitude], {
                  hintContent: String(this.Name),
              }, {
                  iconLayout: 'default#image',
                  iconImageHref: 'img/balloon.png',
                  iconImageSize: [30, 42],
                  iconImageOffset: [-3, -42]
              });
              myMap.geoObjects.add(myPlacemark);
          });
        }
        ymaps.ready(init);
    });
  };
  vm.functionForChat = function(PointId) {
    $http({
      method:'GET',
      url: sessionData.server + 'Chat/ConnectToConversation',
      params: {
        'pointId' : PointId,
        'sessionId' : sessionData.sessionId
      }
    }).success(function(result) {
    });
  };
}]);

restaurantApp.controller("clubMapCtrl", ["$scope", "$http", "sessionData", function($scope, $http, sessionData) {
  var vm = this;
  vm.clubClick = function(point, nameOfClub) {
    localStorage.setItem("point", point);
    sessionData.clubId = point;
    localStorage.setItem("pointName", nameOfClub);
    window.history.back();
  };
  if (navigator.geolocation) {
    //$timeout(function(){vm.clubsWithOutNavigator();}, 10000)
    navigator.geolocation.getCurrentPosition(function(success) {
      $scope.$apply(function(){
        vm.position = success;
        $http({
          method :'GET',
          url : sessionData.server + 'Club/AllClubs',
          params : {
            "latitude" :success.coords.latitude,
            "longitude":success.coords.longitude
          }}).success(function(result) {
          vm.clubs = result.Rest;
          vm.showDistanse = true;
            ymaps.ready(init);
            function init() {
                myMap = new ymaps.Map("map", {
                    center: [success.coords.latitude, success.coords.longitude],
                    zoom: 10
                });
                var myPositionPlacemark = new ymaps.Placemark([success.coords.latitude, success.coords.longitude], {
                    hintContent: "Мое местоположение"
                }, {
                    iconImageSize: [30, 42],
                    iconImageOffset: [-3, -42]
                });
                myMap.geoObjects.add(myPositionPlacemark);
                $.each(result.Rest, function() {
                    var that = this;
                    var myPlacemark = new ymaps.Placemark([this.latitude, this.longitude], {
                        hintContent: String(this.Name),
                        balloonContent: String(this.Name) + "<br>" + String(this.Address) + "<br>Расстояние до клуба: " + String(this.distance) + "<br><button class='club-id' club-id='" + this.PointId + "' club-name='" + this.Name + "'>Перейти</button>"
                    }, {
                        iconLayout: 'default#image',
                        iconImageHref: 'img/balloon.png',
                        iconImageSize: [30, 42],
                        iconImageOffset: [-3, -42]
                    });
                    myMap.geoObjects.add(myPlacemark);
                });
              }
          });
      });
    }, function(error){
      vm.clubsWithOutNavigator();
    }, { frequency:5000, maximumAge: 0, timeout: 100, enableHighAccuracy:true }
    );
  } else {
    vm.clubsWithOutNavigator();
  };
  vm.clubsWithOutNavigator = function() {
    $http({
      method:'GET',
      url: sessionData.server + 'Club/AllClubs',
      params: {
      "sessionId" : sessionData.sessionId,
      "latitude" : null,
      "longitude" : null
      }
    }).success(function(result) {
      vm.clubs = result.Rest;
        function init() {
          myMap = new ymaps.Map("map", {
              center: [55.76, 37.64],
              zoom: 10
          });
          var myPositionPlacemark = new ymaps.Placemark([55.76, 37.64], {
              hintContent: "Мое местоположение"
          }, {
              iconImageSize: [30, 42],
              iconImageOffset: [-3, -42]
          });
          myMap.geoObjects.add(myPositionPlacemark);
          $.each(vm.clubs, function() {
              var that = this;
              var myPlacemark = new ymaps.Placemark([this.latitude, this.longitude], {
                  hintContent: String(this.Name),
                  balloonContent: String(this.Name) + "<br>" + String(this.Address) + "<br>Расстояние до клуба: " + String(this.distance) + "<br><button class='club-id' club-id='" + this.PointId + "' club-name='" + this.Name + "'>Перейти</button>"
              }, {
                  iconLayout: 'default#image',
                  iconImageHref: 'img/balloon.png',
                  iconImageSize: [30, 42],
                  iconImageOffset: [-3, -42]
              });
              myMap.geoObjects.add(myPlacemark);
          });
        }
        ymaps.ready(init);
    });
  };
}]);

restaurantApp.controller("getParamsCtrl", ["$location", "$routeParams" ,"$rootScope", "userIdService", "sessionData", "$http",
  function ($location, $routeParams, $rootScope, userIdService, sessionData, $http) {

    var vm = this;
    vm.userId = $routeParams["userId"];
    vm.pointId = $routeParams["pointId"];
    userIdService.deleteUserId();

    $rootScope.vmm.globalPoint = $routeParams["pointId"];
    localStorage.setItem("globalPoint", $routeParams["pointId"]);

    userIdService.setUserId(vm.userId);

    $http({
      method :'GET',
      url : sessionData.server + 'Cabinet/GetSession',
      params : {
        "pointId": vm.pointId,
        "userId" : vm.userId
      }}).success(function(result) {

      sessionData.sessionId = result.Session;
      localStorage.sessionId = result.Session;

      localStorage.setItem("point", result.point);
      sessionData.clubId = result.point;
      $rootScope.vmm.pointId = result.point;

      $location.path("/home");
    });

  }]);

restaurantApp.controller("homeCtrl", ["$routeParams" ,"$rootScope", "$scope", "$http", "sessionData", "userIdService",
  function ($routeParams, $rootScope, $scope, $http, sessionData, userIdService) {
  var vm = this;

  $scope.sliderProportions = 0.57;
  vm.photoPath = sessionData.imagePath + "mainphoto/";
  $http({
    method:'GET',
    url: sessionData.server + 'Club/GetMainPhotos',
    params: {
      'pointId' : sessionData.clubId
    }
  }).success(function(result) {
    vm.slides = result;
    for (var i = vm.slides.length - 1; i >= 0; i--) {
      vm.slides[i] = {'pictureUrl' : vm.photoPath + vm.slides[i].Photo};
    };
  });
  $http({
    method:'GET',
    url: sessionData.server + 'Club/RestNetworkInfo',
    params: {
      'pointId':sessionData.clubId
    }
  }).success(function(result) {
    vm.clubDescription = result.ClubDescription;
  });
}]);

restaurantApp.controller("profileCtrl", [ "$scope", "$http", "sessionData", function($scope, $http, sessionData) {
  var vm = this;

  $http({
    method:'GET',
    url: sessionData.server + 'Cabinet/GetUserInfo',
    params: {
      'sessionId' : sessionData.sessionId
    }
  }).success(function(result) {
    vm.userName = result[0].Name;
    vm.userEmail = result[0].Email;
    vm.userPhone = result[0].Phone;
    vm.userPhoto = sessionData.imagePath + "photo/" + result[0].Photo;

  });
  vm.userChange = function(type) {
    if ($(".validate-error")[0] == undefined) {

      if (type == "name") {
        $http({
          method:'GET',
          url: sessionData.server + 'Cabinet/NameEdit',
          params: {
            'sessionId' : sessionData.sessionId,
            'name' : vm.userName
          }
        }).success(function(result) {
          if (result) {
            popup.popupChangeHide();
            vm.errorMark = false;
          } else {
            vm.errorMark = true;
          };
        });
      };

      if (type == "email") {
        $http({
          method:'GET',
          url: sessionData.server + 'Cabinet/EmailEdit',
          params: {
            'sessionId' : sessionData.sessionId,
            'email' : vm.userEmail
          }
        }).success(function(result) {
          if (result) {
            popup.popupChangeHide();
            vm.errorMark = false;
          } else {
            vm.errorMark = true;
          };
        });
      };

      if (type == "password") {
        $http({
          method:'GET',
          url: sessionData.server + 'Cabinet/ChangePassword',
          params: {
            'sessionId' : sessionData.sessionId,
            'oldPassword' : vm.userPasswordOld,
            'newPassword' : vm.userPasswordNew
          }
        }).success(function(result) {
          if (result) {
            popup.popupChangeHide();
            vm.errorMark = false;
          } else {
            vm.errorMark = true;
          };
        });
      };

      if (type == "phone") {
        vm.codeChangeStage = false;
        $http({
          method:'GET',
          url: sessionData.server + 'Cabinet/SendSms',
          params: {
            'phone' : vm.userPhone,
            'sessionId' : sessionData.sessionId
          }
        }).success(function(result) {
          vm.codeChangeStage = true;
        });
      };

    };
  }
  vm.popupCall = function(type){
    popup.popupChangeShow(type);
  };
  vm.popupHide = function () {
    popup.popupChangeHide();
  };
  vm.changePhone = function(){
    $http({
      method:'GET',
      url: sessionData.server + 'Cabinet/ChangePhone',
      params: {
        'code' : vm.userCode,
        'sessionId' : sessionData.sessionId,
        'newPhone' : vm.userPhone
      }
    }).success(function(result) {
      if (result) {
        popup.popupChangeHide();
        vm.errorMark = false;
        vm.codeChangeStage = false;
      } else {
        vm.errorMark = true;
      };
    });
  };
  vm.changePhoto = function() {
    var formData = new FormData();
    var file = document.getElementById("file").files[0];
    formData.append("file", file);
    $http({
      method:'POST',
      url: sessionData.server + 'Cabinet/AddUserPhoto',
      data: formData,
      headers: {'Content-Type': undefined}
    }).success(function(result) {
      vm.userPhoto = sessionData.imagePath + "photo/" + result.photo;
      $http({
        method:'get',
        url: sessionData.server + 'Cabinet/PhotoEdit',
        params: {
          'sessionId' : sessionData.sessionId,
          'photo' : result.photo
        }
      }).success(function(result) {
      });
    });
  }
}]);

restaurantApp.controller("menuCtrl", ["$rootScope", "$scope", "$location", "$http", "$routeParams", "sessionData", function($rootScope, $scope, $location, $http, $routeParams, sessionData) {
  var vm = this;
  $scope.goBack = goBack;
  $scope.$on( "$routeChangeSuccess", function() {
    $scope.clubName = localStorage.pointName;
    var str = location.hash;
    //страницы со скрытым верхним меню
    var pagesMenuHide = 
    [
    //"/enter" //вернуть в апп
      "/admin_panel",
      "/admin_auth"
    ];
    for (var i = pagesMenuHide.length - 1; i >= 0; i--) {
      result = str.search(pagesMenuHide[i]);
      if (result !== -1) {$rootScope.hideMenuValue = true; break;}
      $rootScope.hideMenuValue = false;
    };
    //страницы со скрытым "бургером"
    var pagesBurgerHide = 
    [

    ];
    for (var i = pagesMenuHide.length - 1; i >= 0; i--) {
      result = str.search(pagesMenuHide[i]);
      if (result !== -1) {$rootScope.vmm.hideBurgerValue = true; break;}
      $rootScope.vmm.hideBurgerValue = false;
    };
    if (localStorage.getItem("point") === null) {//если заходим в выпадайку впервые, скрываем верхнее меню
      $rootScope.vmm.hideBurgerValue = true;
    }
    //страницы с кнопкой "назад" вместо "бургера"
    var pagesMenuBack = 
    [
    "/password_recovery",
    "/authorization",
    "/registration",
    "/club_map",
    "/menu_first/",
    "/menu_next",
    "/menu_last",
    "/favorites",
    "/enter", 
    "/news/", 
    "/order"
    ];
    for (var i = pagesMenuBack.length - 1; i >= 0; i--) {
      result = str.search(pagesMenuBack[i]);
      if (result !== -1) {$scope.backMenuValue = true; break;}
      $scope.backMenuValue = false;
    };
    //небольшой костыль для "бургера" вместо кнопки "назад"))
    var pagesMenuBurger = 
    [
    "/menu_first/null"
    ];
    for (var i = pagesMenuBurger.length - 1; i >= 0; i--) {
      result = str.search(pagesMenuBurger[i]);
      if (result !== -1) {$scope.backMenuValue = false; break;}
    };
    //отключение выпадайки
    var pagesWithNoVipadaika = 
    [
    "/club_map",
    "/club_select",
    "/authorization",
    "/registration"
    ];
    for (var i = pagesWithNoVipadaika.length - 1; i >= 0; i--) {
      result = str.search(pagesWithNoVipadaika[i]);
      if (result !== -1) {$scope.vipadaika = false; break;}
      $scope.vipadaika = true;
    };
    //Клик по выпадайке возвращает на предыдущий экран
    var pagesWithNoVipadaikaBack = 
    [
    "/club_select"
    ];
    for (var i = pagesWithNoVipadaikaBack.length - 1; i >= 0; i--) {
      result = str.search(pagesWithNoVipadaikaBack[i]);
      if (result !== -1) {$scope.vipadaikaBack = false; break;}
      $scope.vipadaikaBack = true;
    };
    $scope.goToFromTopMenuClick = function(){

      if ($scope.vipadaikaBack == false) {
        $scope.goBack();
      }
      if ($scope.vipadaika == true) {
        $location.path("/club_select");
      }
    };
    //отключение корзины
    var pagesWithNoCart = 
    [
    "/club_select",
    "/authorization",
    "/registration"
    ];
    for (var i = pagesWithNoCart.length - 1; i >= 0; i--) {
      result = str.search(pagesWithNoCart[i]);
      if (result !== -1) {$scope.cart = false; break;}
      $scope.cart = true;
    };
  });
}]);

restaurantApp.controller("sharesCtrl", ["$scope", "$http", "$routeParams", "sessionData", function($scope, $http, $routeParams, sessionData) {
  var vm = this;
  vm.imagePath = sessionData.imagePath + "article/";
    $http({
      method:'GET', 
      url: sessionData.server + 'Article/Article', 
      params: {
        'pointId': sessionData.clubId
      }
    }).success(function(result) {
      vm.shares = result.article;
    });
}]);

restaurantApp.controller("bonusesCtrl", ["$scope", "$http", "$routeParams", "sessionData", function($scope, $http, $routeParams, sessionData) {
  var vm = this;
  vm.ifNotAuthorized = false;
  if(!localStorage.getItem('sessionId')) {
    vm.ifNotAuthorized = true;
  };
}]);

restaurantApp.controller("reviewsCtrl", ["$scope", "$http", "$timeout", "sessionData", "customFunctions", function($scope, $http, $timeout, sessionData, customFunctions) {
  var vm = this;
  vm.maxlength = false;
  vm.review = "";
  vm.scrollPage = function (){
    customFunctions.scrollToBottomReview();
  };
  vm.scrollPageNoAnimate = function (){
    customFunctions.scrollToBottomReviewNoAnimate();
  };
  vm.refreshReviewsList = function(realRefresh){
    $http({
      method:'GET', 
      url: sessionData.server + 'Cabinet/GetReviewList', 
      params: {
        'pointId': sessionData.clubId
      }
    }).success(function(result) {
      vm.listOfReviews = result;
      $timeout(function(){vm.scroll = true;},10)
    });
  };
  vm.refreshReviewsList();
  vm.sendReview = function () {
    if (vm.review == "") {
      vm.noMessage = true;
    } else {
      $http({
        method:'POST', 
        url: sessionData.server + 'Cabinet/SendReview', 
        data: {
          'pointId': sessionData.clubId,
          'sessionId': sessionData.sessionId,
          'comment': vm.review
        }
      }).success(function(result) {
        vm.review = "";
        $timeout(function(){vm.messageSent = false;}, 3000);
        vm.messageSent = true;
        vm.refreshReviewsList(1);
      });
    };
  };
}]);

restaurantApp.controller("historyCtrl", ["$scope", "$http", "$routeParams", "sessionData", function($scope, $http, $routeParams, sessionData) {
  var vm = this;
    $http({
      method:'GET', 
      url: sessionData.server + 'Cabinet/GetOrderHistory', 
      params: {
        'sessionId': sessionData.sessionId
      }
    }).success(function(result) {
      vm.orders = result.orderHistory;
    });
}]);

restaurantApp.controller("bookingCtrl", ["$scope", "$http", "$timeout", "sessionData", function($scope, $http, $timeout, sessionData) {
  var vm = this;
  vm.bookingSuccess = false;
  var currentDate = new Date();
  vm.day = currentDate.getDate();
  vm.month = currentDate.getMonth() + 1;
  vm.hour = 18;
  vm.minute = 30;
  vm.comment = "";
  $http({
    method:'GET', 
    url: sessionData.server + 'Cabinet/GetUserInfo', 
    params: {
      'sessionId' : sessionData.sessionId
    }
  }).success(function(result) {
    vm.userPhone = result[0].Phone;
  }).error(function(){
    vm.userPhone = "";
  });
  var time = vm.hour + ':' + vm.minute;
  vm.sendReservationRequest = function(){
    if (vm.userPhone == "") {

      vm.noMessage = true;
    } else {
      $http({
        method:'GET', 
        url: sessionData.server + 'Cabinet/ReservationTable', 
        params: {
          "hour" : vm.hour,
          "minute" : vm.minute,
          "day" : vm.day,
          "month" : vm.month,
          "year" : currentDate.getFullYear(),
          "phone" : vm.userPhone,
          "people" : vm.peopleQuantity,
          "comment" : vm.comment,
          "pointId" : sessionData.clubId
        }
      }).success(function(result) {
        vm.bookingSuccess = true;
      });
    };
  };

}]);

restaurantApp.controller("feedbackCtrl", ["$scope", "$http", "$timeout", "sessionData", function($scope, $http, $timeout, sessionData) {
  var vm = this;
  vm.noMessage = false;
  vm.messageSent = false;
  vm.comment = "";
  $http({
    method:'GET', 
    url: sessionData.server + 'Cabinet/GetUserInfo', 
    params: {
      'sessionId' : sessionData.sessionId
    }
  }).success(function(result) {
    vm.userPhone = result[0].Phone;
    vm.userEmail = result[0].Email;
  }).error(function(){
    vm.userPhone = "";
    vm.userEmail = "";
  });
  vm.sendFeedback = function(){

    if (vm.comment == "" || vm.userPhone == "" || vm.userEmail == "") {
      vm.noMessage = true;
    } else {
      $http({
        method:'POST', 
        url: sessionData.server + 'Cabinet/LeaveFeedback', 
        data: {
          'pointId' : sessionData.clubId,
          'phone' : vm.userPhone,
          'email' : vm.userEmail,
          'comment' : vm.comment
        }
      }).success(function(result) {
        vm.messageSent = true;
      });
    };
  };
}]);

restaurantApp.controller("newsListCtrl", ["$scope", "$http", "sessionData", function($scope, $http, sessionData) {
  var vm = this;
  $http({
    method:'GET', 
    url: sessionData.server + 'News/News', 
    params: {
      'pointId' : sessionData.clubId
    }
  }).success(function(result) {
    vm.news = result;
  });
}]);

restaurantApp.controller("newsCtrl", ["$scope", "$http", "$routeParams", "sessionData",
  function($scope, $http, $routeParams, sessionData) {
  var vm = this;
  vm.imagePath = sessionData.imagePath + "news/";
  $scope.$on("$routeChangeSuccess", function () {
    var newsId = $routeParams["newsId"];
    $http({
      method:'GET', 
      url: sessionData.server + 'News/OneNews', 
      params: {
        'newsId' : newsId
      }
    }).success(function(result) {
      vm.newsName = result.Name;
      vm.newsDescription = result.Description;
      vm.newsImage = result.Image;
    });
  });
}]);

restaurantApp.controller("bottomMenuCtrl", ["$rootScope", "$scope", "$http", "$routeParams", "sessionData", "$location", function($rootScope, $scope, $http, $routeParams, sessionData, $location) {
  var vm = this;
  $scope.goBack = goBack;
  vm.Name = $scope.clubName;
  vm.goToChat = function(){
    if ($rootScope.vmm.userIsNotRegistred) {
    } else {
      $location.path("/chat");
    };
  };
  $scope.$on( "$routeChangeSuccess", function() {
    $scope.clubName = localStorage.pointName;
    var str = location.hash;
    var pagesBottomMenuHide = 
    [
      "/enter",
      "/registration",
      "/admin_panel",
      "/admin_auth",
      "/authorization"
    ];
    for (var i = pagesBottomMenuHide.length - 1; i >= 0; i--) {
      result = str.search(pagesBottomMenuHide[i]);
      if (result !== -1) {$rootScope.vmm.hideBottomMenuValue = true; break;}
      $rootScope.vmm.hideBottomMenuValue = false;
    };
    if (localStorage.getItem("point") === null) {//если заходим в выпадайку впервые, скрываем нижнее меню
      $rootScope.vmm.hideBottomMenuValue = true;
    }
  });
}]);

restaurantApp.controller("aboutCtrl", ["$scope", "$http", "$routeParams", "sessionData", function($scope, $http, $routeParams, sessionData) {
  var vm = this;
  vm.slides = [];
  $scope.sliderProportions = 0.57;
    $http({
      method:'GET', 
      url: sessionData.server + 'Club/RestNetworkInfo', 
      params: {
        'pointId':sessionData.clubId
      }
    }).success(function(result) {
      vm.ClubDescription = result.ClubDescription;
      vm.address = result.ClubAddress;
      vm.workingHours = result.WorkTime;
      vm.phone = result.ClubTelephone;
      vm.email = result.Email;
      for (var i = result.Photos.length - 1; i >= 0; i--) {
        vm.slides[i] = {'pictureUrl' : 'http://stas.intouchclub.ru/intouchsoft/Content/Restaurant/gallery/' + result.Photos[i].PhotoName};
      };
    });
}]);


restaurantApp.controller("downloadAppCtrl", ["$scope", "$http", "$routeParams", "sessionData", function($scope, $http, $routeParams, sessionData) {
  var vm = this;
}]);
