var myAppModule = angular.module('SafetyDiscussionApp', ['ui.bootstrap']);

myAppModule.directive('toggle', function () {
    return function(scope, elem, attrs) {
        scope.$on('event:open', function () {
            elem.addClass('open');
        });

        scope.$on('event:close', function () {
            elem.removeClass('open');
        });
    };
});

myAppModule.service('safetyDiscussionService', function ($http) {
    return ({
        getDiscussions: getDiscussions,
        add: add
    });

    function add(safetyDiscussion)
    {
        var request = $http({
            method: "post",
            url: "/api/SafetyDiscussions",
            data: safetyDiscussion
        });

        return (request.then(handleSuccess, handleError));
    }

    function getDiscussions() {

        var request = $http({
            method: "get",
            url: "/api/SafetyDiscussions",
            params: { 'cache': new Date().getTime() }
        });

        return (request.then(handleSuccess, handleError));

    }

    function handleError(response) {
        console.log("Error logging");

    }

    function handleSuccess(response) {
        return (response.data);

    }
});

myAppModule.controller('ModalInstanceCtrl', function ($scope, $modalInstance, discussion) 
{
    $scope.discussion = discussion;
    console.log($scope.discussion);

    $scope.close = function () {
        $modalInstance.dismiss('cancel');
    };
});


myAppModule.controller('SafetyDiscussionController', function ($scope, $http, $modal, safetyDiscussionService) {

    var fullDiscussionList = [];
    var modalInstance;

    // I load the remote data from the server.
    function loadRemoteData() {

        // The friendService returns a promise.
        return safetyDiscussionService.getDiscussions()
            .then(
                function (discussionList) {

                    fullDiscussionList = discussionList;
                    $scope.totalItems = discussionList.length;
                    $scope.refreshDiscussionListData();
                }
            )
        ;

    }


    $scope.totalItems = 0;
    $scope.currentPage = 1;
    $scope.itemsPerPage = 5;
    $scope.formShown = false;
    $scope.buttonText = "Add";

    $scope.openView = function(discussion)
    {
        modalInstance = $modal.open({
            animation: true,
            templateUrl: 'viewSafetyDiscussion',
            controller: 'ModalInstanceCtrl',
            resolve: {
                discussion: function () {
                    return discussion;
                }
            }
        });
    }

    $scope.closeView = function()
    {
        console.log(modalInstance);
        modalInstance.dismiss('cancel');
    }

    $scope.toggleForm = function () 
    {

        if ($scope.formShown)
        {
            $scope.hideForm(); 
        }
        else
        {
            $scope.showForm();
        }
    }

    $scope.hideForm = function () {
        $scope.buttonText = "Add";
        $scope.formShown = false;
        $scope.$broadcast('event:close');

    }

    $scope.showForm = function(){
        $scope.buttonText = "Hide Form";
        $scope.formShown = true;
        $scope.$broadcast('event:open');
    }

    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();

    $scope.openDate = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened = true;
    };

    $scope.refreshDiscussionListData = function()
    {
        var start = ($scope.currentPage - 1) * $scope.itemsPerPage;
        $scope.discussionList = fullDiscussionList.slice(start, start + $scope.itemsPerPage);
    }

    $scope.pageChanged = function () {
        $scope.refreshDiscussionListData();
    };

    $scope.clearForm = function () {
        $scope.observer = "";
        $scope.date = "";
        $scope.location = "";
        $scope.colleage = "";
        $scope.subject = "";
        $scope.outcome = "";
        $scope.hideForm();
    }

    $scope.logRecord = function () {
        var safetyDiscussion = {};
        safetyDiscussion.Observer = $scope.observer;
        safetyDiscussion.Date = $scope.date;
        safetyDiscussion.Location = $scope.location;
        safetyDiscussion.Colleague = $scope.colleage;
        safetyDiscussion.Subject = $scope.subject;
        safetyDiscussion.Outcomes = $scope.outcome;
        safetyDiscussionService.add(safetyDiscussion).then(function () {
            $scope.clearForm();
            loadRemoteData();
        });        
    };

    $scope.discussionList = [];
    loadRemoteData();
    
});




