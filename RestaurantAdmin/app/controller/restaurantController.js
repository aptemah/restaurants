restaurantApp.controller('restaurantController', ['$scope', '$http', 'RestaurantService', 'fileUploadRestaurant', function ($scope, $http, RestaurantService, fileUploadRestaurant) {
    getRestaurant();
    function getRestaurant() {
        RestaurantService.getRestaurant()
            .success(function (restaurantData) {
                $scope.restaurant = restaurantData;
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
            });
    }
    $scope.save = function (data)
    {
        var name = data.Name;
        var phone = data.Phone;
        var kitchen = data.Kitchen;
        var address = data.Address;
        var mail = data.Email;
        var description = data.Description;
        var price = data.Price;
        var time = data.Time;
        var skype = data.Skype;
        var feedback = data.EmailFeedback;
        var reservation = data.EmailReservation;
        var file = data.Logo;
        var id = 102;
        var uploadUrl = server + 'Restaurant/EditRestaurant';
        fileUploadRestaurant.uploadFileToUrl(id, name, phone, kitchen, address, mail, description, price, time, skype, feedback, reservation, file, uploadUrl)
            .success(function () {
                alert("Ваши изменения успешно сохранены");
                getRestaurant();
            }).error(function (error) {
                alert(error.Status);
            });
    }
}]);

restaurantApp.factory('RestaurantService', ['$http', function ($http) {
    var RestaurantService = {};
    RestaurantService.getRestaurant = function () {
        return $http({
            method: 'post',
            url: server + 'Restaurant/Restaurant',
            params: { 'pointId': 102 }
        });
    };
    return RestaurantService;
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

restaurantApp.service('fileUploadRestaurant', ['$http', function ($http) {
    this.uploadFileToUrl = function (id, name, phone, kitchen, address, mail, description, price, time, skype, feedback, reservation, file, uploadUrl) {
        var fd = new FormData();
        var data = { 'id': id, 'name': name, 'phone': phone, 'kitchen': kitchen, 'address': address, 'mail': mail, 'description': description, 'price': price, 'time': time, 'skype': skype, 'feedback': feedback, 'reservation': reservation };
        fd.append('file', file);
        return $http.post(uploadUrl, data, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        });
    }
}]);