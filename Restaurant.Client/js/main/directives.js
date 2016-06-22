restaurantApp.directive('theLastSlideScript', function() {
    return function(scope) {
        if (scope.$last) {
            function sliderOnMain(sliderProportions) {
                jQuery(function($) {
                    $(".touchslider").touchSlider({ mouseTouch: true, autoplay: true, delay: 5000 });
                });
                $(document).ready(function() {
                    var width = parseInt($(".slider").css("width"));
                    $(".slider").css("height", width * sliderProportions * 1.2 + "px");
                    var height = Math.round((width * sliderProportions)) + "px";
                    $(".touchslider-item").css("width", width + "px");
                    $(".touchslider-item").css("height", height);
                    $(".touchslider-item").parent().css("height", height);
                    $(".touchslider-viewport").css("height", height);
                    $(".touchslider1").css("height", height);
                });
            }
            sliderOnMain(scope.sliderProportions);
        }
    };
});
restaurantApp.directive('myMaxlength', ['$compile', function($compile) {
  return {
    restrict: 'A',
    scope: {},
    require: 'ngModel',
    link: function (scope, elem, attrs, ctrl) {
      attrs.$set("ngTrim", "false");
              var maxlength = parseInt(attrs.myMaxlength, 10);
              ctrl.$parsers.push(function (value) {
                  if (value.length > maxlength)
                  {
                      scope.$parent.vm.maxlength = true;
                      value = value.substr(0, maxlength);
                      ctrl.$setViewValue(value);
                      ctrl.$render();
                  }
                  return value;
              });
    }
  };
}]);
restaurantApp.directive('contenteditable', ['$sce', function($sce) {
  return {
    restrict: 'A', // only activate on element attribute
    require: '?ngModel', // get a hold of NgModelController
    link: function(scope, element, attrs, ngModel) {
      if (!ngModel) return; // do nothing if no ng-model
      // Specify how UI should be updated
      ngModel.$render = function() {
        element.html($sce.getTrustedHtml(ngModel.$viewValue || ''));
      };
      // Listen for change events to enable binding
      element.on('blur keyup change', function() {
        scope.$evalAsync(read);
      });
      read(); // initialize
      // Write data to the model
      function read() {
        var html = element.html();
        // When we clear the content editable the browser leaves a <br> behind
        // If strip-br attribute is provided then we strip this out
        if ( attrs.stripBr && html == '<br>' ) {
          html = '';
        }
        ngModel.$setViewValue(html);
      }
    }
  };
}]);
//restaurantApp.directive('myMaxlengthDiv', ['$compile', function($compile) {
//  return {
//    restrict: 'A',
//    scope: {},
//    require: 'ngModel',
//    link: function (scope, elem, attrs, ctrl) {
//      var maxlength = parseInt(attrs.myMaxlengthDiv, 10);
//
//      //binding keyup/down events on the contenteditable div
//      function check_charcount(content_id, max, e)
//      {
//          if(e.which != 8 && $(elem).text().length > maxlength)
//          {
//             // $('#'+content_id).text($('#'+content_id).text().substring(0, maxlength));
//             e.preventDefault();
//          }
//      }
//      $(elem).keyup(function(e){ check_charcount(elem, maxlength, e); });
//      $(elem).keydown(function(e){ check_charcount(elem, maxlength, e); });
//    }
//  };
//}]);
restaurantApp.directive('popupCloseAble', function() {//Закрытие поп-апа кликом по пустому месту
    return {
        restrict: 'A',
        scope: {
          ngShowModel :'=ngShow'
        },
        link: function ($scope, $element, $attrs) {
            $($element).on("click", function(){
              $scope.ngShowModel = false;
              $scope.$apply();
            });
            $($element).find("*").on("click", function(e){
              e.stopPropagation();
            });
        }
    };
});
restaurantApp.directive('myDirective', ['httpPostFactory', function (httpPostFactory) {
    return {
        restrict: 'A',
        scope: {},
        link: function (scope, element, attr) {
            element.bind('change', function () {
                var formData = new FormData();
                formData.append('file', element[0].files[0]);
                httpPostFactory('Chat/AddMessagePhoto', formData, function (callback) {
                    scope.$parent.vm.msgPhotoName = callback.photo;
                });
            });

        }
    };
}]);

restaurantApp.directive('topMenu', function() {
    return {
        restrict: "E",
        replace: true,
        templateUrl: 'views/modules/topmenu.html',
        link: function($scope, $element, $attrs) {
        }
    }
});

restaurantApp.directive('bottomMenu', function() {
    return {
        restrict: "E",
        replace: true,
        templateUrl: 'views/modules/bottommenu.html',
        link: function($scope, $element, $attrs) {
        }
    }
});
restaurantApp.directive('inputJumpNext', function() {
    return {
        restrict: "A",
        replace: false,
        link: function($scope, $element, $attrs) {
          $($element).keyup(function(){
              if(this.value.length==$(this).attr("maxlength")){
                  $(this).next().focus();
              }
          });
        }
    }
});
restaurantApp.directive('inputValidate', [ "validationService", function(validationService) {
    return {
        restrict: "E",
        replace: true,
        link: function($scope, $element, $attrs) {

            $element.append($("<div>", {"class" : "validate-sign", html: "Ошибка"}));
            $($element).find("input").on("blur", function(){
              validationService.validateFunction(this);
            })
        }
    }
}]);
restaurantApp.directive('sideMenu', ['sessionData', function(sessionData) {
    return {
        restrict: "E",
        replace: true,
        templateUrl: 'views/modules/sidemenu.html',
        link: function($scope, $element, $attrs) {
          $(function menuToggle() {
            $scope.bonusCardUrl = sessionData.bonusCardUrl;
              var sidebar = $("#sidebar");
              var sidebarInnerBlock = $("#sidebar").find(".nav");
              var sidebarWidth = (parseInt(sidebar.css("width")) * -1);
              var shade = $(".shade");
              $(window).on("resize", function(){
                sidebarWidth = (parseInt(sidebar.css("width")) * -1);
                sidebar.css("transform", "translateX(" + sidebarWidth + "px)");
              });

              var sidebarHide = function(){
                sidebar.removeClass("show");
                shade.removeClass("show");
                sidebar.css("transform", "translateX(" + sidebarWidth + "px)");
                sidebar.css("-webkit-transform", "translateX(" + sidebarWidth + "px)");
                sidebarCurrentPosition = sidebarWidth;
                $(".nav").scrollTop(0);
              }
              sidebarHide();

              var sidebarShow = function(){
                sidebar.addClass("transition");
                sidebar.css("transform", "");
                sidebar.css("-webkit-transform", "");
                sidebar.addClass("show");
                shade.addClass("show");
                sidebarCurrentPosition = 0;
              }

              $(document).on("click", ".wrap", function () {
                if ($($element).hasClass("show")) {
                  sidebarHide();
                }
              });
              $(document).on("click", ".wrap li", function () {
                if ($($element).hasClass("show")) {
                  sidebarHide();
                }
              });
              $(document).on("click", "#sidebar", function (e) {
                e.stopPropagation();
              });
              $(document).on("click", ".menu", function (e) {
                e.stopPropagation();
                if ($($element).hasClass("show")) {
                  sidebarHide();
                } else {
                  sidebarShow();
                };
              });
              $(document).on("click", ".link-logout", function (e) {
                e.stopPropagation();
              });
              $(document).on("click", ".nav .disabled", function (e) {
                e.stopPropagation();
              });
              var myElement = document.getElementById("sidebar");
              var hammertime = new Hammer(myElement);
              var style = window.getComputedStyle($('#sidebar').get(0));  // Need the DOM object
              hammertime.on('pan', function(ev) {
                if (!sidebar.hasClass("show")) {
                  sidebar.removeClass("transition");
                  var currentOffset = sidebar.css("transform");
                  var offsetValue = ev.deltaX + sidebarCurrentPosition;
                  sidebar.css("transform", "translateX(" + offsetValue + "px)");
                  sidebar.css("-webkit-transform", "translateX(" + offsetValue + "px)");
                  if (ev.deltaX > 50) {
                    shade.addClass("show");
                  };
                };
                if (sidebar.hasClass("show")) {//если меню открыто, нужно запертить ему двигаться дальше вправо
                  if (ev.deltaX < 0) {
                    sidebar.removeClass("transition");
                    var currentOffset = sidebar.css("transform");
                    var offsetValue = ev.deltaX + sidebarCurrentPosition;
                    sidebar.css("transform", "translateX(" + offsetValue + "px)");
                    sidebar.css("-webkit-transform", "translateX(" + offsetValue + "px)");
                  };
                };
              });
              hammertime.on('panend', function(ev) {
                sidebar.css("transform", "");
                sidebar.addClass("transition");
                if (ev.deltaX > 30) {
                  //show menu
                  sidebarShow();
                }
                if (ev.deltaX <= -70) {
                  //hide menu
                  sidebarHide();
                }
              });

          });
        }
    }
}]);

restaurantApp.directive('popupBlock', [ "validationServiсe", function(validationServiсe) {
    return {
        restrict: "E",
        replace: true,
        templateUrl: 'views/modules/popup_block.html',
        link: function($scope, $element, $attrs) {

        }
    }
}]);

restaurantApp.directive('button', [ function() {
    return {
        restrict: "C",
        link: function($scope, $element) {
            $($element).on("mousedown", function(){
                $(this).addClass("tap");
            });
            $($element).on("mouseup", function(){
                var that = this;
                setTimeout(function() {$(that).removeClass("tap")}, 500);
            });
        }
    }
}]);
//restaurantApp.directive('contenteditable', function () {
//    return {
//        require: 'ngModel',
//        link: function (scope, element, attrs, ctrl) {
//            // view -> model
//            element.bind('blur', function () {
//                scope.$apply(function () {
//                    ctrl.$setViewValue(element.text());
//                });
//            });
//
//            // model -> view
//            ctrl.$render = function () {
//                element.text(ctrl.$viewValue);
//            };
//
//            // load init value from DOM
//            ctrl.$setViewValue(element.text());
//        }
//    };
//});
//fitnessView.directive('theLastSlideScript', function(vm) {
//  return function(scope) {
//    if (scope.$last){
//      sliderOnMain(scope.sliderProportions);
//      function sliderOnMain(sliderProportions) {
//        jQuery(function ($) {
//            $(".touchslider").touchSlider({ mouseTouch: true, autoplay: true, delay: 5000 });
//        });
//        $(document).ready(function () {
//            var width = parseInt($(".picture").css("width"));
//            $(".picture").css("height", width * sliderProportions * 1.2 +"px");
//            var height = Math.round((width * sliderProportions)) + "px";
//            $(".touchslider-item").css("width", width + "px");
//            $(".touchslider-item").css("height", height);
//            $(".touchslider-item").parent().css("height", height);
//            $(".touchslider-viewport").css("height", height);
//            $(".touchslider1").css("height", height);
//        });
//      }
//    }
//  };
//})
//
restaurantApp.directive('autoscroll', [ function() {
    return {
      restrict: 'A',
      scope: {
        autoscroll: '='
      },
      link: function($scope, $element) {
        $scope.$watch(function() { return $scope.autoscroll; }, function(newValue) {
          if (newValue) {
            var height = parseInt($($element)[0].scrollHeight);
            $($element).animate({scrollTop:9999999}, 1000);
            $scope.$evalAsync(function() {
              $scope.autoscroll = null;
            });
          }
        })
      }
    };
  }
]);