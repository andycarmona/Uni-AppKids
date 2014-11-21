var EditPageController = function ($scope, $http) {
   
    $scope.models = {
        helloAngular: 'New one!'
    };
 
    $scope.words = ['bob', 'sean', 'rocky', 'john'];
    $scope.sentence = 'Hello there how are you today?';

    $scope.formData = {};
    $scope.processForm = function () {
       
        $scope.formData.words = $scope.words;
        var json_text = JSON.stringify($scope.words, null, 2);
        var urlPhrase = "http://dnndev.me/DesktopModules/DataExchange/API/Example/AddPhrase?listOfWords="+json_text;
        $http({
            url: urlPhrase,
            method: "POST",
            dataType: "json",
            headers: {
                'Content-Type': 'x-www-form-urlencoded'
            }
        }).success(function (data) {
                console.log(data);

                if (!data.success) {
                    // if not successful, bind errors to error variables
                    $scope.errorName = data.errors.name;
                    $scope.errorSuperhero = data.errors.superheroAlias;
                } else {
                    // if successful, bind success message to message
                    $scope.message = data.message;
                }
            });
    };
    // this is called when the textarea is changed
    // it splits up the textarea's text and updates $scope.words 
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

    //$scope.parseSentenceDebounced = debounce($scope.parseSentence, 1000, false);

    $scope.buildSentance = function (w) {

        var words = [];

        for (var i = 0; i < $scope.words.length; i++) {
            var word = $scope.words[i].WordName;
            if (word.replace(/\s+/g, '') !== '') {
                words.push(word);
            }
        }

        $scope.sentence = words.join(' ');

        // if the user puts a space in the input
        // call parseSentence() to update $scope.words
        if (w.WordName.indexOf(' ') > -1) {
            $scope.parseSentenceDebounced();
        }

    }

    $scope.parseSentence();

}

// The $inject property of every controller (and pretty much every other type of object in Angular) needs to be a string array equal to the controllers arguments, only as strings
EditPageController.$inject = ['$scope', '$http'];