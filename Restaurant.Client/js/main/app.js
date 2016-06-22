var restaurantApp = angular.module("restaurantApp", ['ngSanitize', 'ngRoute', 'ngAnimate', 'menuApp']);

restaurantApp.config(['$routeProvider', function($routeProvider) {
    $routeProvider
      .when('/start', {template: '', controller: 'getParamsCtrl'})///start?userId&pointId&reg
      .when('/home', {templateUrl: 'views/home.html'})

      .when('/club_select', {templateUrl: 'views/club_select.html'})

      .when('/club_map', {templateUrl: 'views/club_map.html'})

      .when('/about', {templateUrl: 'views/about.html'})

      .when('/chat', {templateUrl: 'views/chat.html'})
      .when('/shares', {templateUrl: 'views/shares.html'})

      .when('/news/:newsId', {templateUrl: 'views/news.html'})
      .when('/news_list', {templateUrl: 'views/news_list.html'})

      .when('/booking', {templateUrl: 'views/booking.html'})

      .when('/feedback', {templateUrl: 'views/feedback.html'})

      .when('/favorites', {templateUrl: 'views/favorites.html'})

      .when('/reviews', {templateUrl: 'views/reviews.html'})

      .when('/menu_first/:catId', {templateUrl: 'views/menu_first.html'})
      .when('/menu_next/:catId', {templateUrl: 'views/menu_next.html'})
      .when('/menu_last/:prodId', {templateUrl: 'views/menu_last.html'})
      .when('/order', {templateUrl: 'views/order.html'})
      .when('/card_payment', {templateUrl: 'views/card_payment.html'})

      .when('/error', {template: '<div style="margin-top: 100px;">Нет аккаунта бонусной карты</div>'})

      .otherwise({redirectTo: '/home'});
  }]);