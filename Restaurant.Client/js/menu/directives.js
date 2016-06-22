menuApp.directive('ngFavorites', ["$timeout", "$http", "menuData", "sessionData", "$location", function($timeout, $http, menuData, sessionData, $location) {
  return {
    restrict: "A",
    scope: {},
    link: function($scope, $element, $attrs) {
    if (!localStorage.getItem('sessionId')) {//если у пользователя нет sessionId, он не может добавлять в избранное
      $($element).hide();
    };
  //описываем функции удаления или добавления в избранное. Далее проверяем страницу для того что нам нужно, добавление или удаление.
  var removeFromFavorites = function() {
    $($element).on("click", function(e){
      e.stopPropagation();
      $http({
        method:'GET',
        url: sessionData.server + 'Menu/RemoveProdFromFavorite',
        params: {
          "sessionId": sessionData.sessionId,
          "prodId": $attrs.ngFavorites
        }
      }).success(function(result) {
        if (result == true) {
          $($element).fadeOut(10, function(){
            $($element).toggleClass("out-of-favorites");
          });
          $($element).fadeIn();
        };
      });
    });
  };
  var addToFavorites = function() {
    $($element).on("click", function(e){
      e.stopPropagation();
      $http({
        method:'GET',
        url: sessionData.server + 'Menu/AddProdToFavorite',
        params: {
          "prodId": $attrs.ngFavorites,
          "sessionId": sessionData.sessionId
        }
      }).success(function(result) {
        $($element).fadeOut(10, function(){
          $($element).toggleClass("out-of-favorites");
        });
        $($element).fadeIn();
      });
    });
  };
  //проверка на то какую функцию вызывать, удаление или добавление в избранное
    var checkVariableForTrackPage;
    var str = location.hash;
    var pagesWhereWeNeedDeleteMethod = 
    [
    "/favorites"
    ];
      for (var i = pagesWhereWeNeedDeleteMethod.length - 1; i >= 0; i--) {
        checkVariableForTrackPage = str.search(pagesWhereWeNeedDeleteMethod[i]);
        if (checkVariableForTrackPage !== -1) {
          removeFromFavorites();
        } else {
          $timeout(addToFavorites, 0);
        };
      }
    }
  }
}]);

menuApp.directive('ngDeleteButton', [ function() {
  return {
    restrict: "A",
    scope: {},
    link: function($scope, $element, $attrs) {
      $($element).on("click", function(){
        var quantity = parseInt($($element).prev(".quantity").find(".cart-count-number").val());
        var minus = $($element).prev(".quantity").find(".minus");
        for (var i = quantity - 1; i >= 0; i--) {
          minus.trigger("click");
        }
      });
    }
  }
}]);

menuApp.directive('ngActiveElement', [ function() {
  return {
    restrict: "A",
    scope: {},
    link: function($scope, $element, $attrs) {
      $($element).on("mousedown", function(){
        $(this).addClass("tap");
      });
      $($element).on("mouseup", function(){
        var that = this;
        setTimeout(function() {$(that).removeClass("tap")}, 500);
      });
    }
  }
}]);