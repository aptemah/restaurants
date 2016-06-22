restaurantApp.controller('newsController', ['$scope', 'NewsService', 'fileUploadNews', '$http', function ($scope, NewsService, fileUploadNews, $http) {
    getNews();
    function getNews() {
        NewsService.getNews()
            .success(function (newsData) {
                var newNewsData = newsData.NewsList.map(function (obj) { obj.DateCreate = moment(obj.DateCreate).format("YYYY MMMM DD"); return obj; });
                $scope.news = newNewsData;
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
            });
    }

    $scope.divNews = function () {
        $scope.newDivNews = true;
        $scope.Action = "Create";
    };

    $scope.edit = function (data) {
        $scope.newDivNews = true;
        $scope.Action = "Edit";
        $scope.id = data.Id;
        $scope.name = data.Name;
        $scope.description = data.Description;
        $scope.smallDescription = data.SmallDescription;
        if (data.Image != null)
        { $scope.imageName = data.Image; }
    };

    $scope.addUpdateNews = function () {
        var name = $scope.name;
        var description = $scope.description;
        var smallDescription = $scope.smallDescription;
        var file = $scope.fileName;
        var point = 102;
        if ($scope.Action == "Create") {
            var id = 0;
            var uploadUrl = server + 'News/CreateNews';
            fileUploadNews.uploadFileToUrl(id, name, smallDescription, description, file, point, uploadUrl)
            .success(function () {
                alert("Ваши изменения успешно сохранены");
                getNews();
                ClearFields();
                $scope.newDivNews = false;
            }).error(function (error) {
                alert(error.Status);
            });
        }
        else {
            var id = $scope.id;
            var uploadUrl = server + 'News/EditNews';
            fileUploadNews.uploadFileToUrl(id, name, smallDescription, description, file, point, uploadUrl)
            .success(function (data) {
                alert("Ваши изменения успешно сохранены");
                getNews();
                ClearFields();
                $scope.newDivNews = false;
            }).error(function (error) {
                alert(error.Status);
            });
        }
    };

    function ClearFields() {
        $scope.name = "";
        $scope.description = "";
        $scope.smallDescription = "";
        $scope.fileName = "";
    }

}]);

restaurantApp.factory('NewsService', ['$http', function ($http) {
    var NewsService = {};
    NewsService.getNews = function () {
        return $http({
            method: 'post',
            url: server + 'News/News',
            params: { 'pointId': 102 }
        });
    };
    return NewsService;
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

restaurantApp.service('fileUploadNews', ['$http', function ($http) {
    this.uploadFileToUrl = function (id, name, smallDescription, description, file, point, uploadUrl) {
        var fd = new FormData();
        fd.append('file', file);
        return $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined },
            params: { id: id, name: name, smallDescription: smallDescription, description: description, point: point }
        });
    }
}]);