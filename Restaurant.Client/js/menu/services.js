menuApp.value('menuData', {
  "arrayOfFavoritId" : []
});
restaurantApp.service('menuBounce', function(){
  var parentObj = {
    badgeBounce : function(){
      $(".cart .badge").toggleClass("bounce");
      setTimeout(function(){$(".cart .badge").toggleClass("bounce")},500)
    }
  }
  return parentObj;
});

menuApp.service("service.orders", ["$rootScope", "sessionData", function($rootScope, sessionData){

  var parentObj =
  {
    "addToOrders": function($event, state, productId, index) {

      var repeats = false;//флаг повторов на "false"

      for (var i = sessionData.order.length - 1; i >= 0; i--) {
        for (key in sessionData.order[i]) {
          if (sessionData.order[i].ProductId == productId) {
            sessionData.order[i].Quantity++
            //sessionData.order[i] = { 'ProductId': productId, 'Quantity': quantity }
            var str = JSON.stringify(sessionData.order);
            localStorage.order = str;
            $rootScope.vmMenu.toggleCartBadge();
            var repeats = true;
            break
          }
          ;
        }
      };

      if (!repeats) {
        sessionData.order.push({'ProductId': productId, 'Quantity': 1});
        var str = JSON.stringify(sessionData.order);
        localStorage.order = str;
        $rootScope.vmMenu.toggleCartBadge();
      };

      $rootScope.vmMenu.toggleCartBadge();
    }
  }
  return parentObj;
}]);