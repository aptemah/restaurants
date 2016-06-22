var server = 'http://kate.intouchclub.ru/RestaurantServer/';
var restaurantApp = angular.module('restaurantApp', ['ngRoute']);
restaurantApp.config(['$routeProvider',
        function ($routeProvider) {
            $routeProvider.
                when('/action', {
                    templateUrl: 'app/view/action.html',
                }).
                when('/news', {
                    templateUrl: 'app/view/news.html',
                }).
                when('/promo', {
                    templateUrl: 'app/view/promo.html',
                }).
                when('/restaurant', {
                    templateUrl: 'app/view/restaurant.html',
                }).
                when('/product', {
                    templateUrl: 'app/view/product.html',
                }).
                otherwise({
                    redirectTo: 'index'
                });
        }]);
