/* facebook */

(function(d, s, id) {
	var js, fjs = d.getElementsByTagName(s)[0];
	if (d.getElementById(id)) return;
	js = d.createElement(s); js.id = id;
	js.src = "//connect.facebook.net/en_GB/all.js#xfbml=1";
	fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));


/* twitter */

!function (d, s, id) {
	var js, fjs = d.getElementsByTagName(s)[0];
	if (!d.getElementById(id)) {
		js = d.createElement(s);
		js.id = id;
		js.src = "//platform.twitter.com/widgets.js";
		fjs.parentNode.insertBefore(js, fjs);
	}
}(document, "script", "twitter-wjs");


/* vk */

VK.init({apiId: 2798749, onlyWidgets: true});

$(function(){
	$('.vk-init').each(function(){
		var id=$(this).attr('id');
		VK.Widgets.Like(id, {type: "mini"});
	});
	$('.vk-init-comments').each(function(){
		var id=$(this).attr('id');
		VK.Widgets.Comments(id, {limit: 10, width: "400", attach: "*"});
	});

});