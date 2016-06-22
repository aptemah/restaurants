restaurantApp.controller('promoController', ['$scope', '$http', 'fileUploadPromo', 'PromoService', function ($scope, $http, fileUploadPromo, PromoService) {
    getPromo();
    function getPromo() {
        PromoService.getPromo()
            .success(function (promoData) {
                $scope.promo = promoData;
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
            });
    }

    $scope.divPromo = function () {
        $scope.newDivPromo = true;
        $scope.Action = "Create";
    };

    $scope.addUpdatePromo = function () {
        var name = $scope.name;
        var description = $scope.description;
        var price = $scope.price;
        var link = $scope.link;
        var vk = $scope.vk;
        var fb = $scope.fb;
        var tw = $scope.tw;
        var file = $scope.fileName;
        var point = 102;
        if ($scope.Action == "Create") {
            var id = 0;
            var uploadUrl = server + 'Promo/CreatePromo';
            fileUploadPromo.uploadFileToUrl(id, name, description, price, link, vk, fb, tw, file, point, uploadUrl)
            .success(function () {
                alert("Ваши изменения успешно сохранены");
                getPromo();
                ClearFields();
                $scope.newDivPromo = false;
            }).error(function (error) {
                alert(error.Status);
            });
        }
        else {
            var id = $scope.id;
            var uploadUrl = server + 'Promo/EditPromo';
            fileUploadPromo.uploadFileToUrl(id, name, description, price, link, vk, fb, tw, file, point, uploadUrl)
            .success(function (data) {
                alert("Ваши изменения успешно сохранены");
                getPromo();
                ClearFields();
                $scope.newDivPromo = false;
            }).error(function (error) {
                alert(error.Status);
            });
        }
    };

    function ClearFields() {
        $scope.name = "";
        $scope.description = "";
        $scope.link = "";
        $scope.price = "";
        $scope.vk = "";
        $scope.fb = "";
        $scope.tw = "";
    }

}]);

restaurantApp.factory('PromoService', ['$http', function ($http) {
    var PromoService = {};
    PromoService.getPromo = function () {
        return $http({
            method: 'post',
            url: server + 'Promo/PromoList',
            params: { 'pointId': 102 }
        });
    };
    return PromoService;
}]);

restaurantApp.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

restaurantApp.service('fileUploadPromo', ['$http', function ($http) {
    this.uploadFileToUrl = function (id, name, description, price, link, vk, fb, tw, file, point, uploadUrl) {
        //var data = { 'id': id, 'name': name, 'description': description, 'price': price, 'link': link, 'vk': vk, 'fb': fb, 'tw': tw, 'point': point };
        var fd = new FormData();
        fd.append('file', file);
        return $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined },
            params: { 'id': id, 'name': name, 'description': description, 'price': price, 'link': link, 'vk': vk, 'fb': fb, 'tw': tw, 'point': point }
        });
    }
}]);