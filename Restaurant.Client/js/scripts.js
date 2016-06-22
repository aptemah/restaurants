// @koala-append "main/app.js"
// @koala-append "main/ctrl.js"
// @koala-append "main/directives.js"
// @koala-append "main/services.js"

// @koala-append "menu/app.js"
// @koala-append "menu/ctrl.js"
// @koala-append "menu/directives.js"
// @koala-append "menu/services.js"

function goBack() {
    window.history.back();
};
function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
      "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}