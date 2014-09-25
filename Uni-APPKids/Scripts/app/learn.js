angular.module('main')
    .controller('LearnController', ['$scope', '$location', '$resource', function ($scope, $location, $resource) {

        var userName = "andy";
        var wordIds = "2";
        var dictionaryResource = $resource('/api/dictionary/:'+userName+'', { userName: "andy" }, { update: { method: 'PUT' } });
        var wordResource =       $resource('/api/words/', { wordsIds: "2" }, { update: { method: 'PUT' } });

        $scope.groups = [];
    $scope.wordsId = [];
   
  dictionaryResource.query(function (data) {
      $scope.groups = [];
        angular.forEach(data, function (dictionaryData) {
            $scope.groups.push(dictionaryData);
        });
  });
    wordResource.query(function(data) {
        $scope.wordsId = [];
        angular.forEach(data, function(wordsdata) {
            $scope.wordsId.push(wordsdata);
        });
    });

    $scope.oneAtATime = true;

  

    $scope.status = {
        isFirstOpen: true,
        isFirstDisabled: false
    };

    

}]);