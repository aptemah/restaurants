$(".wrap").append("<!--Navigation bar, launching by JS--><ul class='nav'><li class='header-menu'></li><a href='about.html'><li><img src='img/about.png'>О клубе</li></a><a href='personnel.html'><li><img src='img/personnel.png'>Наш персонал</li></a><a href='news.html'><li><img src='img/news.png'>Наши новости</li></a><a href='albums.html'><li><img src='img/photo.png'>Фото клуба</li></a><a href='privileges.html'><li><img src='img/privileges.png'>Привилегии</li></a><a href='feedback.html'><li><img src='img/feedback.png'>Обратная связь</li></a></ul>");
$(function menuToggle(){
	$("body").off();
	$(".menu").off();
	$(".menu").on("click", function(e){
		e.stopPropagation();
		$(".menu").unbind()
		$(".nav").animate({right:"0px"}, 300);
		$(".menu").on("click", function(e){
			e.stopPropagation();
			$(".nav").animate({right:"-100%"}, 300);
			menuToggle();
		});
	});
	$("body").on("click", function(){
		$(".nav").css({right:"-100%"});
		menuToggle();
	});
})
