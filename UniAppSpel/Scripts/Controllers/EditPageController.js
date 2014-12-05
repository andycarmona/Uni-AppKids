var EditPageController = function ($scope, $http, $window,$filter, userService) {

    var urlPhrase = "http://dnndev.me/DesktopModules/DataExchange/API/Example/AddPhrase?dictionaryId=1&listOfWords=";
    var urlWordList = "http://dnndev.me/DesktopModules/DataExchange/API/Example/GetAllWordsInDictionary";
    var urlDictionary = "http://dnndev.me/DesktopModules/DataExchange/API/Example/GetDictionary?dictionaryId=1";

    $scope.dictionaryName = window.localStorage['dictionary_name'];

    $scope.words = [];

    $scope.sentence = 'Hello there how are you today?';

    GetWordList(urlWordList);
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
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
        });
      
    }

    function applyRemoteDataToDictionary(request) {

        $scope.dictionaries = request.DictionaryName;
      

    }
    function applyRemoteDataToWordList(request) {

        $scope.words_in_dictionary = request;
        
    }

    $scope.processForm = function () {
        var wordsJsonFormat = "";

        wordsJsonFormat = JSON.stringify($scope.words, null, 2);
        
      
        urlPhrase = urlPhrase + wordsJsonFormat;
       
        userService.PostRequest(urlPhrase).success(function (request) {
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