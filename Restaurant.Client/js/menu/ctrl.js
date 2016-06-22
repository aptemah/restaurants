menuApp.run(["$rootScope", "$location", "$http", "$interval", "menuData", "sessionData", "signalRHubProxy", function ($rootScope, $location, $http, $interval, menuData, sessionData, signalRHubProxy) {
  $rootScope.vmMenu = {};
  $rootScope.vmMenu.imagePath = sessionData.imagePath + "icons/menu/";
  //Функция запускает интервальную проверку наличия неподтвержденных заказов. При подтверждении официантом — vmMenu.hasOpenOrder = false;
  $rootScope.vmMenu.waitingForOrderConfirm = function() {
    $rootScope.vmMenu.hasOpenOrder = false;
    var stopTime = $interval(function() {
    $http({
      method:'GET',
      url: sessionData.server + 'Order/ActiveInactive',
      params: {
        "sessionId" : sessionData.sessionId
      }
    }).success(function(result) {
      if (result == true) {
        $interval.cancel(stopTime);
        $rootScope.vmMenu.hasOpenOrder = false;
      } else {
        $rootScope.vmMenu.hasOpenOrder = true;
      }
    }).error(function(){
        $interval.cancel(stopTime);
        $rootScope.vmMenu.hasOpenOrder = false;
    });
    }, 10000);
  };
  $rootScope.vmMenu.waitingForOrderConfirm();
  //Функции отображения корзины
  $rootScope.vmMenu.toggleCartBadge = function() {

    var amountOfItemsIncart = 0;
    for (var i = sessionData.order.length - 1; i >= 0; i--) {
      amountOfItemsIncart += parseInt(sessionData.order[i].Quantity);
    }
    $rootScope.vmMenu.orderLength = amountOfItemsIncart;
    if ($rootScope.vmMenu.orderLength > 0) {
      $rootScope.vmMenu.badgeShow = true;
    } else {
      $rootScope.vmMenu.badgeShow = false;//убираем бейджик с корзины
    };
  };
  $rootScope.$on( "$routeChangeSuccess", function() {
    $rootScope.vmMenu.toggleCartBadge();
  });
  $rootScope.vmMenu.toggleCartBadge();

  $rootScope.vmMenu.changeAmount = function($event, state, productId, quantity, index) {
    var input = $($event.currentTarget).closest(".quantity").find("input");
    var item = $($event.currentTarget).closest(".cart-item");
    if(state == "+"){
      var number = $rootScope.vmMenu.quantityRepeat[index];
      ++number;
      $rootScope.vmMenu.quantityRepeat[index] = number;
      input.val(number)
    }
    if(state == "-"){
      var number = $rootScope.vmMenu.quantityRepeat[index];
      if (number > 1) {
        number--;
        $rootScope.vmMenu.quantityRepeat[index] = number;
        input.val(number)
      } else {
        number--;
        $rootScope.vmMenu.quantityRepeat[index] = number;
        input.val(number)
        item.remove();
        if (sessionData.order.length == 1) {$rootScope.vmMenu.ifNoOrder = true}
      };
    }
    $rootScope.vmMenu.refreshOrderList($event, productId);
  }


  $rootScope.vmMenu.refreshOrderList = function($event) {

    sessionData.order = [];
    localStorage.order = "";

    $(".cart-item").each(function(){
      var quantity = $(this).find(".cart-count-number").val();
      var productId = $(this).find("input[type=hidden]").attr("data-productId");
      sessionData.order.push({ 'ProductId': productId, 'Quantity': quantity });
    });
    var str = JSON.stringify(sessionData.order);
    localStorage.order = str;
    $rootScope.vmMenu.toggleCartBadge();
  };

  $rootScope.vmMenu.addToOrderList = function($event, productId, quantity) {
    $event.stopPropagation();
    var repeats = false;//флаг повторов на "false"
    for (var i = sessionData.order.length - 1; i >= 0; i--) {
      for (key in sessionData.order[i]) {
        if (sessionData.order[i].ProductId == productId) {
          var currentQuantity = parseInt(sessionData.order[i].Quantity);
          sessionData.order[i] = { 'ProductId': productId, 'Quantity': quantity + currentQuantity };
          var str = JSON.stringify(sessionData.order);
          localStorage.order = str;
          $rootScope.vmMenu.toggleCartBadge();
          var repeats = true;
          break
        };
      }
    };
    if (productId != undefined && quantity != undefined && !repeats) {
      sessionData.order.push({ 'ProductId': productId, 'Quantity': quantity });
      var str = JSON.stringify(sessionData.order);
      localStorage.order = str;
      $rootScope.vmMenu.toggleCartBadge();
    };
    $rootScope.vmMenu.toggleCartBadge();
  };


}]);

menuApp.controller("menuFirstCtrl", ["$scope", "$http", "sessionData", "$routeParams", function($scope, $http, sessionData, $routeParams) {
  var vm = this;
  vm.server = sessionData.server;
  var catId = null;
  $scope.$on("$routeChangeSuccess", function () {
    var catId = $routeParams["catId"];
    if (catId == "null") {
      $http({
        method:"GET",
        url: sessionData.server + "Menu/CatMenu",
        params: {
          "restId": sessionData.clubId,
          "catId": catId
        }
      }).success(function(result){
        vm.items = result;
      });
    } else {
      $http({
        method:"GET",
        url: sessionData.server + "Menu/CatMenu",
        params: {
          "restId": sessionData.clubId,
          "catId": catId
        }
      }).success(function(result){
        vm.itemsNext = result.Array;
      });
    };
  });
}]);

menuApp.controller("menuNextCtrl", ["$rootScope", "$scope", "$http", "sessionData", "$routeParams", "menuBounce", function($rootScope, $scope, $http, sessionData, $routeParams, menuBounce) {
  var vm = this;
  vm.bounce = function() {
    menuBounce.badgeBounce();
  };
  $scope.$on("$routeChangeSuccess", function () {
    var catId = $routeParams["catId"];
    if(catId!=='undefined'){
      $http({
        method:'GET',
        url: sessionData.server + 'Menu/CatProd',
        params: {
          "categoryId": catId,
          "sessionId": sessionData.sessionId
        }
      }).success(function(result) {
        vm.items = result.Array;
        vm.parentBlock = result.Parent;
        vm.statusOfPage = result.status;
        vm.parentImage = sessionData.imagePath + "icons/menu/" + result.Logo;
      });
    }
  });
}]);

menuApp.controller("menuLastCtrl", ["$scope", "$http", "sessionData", "$routeParams", "menuBounce", function($scope, $http, sessionData, $routeParams, menuBounce) {
  var vm = this;
  vm.bounce = function() {
    menuBounce.badgeBounce();
  };
  $scope.$on("$routeChangeSuccess", function () {
    var prodId = $routeParams["prodId"];
    if(prodId!=='undefined'){
      $http({
        method:'GET',
        url: sessionData.server + 'Menu/OneProd',
        params: {
          "productId": prodId,
          "sessionId": sessionData.sessionId
        }
      }).success(function(result) {
        vm.info   = result[0];
        vm.Name   = result[0].Name;
        vm.Id     = result[0].Id;
        vm.Parent = result[0].Parent;
        vm.Price  = result[0].Price;
        vm.Weight = result[0].Weight;
        vm.inFavorites = result[0].Favorite;
      });
    }
  });
  vm.addToFavorites = function() {
    $http({
      method:'GET',
      url: sessionData.server + 'Menu/AddProdToFavorite',
      params: {
        "prodId": vm.Id,
        "sessionId": sessionData.sessionId
      }
    }).success(function(result) {
    });
  };
}]);

menuApp.controller("orderCtrl", ['service.orders', "$rootScope", "$scope", "$location", "$http", "$interval", "$timeout", "sessionData", "customFunctions",
  function(orders, $rootScope, $scope, $location, $http, $interval, $timeout, sessionData, customFunctions) {
  var vm = this;
  vm.items = {};
  vm.amountObj = 0;

  vm.addToOrders = orders.addToOrders;

  vm.scrollTop = customFunctions.scrollToTop;

  vm.ifNoOrder = function(){

      if (sessionData.order.length > 0 ) {
        return false;
      } else {
        return true; //если в козине ничего нет, скрываем некоторые пункты
      };

  };

  vm.reordering = false; //настройки типа заказа присутствуют
  //$rootScope.vmm.qrCode = localStorage.qrCodeForNotConfirmedOrder;
  $rootScope.vmm.orderID = localStorage.orderID;
  $rootScope.vmm.orderCode = localStorage.orderCodeForNotConfirmedOrder;
  //функция подсчета общей стоимости
  vm.getCartAmount = function(amountObj) {
    vm.amountObj = 0;
    for ( var x in amountObj ) {
      vm.amountObj = vm.amountObj + amountObj[x];
    }
    return vm.amountObj;
  }
  //проверка наличия уже существующего заказа
  $http({
    method:'GET',
    url: sessionData.server + 'Order/CheckReorder',
    params: {
      "sessionId": sessionData.sessionId
    }
  }).success(function(result) {
    vm.orderOptions = result;
    if (result == true) {//если заказ существует
      vm.reordering = true;
    };
  });

  //получение списка заказанных товаров
  vm.getCartList = function() {

    $http({
      method:'GET',
      url: sessionData.server + 'Order/ProdToBag',
      params: {
        "products": localStorage.order
      }
    }).success(function(result) {
      vm.items = {};
      vm.items = result;
      vm.amount = function(){
        var amountOfDishes = 0;
        for (var i = vm.items.length - 1; i >= 0; i--) {
          amountOfDishes += vm.items[i].Price * vm.items[i].Quantity;
        }
        return amountOfDishes;
      }
    }).error(function(){
      vm.items = {};
      $scope.vmMenu.ifNoOrder = true;
    });

  };
  vm.getCartList();

  vm.getTotal = function(){
    var total = 0;
    for(var i = 0; i < vm.items.length; i++){
        total += (vm.items[i].Price * vm.items[i].Quantity);
    }
    return total;
  };

  vm.method = 0;
  vm.time = {
    hour : 12,
    minute : 00
  }
  vm.timeToTime = false;
  vm.popupShowQr = false;
  vm.popupShowWait = false;
  vm.popupAuthAsk = false;
  vm.openOrder = function(fromOpenOrderWithTelephon) {
    if(localStorage.getItem('sessionId')) {//проверяем есть ли sessionId, если нет, выдаем поп-ап
      if (localStorage.getItem('agent') && !fromOpenOrderWithTelephon) {//проверяем, если у пользователя временный sessionId, который дается про заказе "по телефону", а также не вызвана ли функция из функции fromOpenOrderWithTelephon
        if (vm.reordering == true) {//если уже открыт заказ, получаем телефон с сервера и делаем дозаказ
          $http({
            method:'GET',
            url: sessionData.server + 'Cabinet/GetUserInfo',
            params: {
              'sessionId' : sessionData.sessionId
            }
          }).success(function(result) {
            vm.phone = result[0].Phone;
            vm.openOrderWithTelephon();
          }).error(function(){
            vm.popupAuthAsk = true;
            vm.telephoneShow = false;
            localStorage.removeItem('sessionId');
            localStorage.removeItem('agent');
          });
        } else {//если открытых заказов нет, то выдаем попап
          vm.popupAuthAsk = true;
          vm.telephoneShow = false;
        };
      } else {//есле нормальный sessionId, то всё как обычно
        vm.timeHM = vm.time.hour + ":" + vm.time.minute;
        if (vm.timeToTime === false) {//если не выбрано время, то сценарий с QR кодом
          vm.timeHM = "";
        };
        $http({
          method:'GET',
          url: sessionData.server + 'Order/OpenOrder',
          params: {
            "sessionId" : sessionData.sessionId,
            "orders" : localStorage.order,
            "typeOrder" : vm.method,
            "pointId" : sessionData.clubId,
            "cookTime" : vm.timeHM
          }
        }).success(function(result) {
          if (result != true) {//проверяем нет ли оплаченных, но неподтвержденных заказов.
            vm.getOrdersList();
            localStorage.orderCodeForNotConfirmedOrder = result.code;
            $rootScope.vmm.orderCode = result.code;
            sessionData.orderID = result.OrderId;
            localStorage.orderID = result.OrderId;
            $rootScope.vmMenu.orderID = result.OrderId;
            if (vm.method == 0 || vm.method == 1) {//если человек в ресторане,
              if (vm.timeToTime === true || vm.reordering === true) {//если выбран заказ по времени, или мы имеем дело с дозаказом Qr код не выводим
                vm.popupShowWait = true;
                $scope.vmMenu.waitingForOrderConfirm();
              } else if (sessionData.orderID !== false) {
                vm.popupShowQrPopup();
              };
            };
            if (vm.method == 3) {
              if (sessionData.orderID !== false) {
                $scope.vmMenu.waitingForOrderConfirm();
                vm.popupShowWait = true;
              };
            };
            sessionData.order = [];
            delete localStorage.order;
            $scope.vmMenu.badgeShow = false;
          } else {//если есть оплаченные, но неподтвержденные заказы, выводим поп-ап.
            vm.havePaidButNotClosedOrders = true;
          };
          vm.getCartList();

        });
      };
    } else {
      vm.popupAuthAsk = true;
      vm.telephoneShow = false;
    };
  };
  vm.clearOrder = function(){
    sessionData.order = [];
    delete localStorage.order;
    vm.items = {};
    $scope.vmMenu.badgeShow = false;
  };
  vm.popupShowQrPopup = function () {
    vm.popupShowQr = true;
    //проверяем подтверджен ли заказ, и держим кнопку "оплатить неактивной до подтверждения заказа"
    $scope.vmMenu.waitingForOrderConfirm();
  };
  vm.redirectToMyOrders = function() {
    if ($rootScope.vmMenu.hasOpenOrder == false) {
      $location.path("/my_orders");
    };
  };
  vm.openOrderWithTelephon = function() {
    $http({
      method:'GET',
      url: sessionData.server + 'User/Registration',
      params: {
        "name" : "",
        "password" : "",
        "phone" : vm.phone,
        "email" : ""
      }
    }).success(function(result) {
      if (result.message == "already registered") {
        vm.alreadyRegistred = true;
      } else {
        $scope.vmMenu.waitingForOrderConfirm();
        localStorage.sessionId = result.sessionId;
        sessionData.sessionId = result.sessionId;
        localStorage.agent = true;
        vm.openOrder(true);
        vm.popupAuthAsk = false;
        if (localStorage.sessionId != "undefined" && localStorage.sessionId != undefined ) {
          //тут можно сделать переадресацию
        };
      };
    });
  };
  vm.addToOrder = function(productId, quantity) {
    if (productId != undefined && quantity != undefined) {
      sessionData.order.push({ 'ProductId': productId, 'Quantity': quantity });
      var str = $(sessionData.order).serialize();
      localStorage.order = str;
      $rootScope.vmMenu.toggleCartBadge();
    };
  };
  vm.returnHours = function () {
    var d = new Date();
    var n = d.getHours() + 1;
    return n;
  }
// заказынные товары
  vm.scrollToBottom = function() {
    customFunctions.scrollToBottom();
  };
  if (localStorage.agent == "true") {// проверка на пользователя без регистрации
    vm.notRegistredUser = true;
  }
  $scope.vmMenu.waitingForOrderConfirm();//запуск функции проверки неподтвержденных заказов

  vm.orders = {};

  vm.noOpenedOrder = function() {
    if (vm.orders.length != undefined && vm.orders.length > 0) {
      return false;
    }
    return true; //если заказов нет, скрываем некоторые пункты
  }

  $rootScope.vmMenu.orderHasBeenPaid = false;

  vm.getOrdersList = function(){

    $http({
      method:'GET',
      url: sessionData.server + 'Cabinet/OrderInfo',
      params: {
        "orderId" : sessionData.orderID,
        "sessionId" : sessionData.sessionId
      }
    }).success(function(result) {
      if (result.OpenClose == 0) {//преверяем открыт или закрыт заказ. 0 — открыт
        vm.orders = {};
        vm.orders = result.Orders.reverse();
        //вписать номер заказа
        if (result.TypeOfPayment != null) { //если заказ уже оплачен, запрещаем пользователю открывать новые заказы, дозаказывать, и т.д.
          $rootScope.vmMenu.orderHasBeenPaid = true;
        }
      }
    })

  };
  vm.getOrdersList();

  vm.getAmount = function(amountObj) {

    vm.amountObj = 0;

    for ( var x in amountObj ) {
      vm.amountObj = vm.amountObj + amountObj[x];
    }

    return vm.amountObj;

  }

  vm.askPrice = function(payMethod) {
    if (!$scope.vmMenu.hasOpenOrder && !$scope.vmMenu.orderHasBeenPaid) {
      vm.paymantVariantsShow = true;

      if (payMethod == 0) {
        vm.popupCashPayment = true;
      };

      if (payMethod == 1) {
        $location.path("/card_payment");
      };

      //оплата бонусами
      if (payMethod == 2) {
        //запрос на интач бар
        $http({
          method:'GET',
          url: sessionData.server + 'Order/ReduceBonusByPurchase',
          params: {
            "userId"  : $scope.vmm.userId,
            "pointId" : $scope.vmm.globalPoint,
            "amount"  : vm.getAmount(vm.amount)
          }
        }).success(function(result) {
          if (result.search(/true/i) != -1) {

            //если операция прошла в интач баре, выполняем запрос на оплату бонусами в приложении
            $http({
              method:'GET',
              url: sessionData.server + 'Order/PaymentByBonus',
              params: {
                "orderId"   : sessionData.orderID,
                "sessionId" : sessionData.sessionId,
                "orderSum"  : vm.getAmount(vm.amount)
              }
            }).success(function(result) {
              vm.popupBonusPaymentSuccess = true;
            }).error(function() {
              alert("Ошибка!");
            });
          } else {
            vm.popupLowBonuses = true;
          }
        });
      };
    };
  };

}]);

menuApp.controller("cardCtrl", ["$scope", "$http", "$timeout", "$location", "sessionData", function($scope, $http, $timeout, $location, sessionData) {

  var vm = this;

  if (localStorage.agent == "true") {// проверка на пользователя без регистрации
    vm.notRegistredUser = true;
  }

  $http({
    method:'GET',
    url: sessionData.server + 'Order/GetOrderSum',
    params: {
      "orderId"   : sessionData.orderID,
      "sessionId" : sessionData.sessionId
    }
  }).success(function(result) {
    vm.orderAmount = result;
  });

  //выполняем в самом конце, когда оплата картой прошла успешно
  vm.paymentSuccess = function(){
    $http({
      method:'GET',
      url: sessionData.server + 'Order/PaymentByCard',
      params: {
        "orderId"   : sessionData.orderID,
        "sessionId" : sessionData.sessionId,
        "orderSum"  : vm.orderAmount
      }
    }).success(function(result) {
      vm.bonusAmount = result;
      vm.popupBonusesShow = true;
    });
    //часть для Интач бара
    $http({
      method:'GET',
      url: sessionData.server + 'Order/AddBonusByPurchase',
      params: {
        "userId"  : $scope.vmm.userId,
        "pointId" : $scope.vmm.globalPoint,
        "amount"  : vm.orderAmount
      }
    }).success(function(result) {
      //TODO обработка ошибки!!!
    });

  };
}]);

menuApp.controller("favoritesCtrl", ["$scope", "$http", "$location", "sessionData", "menuBounce", function($scope, $http, $location, sessionData, menuBounce) {
  var vm = this;
  vm.bounce = function() {
    menuBounce.badgeBounce();
  };
  vm.noSessionId = false;
  vm.noFavorites = false;
  $http({
    method:'GET',
    url: sessionData.server + 'Menu/GetFavorite',
    params: {
      "sessionId": sessionData.sessionId
    }
  }).success(function(result) {
    vm.items = result;
    if (vm.items.length == 0) {
      vm.noFavorites = true;
    };
  }).error(function(){
    vm.noSessionId = true;
  });
}]);
