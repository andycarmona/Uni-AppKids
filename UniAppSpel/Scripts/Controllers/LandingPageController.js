"use strict";
var LandingPageController = function ($scope, $http, $window, $sce, userService) {



    $scope.dictionaryName = "";
    $scope.actualWord="";
    $scope.Phrases = [];
    $scope.wordsInPhrases = [];
    $scope.actualPhraseIndex = 0;
    $scope.errorMessage = ['Undefined error. Could not contact server.'];
    $scope.navLeft = false;
    $scope.navRight = false;
    $scope.soundObject = null;
    $scope.WikiContent = "";
    $scope.image = "";

    var urlDictionary = "http://dnndev.me/DesktopModules/DataExchange/API/WordHandler/GetDictionary?dictionaryId=1";

    var urlPhrase = "http://dnndev.me/DesktopModules/DataExchange/API/WordHandler/GetWordsList?dictionaryId=1&indexOfPhraseList=0&totalPages=10";

    var urlCheckFileExist = "http://dnndev.me/DesktopModules/DataExchange/API/RemoteService/CheckIfFileExists?path=";

    var urlWikiContent = "http://dnndev.me/DesktopModules/DataExchange/API/RemoteService/GetWordDescriptionFromWiki?keyWord=";

    var urlRaeContent = "http://dnndev.me/DesktopModules/DataExchange/API/RemoteService/GetWordDescriptionFromRae?keyWord=";

    GetDictionary();
    GetPhrase();

    $scope.getErrorMessagStatus = function () {
        var status = false;
        if ($scope.error != null) {
            if ($scope.error.length > 0) {
                status = true;
            }
        }
        return status;
    }


    function GetDictionary() {
        userService.GetRequest(urlDictionary).success(function (request) {
            applyRemoteDataToDictionary(request);
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
        });
    }

    function GetPhrase() {
        userService.GetRequest(urlPhrase).success(function (request) {
            applyRemoteDataToPhrase(request);
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
        });

    }

    function applyRemoteDataToDictionary(request) {
        $scope.dictionaries = request.DictionaryName;
    }

    $scope.RenderWikiContent = function () {
        userService.GetRequest(urlWikiContent + $scope.actualWord).success(function (request) {
            applyRemoteWikiContent(request);
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
        });
    }

    $scope.RenderRaeContent = function () {
        userService.GetRequest(urlRaeContent + $scope.actualWord).success(function (request) {
            applyRemoteRaeContent(request);
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
            applyRemoteRaeContent(request);
        });
        
    }

    function applyRemoteRaeContent(request) {
        $scope.RaeWordResult = $sce.trustAsResourceUrl(request);
    }
    function applyRemoteWikiContent(request) {
       
        $scope.WikiContent =  $sce.trustAsHtml(request);
    }
 

    function applyRemoteDataToPhrase(request) {
       
        $scope.Phrases = request;
        $scope.wordsInPhrases = request[$scope.actualPhraseIndex].ListOfWords;
        updateNavButtonVisibility();
    }

    function updateNavButtonVisibility() {
        var phrasesSize = $scope.Phrases.length;
        if (phrasesSize == 1) {
            $scope.navLeft = false;
            $scope.navRight = false;
        } else if ($scope.actualPhraseIndex == phrasesSize - 1) {
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

    $scope.CheckFileExist = function (keyWord) {
        //alert(keyWord);
        userService.GetRequest(urlCheckFileExist+"/Uploads/"+keyWord+"RecordSound.wav")
            .success(function (request) {
            return request;
            })
            .error(function (request) {
            return request;
        });
           
  
    }

    $scope.PlaySound = function (keyword) {

        if ($scope.soundObject != null) {
            document.body.removeChild($scope.soundObject);
            $scope.soundObject.removed = true;
            $scope.soundObject = null;
        }
        $scope.soundObject = document.createElement("embed");
        $scope.soundObject.setAttribute("src", "/Uploads/"+keyword+"RecordSound.wav");
        $scope.soundObject.setAttribute("hidden", true);
        $scope.soundObject.setAttribute("autostart", true);
        document.body.appendChild($scope.soundObject);
    }

    $scope.moveIndex = function (position) {
        var phrasesLimit = $scope.Phrases.length;
        var nextIndex = $scope.actualPhraseIndex + position;
        if ((nextIndex < phrasesLimit) && (nextIndex > -1)) {
            $scope.actualPhraseIndex = nextIndex;
            $scope.wordsInPhrases = $scope.Phrases[$scope.actualPhraseIndex].ListOfWords;
            $scope.actualWord = "";
        }
        updateNavButtonVisibility();
    }

    $scope.getWordsStatus = function () {
        var status = false;
        if ($scope.actualWord != null) {
            if ($scope.actualWord !=="") {
                status = true;
            }
        }
        return status;
    }

    $scope.getWordName = function (wordId, event) {
        
        var i = 0;
        if (event != null) {
            $scope.bubblePosition = event.x - 40;
        }
        for (; i < $scope.wordsInPhrases.length;) {
            if ($scope.wordsInPhrases[i].WordId == wordId) {
                $scope.sound = $scope.wordsInPhrases[i].SoundFile;
                $scope.image = $scope.wordsInPhrases[i].Image;
                $scope.actualWord = $scope.wordsInPhrases[i].WordName;
                $scope.RenderWikiContent();
                $scope.RenderRaeContent();
            }
            i++;

        }
        return null;
    };

        $scope.setWordContent = function (idPassedFromClickedWord) {
        $scope.titleDescription = $scope.getWordName(idPassedFromClickedWord);
    };

}


LandingPageController.$inject = ['$scope', '$http', '$window', '$sce', 'userService'];



var init = function () {

};

init();
