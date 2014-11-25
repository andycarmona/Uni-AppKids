var LandingPageController = function ($scope, $http, $window, userService) {
    $scope.dictionaryName = "";
    $scope.lookupResult = {
        dictionaryName: "Not yet retrieved"
    };
    $scope.wordsInPhrases = [];
    $scope.description = "No description";
    $scope.sound = "No sound";
 
    $scope.errorMessage = ['Undefined error. Could not contact server.']

    var urlDictionary = "http://dnndev.me/DesktopModules/DataExchange/API/Example/GetDictionary?userName=andy";

    var urlPhrase = "http://dnndev.me/DesktopModules/DataExchange/API/Example/GetWordsList?dictionaryId=1&indexOfPhraseList=0";

    GetDictionary(urlDictionary);
    GetPhrase(urlPhrase);


    function GetDictionary(url) {

        userService.GetRequest(url).then(
                           function (request) {
                               if (request==true) {
                                   $scope.error = $scope.errorMessage[0];
                               }
                               applyRemoteDataToDictionary(request);

                           }
                       );


    }

    function GetPhrase(url) {
        userService.GetRequest(url).then(
                         function (request) {
                             if (request==true) {
                                 $scope.error = $scope.errorMessage[0];
                             }
                             applyRemoteDataToPhrase(request);

                         }
                     );
    }

    function applyRemoteDataToDictionary(request) {

        $scope.dictionaries = request;
        window.localStorage['dictionary_name'] = request[0].DictionaryName;
    }

    function applyRemoteDataToPhrase(request) {
        $scope.wordsInPhrases = request;
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