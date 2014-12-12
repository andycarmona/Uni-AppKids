/// <reference path="~/Scripts/UniAppSpelApp.js" />
/// <reference path="~/Scripts/angular/angular-mocks.js" />
/// <reference path="~/Scripts/jasmine/jasmine.js" />
/// <reference path="~/Scripts/angular/angular.js" />
var LandingPageController = function ($scope, $http, $window, userService) {

   

    $scope.dictionaryName = "";
    $scope.lookupResult = {
        dictionaryName: "Not yet retrieved"
    };
    $scope.Phrases = [];
    $scope.wordsInPhrases = [];
    $scope.description = "No description";
    $scope.sound = "No sound";
    $scope.image = "http://dummyimage.com/100";
    $scope.actualPhraseIndex = 0;
    $scope.errorMessage = ['Undefined error. Could not contact server.'];
    $scope.navLeft = false;
    $scope.navRight = false;

    var urlDictionary = "http://dnndev.me/DesktopModules/DataExchange/API/WordHandler/GetDictionary?dictionaryId=1";

    var urlPhrase = "http://dnndev.me/DesktopModules/DataExchange/API/WordHandler/GetWordsList?dictionaryId=1&indexOfPhraseList=0&totalPages=10";

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
          

    }

    function applyRemoteDataToPhrase(request) {
  
        $scope.Phrases = request;

        $scope.wordsInPhrases = request[$scope.actualPhraseIndex].ListOfWords;
        updateNavButtonVisibility();
    }

    function updateNavButtonVisibility() {
        var phrasesSize=$scope.Phrases.length;
        if (phrasesSize == 1) {
            $scope.navLeft = false;
            $scope.navRight = false;
        }else if ($scope.actualPhraseIndex == phrasesSize - 1) {
            $scope.navLeft = true;
            $scope.navRight = false;
        } else if ($scope.actualPhraseIndex == 0) {
            $scope.navLeft = false;
            $scope.navRight = true;
        } else {
            $scope.navLeft = true;
            $scope.navRight = true;
        }

    }

    $scope.moveIndex = function(position) {
        var phrasesLimit = $scope.Phrases.length;
        var nextIndex = $scope.actualPhraseIndex + position;
        if ((nextIndex < phrasesLimit) && (nextIndex > -1)) {
            $scope.actualPhraseIndex = nextIndex;
            $scope.wordsInPhrases = $scope.Phrases[$scope.actualPhraseIndex].ListOfWords;
        }
        updateNavButtonVisibility();
    }

    $scope.getWordName = function (wordId) {
        var i = 0;
       
        for (; i < $scope.wordsInPhrases.length;) {
            if ($scope.wordsInPhrases[i].WordId == wordId) {
                $scope.description = [$scope.wordsInPhrases[i].WordName] + ": " + $scope.wordsInPhrases[i].WordDescription;
                $scope.sound = $scope.wordsInPhrases[i].SoundFile;
                $scope.image = $scope.wordsInPhrases[i].Image;
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



var init = function () {
 
};

init();
