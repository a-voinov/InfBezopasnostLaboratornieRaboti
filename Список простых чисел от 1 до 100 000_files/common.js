function ErrorImg(pointer){
	pointer.src = main_domain + "_i/no-photo_64x64.gif";
	pointer.width = "64";
	pointer.height = "64";
	return false;
}

var message_number_limit_over = "!";


$(document).ready(function(){

	if ( $('#rss_tag_choose').length > 0 ){
		var full_n = 0;
		$('#rss_tag_choose span').click(function(){
			
			var this_n = $(this).next().html();
			var span_data = $(this).next().next();
			if ( span_data.html() == 'n' ){
				full_n += Math.pow( 2, this_n );
				span_data.html( 'y' );
				$(this).addClass('on');
			}else{
				if ( span_data.html() == 'y' ){
					full_n -= Math.pow( 2, this_n );
					span_data.html( 'n' );
				$(this).removeClass('on');
				}
			}
			var str_in = '';
			if ( full_n > 0 ){ str_in = '?tags=' + full_n; }
			$('#rss_tag_result').html( main_domain + 'rss/' + str_in );
			$('#rss_tag_result').attr( 'href', main_domain + 'rss/' + str_in );
			
		});
	}

	// проверяем поля ввода при загрузке страницы
	$("input, textarea").each(function(){
		input_check($(this));
	});

	// показывать код в разделах кода
	if  ( $('.codeExample').length > 0 ){
		$(this).find('.codeExample').next().toggle();
		$('.codeExample span').click(function(){
			$(this).parent().next().toggle('slow');
		});
	}

	// показывать блок запуска примера ( от кода )
	if  ( $('.actionCode').length > 0 ){
		$('.actionCode').click(function(){
			if ( $(this).next().hasClass('codeAnswer') ){
				$(this).next().html("");
			}else{
				$(this).after('<div class="codeAnswer"></div>');
			}
			var id_obj = $(this).attr('id').replace(/action_/gi,"");
			var answer = samplePage(id_obj);
			$(this).next().html('<p>' + answer + '</p>');	
		});	
	}

	// рейтинг комментов
	if ( $('p.ratemade').length > 0 ){
		var xmlDocument = '';
		var BindRateClick = false;
		
		// загрузка первичных данных страницы с сервера		
		$('p.aboutrate span').each(function(){
		
			var userLogin = '';
			var flag = 0;
			if ( $.cookie('mylogin') != null ){ 
				userLogin = $.cookie('mylogin');
				if ( $(this).parent().parent().prev().find('p').html() == userLogin ){ flag = 1; }					 
				
			}else{ if ( $.cookie('myajaxlogin') != null ){
				userLogin = $.cookie('myajaxlogin'); 
				if ( $(this).parent().parent().prev().find('p a').html() == userLogin ){ flag = 1; }
				}
			}
		
			if ( flag == 0 ){
				xmlDocument += '<el><class>';
				xmlDocument += $(this).attr('class');
				xmlDocument += '</class>';
			
				xmlDocument += '<id>';
				xmlDocument += $(this).html();
				xmlDocument += '</id></el>';
			}
		});
		try{
        $.ajax({
            type: "POST",
            url: main_domain + "_ajax/?act=loadrate",            
            data: xmlDocument,
            dataType: 'json',
            success: function(json){
                // загрузка данных на страницу				
					writeRateParam(json); 
					BindRateClick = true;
            }
        });
		}catch(err){}
		
		// отправляем голосовалку на сервер
		function addRate(obj_click,rate,mood){								
				var obj = $(obj_click).parent().next().find('span');
				var xmlDocument = '';
		
				// подготовка данных
					xmlDocument += '<el><class>';
					xmlDocument += $(obj).attr('class');
					xmlDocument += '</class>';
			
					xmlDocument += '<id>';
					xmlDocument += $(obj).html();
					xmlDocument += '</id>';

					xmlDocument += '<rate>';
					xmlDocument += rate;
					xmlDocument += '</rate></el>';
					
				obj_click = $(obj_click).parent();
				showRateInProcess(obj_click);
				$.ajax({
					type: "POST",
					url: main_domain + "_ajax/?act=addrate",
					data: xmlDocument,
					dataType: 'json',
					success: function(json){
						// загрузка данных на страницу
						showRateSuccess(obj_click,mood);
					},
					error: function (XMLHttpRequest, textStatus, errorThrown){
						showRateStart(obj_click);
					}
				});
		};
		function showRateStart(obj){
			$(obj).html('<span class="ratedown">&minus;</span><span class="rateup">+</span>');
		// вешаем перехватчики событий
		$('p.ratemade span.ratedown').click(function(){
			if ( BindRateClick ){ addRate(this,'-1','bad'); }
		});
		$('p.ratemade span.rateup').click(function(){
			if ( BindRateClick ){ addRate(this,'1','good'); }
		});

		};
		function showRateInProcess(obj){
			$(obj).html('<span>:b</span>');
		};
		function showRateSuccess(obj,mood){
			if ( mood == 'good' ){
				$(obj).html('<span>:)</span>');
			}else if ( mood == 'bad' ) {
				$(obj).html('<span>:(</span>');
			}
		};
        // загрузка данных на страницу 
		function writeRateParam(a_json){
			var f;
			var n = a_json.length;
			var i;
			
			$('p.aboutrate span').each(function(){
				f = 0;
				i = 0;   
				while ( f == 0 && i < n ){
					if ( a_json[i].name == $(this).attr('class') && a_json[i].id == $(this).html() ){
						f = 1;
						var obj = $(this).parent().parent().find('p.ratemade');
						showRateStart(obj);
					}
					i++;
				}
				/*if ( f == 0 ){
					$(this).parent().parent().find('p.ratemade').html('<span>:)</span>');
				}*/
			});
        }      
    }
	// чат
	if ( $('ul.chat').length > 0 ){
		// расставляем цвета
		var a_links = [];
		var a_colors = [];
		var a_links_count = -1;
		var i = 0;
		var f = 0;
		$('ul.chat li p.post_num').each(function(){
			var obj_text = $(this).html();
			if ( a_links_count == -1 ){ 
				a_links_count++; 
				a_links[a_links_count] = obj_text;
				a_colors[a_links_count] = gen_rnd_hex();
				$(this).css({'background-color':'#' + a_colors[a_links_count] });				
			}else{
				i = -1; f = 0;
				while( i < a_links_count && f == 0 ){
					i++;
					if ( a_links[i] == obj_text ){
						f = 1;
						$(this).css({'background-color':'#' + a_colors[i] });
					}
				}
				if ( f == 0 ){
					a_links_count++; 
					a_links[a_links_count] = obj_text;
					a_colors[a_links_count] = gen_rnd_hex();
					$(this).css({'background-color':'#' + a_colors[a_links_count] });
				}
			}
		});
		
		// вешаем событие
		$('ul.chat li').hover(function(){
			var obj = this;
			var color = $(obj).find('p.post_num').css('background-color');
			$('ul.chat li p.post_num').each(function(){
				if ( $(this).css('background-color') == color ){
					$(this).parent().find('p.dtime').css({'background-color':color});
				}
			});			
		},
		function(){			
			$('ul.chat li p.dtime').each(function(){
				$(this).css({'background-color':'transparent'});
			});			
		});
		
	}
	
	if ( $("div.alert_base").html() == "y" ){
		$("div p.base_error").css({
			'display':'none'
		});
	}	

	if ( $("#update-browser-page").html() == "yes" ){
		if (( $.browser.msie && $.browser.version < 7 ) ||
		( $.browser.mozilla && $.browser.version < 1.9 ) ||
		( $.browser.opera && $.browser.version < 9 ) ||
		( $.browser.safari && $.browser.version < 522.11 )) {
			$("#user-browser").html("Ваш браузер необходимо обновить! Выберите ниже любой из представленных. Любой из них лучше, чем ваш.");
			$("#user-browser").addClass("bad_browser");
		}
		else {
			$("#user-browser").html("Ваш браузер современный. При желании, вы можете попробовать в использовании другой.");
			$("#user-browser").addClass("good_browser");
		}		
	}
	
	if ( $("p.comments_you_can").html() == "1" ){
			$("#post_message").show();		
		}	
	if ( $("p.openid_show").html() == "1" ){
			$("#OpenID_Ajax_Login").attr({
			src:$("p.iframe_src").html()
		});
	}

	
	// отлов событий на форме
	$("input, textarea").keyup(function(){
		input_check($(this));
	});
	$("input, textarea").mouseup(function(){
		input_check($(this));
	});
	$("input, textarea").change(function(){
		input_check($(this));
	});
	

	// страница с комментариями
	//if ( $("iframe#OpenID_Ajax_Login").attr("name") == "OpenID_Ajax_Login" ){
	if ( $("p.iframe_src").html() != "" ){
		$("p.iframe_src").after('<iframe class="hidden2" id="OpenID_Ajax_Login" src="' + $("p.iframe_src").html() + '" name="OpenID_Ajax_Login" width="0" height="0"></iframe>');
	
		$("#OpenID_Ajax_Login").css({"margin-top":"-5000px"}); // hide
		$("#ajax_link").click(function(){
			if ( $("#OpenID_Ajax_Login").hasClass("hidden") ){
				$("#OpenID_Ajax_Login").removeClass("hidden");
			}
			var width = 500;
			var height = 100;
			$("#OpenID_Ajax_Login").width(width);
			$("#OpenID_Ajax_Login").height(height);
			$("#OpenID_Ajax_Login").height(height);
			$("#OpenID_Ajax_Login").css({"margin-top":"0px"}); // show
			$("#close_iframe").show();
			OpenID_Ajax_Login.document.getElementById("OpenIdAjaxTextBox1").value = "";				
		});
		$("#close_iframe").click(function(){
			$("#close_iframe").hide();
			$("#OpenID_Ajax_Login").css({"margin-top":"-5000px"}); // hide
		});
	}
	
	// картинки с сайта Станис блог
	if ( $("div.blog_pictures").attr("title") == "yes" ){
		
		var path1 = main_domain + '_i/2007-03-29/' + 'head_chx';
		//console.debug('main_domain_base');
		var path2 = '_smoofy.gif';
		var path3 = '_darxyde.gif';
		var n = 0;
		var n_black = 0;
		var n_max = 26;
		var d = document;
	
		$("#smoofy_left").click(function(){
			n = move('left',n,n_max,path1,path2);
		});
		$("#smoofy_right").click(function(){
			n = move('right',n,n_max,path1,path2);
		});
		$("#darxyde_left").click(function(){
			n_black = move_black('left',n_black,n_max,path1,path3);
		});
		$("#darxyde_right").click(function(){
			n_black = move_black('right',n_black,n_max,path1,path3);
		});	
	}	
});

function move(side,n,n_max,path1,path2){
	d = document;
	if ( side == 'left' ){
		n--; 
		if ( n < 0 ){
			n = n_max; 
		}
	}
	if ( side == 'right' ){ 
		n++; 
		if ( n > n_max ){
			n = 0;
		}
	}
	d.getElementById('stanis_img_1').src = path1 + String(n) + path2;
	d.getElementById('mytext_1').value = '    ' + String(n+1) + '    ';
	return n;
}

function move_black(side,n_black,n_max,path1,path3){
	d = document;
	if ( side == 'left' ){
		n_black--;
			if ( n_black < 0 ){
				n_black = n_max; 
			}
		}
	if ( side == 'right' ){
		n_black++; 
		if ( n_black > n_max ){
			n_black = 0; 
		}
	}
	d.getElementById('stanis_img_2').src = path1 + String(n_black) + path3;
	d.getElementById('mytext_2').value = '    ' + String(n_black+1) + '    ';
	return n_black;
}

	function input_check(object){
		if ( object.next().hasClass("input_help") || object.next().hasClass("textarea_help") ){
			var nMax = 0;
			if ( object.attr("maxlength") ){
				nMax = object.attr("maxlength");
			}else{
				nMax = object.next().next().html();
			}
			var nUser = object.val().length;
			var nCount = nMax - nUser;
			if ( nCount < 0 ){
				object.addClass("alert_border");
				nCount *= -1;
				object.next().html(nCount + " " + message_number_limit_over);
			}else{
				if ( object.hasClass("alert_border") ){
					object.removeClass("alert_border");
				}
				if ( nUser == 0 ){
					object.next().html("");
					$(this).prev().children().removeClass("good_alert");
					$(this).prev().children().addClass("alert");
					
				}
				else{
					object.next().html(nCount);
					$(this).prev().children().removeClass("alert");
					$(this).prev().children().addClass("good_alert");
				}
			}
		}
		// записать рсс в жж
		if ( object.next().hasClass("link_help") ){
			var tmp = "";
			var object_str = object.val();
			if ( object.attr("id") == "rsslink" ){
				tmp = '<a class="external_link" href="' + object_str + '">' + object_str + '</a>';
				if ( object_str.length < 8  ){ object_str = ""; }
			}
			if ( object.attr("id") == "ljnickname" ){
				tmp = '<a class="external_link"  href="http://' + object_str + '.livejournal.com">' + object_str + '.livejournal.com</a>'
			}			
			if ( object_str.length > 0 ){
				object.next().html(tmp);
			}else{
				object.next().html('');
			}
		}
		return false;
	}
	
	function gen_rnd_hex(){
		var color = '';
		var r = 0;
		var rb = 0;
		for ( var i = 0; i < 6; i++ ){
			r = Math.round(Math.random()*7+8);			
			switch( r ){
				case 10: color += 'A'; break;
				case 11: color += 'B'; break;
				case 12: color += 'C'; break;
				case 13: color += 'D'; break;
				case 14: color += 'E'; break;
				case 15: color += 'F'; break;
				default: color += String(r);
			}
		}		
		return color;
	}

// примеры страниц	
function samplePage(param){
	// === === === === === === === === === === === === 
	function guid(){
		var guid = "";
		for (var i = 0; i < 32; i++){
			guid += parseInt(Math.random() * 16).toString(16);
			if ( i == 7 || i == 11 || i == 15 || i == 19 ) guid += "-";
		}
		return guid;
	}
	// === === === === === === === === === === === === 
	function primeSimple(){
		// основная функция
		function prime(){
			// инициализация
			var iStartTime = startTime();
			var iMaxTime = 1000;
			var iTime = 0;
			var aBase = new Array();
			var iColBase = 0;
			var bFlag = false;
			// блок переменных
			aBase[iColBase] = 2;
			iColBase++;
			var i = 3; 
			var j = 2; 
			var k = i; 
			var l = 0;			
			while ( iTime < iMaxTime ){
				if ( i % j == 0 ){
					bFlag = true;
				}else if ( i == j + 1 ){
					aBase[iColBase] = i;
					iColBase++;
					bFlag = true;
				}
				if ( bFlag ){
					j = 1;
					i++;
					bFlag = false;
				}
				j++;
				k = i;
				l++;
				iTime = endTime(iStartTime);
			}
			iTime = endTime(iStartTime);
			// вывод результата
			sAnswer = '';
			sAnswer += 'Количество простых чисел: ' + iColBase + '<br />';
			sAnswer += 'Стали делить: ' + k + '<br />';
			sAnswer += 'Число делений: ' + l + '<br />';
			sAnswer += 'Время выполнения: ' + iTime/1000 + ' сек.<br />';
			sAnswer += 'Найденные числа: <br />';
			for ( i = 0; i < iColBase; i++ ){
				sAnswer += aBase[i] + '; ';
			}
			return sAnswer;
		}
		// функция засечки времени - начало
		function startTime(){
			var time = new Date();
			time = time.getTime();
			return time;
		}
		// функция засечки времени - конец
		function endTime(startTime){
			var time = new Date();
			time = time.getTime();	
			time -= startTime;
			return time;	
		}
		return prime();
	}
	// === === === === === === === === === === === === 
	function primeSimpleAdvanced(){
		// основная функция
		function prime(){
			// инициализация
			var iStartTime = startTime();
			var iMaxTime = 1000;
			var iTime = 0;
			var aBase = new Array();
			var iColBase = 0;
			var bFlag = false;
			// блок переменных
			aBase[iColBase] = 2;
			iColBase++;
			var i = 3; 
			var j = 2; 
			var k = i; 
			var l = 0; 
			var iSquare = Math.floor(Math.sqrt(i));
			while( iTime < iMaxTime ){
				if ( i % j == 0 ){ 
					bFlag = true; 
				}else if ( j >= iSquare ){
					aBase[iColBase] = i;
					iColBase++;
					bFlag = true;
				}
				if ( bFlag ){
					j = 1;
					i++;
					iSquare = Math.floor(Math.sqrt(i));
					bFlag = false;
				}
				j++;
				k = i;
				iTime = endTime(iStartTime);
				l++;
			}
			iTime = endTime(iStartTime);
			// вывод результата
			sAnswer = '';
			sAnswer += 'Количество простых чисел: ' + iColBase + '<br />';
			sAnswer += 'Стали делить: ' + k + '<br />';
			sAnswer += 'Число делений: ' + l + '<br />';
			sAnswer += 'Время выполнения: ' + iTime/1000 + ' сек.<br />';
			sAnswer += 'Найденные числа: <br />';
			for ( i = 0; i < iColBase; i++ ){
				sAnswer += aBase[i] + '; ';
			}
			return sAnswer;
		}
		// функция засечки времени - начало
		function startTime(){
			var time = new Date();
			time = time.getTime();
			return time;
		}
		// функция засечки времени - конец
		function endTime(startTime){
			var time = new Date();
			time = time.getTime();	
			time -= startTime;
			return time;	
		}
		return prime();
	}
	// === === === === === === === === === === === === 
	function primeMiddle(){
		// основная функция
		function prime(){
			// инициализация
			var iStartTime = startTime();
			var iMaxTime = 1000;
			var iTime = 0;
			var aBase = new Array();
			var iColBase = 0;
			var bFlag = false;
			// блок переменных
			aBase[iColBase] = 2;
			iColBase++;
			aBase[iColBase] = 3;
			iColBase++;
			aBase[iColBase] = 5;
			iColBase++;
			var i = 7; 
			var j = 3; 
			var k = i; 
			var l = 0; 
			var iShag = 0;
			var iSquare = Math.floor(Math.sqrt(i));
			while ( iTime < iMaxTime ){
				if ( i % j == 0 ){
					bFlag = true;
				}else if ( j >= iSquare ){
					aBase[iColBase] = i;
					iColBase++;
					bFlag = true;
				}
				if ( bFlag ){
					j = 2;		
					if ( iShag == 3 ){ 
						i+=4; 
						iShag = 0; 
					}else { 
						i+=2; 
						iShag++; 
					}
					iSquare = Math.floor(Math.sqrt(i));
					bFlag = false;
				}
				j++;
				k = i;
				iTime = endTime(iStartTime);
				l++;
			}
			iTime = endTime(iStartTime);
			// вывод результата
			sAnswer = '';
			sAnswer += 'Количество простых чисел: ' + iColBase + '<br />';
			sAnswer += 'Стали делить: ' + k + '<br />';
			sAnswer += 'Число делений: ' + l + '<br />';
			sAnswer += 'Время выполнения: ' + iTime/1000 + ' сек.<br />';
			sAnswer += 'Найденные числа: <br />';
			for ( i = 0; i < iColBase; i++ ){
				sAnswer += aBase[i] + '; ';
			}
			return sAnswer;
		}
		// функция засечки времени - начало
		function startTime(){
			var time = new Date();
			time = time.getTime();
			return time;
		}
		// функция засечки времени - конец
		function endTime(startTime){
			var time = new Date();
			time = time.getTime();	
			time -= startTime;
			return time;	
		}
		return prime();
	}
	// === === === === === === === === === === === === 
	function primeMiddleAdvanced(){
		// основная функция
		function prime(){
			// инициализация
			var iStartTime = startTime();
			var iMaxTime = 1000;
			var iTime = 0;
			var aBase = new Array();
			var iColBase = 0;
			var bFlag = false;
			// блок переменных
			aBase[iColBase] = 2;
			iColBase++;
			aBase[iColBase] = 3;
			iColBase++;
			aBase[iColBase] = 5;
			iColBase++;
			var i = 7; 
			var j = 1; 
			var k = i; 
			var l = 0; 
			var iShag = 0;
			var iSquare = Math.floor(Math.sqrt(i));
			while ( iTime < iMaxTime ){	
				if ( i % aBase[j] == 0 ){
					bFlag = true;
				}else if ( j >= iSquare ){
					aBase[iColBase] = i;
					iColBase++;
					bFlag = true;
				}
				if ( bFlag ){
					j = 0;
					if ( iShag == 3 ){ 
						i+=4; 
						iShag = 0; 
					}else { 
						i+=2; 
						iShag++; 
					}
					iSquare = Math.floor(Math.sqrt(i));
					bFlag = false;
				}
				j++;
				k = i;
				iTime = endTime(iStartTime);
				l++;
			}
			iTime = endTime(iStartTime);
			// вывод результата
			sAnswer = '';
			sAnswer += 'Количество простых чисел: ' + iColBase + '<br />';
			sAnswer += 'Стали делить: ' + k + '<br />';
			sAnswer += 'Число делений: ' + l + '<br />';
			sAnswer += 'Время выполнения: ' + iTime/1000 + ' сек.<br />';
			sAnswer += 'Найденные числа: <br />';
			for ( i = 0; i < iColBase; i++ ){
				sAnswer += aBase[i] + '; ';
			}
			return sAnswer;
		}
		// функция засечки времени - начало
		function startTime(){
			var time = new Date();
			time = time.getTime();
			return time;
		}
		// функция засечки времени - конец
		function endTime(startTime){
			var time = new Date();
			time = time.getTime();	
			time -= startTime;
			return time;	
		}
		return prime();
	}
	// === === === === === === === === === === === === 
	switch ( param ){
		case 'guid': return guid();
		case 'primeSimple': return primeSimple();
		case 'primeSimpleAdvanced': return primeSimpleAdvanced();
		case 'primeMiddle': return primeMiddle();
		case 'primeMiddleAdvanced': return primeMiddleAdvanced();
		default: return '';
	}
}

