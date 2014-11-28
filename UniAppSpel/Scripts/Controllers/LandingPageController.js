var LandingPageController = function ($scope, $http, $window, userService) {
    $scope.dictionaryName = "";
    $scope.lookupResult = {
        dictionaryName: "Not yet retrieved"
    };
    $scope.wordsInPhrases = [];
    $scope.description = "No description";
    $scope.sound = "No sound";

    $scope.errorMessage = ['Undefined error. Could not contact server.'];

    var urlDictionary = "http://dnndev.me/DesktopModules/DataExchange/API/Example/GetDictionary?dictionaryId=1";

    var urlPhrase = "http://dnndev.me/DesktopModules/DataExchange/API/Example/GetWordsList?dictionaryId=1&indexOfPhraseList=0";

    GetDictionary();
    GetPhrase();


    function GetDictionary() {

    
        userService.GetRequest(urlDictionary).success(function (request) {
            applyRemoteDataToDictionary(request);
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. "+request;
        });

    }

    function GetPhrase() {
       
        userService.GetRequest(urlPhrase).success(function(request) {
            applyRemoteDataToPhrase(request);
        }).error(function() {
            $scope.error = "An internal error has ocurred. "+request;
        });

    }

    function applyRemoteDataToDictionary(request) {

            $scope.dictionaries = request.DictionaryName;
            window.localStorage['dictionary_name'] = request.DictionaryName;

    }

    function applyRemoteDataToPhrase(request) {
        if (request !== Array) {
            $scope.wordsInPhrases = request;
        } else { $scope.error = request; }

    }

    $scope.getWordName = function (wordId) {
        var i = 0;

        for (; i < $scope.wordsInPhrases.length;) {
            if ($scope.wordsInPhrases[i].WordId == wordId) {
                $scope.description = [$scope.wordsInPhrases[i].WordName] + ": " + $scope.wordsInPhrases[i].WordDescription;
                $scope.sound = $scope.wordsInPhrases[i].SoundFile;
            }
            i++;

        }
        return null;
    };

    $scope.setWordContent = function (idPassedFromClickedWord) {
        $scope.titleDescription = $scope.getWordName(idPassedFromClickedWord);
        console.log(idPassedFromClickedWord);

    };

}


LandingPageController.$inject = ['$scope', '$http', '$window', 'userService'];