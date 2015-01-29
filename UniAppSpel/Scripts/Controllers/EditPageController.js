"use strict";

var EditPageController = function ($scope, $http, $window, userService) {

    $scope.imagelist = [];
    $scope.words = [];
    $scope.sentence = "";
    $scope.imageurl = "No image";
    $scope.dictionaryId = 1;
    $scope.totalPages = 10;
    $scope.generalErrorDescription = "An internal error has ocurred.";

    ServiceInit();


    function ServiceInit() {
        userService.GetRequest("/Home/GetServiceUrl").success(function (request) {
            $scope.urlExternalService = request[0];
            $scope.urlWordService = request[1];
            if ($scope.urlWordService !== undefined) {
                console.log($scope.urlWordService);
                GetWordList($scope.urlWordService + "GetAllWordsInDictionary");
                GetPhraseList($scope.urlWordService + "GetAllPhrasesInDictionary?", $scope.totalPages);
                GetDictionary($scope.urlWordService + "GetDictionary?dictionaryId=" + $scope.dictionaryId);
            }
        }).error(function (request) {
            $scope.error = $scope.generalErrorDescription + request;
        });

    }

    function GetDictionary(urlService) {
        userService.GetRequest(urlService).success(function (request) {
            applyRemoteDataToDictionary(request);
        }).error(function (request) {
            $scope.error = $scope.generalErrorDescription + request;
        });
    }

    function GetWordList(url) {
        userService.GetRequest(url).success(function (request) {
            applyRemoteDataToWordList(request);
            $scope.wordsInList = true;
        }).error(function (request) {
            $scope.wordsInList = false;
            $scope.error = $scope.generalErrorDescription + request;
        });

    }

    function GetPhraseList(url) {
    
        url = url + "dictionaryId=" + $scope.dictionaryId
                  + "&totalPages=" + $scope.totalPages;
        userService.GetRequest(url).success(function (request) {
            $scope.phraseToDelete = true;
            applyRemoteDataToPhraseList(request);
        }).error(function (request) {
            $scope.phraseToDelete = false;
            $scope.error = $scope.generalErrorDescription + request;
        });

    }

    $scope.getErrorMessagStatus = function () {
        var status = false;
        if ($scope.error != null) {
            if ($scope.error.length > 0) {
                status = true;
            }
        }
        return status;
    }
    $scope.getWordsStatus = function () {
        var status = false;
        if ($scope.words != null) {
            if ($scope.words.length > 0) {
                status = true;
            }
        }
        return status;
    }

    $scope.GetImageList = function (keyWord) {
        var url = $scope.urlExternalService + "GetListOfImageUrl?wordToSearch=";
        userService.GetRequest(url + keyWord).success(function (request) {
            applyRemoteDataToImageList(request);
        }).error(function (request) {
            $scope.error = $scope.generalErrorDescription + request;
        });
    }

    function applyRemoteDataToImageList(request) {
        if (request !== null && request !==undefined) {
            for (var index = 0; index < request.length; index++) {
                $scope.imagelist.push(request[index]);
            }
        }
    }

    function applyRemoteDataToDictionary(request) {

        $scope.dictionaries = request.DictionaryName;
    }

    function applyRemoteDataToWordList(request) {

        $scope.words_in_dictionary = request;
    }

    function applyRemoteDataToPhraseList(request) {

        $scope.phrases_in_dictionary = request;

    }

    $scope.SetSoundFile = function (keyWord) {
        var iterator = 0;
        angular.forEach($scope.words, function (word) {
            if (word["WordName"] === keyWord) {
                $scope.words[iterator].SoundFile = keyWord + "RecordSound.wav";
            }
            iterator++;
        });
        var input = $('#Sound' + keyWord);
        input.val(keyWord + "RecordSound.wav");
        input.trigger('input');
    }

    $scope.chooseImage = function (imageUrl, keyWord) {

        var iterator = 0;
        angular.forEach($scope.words, function (word) {
            if (word["WordName"] === keyWord) {
                $scope.words[iterator].Image = imageUrl;
            }
            iterator++;
        });

        var input = $('#' + keyWord);
        input.val(imageUrl);
        input.trigger('input');
    }

    $scope.ShowDebugInfo = function () {

        for (var i = 0; i < $scope.words.length; i++) {
            console.log("Actual Image url: " + $scope.words[i].Image + " Name in word array " + $scope.words[i].WordName + " and description" + $scope.words[i].WordDescription);
        }
    }

    $scope.makeUrl = function (phraseId) {
        return $scope.urlWordService + "DeletePhrase?phraseId=" + phraseId;
    }

    $scope.clearContent = function () {
        $scope.sentence = "";
        $scope.words = [];
    }

    $scope.deletePhrase = function (phraseId) {
        userService.GetRequest($scope.urlWordService + "DeletePhrase?phraseId=" + phraseId).success(function () {
            GetPhraseList($scope.urlWordService + "GetAllPhrasesInDictionary?", $scope.totalPages);
        }).error(function (request) {
            $scope.error = $scope.generalErrorDescription + request;
        });
    }

    $scope.choosePhrase = function (sentence) {
        $scope.sentence = sentence;
        $scope.parseSentence();

    }
   
 
    $scope.processForm = function () {

        var wordsJsonFormat = JSON.stringify($scope.words, null, '');
        userService.PostRequest($scope.urlWordService + "AddPhrase?dictionaryId=" + $scope.dictionaryId + "&listOfWords=" + wordsJsonFormat).success(function (request) {
            for (var i = 0; i < request.length; i++) {

                console.log("la palabra " + request[i].WordName + " es " + request[i].Repeated);
            }
            GetPhraseList($scope.urlWordService + "GetAllPhrasesInDictionary?", $scope.totalPages);
            $scope.iconSuccess = true;
        }).error(function (request) {
            $scope.error = $scope.generalErrorDescription + request;
        });

        $scope.iconSuccess = false;

    };

    $scope.parseSentence = function () {

        var wordObjects = [];
        var words = $scope.sentence.split(/\s+/g);
        for (var i = 0; i < words.length; i++) {
            wordObjects.push({ WordName: words[i] });
        }

        if ((words.length == 1) && (words[0] === '')) {
            $scope.words = [];
        } else {
            $scope.words = wordObjects;

        }

    };

    $scope.PlaySound = function (keyword) {

        if ($scope.soundObject != null) {
            document.body.removeChild($scope.soundObject);
            $scope.soundObject.removed = true;
            $scope.soundObject = null;
        }
        $scope.soundObject = document.createElement("embed");
        $scope.soundObject.setAttribute("src", "/Uploads/" + keyword + "RecordSound.wav");
        $scope.soundObject.setAttribute("hidden", true);
        $scope.soundObject.setAttribute("autostart", true);
        document.body.appendChild($scope.soundObject);
    }

    $scope.addWordFromDictionary = function (aWord) {

        $scope.words.push({ WordName: aWord });
        $scope.buildSentance();
        $scope.GetImageList(aWord);
    }

    $scope.buildSentance = function () {

        var words = [];

        for (var i = 0; i < $scope.words.length; i++) {
            var word = $scope.words[i].WordName;
            if (word.replace(/\s+/g, '') !== '') {
                words.push(word);

            }
        }

        $scope.sentence = words.join(' ');

        //if (w.WordName.indexOf(' ') > -1) {
        //    $scope.parseSentenceDebounced();
        //}

    }

    $scope.parseSentence();

}

EditPageController.$inject = ['$scope', '$http', '$window', 'userService'];


