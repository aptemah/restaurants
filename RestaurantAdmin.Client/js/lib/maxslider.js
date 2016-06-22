function distributionOfContent() {
    $(".slider-content-storage").remove();
    var pageContainer = $(".pages-container");
    var height = (parseInt($(".page").css("height"))) * 0.9;
    var classNames = $(".page").attr("class");
    if (classNames.search("distribution-content") != -1) {
        $(".page").eq(0).append($(".pages-container span"));
        for (var i = $(".page").length - 1; i >= 1; i--) {
            $(".page").eq(i).remove();
        };
        $(".nav div").remove();
        $(".pages").append("<div class='slider-content-storage'></div>");
        var contentPage = $(".slider-content-storage");
        var firstPage = $(".page").eq(0);
        contentPage.append($(".pages-container span"));
        var heightOfContent;
        var lastTextNode = 0;
        var targetPage = -1;
        contentHeightCheck(lastTextNode);
        function contentHeightCheck(lastTextNode){
            targetPage++;
            heightOfContent = 0;
            if (contentPage[0].children.length > 0) {
                for (var i = lastTextNode; i < contentPage[0].children.length; i++) {
                    heightOfContent += contentPage[0].children[i].clientHeight;
                    //console.log("content: " + heightOfContent);
                    if (contentPage[0].children[i].clientHeight > height) {
                        overflowMessage();
                        break;
                    };
                    if (heightOfContent > (height)) {
                            //console.log("blocks will build: " + i);
                            //console.log("result page-height: " + height);
                            //console.log("result content: " + heightOfContent);
                        addingContent(i, targetPage);
                        break;
                    }
                    if (i == contentPage[0].children.length - 1) {
                        addingContent(contentPage[0].children.length, targetPage);
                        break;
                    };
                };
            };
        };
        function addingContent(numberOfTextBlocks, targetPage){
            for (var i = 0; i < numberOfTextBlocks; i++) {
                $(".page").eq(targetPage).append(contentPage[0].children[0]);
            };
            if (contentPage[0].children.length != 0) {
                pageContainer.append("<div class='page'></div>");
                contentHeightCheck(lastTextNode);
            };
        }
        function overflowMessage(){
            alert("One of text blocks is too big for this screen!")
        }
    }
}
function Carousel(pageContainer, imageUrls) {
    var pageCount = $(".page").length;
    $(window).resize(function() {
        var widthOfSlider = $(".slider2").css("width");
        for (var i = pageCount - 1; i >= 0; i--) {
            $(".page").eq(i).css("width", widthOfSlider);
        };
        var pageContainer = $(".pages-container");
        pageContainer.css("width", pageCount * parseInt(widthOfSlider) + "px");
    });
    var nav = $(".nav", pageContainer.parent().parent());
    var viewportWidth = Math.max(document.documentElement.clientWidth, window.innerWidth || 0);
    var slider = $(".pages").css("width");
    for (var i = 0; i < pageCount; i++) {
        $(".page").eq(i).css("width", slider);
        nav.append($("<div>"));
    }
    var pageWidth = $(".page", pageContainer).width();
    pageContainer.css({ width: pageCount * pageWidth });
    var currentPage = 0;
    var dragX = 0;
    pageContainer.drag(function(e) {
        pageContainer.parent().scrollLeft(pageContainer.parent().scrollLeft() - e.originalEvent.dX);
        dragX = e.originalEvent.dragX;
    }).drop(function() {
        if (Math.abs(dragX) > 30) {
            if (dragX > 0) {
                currentPage--;
            } else {
                currentPage++;
            }
            if (currentPage < 0) {
                currentPage = 0;
            }
            if (currentPage >= pageCount) {
                currentPage = 0;
            }
        }
        scrollToCurrentPage();
    });
    function scrollToCurrentPage() {
        pageContainer.parent().animate({ scrollLeft: currentPage * pageWidth }, 300);
        $("div:eq(" + currentPage + ")", nav).addClass("active").siblings().removeClass("active");
    }
    scrollToCurrentPage();
}