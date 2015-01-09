"use strict";

var EditPageController = function ($scope, $http, $window, userService) {

   
    var urlPhraseList = "/DesktopModules/DataExchange/API/WordHandler/GetAllPhrasesInDictionary?";
    var urlWordList = "/DesktopModules/DataExchange/API/WordHandler/GetAllWordsInDictionary";
    var urlDictionary = "/DesktopModules/DataExchange/API/WordHandler/GetDictionary?dictionaryId=1";
    var urlDeletePhrase = "/DesktopModules/DataExchange/API/WordHandler/DeletePhrase?";


  
    $scope.imagelist = [];
    $scope.words = [];
    $scope.sentence = "";
    $scope.imageurl = "No image";
    GetWordList(urlWordList);
    GetPhraseList(urlPhraseList,10);
    GetDictionary();


    function GetDictionary() {
        userService.GetRequest(urlDictionary).success(function (request) {
            applyRemoteDataToDictionary(request);
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
        });

    }

    function GetWordList(url) {
        userService.GetRequest(url).success(function (request) {
            applyRemoteDataToWordList(request);
            $scope.wordsInList = true;
        }).error(function (request) {
            $scope.wordsInList = false;
            $scope.error = "An internal error has ocurred. " + request;
        });

    }

    function GetPhraseList(url) {
        var dictionaryId = 1;
        var totalPages = 10;
        url = url + "dictionaryId="+dictionaryId + "&totalPages=" + totalPages;
        userService.GetRequest(url).success(function (request) {
            $scope.phraseToDelete = true;
            applyRemoteDataToPhraseList(request);
        }).error(function (request) {
            $scope.phraseToDelete = false;
            $scope.error = "An internal error has ocurred. " + request;
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
        var url = "/DesktopModules/DataExchange/API/RemoteService/GetListOfImageUrl?wordToSearch=";
        userService.GetRequest(url + keyWord).success(function (request) {
            applyRemoteDataToImageList(request);
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
        });
    }

    function applyRemoteDataToImageList(request) {
        if (request !== null) {
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
    $scope.ShowDebugInfo = function() {
    
        for (var i = 0; i < $scope.words.length; i++) {
            console.log("Actual Image url: " + $scope.words[i].Image + " Name in word array " + $scope.words[i].WordName + " and description" + $scope.words[i].WordDescription);
        }
    }

    $scope.makeUrl = function (phraseId) {
        return urlDeletePhrase + "phraseId=" + phraseId;
    }

    $scope.clearContent = function() {
        $scope.sentence = "";
        $scope.words = [];
    }

    $scope.deletePhrase = function (phraseId) {
        userService.GetRequest(urlDeletePhrase + "phraseId=" + phraseId).success(function () {
            GetPhraseList(urlPhraseList,10);
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
        });
    }

    $scope.choosePhrase = function(sentence)
    {
        $scope.sentence = sentence;
        $scope.parseSentence();
     
    }

    $scope.processForm = function () {
       
       var wordsJsonFormat = JSON.stringify($scope.words, null, '');
       var urlPhrase = "/DesktopModules/DataExchange/API/WordHandler/AddPhrase?dictionaryId=1&listOfWords=";

        userService.PostRequest(urlPhrase + wordsJsonFormat).success(function (request) {
            for (var i = 0; i < request.length; i++) {

                console.log("la palabra " + request[i].WordName +" es "+ request[i].Repeated);
            }
            GetPhraseList(urlPhraseList, 10);
            $scope.iconSuccess = true;
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
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


