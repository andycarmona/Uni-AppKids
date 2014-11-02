angular.module('main')
    .controller('LearnController', ['$scope', '$location', '$resource', function ($scope, $location, $resource) {

        var userName = "andy";
        $scope.dictId = 1;
        $scope.phraseId = 1;
        var dictionaryResource = $resource('/api/dictionary/:' + userName + '', { userName: "andy" }, { update: { method: 'PUT' } });
        //var wordResource = $resource('/api/words/?dictionaryId=' + dictionaryId + '', { indexOfPhraseList: "1" }, { update: { method: 'PUT' } });
        $scope.wordResource = $resource('/api/words/', { dictionaryId: $scope.dictId, indexOfPhraseList: $scope.phraseId });
    

        $scope.groups = [];
        $scope.words = [];
        $scope.titleDescription = "casa";

        dictionaryResource.query(function (data) {
            $scope.groups = [];
            angular.forEach(data, function (dictionaryData) {
                $scope.groups.push(dictionaryData);
            });
        });

        function parseErrors(response) {
            var errors = [];
            for (var key in response.ModelState) {
                for (var i = 0; i < response.ModelState[key].length; i++) {
                    errors.push(response.ModelState[key][i]);
                }
            }
            return errors;
        }

        $scope.wordResource.query(function (data) {
  
            $scope.words = [];
            angular.forEach(data, function (wordsdata) {
          
                $scope.words.push(wordsdata);
            });
        }, function (error) {
            $scope.errors = parseErrors(error);
            alert(error[0].wordName);
        });

        $scope.oneAtATime = true;
        $scope.getPhrase = function (phraseIndex) {
  
            $scope.phraseId = 0;
           $scope.wordResource.get('/api/words/', { dictionaryId: 1, indexOfPhraseList: $scope.phraseId });
        }
      

        $scope.getWordName = function (wordId) {
            var i = 0;
            for (; i < $scope.words.length;) {
                if ($scope.words[i].wordId == wordId) {
                    return $scope.words[i].wordName;

                }
                i++;

            }
            return null;
        };

        $scope.setWordContent = function (idPassedFromClickedWord) {
            $scope.titleDescription = $scope.getWordName(idPassedFromClickedWord);
            console.log(idPassedFromClickedWord);

        };

        $scope.status = {
            isFirstOpen: true,
            isFirstDisabled: false
        };



    }]);