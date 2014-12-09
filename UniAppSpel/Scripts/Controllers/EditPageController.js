var EditPageController = function ($scope, $http, $window,$filter, userService) {

    var urlPhrase = "http://dnndev.me/DesktopModules/DataExchange/API/WordHandler/AddPhrase?dictionaryId=1&listOfWords=";
    var urlWordList = "http://dnndev.me/DesktopModules/DataExchange/API/WordHandler/GetAllWordsInDictionary";
    var urlDictionary = "http://dnndev.me/DesktopModules/DataExchange/API/WordHandler/GetDictionary?dictionaryId=1";
    var urlImageList = "http://dnndev.me/DesktopModules/DataExchange/API/RemoteService/GetListOfImageUrl?wordToSearch=";

    $scope.dictionaryName = window.localStorage['dictionary_name'];
    $scope.imagelist = [];
    $scope.words = [];

    $scope.sentence = 'Hola, Cómo estás?';

    GetWordList(urlWordList);
    GetDictionary();
    GetImageList(urlImageList, "manzana");

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
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
        });
      
    }

    function GetImageList(url,keyWord) {
        userService.GetRequest(url+keyWord).success(function (request) {
            applyRemoteDataToImageList(request);
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
        });
    }

    function applyRemoteDataToImageList(request) {
   
        $scope.imagelist.push(request[0]);
    }

    function applyRemoteDataToDictionary(request) {

        $scope.dictionaries = request.DictionaryName;
      

    }
    function applyRemoteDataToWordList(request) {

        $scope.words_in_dictionary = request;
        
    }

    $scope.processForm = function () {
        var wordsJsonFormat = JSON.stringify($scope.words, null, '');
        //alert(wordsJsonFormat);
        $scope.words = [];
        urlPhrase = urlPhrase + wordsJsonFormat;
     
        userService.PostRequest(urlPhrase).success(function (request) {
            wordsJsonFormat = "";
            return(request);
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
        });
  
    };

    $scope.parseSentence = function () {

        var words = $scope.sentence.split(/\s+/g);
        var wordObjects = [];

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

EditPageController.$inject = ['$scope', '$http', '$window','$filter', 'userService'];