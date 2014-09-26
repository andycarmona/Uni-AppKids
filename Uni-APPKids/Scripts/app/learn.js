angular.module('main')
    .controller('LearnController', ['$scope', '$location', '$resource', function ($scope, $location, $resource) {

        var userName = "andy";
        var wordIds = "2";
        var dictionaryResource = $resource('/api/dictionary/:'+userName+'', { userName: "andy" }, { update: { method: 'PUT' } });
        var wordResource =       $resource('/api/words', { wordsId: "1,2,4" }, { update: { method: 'PUT' } });

        $scope.groups = [];
        $scope.words = [];
    $scope.titleDescription = "casa";
   
  dictionaryResource.query(function (data) {
      $scope.groups = [];
        angular.forEach(data, function (dictionaryData) {
            $scope.groups.push(dictionaryData);
        });
  });
    wordResource.query(function(data) {
        $scope.words = [];
        angular.forEach(data, function(wordsdata) {
            $scope.words.push(wordsdata);
        });
    });

    $scope.oneAtATime = true;

    $scope.getWordName = function (wordId) {
        var i = 0;
        for (; i < $scope.words.length; ) {
            if ($scope.words[i].wordId == wordId) {
                return $scope.words[i].wordName;

            }
            i++;
      
        }
        return null;
    }

    $scope.setWordContent = function (idPassedFromClickedWord) {
        $scope.titleDescription = $scope.getWordName(idPassedFromClickedWord);
        console.log(idPassedFromClickedWord);
       
        }
  

    $scope.status = {
        isFirstOpen: true,
        isFirstDisabled: false
    };

    

}]);