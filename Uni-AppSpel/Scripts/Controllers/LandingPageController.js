var LandingPageController = function ($scope, $http) {
    
    //var dictionaryResource = $resource('http://dnndev.me/DesktopModules/DataExchange/API/Example/GetDictionary:' + userName + '', { userName: "andy" }, { update: { method: 'PUT' } });
    //dictionaryResource.query(function (data) {
    //    $scope.models = [];
    //    angular.forEach(data, function (dictionaryData) {
    //        $scope.models.push(dictionaryData);
    //    });
    //});
    //$scope.models = {
    //    helloAngular: 'I work!'
    //};
    $scope.lookupResult = {
        dictionaryName: "Not yet retrieved"
    };
        var url = "http://dnndev.me/DesktopModules/DataExchange/API/Example/GetDictionary?" +
           "userName=andy";

    $http.get(url).success(function (data) {
     
            $scope.posts = data;
            $scope.loading = false;
        })
         .error(function () {
             $scope.error = "An Error has occured while loading posts!";
             $scope.loading = false;
         });

}

// The $inject property of every controller (and pretty much every other type of object in Angular) needs to be a string array equal to the controllers arguments, only as strings
LandingPageController.$inject = ['$scope','$http'];