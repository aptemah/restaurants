restaurantApp.controller('productController', ['$scope', 'ProductService', function ($scope, ProductService) {
    getProduct();
    function getProduct() {
        ProductService.getProduct()
            .success(function (productData) {
                $scope.product = productData;
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
            });
    }
}]);

restaurantApp.factory('ProductService', ['$http', function ($http) {
    var ProductService = {};
    ProductService.getProduct = function () {
        return $http({
            method: 'post',
            url: server + 'Product/Product'
        });
    };
    return ProductService;
}]);