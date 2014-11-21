var LandingPageController = function ($scope, $http) {

    $scope.lookupResult = {
        dictionaryName: "Not yet retrieved"
    };
    $scope.wordsInPhrases = [];
    $scope.description = "No description";
    $scope.sound = "No sound";

    var urlDictionary = "http://dnndev.me/DesktopModules/DataExchange/API/Example/GetDictionary?userName=andy";

    var urlPhrase = "http://dnndev.me/DesktopModules/DataExchange/API/Example/GetWordsList?dictionaryId=1&indexOfPhraseList=0";

    GetDictionary(urlDictionary);
    GetPhrase(urlPhrase);

    function GetDictionary(url) {
        $http.get(url).success(function (data) {

            $scope.dictionaries = data;
            $scope.loading = false;

        }).error(function () {
                $scope.error = "An Error has occured while loading dictionaries!";
                $scope.loading = false;
            });
    }

    function GetPhrase(url) {
        $http.get(url).success(function (data) {

            $scope.wordsInPhrases = data;
            $scope.loading = false;

        }).error(function () {
        $scope.error = "An Error has occured while loading a phrase!";
        $scope.loading = false;
    });
    }

    $http.get(urlDictionary).success(function (data) {

        $scope.posts = data;
        $scope.loading = false;

    }).error(function () {
         $scope.error = "An Error has occured while loading posts!";
         $scope.loading = false;
     });

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


LandingPageController.$inject = ['$scope', '$http'];