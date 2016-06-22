restaurantApp.controller('actionController', ['$scope', '$http', 'ActionService', 'fileUpload', function ($scope, $http, ActionService, fileUpload) {
    getAction();
    function getAction() {
        ActionService.getAction()
            .success(function (actionData) {
                var newActionData = actionData.ActionList.map(function (obj) { obj.DateStart = moment(obj.DateStart).format("YYYY MMMM DD"); return obj; });
                $scope.action = newActionData;
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
            });
    }

    $scope.divAction = function ()
    {
        $scope.newDivAction = true;
        $scope.Action = "Create";
    };

    $scope.edit = function (data)
    {
        $scope.newDivAction = true;
        $scope.Action = "Edit";
        $scope.id = data.Id;
        $scope.name = data.Name;
        $scope.description = data.Description;
        $scope.date = new Date(data.DateStart);
        if (data.Image != null)
        { $scope.imageName = data.Image;}       
    };

    $scope.addUpdateAction = function ()
    {
        var name = $scope.name;
        var description = $scope.description;
        var date = $("#dateStart").val();
        var file = $scope.fileName;
        var point = 102;
        if ($scope.Action == "Create")
        {
            var id = 0;
            var uploadUrl = server + 'Action/CreateAction';
            fileUpload.uploadFileToUrl(id, name, description, date, file, point, uploadUrl)
            .success(function (data) {
                alert("Ваши изменения успешно сохранены");
                getAction();
                ClearFields();
                $scope.newDivAction = false;
            }).error(function (error) {
                alert(error.Status);
            });
        }
        else
        {
            var id = $scope.id;
            var uploadUrl = server + 'Action/EditAction';
            fileUpload.uploadFileToUrl(id, name, description, date, file, point, uploadUrl)
            .success(function (data) {
                alert("Ваши изменения успешно сохранены");
                getAction();
                ClearFields();
                $scope.newDivAction = false;
            }).error(function (error) {
                alert(error.Status);
            });
        }
    };

    $scope.delete = function (data)
    {

    };

    function ClearFields() {
        $scope.name = "";
        $scope.description = "";
        $scope.date = "";
        $scope.fileName = "";      
    }

}]);

restaurantApp.factory('ActionService', ['$http', function ($http) {
    var ActionService = {};
    ActionService.getAction = function () {
        return $http({
            method: 'post',
            url: server + 'Action/Action',
            params: { 'pointId': 102 }
        });
    };
    return ActionService;
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

restaurantApp.service('fileUpload', ['$http', function ($http) {
    this.uploadFileToUrl = function (id, name, description, date, file, point, uploadUrl) {
        var fd = new FormData();
        fd.append('file', file);
        return $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined },
            params: { id: id, name: name, description: description, date: date, point: point }
        });
    }
}]);