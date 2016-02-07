function twitter_widget(){
	new TWTR.Widget({
		version: 2,
		type: 'profile',
		rpp: 7,
		interval: 2000,
		width: 'auto',
		height: 300,
		theme: {
			shell: {
				background: '#c8ecff',//'#6bb5da',//'#3b85aa',
				color: '#c8ecff'
			},
			tweets: {
				background: '#c8ecff',
				color: '#1b658a',
				links: '#1b658a'//'#0b355a'
			}
		},
		features: {
			scrollbar: false,
			loop: false,
			live: false,
			hashtags: true,
			timestamp: true,
			avatars: false,
			behavior: 'all'
		}
	}).render().setUser('denisx').start();
	return false;
}

function get_page_data(){
	var title = encodeURI( document.title );
	var uri = encodeURI( window.location.href );
	return [uri, title]
}

function twitter_button(){
	var page_data = get_page_data();
	var uri = page_data[0];
	var title = page_data[1];
	return '<iframe scrolling="no" frameborder="0" class="twitter-share-button twitter-count-horizontal" tabindex="0" allowtransparency="true" src="http://platform0.twitter.com/widgets/tweet_button.html?_=1297203637484&amp;count=horizontal&amp;lang=en&amp;related=Денис%20Хрипков&amp;text='+title+'&amp;url='+uri+'&amp;via=denisx" style="width: 9em; height: 2em;" title="Twitter For Websites: Tweet Button"></iframe>';
}

function facebook_button(){
	var page_data = get_page_data();
	var uri = page_data[0];
//	var title = page_data[1];
	return '<iframe src="http://www.facebook.com/plugins/like.php?href='+uri+'&amp;layout=button_count&amp;show_faces=false&amp;width=11em&amp;action=like&amp;colorscheme=light&amp;height=2em" scrolling="no" frameborder="0" style="border:none; height:2em; width: 11em;" allowTransparency="true"></iframe>';
}

function google_plus_button(){
	var page_data = get_page_data();
	var uri = page_data[0];
//	var title = page_data[1];
	return '<div class="g-plusone" data-size="medium" data-annotation="inline" data-width="120" data-href="'+uri+'"></div>';
}

var f = {};
//f.twitter_widget = twitter_widget();
f.twitterButton = twitter_button();
f.facebookButton = facebook_button ();
f.twitterButtonSubPage = twitter_button ();
f.facebookButtonSubPage = facebook_button();
f.googlePlusButton = google_plus_button();


$(function(){
	$('div.function_call').each(function(){
		var that = $(this);
		that.removeClass('function_call');
		var s = that.attr('class').replace(' ','');
		var f_name = s.replace('call_', '');
		that.after( f[f_name] );
		that.remove();
		if ( window.console ){
//			console.log( f_name );
		}
	});

	var joke_button = $('.joke-button');
	if (joke_button!=null){
		joke_button.bind('click keypress',function(){
			if ( window.console ){
				console.log( 'Нажми меня ещё!' );
			}
		});
		joke_button.bind('mouseover',function(){
			if ( window.console ){
				console.log( 'Погладь кота, сука!' );
			}
		});
	}

});
